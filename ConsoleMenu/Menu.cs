namespace ConsoleHero;

public class Menu(List<MenuOption>? options = null)
{
    private List<MenuOption> Options { get; } = options ?? [];
    private IEnumerable<MenuOption> CheckedOptions => Options.Where(static x => x.Check?.Invoke() != false);
    private MenuOption? Find(Predicate<MenuOption> match) => CheckedOptions.FirstOrDefault(x => match(x));
    internal int Count => Options.Count;
    internal string Title { get; set; } = string.Empty;

    public string Seperator { get; set; } = " => ";
    public void Add(MenuOption option) => Options.Add(option);
    public string Print() => string.Join(Environment.NewLine, CheckedOptions.Select(x => x.Print(Seperator)));
    public void Ask()
    {
        if (Count == 0) return;

        if (Title != string.Empty)
        {
            WriteLine(Title + Environment.NewLine);
        }

        WriteLine(Print());
        MenuOption? choice = null;

        while (choice == null)
        {
            string? line = ReadLine();
            choice = Find(x => x.IsCaseSensitive
                ? string.Equals(x.Key, line)
                : string.Equals(x.Key, line, StringComparison.OrdinalIgnoreCase));
            if (choice == null)
            {
                WriteLine("Not a valid choice" + Environment.NewLine);
            }
            else
            {
                choice.Invoke();
            }
        }
    }
}