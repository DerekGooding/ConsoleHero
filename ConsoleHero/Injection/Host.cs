namespace ConsoleHero.Injection;

/// <summary>
/// Represents a container that holds and manages singleton instances. It provides functionality to register
/// singletons, resolve dependencies, and retrieve singleton instances.
/// </summary>
/// <remarks>
/// This class is responsible for initializing and managing singletons, ensuring that each singleton is 
/// only instantiated once and that its dependencies are resolved before initialization. It uses a builder pattern
/// for constructing the host with a series of singleton registrations.
/// </remarks>
public sealed class Host
{
    private Host(List<Singleton> list)
    {
        singletons = list;
        Initialize();
    }

    private Host() { }

    private readonly List<Singleton> singletons = new();

    private readonly Dictionary<Type, object> _map = new();

    public interface ILoadSingletons
    {
        public ILoadSingletons Singleton<T>();
        public Host Build();
    }

    public static ILoadSingletons Singleton<T>()
        => new Builder().Singleton<T>();

    internal class Builder : ILoadSingletons
    {
        private readonly Host _host = new();

        public ILoadSingletons Singleton<T>()
        {
            _host.singletons.Add(new(typeof(T)));
            return this;
        }

        public Host Build()
        {
            _host.Initialize();
            return _host;
        }
    }

    private void Initialize()
    {
        List<Singleton> toProcess = singletons.ToList();
        for (int i = 0; i < toProcess.Count; i++)
        {
            Singleton singleton = toProcess[i];
            if (singleton.Dependencies.Count == 0)
            {
                singleton.Initialize();
                _map.Add(singleton.Type, singleton.Instance);
                toProcess.RemoveAt(i);
                i--;
            }
        }

        int lastTry = int.MaxValue;
        while (toProcess.Count != 0 && lastTry != toProcess.Count)
        {
            lastTry = toProcess.Count;
            for (int i = 0; i < toProcess.Count; i++)
            {
                Singleton singleton = toProcess[i];
                if (singleton.Dependencies.All(_map.ContainsKey))
                {
                    IEnumerable<object> dependencies = singleton.Dependencies.Select(x => _map[x]);
                    singleton.Initialize(dependencies.ToArray());
                    _map.Add(singleton.Type, singleton.Instance);
                    toProcess.RemoveAt(i);
                    i--;
                }
            }
        }

        if (toProcess.Count != 0)
            throw new Exception("Missing singletons or circular dependencies issue.");
    }

    /// <summary>
    /// Initializes a new <see cref="Host"/> instance by scanning the current domain for types that are marked
    /// with the <see cref="SingletonAttribute"/>. These types are then registered as singletons within the host.
    /// </summary>
    /// <returns>A new <see cref="Host"/> instance populated with singletons based on the types found in the current domain.</returns>
    /// <remarks>
    /// This method scans all loaded assemblies in the current application domain for types that are decorated
    /// with the <see cref="SingletonAttribute"/>. It then creates a new host and registers these types as singletons.
    /// If no types with the <see cref="SingletonAttribute"/> are found, an empty host is returned.
    /// </remarks>
    public static Host InitializeUsingAttribute()
    {
        Host host = new Host(AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
                           .Where(t => t.GetCustomAttributes(typeof(SingletonAttribute), false).Length != 0)
                           .Select(x => new Singleton(x))
                           .ToList());
        return host._map.Count == 0 ? new Host() : host;
    }

    /// <summary>
    /// Retrieves the instance of a registered singleton of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the singleton to retrieve. Must be a class that has been registered with the host.</typeparam>
    /// <returns>The instance of the singleton registered for the specified type <typeparamref name="T"/>.</returns>
    /// <exception cref="Exception">Thrown if the host has not been initialized or if the requested singleton is not found.</exception>
    public T Get<T>() where T : class
        => _map.Count == 0 ? throw new Exception("Host must be initialized") : (T)_map[typeof(T)];
}
