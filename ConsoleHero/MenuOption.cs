namespace ConsoleHero;

public sealed class MenuOption
{
    public MenuOption(string key, string description, Action? effect = null, Func<bool>? check = null)
    {
        Key = key;
        Description = description;
        Effect = effect ?? (() => { });
        Check = check ?? (static () => true);
    }

    internal MenuOption()
    {
        Key = string.Empty;
        Description = string.Empty;
        Effect = () => { };
        Check = static () => true;
    }

    internal string Key { get; set; }
    internal string Description { get; set; }
    internal Action Effect { get; set; }
    internal Func<bool> Check { get; set; }
    internal bool IsCaseSensitive { get; set; }
    internal bool UsesAutoKey { get; set; } = true;
    internal bool IsHidden { get; set; }
    internal ConsoleColor Color { get; set; } = ConsoleColor.White;

    internal void Invoke() => Effect.Invoke();

    internal void Print(string seperator = " => ")
    {
        ForegroundColor = Color;
        WriteLine(Key + seperator + Description);
        ForegroundColor = GlobalSettings.DefaultTextColor;
    }
}