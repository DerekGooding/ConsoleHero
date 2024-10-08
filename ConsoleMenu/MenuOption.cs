namespace ConsoleMenu;

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

    internal void Invoke() => Effect.Invoke();

    internal string Print(string seperator = " => ") => Key + seperator + Description;
}