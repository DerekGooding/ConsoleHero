namespace ConsoleMenu;

internal class MenuOption(string key, string description, Action? effect = null, Func<bool>? check = null)
{
    public string Key { get; set; } = key;
    public string Description { get; set; } = description;
    public Action Effect { get; set; } = effect ?? (() => { });

    public Func<bool> Check = check ?? (static () => true);

    public void Invoke() => Effect.Invoke();

    public string Print() => $"{Key} => {Description}";
}