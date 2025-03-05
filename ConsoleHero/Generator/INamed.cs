namespace ConsoleHero.Generator;

/// <summary>
/// Represents an object that has a <see cref="Name"/> property.
/// </summary>
/// <remarks>
/// The <see cref="INamed"/> interface is designed to be implemented by types that require a unique,
/// identifying name. It is typically used to ensure that objects in collections can be compared
/// and grouped by their name.
/// <br>
/// This interface is especially useful for types that are used as keys in dictionaries or other
/// data structures that rely on equality comparisons.
/// </br>
/// </remarks>
public interface INamed
{
    /// <summary>
    /// Gets the name of the object, which should be unique within a given context.
    /// </summary>
    /// <value>
    /// A string representing the name of the object.
    /// </value>
    /// <remarks>
    /// The <see cref="Name"/> property is used to compare objects implementing <see cref="INamed"/>
    /// for equality. In most cases, a unique name should be assigned to each instance implementing this
    /// interface to maintain proper equality semantics in data structures like dictionaries.
    /// </remarks>
    string Name { get; }
}
