using System.Drawing;
using static ConsoleHero.MenuBuilder;

namespace ConsoleHero.StaticExample;
public static class Menus
{
    public static Menu MainMenu =>
    Title("|---- Main Menu ----|", Color.Red).
    ClearOnCall().
    Description("Approach Door").GoTo(DoorMenu).
    Description("Check Surroundings").GoTo(NumberMenu).
    Description("Take a breath").GoTo(Paragraphs.Part1).
    Description("Change Name").GoTo(Requests.AskForName).
    Description("Review Players").GoTo(ReviewPlayers).
    Key("Mary").Description("Listen to Mary").GoTo(Tunes.Mary).
    Description("Ask Yes or No").GoTo(Requests.AskYesOrNo).
    Exit();

    public static Menu DoorMenu =>
    NoTitle().
    Description("Open Door").If(!Program.IsOpen).GoTo(Program.ToggleDoor).
    Description("Close Door").If(Program.IsOpen).GoTo(Program.ToggleDoor).
    Description("Try to Eat").GoTo(FruitMenu).
    Description("Try to Eat COLORFUL").GoTo(FruitMenuColored).
    Key('A').Description("Try to Eat if starts with A").GoTo(FruitMenuWithA).
    Key("Cry").IsHidden().GoTo(Paragraphs.Crying).
    Cancel();

    public static Menu FruitMenu =>
    Title("|---- Fruit ----|", Color.Green).
    OptionsFromList(Data.Fruit, Paragraphs.Eat).
    Cancel();

    public static Menu FruitMenuColored =>
    Title("|---- Fruit ----|", Color.Green).
    OptionsFromList(Data.ColoredFruit, Paragraphs.Eat).
    Cancel();

    public static Menu FruitMenuWithA =>
    Title("|---- Fruit ----|", Color.Green).
    OptionsFromList(Data.Fruit, Paragraphs.Eat, x => x.StartsWith('A')).
    Cancel();

    public static Menu NumberMenu =>
    NoTitle().
    OptionsFromList(Data.Numbers, Paragraphs.ReadNumbers).
    Cancel();

    public static Menu ReviewPlayers =>
    NoTitle().
    OptionsFromList(Data.Players, Logic.ReviewPlayer).
    Cancel();
}
