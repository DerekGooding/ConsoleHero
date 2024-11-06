namespace ConsoleHero;

/// <summary>
/// Inherit this interface to make any custom class compatable with <see cref="MenuBuilder.IAddOptions.OptionsFromList{T}(IEnumerable{T}, Action{T}, Func{string, bool}?)"/>
/// </summary>
public interface IMenuOption
{
    /// <summary>
    /// The text result when turned into a <see cref="MenuOption"/> by list using <see cref="MenuBuilder.IAddOptions.OptionsFromList{T}(IEnumerable{T}, Action{T}, Func{string, bool}?)"/>.
    /// </summary>
    public abstract ColorText Print();
}
