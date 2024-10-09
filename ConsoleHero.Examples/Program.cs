using static ConsoleHero.MenuBuilder;

namespace ConsoleHero.Examples;
public static class Program
{
    public static Menu MainMenu =>
    Title("|---- Main Menu ----|", ConsoleColor.Red)
    .ClearWhenAsk()
    .Options
    (
        Description("Approach Door").GoTo(DoorMenu.Ask),
        Description("Check Numbers").GoTo(NumberMenu.Ask)
    ).Exit();

    public static Menu DoorMenu =>
    NoTitle()
    .Options
    (
        Description("Open Door").If(() => !_isOpen).GoTo(() => _isOpen = true),
        Description("Close Door").If(() => _isOpen).GoTo(() => _isOpen = false),
        Description("Try to Eat").GoTo(FruitMenu.Ask),
        Description("Try to Eat COLORFUL").GoTo(FruitMenuColored.Ask),
        Key('A').Description("Try to Eat if starts with A").GoTo(FruitMenuWithA.Ask),
        Key("Cry").IsHidden().GoTo(Crying)
    ).Cancel();

    public static Menu FruitMenu =>
    Title("|---- Fruit ----|", ConsoleColor.Green)
    .Options(Fruit.ToOptions(Eat))
    .Cancel();

    public static Menu FruitMenuColored =>
    Title("|---- Fruit ----|", ConsoleColor.Green)
    .Options(ColoredFruit.ToOptions(Eat))
    .Cancel();

    public static Menu FruitMenuWithA =>
    Title("|---- Fruit ----|", ConsoleColor.Green)
    .Options(Fruit.ToOptions(Eat, x => x.StartsWith('A')))
    .Cancel();

    public static Menu NumberMenu =>
    NoTitle()
    .Options(Numbers.ToOptions(ReadNumbers))
    .Cancel();

    private static bool _isOpen = false;

    public readonly static List<string> Fruit =
    [
        "Apple",
        "Banana",
        "Cantaloupe",
        "Artichoke"
    ];

    public readonly static List<ColorLine> ColoredFruit =
    [
        new("Apple", ConsoleColor.Red),
        new("Banana", ConsoleColor.Yellow),
        new("Cantaloupe"),
        new("Artichoke", ConsoleColor.DarkGreen)
    ];

    public readonly static List<int> Numbers =
    [
        7000,
        8888,
        9001,
        55,
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

    private static void ReadNumbers(int number)
    {
        Console.WriteLine($"You read the number {number}");
        Console.WriteLine($"Twice that number is {number * 2}");
        Console.ReadKey();
    }

    private static void Crying()
    {
        Console.WriteLine("You cry and cry!");
        Console.ReadKey();
    }
}