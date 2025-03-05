namespace ConsoleHero.Generator;

public class NamedComparer<T> : IEqualityComparer<T> where T : INamed
{
    public bool Equals(T? x, T? y) => x?.Name == y?.Name;
    public int GetHashCode(T obj) => obj.Name.GetHashCode();
}