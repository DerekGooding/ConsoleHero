using System.Drawing;
using static ConsoleHero.ParagraphBuilder;

namespace ConsoleHero.InjectionExample;
public class Paragraphs(Data data)
{
    private readonly Data _data = data;

    public Paragraph Eat =>
    Line("You just ate a ").Input().Text($", {_data.Name}.").
    PressToContinue();

    public Paragraph Crying =>
    Line("You cry and cry!", Color.DarkBlue).
    PressToContinue();

    public Paragraph ReadNumbers =>
    Line("You read the number ").Input().Text(".").
    Line("Twice that number is ").ModifiedInput((x) => $"{int.Parse(x) * 2}").Text(".").
    Line("This is ").Text("red", Color.Red).Text(" Text.").
    PressToContinue();

    public Paragraph Part1 =>
    Line("You take a deep breath...").
    GoTo(Part2).
    Delay(TimeSpan.FromSeconds(2));

    public Paragraph Part2 =>
    Line("You release the breath...").
    GoTo(Part3).
    Delay(TimeSpan.FromSeconds(2));

    public Paragraph Part3 =>
    Line("Everything is fine.").
    Delay(TimeSpan.FromSeconds(2));

    public Paragraph YourNameIs =>
    Line("Your Name is now ").Input().Text(".").
    DelayInSeconds(2);
}
