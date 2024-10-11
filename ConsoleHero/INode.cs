namespace ConsoleHero;

/// <summary>
/// The base node for ConsoleHero.
/// </summary>
public interface INode
{
    public abstract void Call();
    public abstract void Call(string input);
}
