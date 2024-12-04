using ConsoleHero.Interfaces;

namespace ConsoleHero;

/// <summary>
/// Start making a new Menu with either <see cref="MenuBuilder.Title(string, Color?)"/> or <see cref="MenuBuilder.NoTitle"/>.
/// </summary>
public record Menu : INode, IListeningNode
{
    internal Menu() { }

    internal List<MenuOption> Options { get; } = new();

    internal int Count => Options.Count;
    internal ColorText Title { get; set; } = new(string.Empty);
    internal bool ClearOnCall { get; set; }
    internal string Seperator { get; set; } = " => ";
    internal void Add(MenuOption option) => Options.Add(option);

    /// <summary>
    /// Display the menu Title and each unhidden option. Then wait for a user response.
    /// Failed responses will loop back and ask again.
    /// </summary>
    public void Call(string input = "")
    {
        if (Count == 0) return;
        AutoIncrimentKeys();

        if (ClearOnCall)
            GlobalSettings.Service.Clear();

        if (Title.Text != string.Empty)
        {
            Title.Print();
        }

        OuputOptions.Print(Seperator);

        GlobalSettings.Service.SetListener(this);
    }

    void IListeningNode.ProcessResult(string response)
    {
        MenuOption? choice = FindFirst(x => x.IsCaseSensitive
            ? string.Equals(x.Key, response)
            : string.Equals(x.Key, response, StringComparison.OrdinalIgnoreCase));
        if (choice == null)
        {
            GlobalSettings.Service.WriteLine("Not a valid choice" + Environment.NewLine);
        }
        else
        {
            for (int i = 0; i < GlobalSettings.Spacing; i++)
                GlobalSettings.Service.WriteLine();
            choice.Invoke();
        }
    }

    internal IEnumerable<MenuOption> OuputOptions => CheckedOptions.Where(x => !x.IsHidden);

    private IEnumerable<MenuOption> CheckedOptions => Options.Where(static x => x.Check?.Invoke() != false);
    private MenuOption? FindFirst(Predicate<MenuOption> match) => CheckedOptions.FirstOrDefault(x => match(x));
    private void AutoIncrimentKeys()
    {
        int x = 1;
        foreach (MenuOption option in CheckedOptions.Where(option => option.UsesAutoKey))
        {
            option.Key = $"{x++}";
        }
    }
}