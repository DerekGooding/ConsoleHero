namespace ConsoleHero;

public class Menu(List<MenuOption>? options = null)
{
    private List<MenuOption> Options { get; } = options ?? [];

    internal int Count => Options.Count;
    internal ColorLine Title { get; set; } = new(string.Empty, ConsoleColor.White);
    internal bool ClearWhenAsk { get; set; }

    public string Seperator { get; set; } = " => ";
    public void Add(MenuOption option) => Options.Add(option);
    public void PrintOptions()
    {
        foreach(MenuOption option in CheckedOptions.Where(x => !x.IsHidden))
        {
            option.Print(Seperator);
        }
    }

    public void Ask()
    {
        if (Count == 0) return;
        AutoIncrimentKeys();

        if (ClearWhenAsk)
            Clear();

        if (Title.Text != string.Empty)
        {
            Title.Print();
            WriteLine();
        }

        PrintOptions();
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
                WriteLine();
                choice.Invoke();
            }
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