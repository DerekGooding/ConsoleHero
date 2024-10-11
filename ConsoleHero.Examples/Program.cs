using System.Drawing;
using static ConsoleHero.MenuBuilder;
using static ConsoleHero.ParagraphBuilder;
using static ConsoleHero.Tune;

namespace ConsoleHero.Examples;
public static class Program
{
    private static void Main()
    {
        GlobalSettings.Spacing = 2;
        GlobalSettings.DefaultTextColor = Color.AliceBlue;
        while (true)
        {
            MainMenu.Ask();
        }
    }

    public static Menu MainMenu =>
    Title("|---- Main Menu ----|", ConsoleColor.Red).
    ClearWhenAsk().
    Options
    (
        Description("Approach Door").GoTo(DoorMenu),
        Description("Check Surroundings").GoTo(CheckSurroundingsMenu),
        Key("Mary").Description("Listen to Mary").GoTo(Mary)
    ).Exit();

    public static Menu DoorMenu =>
    NoTitle().
    Options
    (
        Description("Open Door").If(!_isOpen).GoTo(ToggleDoor),
        Description("Close Door").If(_isOpen).GoTo(ToggleDoor),
        Description("Try to Eat").GoTo(FruitMenu),
        Description("Try to Eat COLORFUL").GoTo(FruitMenuColored),
        Key('A').Description("Try to Eat if starts with A").GoTo(FruitMenuWithA),
        Key("Cry").IsHidden().GoTo(Crying)
    ).Cancel();

    public static Menu FruitMenu =>
    Title("|---- Fruit ----|", ConsoleColor.Green).
    Options(Fruit.ToOptions(Eat)).
    Cancel();

    public static Menu FruitMenuColored =>
    Title("|---- Fruit ----|", ConsoleColor.Green).
    Options(ColoredFruit.ToOptions(Eat)).
    Cancel();

    public static Menu FruitMenuWithA =>
    Title("|---- Fruit ----|", ConsoleColor.Green).
    Options(Fruit.ToOptions(Eat, x => x.StartsWith('A'))).
    Cancel();

    public static Menu NumberMenu =>
    NoTitle().
    Options(Numbers.Select(x=>x.ToString()).ToOptions(ReadNumbers)).
    Cancel();

    public static Menu CheckSurroundingsMenu =>
    NoTitle().
    Options(Numbers.Select(x => x.ToString()).ToOptions(ReadNumbers)).
    Cancel();

    public static Paragraph Eat =>
    Line("You just at a ").Input().
    PressToContinue();

    public static Paragraph Crying =>
    Line("You cry and cry!", ConsoleColor.DarkBlue).
    PressToContinue();

    public static Paragraph ReadNumbers =>
    Line("You read the number ").Input().
    Line("Twice that number is {number * 2}").
    PressToContinue();

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
        "Apple".             Color(Color.Red),
        "Banana".            Color(Color.Yellow),
        "Cantaloupe".        DefaultColor(),
        "Another Cantaloupe".Color(Color.White),
        "Artichoke".         Color(Color.Green)
    ];

    public readonly static List<int> Numbers =
    [
        7000,
        8888,
        9001,
        55,
    ];

    public readonly static Tune Mary = new(
    [
        new(Tone.B, Duration.QUARTER),
        new(Tone.A, Duration.QUARTER),
        new(Tone.GbelowC, Duration.QUARTER),
        new(Tone.A, Duration.QUARTER),
        new(Tone.B, Duration.QUARTER),
        new(Tone.B, Duration.QUARTER),
        new(Tone.B, Duration.HALF),
        new(Tone.A, Duration.QUARTER),
        new(Tone.A, Duration.QUARTER),
        new(Tone.A, Duration.HALF),
        new(Tone.B, Duration.QUARTER),
        new(Tone.D, Duration.QUARTER),
        new(Tone.D, Duration.HALF)
    ]);

    public static void ToggleDoor() => _isOpen = !_isOpen;
}