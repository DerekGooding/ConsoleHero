using static ConsoleHero.MenuBuilder;

namespace ConsoleHero.Examples;
public static class Program
{
    public static Menu MainMenu =>
    Title("|---- Main Menu ----|", ConsoleColor.Red)
    .ClearWhenAsk()
    .Options
    (
        Description("Approach Door").GoTo(DoorMenu.Ask)
    ).Exit();

    public static Menu DoorMenu =>
    NoTitle()
    .Options
    (
        Description("Open Door").If(() => !_isOpen).GoTo(() => _isOpen = true),
        Description("Close Door").If(() => _isOpen).GoTo(() => _isOpen = false),
        Key('2').Description("Try to Eat").GoTo(FruitMenu.Ask),
        Key('3').Description("Try to Eat if starts with A").GoTo(FruitMenuWithA.Ask),
    ).Cancel();

    public static Menu FruitMenu =>
    Title("|---- Fruit ----|", ConsoleColor.Green)
    .Options(Items.ToOptions(Eat))
    .Cancel();

    public static Menu FruitMenuWithA =>
    Title("|---- Fruit ----|", ConsoleColor.Green)
    .Options(Items.ToOptions(Eat, x => x.StartsWith('A')))
    .Cancel();

    private static bool _isOpen = false;

    public readonly static List<string> Items =
    [
    "Apple",
    "Banana",
    "Cantaloupe",
    "Artichoke"
    ];

    private static void Main()
    {
        while (true)
        {
            MainMenu.Ask();
        }
    }

    private static void Eat(string item)
    {
        Console.WriteLine($"You just at a {item}");
        Console.ReadKey();
    }
}