using System.Drawing;
using static ConsoleHero.ParagraphBuilder;

namespace ConsoleHero.Examples;
public static class Paragraphs
{
    public static Paragraph Eat =>
    Line("You just ate a ").Input().Text(".").
    PressToContinue();

    public static Paragraph Crying =>
    Line("You cry and cry!", Color.DarkBlue).
    PressToContinue();

    public static Paragraph ReadNumbers =>
    Line("You read the number ").Input().Text(".").
    Line("Twice that number is ").ModifiedInput((x) => $"{int.Parse(x.ToString() ?? string.Empty) * 2}").Text(".").
    Line("This is ").Text("red", Color.Red).Text(" Text.").
    PressToContinue();
}
