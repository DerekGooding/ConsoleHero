namespace ConsoleHero;

/// <summary>
/// Start making a new Menu with either <see cref="MenuBuilder.Title(string, Color?)"/> or <see cref="MenuBuilder.NoTitle"/>.
/// </summary>
public class Menu(List<MenuOption>? options = null) : INode
{
    private List<MenuOption> Options { get; } = options ?? [];

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
            Clear();

        if (Title.Text != string.Empty)
        {
            Title.Print();
            for (int i = 0; i < GlobalSettings.Spacing; i++)
                WriteLine();
        }

        Print();
        MenuOption? choice = null;

        while (choice == null)
        {
            string? line = ReadLine();
            choice = FindFirst(x => x.IsCaseSensitive
                ? string.Equals(x.Key, line)
                : string.Equals(x.Key, line, StringComparison.OrdinalIgnoreCase));
            if (choice == null)
            {
                WriteLine("Not a valid choice" + Environment.NewLine);
            }
            else
            {
                for (int i = 0; i < GlobalSettings.Spacing; i++)
                    WriteLine();
                choice.Invoke();
            }
        }
    }

    internal void Print()
    {
        foreach (MenuOption option in CheckedOptions.Where(x => !x.IsHidden))
        {
            option.Print(Seperator);
        }
    }

    private IEnumerable<MenuOption> CheckedOptions => Options.Where(static x => x.Check?.Invoke() != false);
    private MenuOption? FindFirst(Predicate<MenuOption> match) => CheckedOptions.FirstOrDefault(x => match(x));
    private void AutoIncrimentKeys()
    {
        int x = 1;
        foreach (MenuOption? option in CheckedOptions.Where(option => option.UsesAutoKey))
        {
            option.Key = $"{x++}";
        }
    }
}