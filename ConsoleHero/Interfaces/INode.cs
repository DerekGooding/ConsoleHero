namespace ConsoleHero.Interfaces;

/// <summary>
/// The base node for ConsoleHero.
/// </summary>
public interface INode
{
    /// <summary>
    /// The entry points for every Node.
    /// </summary>
    /// <param name="input">Pass through string information that can be used as a dynamic variable.</param>
    public abstract void Call(string input = "");
}
