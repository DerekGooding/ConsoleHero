using System.Drawing;
using static ConsoleHero.MenuBuilder;

namespace ConsoleHero.InjectionExample;
public class Menus(Paragraphs paragraphs, Requests requests, Tunes tunes, Data data, Logic logic)
{
    private readonly Paragraphs _paragraphs = paragraphs;
    private readonly Requests _requests = requests;
    private readonly Tunes _tunes = tunes;
    private readonly Data _data = data;
    private readonly Logic _logic = logic;

    public Menu MainMenu =>
    Title("|---- Main Menu ----|", Color.Red).
    ClearOnCall().
    Description("Approach Door").GoTo(DoorMenu).
    Description("Check Surroundings").GoTo(NumberMenu).
    Description("Take a breath").GoTo(_paragraphs.Part1).
    Description("Change Name").GoTo(_requests.AskForName).
    Description("Review Players").GoTo(ReviewPlayers).
    Key("Mary").Description("Listen to Mary").GoTo(_tunes.Mary).
    Exit();

    public Menu DoorMenu =>
    NoTitle().
    Description("Open Door").If(!Program.IsOpen).GoTo(Program.ToggleDoor).
    Description("Close Door").If(Program.IsOpen).GoTo(Program.ToggleDoor).
    Description("Try to Eat").GoTo(FruitMenu).
    Description("Try to Eat COLORFUL").GoTo(FruitMenuColored).
    Key('A').Description("Try to Eat if starts with A").GoTo(FruitMenuWithA).
    Key("Cry").IsHidden().GoTo(_paragraphs.Crying).
    Cancel();

    public Menu FruitMenu =>
    Title("|---- Fruit ----|", Color.Green).
    OptionsFromList(_data.Fruit, _paragraphs.Eat).
    Cancel();

    public Menu FruitMenuColored =>
    Title("|---- Fruit ----|", Color.Green).
    OptionsFromList(_data.ColoredFruit, _paragraphs.Eat).
    Cancel();

    public Menu FruitMenuWithA =>
    Title("|---- Fruit ----|", Color.Green).
    OptionsFromList(_data.Fruit, _paragraphs.Eat, x => x.StartsWith('A')).
    Cancel();

    public Menu NumberMenu =>
    NoTitle().
    OptionsFromList(_data.Numbers.ListToString(), _paragraphs.ReadNumbers).
    Cancel();

    public Menu ReviewPlayers =>
    NoTitle().
    OptionsFromList(_data.Players, _logic.ReviewPlayer).
    Cancel();
}
