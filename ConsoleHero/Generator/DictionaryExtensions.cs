namespace ConsoleHero.Generator;

/// <summary>
/// Provides extension methods for creating dictionaries with keys that implement <see cref="INamed"/>.
/// </summary>
/// <remarks>
/// The <see cref="ToNamedDictionary{TKey, TValue}"/> method ensures that dictionary keys implementing
/// <see cref="INamed"/> always use <see cref="NamedComparer{T}"/> for equality comparisons,
/// greatly improving performance on key lookups.
/// </remarks>
public static class DictionaryExtensions
{
    /// <summary>
    /// Converts an <see cref="IEnumerable{T}"/> to a <see cref="Dictionary{TKey, TValue}"/>
    /// using <see cref="NamedComparer{T}"/> for key equality.
    /// </summary>
    /// <typeparam name="TKey">The type of dictionary keys, which must implement <see cref="INamed"/>.</typeparam>
    /// <typeparam name="TValue">The type of dictionary values.</typeparam>
    /// <param name="source">The source collection from which to create the dictionary.</param>
    /// <param name="elementSelector">A function that extracts the value from each element in <paramref name="source"/>.</param>
    /// <returns>
    /// A dictionary where keys are compared using <see cref="NamedComparer{TKey}"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/>, or <paramref name="elementSelector"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// This method ensures that all dictionary operations are consistent for types implementing <see cref="INamed"/>.
    /// </remarks>
    public static Dictionary<TKey, TValue> ToNamedDictionary<TKey, TValue>(
        this IEnumerable<TKey> source,
        Func<TKey, TValue> elementSelector)
        where TKey : INamed
        => source.ToDictionary(x => x, elementSelector, new NamedComparer<TKey>());
}