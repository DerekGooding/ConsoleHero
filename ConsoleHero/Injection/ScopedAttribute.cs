namespace ConsoleHero.Injection;

/// <summary>
/// Marks a class as scoped, allowing it to be automatically registered and managed by the <see cref="Host"/>.
/// When applied to a class, an instance of the class is created per scope, as defined by the host.
/// </summary>
/// <remarks>
/// The <see cref="ScopedAttribute"/> should be applied to classes that need to be managed with scoped lifetime.
/// The class will be automatically discovered and added to the <see cref="Host"/> when the host is initialized.
/// A new instance will be created for each scope (typically representing a request or operation).
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public class ScopedAttribute : Attribute { }