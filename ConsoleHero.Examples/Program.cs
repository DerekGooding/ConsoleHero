using static ConsoleHero.MenuBuilder;

namespace ConsoleHero.Examples;
public static class Program
{
    public static Menu MainMenu =>
    Title("|---- Main Menu ----|", ConsoleColor.Red)
    .Options
    (
        Description("Approach Door").GoTo(OtherMenu.Ask),
        Exit()
    );

    public static Menu OtherMenu =>
    NoTitle()
    .Options
    (
        Description("Open Door").If(() => !_isOpen).GoTo(()=> _isOpen = true),
        Description("Close Door").If(() => _isOpen).GoTo(()=> _isOpen = false),
        Description("Try to Eat").GoTo(FruitMenu.Ask),
        Cancel()
    );

    public static Menu FruitMenu =>
    Title("|---- Fruit ----|", ConsoleColor.Green)
    .Options
    (
        Items.ToOptions(Eat)
    );

    private static bool _isOpen = false;

    public static List<string> Items =
    [
    "Apple",
    "Banana",
    "Cantilope",
    ];

    private static void Main()
    {
        while (true)
        {
            Console.Clear();
            MainMenu.Ask();
        }
    }

    private static void Eat(string item)
    {
        Console.WriteLine($"You just at a {item}");
        Console.ReadKey();
    }

    
}