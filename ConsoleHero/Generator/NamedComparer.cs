namespace ConsoleHero.Generator;

/// <summary>
/// Provides an equality comparison for types implementing <see cref="INamed"/>.
/// </summary>
/// <typeparam name="T">
/// The type to compare, which must implement <see cref="INamed"/>.
/// </typeparam>
/// <remarks>
/// This comparer is enforced by the Roslyn Analyzer <c>NamedComparerAnalyzer</c>,
/// ensuring that all <see cref="Dictionary{TKey, TValue}"/> instances using an <see cref="INamed"/> key
/// specify <see cref="NamedComparer{T}"/> explicitly.
/// </remarks>
/// <example>
/// The following example demonstrates how to use <see cref="NamedComparer{T}"/> with a dictionary:
/// <code>
/// readonly record struct MyKey(string Name) : INamed;
/// var dictionary = new Dictionary&lt;MyKey, string&gt;(new NamedComparer&lt;MyKey&gt;());
/// </code>
/// </example>
public class NamedComparer<T> : IEqualityComparer<T> where T : INamed
{
    /// <summary>
    /// Determines whether two objects of type <typeparamref name="T"/> are equal.
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>
    /// <c>true</c> if the <see cref="INamed.Name"/> property of both objects is equal; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method performs a null check before comparing names.
    /// </remarks>
    public bool Equals(T? x, T? y) => x?.Name == y?.Name;

    /// <summary>
    /// Returns a hash code for the specified object based on its <see cref="INamed.Name"/>.
    /// </summary>
    /// <param name="obj">The object for which to get the hash code.</param>
    /// <returns>A hash code derived from the <see cref="INamed.Name"/> property.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="obj"/> is <c>null</c>.</exception>
    public int GetHashCode(T obj) => obj.Name.GetHashCode();
}
