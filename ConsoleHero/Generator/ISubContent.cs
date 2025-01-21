namespace ConsoleHero.Generator;

public interface ISubContent<Tkey, Tvalue> where Tkey : INamed
{
    public abstract Dictionary<Tkey, Tvalue> ByKey { get; }

    public abstract Tvalue this[Tkey key] { get; }
}
