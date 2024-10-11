using static ConsoleHero.MenuBuilder;

namespace ConsoleHero.Examples;
public static class Menus
{
    public static Menu MainMenu =>
    Title("|---- Main Menu ----|", ConsoleColor.Red).
    ClearWhenAsk().
    Options
    (
        Description("Approach Door").GoTo(DoorMenu),
        Description("Check Surroundings").GoTo(CheckSurroundingsMenu),
        Key("Mary").Description("Listen to Mary").GoTo(Tunes.Mary)
    ).Exit();

    public static Menu DoorMenu =>
    NoTitle().
    Options
    (
        Description("Open Door").If(!Program.IsOpen).GoTo(Program.ToggleDoor),
        Description("Close Door").If(Program.IsOpen).GoTo(Program.ToggleDoor),
        Description("Try to Eat").GoTo(FruitMenu),
        Description("Try to Eat COLORFUL").GoTo(FruitMenuColored),
        Key('A').Description("Try to Eat if starts with A").GoTo(FruitMenuWithA),
        Key("Cry").IsHidden().GoTo(Paragraphs.Crying)
    ).Cancel();

    public static Menu FruitMenu =>
    Title("|---- Fruit ----|", ConsoleColor.Green).
    Options(Data.Fruit.ToOptions(Paragraphs.Eat)).
    Cancel();

    public static Menu FruitMenuColored =>
    Title("|---- Fruit ----|", ConsoleColor.Green).
    Options(Data.ColoredFruit.ToOptions(Paragraphs.Eat)).
    Cancel();

    public static Menu FruitMenuWithA =>
    Title("|---- Fruit ----|", ConsoleColor.Green).
    Options(Data.Fruit.ToOptions(Paragraphs.Eat, x => x.StartsWith('A'))).
    Cancel();

    public static Menu NumberMenu =>
    NoTitle().
    Options(Data.Numbers.Select(x => x.ToString()).ToOptions(Paragraphs.ReadNumbers)).
    Cancel();

    public static Menu CheckSurroundingsMenu =>
    NoTitle().
    Options(Data.Numbers.Select(x => x.ToString()).ToOptions(Paragraphs.ReadNumbers)).
    Cancel();
}
