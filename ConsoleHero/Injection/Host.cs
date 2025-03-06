namespace ConsoleHero.Injection;

/// <summary>
/// Represents a container that holds and manages service instances with different lifetimes (Singleton, Scoped, Transient).
/// It provides functionality to register services, resolve dependencies, and retrieve service instances.
/// </summary>
/// <remarks>
/// This class is responsible for initializing and managing services with different lifetimes, ensuring proper
/// initialization and dependency resolution. It automatically discovers attributed classes and registers them
/// with their appropriate lifetimes.
/// </remarks>
public sealed class Host
{
    private readonly List<ServiceDescriptor> _serviceDescriptors = new();
    private readonly Dictionary<Type, object> _singletonInstances = new();
    private readonly Dictionary<Type, Func<Scope, object>> _factories = new();
    private bool _initialized;

    private Host() { }

    /// <summary>
    /// Initializes a new <see cref="Host"/> instance by scanning the current domain for types marked
    /// with service lifetime attributes. These types are then registered as services within the host.
    /// </summary>
    /// <returns>A new <see cref="Host"/> instance populated with services based on the types found in the current domain.</returns>
    /// <remarks>
    /// This method scans all loaded assemblies in the current application domain for types decorated
    /// with service lifetime attributes (<see cref="SingletonAttribute"/>, <see cref="ScopedAttribute"/>, 
    /// <see cref="TransientAttribute"/>). It then creates a new host and registers these types with appropriate lifetimes.
    /// </remarks>
    internal static Host Initialize()
    {
        var host = new Host();
        host.RegisterAttributedTypes();
        host.InitializeUsingAttribute();
        return host;
    }

