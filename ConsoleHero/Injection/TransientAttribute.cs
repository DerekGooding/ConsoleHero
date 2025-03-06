namespace ConsoleHero.Injection;

/// <summary>
/// Marks a class as transient, allowing it to be automatically registered and managed by the <see cref="Host"/>.
/// When applied to a class, a new instance is created each time the class is requested from the host.
/// </summary>
/// <remarks>
/// The <see cref="TransientAttribute"/> should be applied to classes that need to be managed with transient lifetime.
/// The class will be automatically discovered and added to the <see cref="Host"/> when the host is initialized.
/// A new instance will be created each time the service is requested.
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public class TransientAttribute : Attribute { }