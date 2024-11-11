namespace ConsoleHero.Model;

/// <summary>
/// The main component in a <see cref="Menu"/>.
/// </summary>
internal sealed record MenuOption
{
    internal MenuOption() { }

    internal string Key { get; set; } = string.Empty;
    internal string Description { get; set; } = string.Empty;
    internal Action Effect { get; set; } = () => { };
    internal Func<bool> Check { get; set; } = static () => true;
    internal bool IsCaseSensitive { get; set; }
    internal bool UsesAutoKey { get; set; } = true;
    internal bool IsHidden { get; set; }
    internal Color Color { get; set; } = GlobalSettings.DefaultTextColor;

    internal void Invoke() => Effect.Invoke();
}