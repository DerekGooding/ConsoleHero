namespace ConsoleHero.Injection;

/// <summary>
/// Represents a scope for dependency resolution within the dependency injection system.
/// </summary>
/// <remarks>
/// A scope defines a boundary for the lifetime of scoped services. When a scope is created,
/// new instances of scoped services are created and maintained within that scope.
/// Transient services are always created anew, and singleton services are shared across all scopes.
/// </remarks>
public class Scope : IDisposable
{
    private readonly Host _host;
    private readonly Dictionary<Type, object> _scopedInstances = new();
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="Scope"/> class.
    /// </summary>
    /// <param name="host">The host that created this scope.</param>
    internal Scope(Host host)
    {
        _host = host ?? throw new ArgumentNullException(nameof(host));
    }

    /// <summary>
    /// Gets a service of the specified type from the scope.
    /// </summary>
    /// <typeparam name="T">The type of service to get.</typeparam>
    /// <returns>An instance of the service.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the scope has been disposed.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the service is not registered.</exception>
    public T Get<T>() where T : class
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(Scope));

        return _host.GetService<T>(this);
    }

    /// <summary>
    /// Gets or creates a scoped instance of the specified type.
    /// </summary>
    /// <param name="type">The type to get or create.</param>
    /// <param name="factory">A factory function to create the instance if it doesn't exist.</param>
    /// <returns>An instance of the specified type.</returns>
    internal object GetOrCreateScopedInstance(Type type, Func<object> factory)
    {
        if (!_scopedInstances.TryGetValue(type, out var instance))
        {
            instance = factory();
            _scopedInstances.Add(type, instance);
        }
        return instance;
    }

    /// <summary>
    /// Disposes the scope and all disposable scoped instances.
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            foreach (var instance in _scopedInstances.Values)
            {
                (instance as IDisposable)?.Dispose();
            }
            _scopedInstances.Clear();
            _disposed = true;
        }
    }
}