using System.Drawing;
using static ConsoleHero.ParagraphBuilder;

namespace ConsoleHero.Examples;
public static class Paragraphs
{
    public static Paragraph Eat =>
    Line("You just ate a ").Input().Text($", {Data.Name}.").
    PressToContinue();

    public static Paragraph Crying =>
    Line("You cry and cry!", Color.DarkBlue).
    PressToContinue();

    public static Paragraph ReadNumbers =>
    Line("You read the number ").Input().Text(".").
    Line("Twice that number is ").ModifiedInput((x) => $"{int.Parse(x) * 2}").Text(".").
    Line("This is ").Text("red", Color.Red).Text(" Text.").
    PressToContinue();

    public static Paragraph ReadPlayers =>
    Line("").Input(Color.Red).
    PressToContinue();

    public static Paragraph Part1 =>
    Line("You take a deep breath...").
    GoTo(Part2).
    Delay(TimeSpan.FromSeconds(2));

    public static Paragraph Part2 =>
    Line("You release the breath...").
    GoTo(Part3).
    Delay(TimeSpan.FromSeconds(2));

    public static Paragraph Part3 =>
    Line("Everything is fine.").
    Delay(TimeSpan.FromSeconds(2));

    public static Paragraph YourNameIs =>
    Line("Your Name is now ").Input().Text(".").
    DelayInSeconds(2);
}
