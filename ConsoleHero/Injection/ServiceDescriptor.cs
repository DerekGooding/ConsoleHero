namespace ConsoleHero.Injection;

/// <summary>
/// Describes a service with its implementation type and lifetime.
/// </summary>
/// <remarks>
/// This class is used by the <see cref="Host"/> to manage the registration and resolution
/// of services based on their specified lifetime.
/// </remarks>
internal class ServiceDescriptor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceDescriptor"/> class.
    /// </summary>
    /// <param name="serviceType">The type of the service.</param>
    /// <param name="lifetime">The lifetime of the service.</param>
    public ServiceDescriptor(Type serviceType, ServiceLifetime lifetime)
    {
        ServiceType = serviceType;
        Lifetime = lifetime;
    }

    /// <summary>
    /// Gets the type of the service.
    /// </summary>
    public Type ServiceType { get; }

    /// <summary>
    /// Gets the lifetime of the service.
    /// </summary>
    public ServiceLifetime Lifetime { get; }

    /// <summary>
    /// Gets the list of types that this service depends on.
    /// </summary>
    internal List<Type> Dependencies => ServiceType.GetConstructors()
                .SelectMany(c => c.GetParameters())
                .Select(p => p.ParameterType)
                .ToList();
}