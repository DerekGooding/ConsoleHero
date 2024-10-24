using ConsoleHero.Helpers;

namespace ConsoleHero;

/// <summary>
/// The main component in a <see cref="Menu"/>.
/// </summary>
public sealed class MenuOption
{
    /// <summary>
    /// The main component in a <see cref="Menu"/>.
    /// <paragraph>It is recommended to use the <see cref="MenuBuilder"/>.
    /// <br>You can create and add options manually using this constructor.</br></paragraph>
    /// </summary>
    /// <param name="key">The string a user will need to input to select this option.</param>
    /// <param name="description">The string displayed to the user describing this option.</param>
    /// <param name="effect">The outcome when this option is chosen.</param>
    /// <param name="color">Optional color change property if different from <see cref="GlobalSettings.DefaultTextColor"/>.</param>
    /// <param name="check">Optional boolean check to show this option or not.
    /// <br>If this returns false, the option is hidden and not selectable by the user.</br></param>
    public MenuOption(string key, string description, Action? effect = null, Color? color = null, Func<bool>? check = null)
    {
        Key = key;
        Description = description;
        Effect = effect ?? (() => { });
        Check = check ?? (static () => true);
        Color = color ?? GlobalSettings.DefaultTextColor;
    }

    internal MenuOption()
    {
        Key = string.Empty;
        Description = string.Empty;
        Effect = () => { };
        Check = static () => true;
        Color = GlobalSettings.DefaultTextColor;
    }

    internal string Key { get; set; }
    internal string Description { get; set; }
    internal Action Effect { get; set; }
    internal Func<bool> Check { get; set; }
    internal bool IsCaseSensitive { get; set; }
    internal bool UsesAutoKey { get; set; } = true;
    internal bool IsHidden { get; set; }
    internal Color Color { get; set; }

    internal void Invoke() => Effect.Invoke();

    internal void Print(string seperator = " => ")
    {
        ColorHelper.SetTextColor(Color);
        WriteLine(Key + seperator + Description);
        ColorHelper.SetToDefault();
    }
}