    /// <summary>
    /// Registers all types in the current domain that are marked with lifetime attributes.
    /// </summary>
    private void RegisterAttributedTypes()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(SingletonAttribute), false).Length > 0)
                {
                    _serviceDescriptors.Add(new ServiceDescriptor(type, ServiceLifetime.Singleton));
                }
                else if (type.GetCustomAttributes(typeof(ScopedAttribute), false).Length > 0)
                {
                    _serviceDescriptors.Add(new ServiceDescriptor(type, ServiceLifetime.Scoped));
                }
                else if (type.GetCustomAttributes(typeof(TransientAttribute), false).Length > 0)
                {
                    _serviceDescriptors.Add(new ServiceDescriptor(type, ServiceLifetime.Transient));
                }
            }
        }
    }

    private void InitializeUsingAttribute()
    {
        if (_initialized)
            return;

        BuildFactories();
        _initialized = true;
    }

    private void BuildFactories()
    {
        // First, sort services to resolve dependencies in the correct order
        var sortedServices = SortServicesByDependencies();

        // Create factories for each service
        foreach (var descriptor in sortedServices)
        {
            CreateFactory(descriptor);
        }
    }

    private List<ServiceDescriptor> SortServicesByDependencies()
    {
        var result = new List<ServiceDescriptor>();
        var remaining = new List<ServiceDescriptor>(_serviceDescriptors);

        // Process services with no dependencies first
        ProcessServicesWithNoDependencies(result, remaining);

        // Process remaining services based on dependency order
        ProcessRemainingServices(result, remaining);

        return result;
    }

    private static void ProcessServicesWithNoDependencies(List<ServiceDescriptor> result, List<ServiceDescriptor> remaining)
    {
        for (var i = 0; i < remaining.Count; i++)
        {
            var descriptor = remaining[i];
            if (descriptor.Dependencies.Count == 0)
            {
                result.Add(descriptor);
                remaining.RemoveAt(i);
                i--;
            }
        }
    }

    private void ProcessRemainingServices(List<ServiceDescriptor> result, List<ServiceDescriptor> remaining)
    {
        var lastCount = int.MaxValue;
        while (remaining.Count > 0 && lastCount != remaining.Count)
        {
            lastCount = remaining.Count;

            for (var i = 0; i < remaining.Count; i++)
            {
                var descriptor = remaining[i];
                var allDependenciesResolved = descriptor.Dependencies.All(dep =>
                    _serviceDescriptors.Any(sd => sd.ServiceType == dep && result.Contains(sd)));

                if (allDependenciesResolved)
                {
                    result.Add(descriptor);
                    remaining.RemoveAt(i);
                    i--;
                }
            }
        }

        if (remaining.Count > 0)
        {
            throw new InvalidOperationException("Circular dependency detected or missing service registration.");
        }
    }

    private void CreateFactory(ServiceDescriptor descriptor)
    {
        switch (descriptor.Lifetime)
        {
            case ServiceLifetime.Singleton:
                CreateSingletonFactory(descriptor);
                break;
            case ServiceLifetime.Scoped:
                CreateScopedFactory(descriptor);
                break;
            case ServiceLifetime.Transient:
                CreateTransientFactory(descriptor);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void CreateSingletonFactory(ServiceDescriptor descriptor)
    {
        var serviceType = descriptor.ServiceType;

        _factories[serviceType] = scope =>
        {
            if (!_singletonInstances.TryGetValue(serviceType, out var instance))
            {
                instance = CreateInstance(serviceType, scope);
                _singletonInstances[serviceType] = instance;
            }
            return instance;
        };
    }

    private void CreateScopedFactory(ServiceDescriptor descriptor)
    {
        var serviceType = descriptor.ServiceType;

        _factories[serviceType] = scope =>
            scope.GetOrCreateScopedInstance(serviceType, () => CreateInstance(serviceType, scope));
    }

    private void CreateTransientFactory(ServiceDescriptor descriptor)
    {
        var serviceType = descriptor.ServiceType;
        _factories[serviceType] = scope => CreateInstance(serviceType, scope);
    }

    private object CreateInstance(Type type, Scope scope)
    {
        var constructor = type.GetConstructors().FirstOrDefault()
            ?? throw new InvalidOperationException($"No public constructor found for type {type.Name}");

        var parameters = constructor.GetParameters();
        var arguments = new object[parameters.Length];

        for (var i = 0; i < parameters.Length; i++)
        {
            var paramType = parameters[i].ParameterType;
            if (!_factories.TryGetValue(paramType, out var factory))
            {
                throw new InvalidOperationException($"Cannot resolve dependency {paramType.Name} for {type.Name}");
            }

            arguments[i] = factory(scope);
        }

        return constructor.Invoke(arguments);
    }

    /// <summary>
    /// Creates a new scope for resolving scoped services.
    /// </summary>
    /// <returns>A new <see cref="Scope"/> instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the host has not been initialized.</exception>
    public Scope CreateScope()
    {
        if (!_initialized)
            throw new InvalidOperationException("Host must be initialized before creating a scope.");

        return new Scope(this);
    }

    /// <summary>
    /// Retrieves a singleton service of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of service to retrieve.</typeparam>
    /// <returns>The singleton instance of the specified service type.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the host has not been initialized or the service is not registered.</exception>
    internal T Get<T>() where T : class
    {
        if (!_initialized)
            throw new InvalidOperationException("Host must be initialized.");

        using var scope = CreateScope();
        return GetService<T>(scope);
    }

    /// <summary>
    /// Retrieves a service of the specified type from the given scope.
    /// </summary>
    /// <typeparam name="T">The type of service to retrieve.</typeparam>
    /// <param name="scope">The scope from which to retrieve the service.</param>
    /// <returns>An instance of the specified service type.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the service is not registered.</exception>
    internal T GetService<T>(Scope scope) where T : class
    {
        var type = typeof(T);

        if (!_factories.TryGetValue(type, out var factory))
            throw new InvalidOperationException($"Service of type {type.Name} is not registered.");

        return (T)factory(scope);
    }
}