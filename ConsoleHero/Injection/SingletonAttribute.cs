namespace ConsoleHero.Injection;

/// <summary>
/// Marks a class as a singleton, allowing it to be automatically registered and managed by the <see cref="Host"/>.
/// When applied to a class, an instance of the class is created and managed by the <see cref="Host"/> container, ensuring
/// only one instance exists throughout the application's lifecycle.
/// </summary>
/// <remarks>
/// The <see cref="SingletonAttribute"/> should be applied to classes that need to be managed as singletons.
/// The class will be automatically discovered and added to the <see cref="Host"/> when the host is initialized.
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public class SingletonAttribute : Attribute { }
