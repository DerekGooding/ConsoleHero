namespace ConsoleHero;

/// <summary>
/// Inherit this interface to make any custom class compatable with <see cref="MenuBuilder.IAddOptions.OptionsFromList(IEnumerable{IMenuOption}, Action{IMenuOption}, Func{string, bool}?)"/>
/// </summary>
public interface IMenuOption
{
    /// <summary>
    /// The text result when turned into a <see cref="MenuOption"/> by list using <see cref="MenuBuilder.IAddOptions.OptionsFromList(IEnumerable{IMenuOption}, Action{IMenuOption}, Func{string, bool}?)"/>.
    /// </summary>
    public abstract ColorText Print();
}
