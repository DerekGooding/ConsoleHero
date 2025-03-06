namespace ConsoleHero.Injection;

/// <summary>
/// Defines the lifetime of a service within the dependency injection container.
/// </summary>
/// <remarks>
/// Different lifetimes determine how long a service instance exists and when it's recreated:
/// - Singleton: Created once for the entire application lifetime
/// - Scoped: Created once per scope (typically representing a request or operation)
/// - Transient: Created each time the service is requested
/// </remarks>
public enum ServiceLifetime
{
    /// <summary>
    /// Specifies that a single instance of the service will be created and shared throughout the application's lifetime.
    /// </summary>
    Singleton,

    /// <summary>
    /// Specifies that a new instance of the service will be created for each scope.
    /// </summary>
    Scoped,

    /// <summary>
    /// Specifies that a new instance of the service will be created each time it is requested.
    /// </summary>
    Transient
}
