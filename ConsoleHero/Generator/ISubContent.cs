namespace ConsoleHero.Generator;

public interface ISubContent<Tkey, Tenum, Tvalue> where Tkey : INamed
{
    public abstract Dictionary<Tkey, Tvalue> ByKey { get; }

    public abstract Tvalue this[Tenum key] { get; }
}
