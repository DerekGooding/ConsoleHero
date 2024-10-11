using static ConsoleHero.ParagraphBuilder;

namespace ConsoleHero.Examples;
public static class Paragraphs
{
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
}
