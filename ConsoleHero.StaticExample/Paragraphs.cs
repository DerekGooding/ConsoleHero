using System.Drawing;
using static ConsoleHero.ParagraphBuilder;

namespace ConsoleHero.StaticExample;
public static class Paragraphs
{
    public static Paragraph Eat(ColorText fruit) =>
        Line("You just ate a ").Text(fruit).Text($", {Data.Name}.").
        PressToContinue();

    public static Paragraph Eat(string fruit) =>
        Line($"You just ate a {fruit}, {Data.Name}.").
        PressToContinue();

    public static Paragraph Crying =>
        Line("You cry and cry!", Color.DarkBlue).
        PressToContinue();

    public static Paragraph ReadNumbers(int n) =>
        Line($"You read the number {n}.").
        Line($"Twice that number is {n * 2}").Text(".").
        Line("This is ").Text("red", Color.Red).Text(" Text.").
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

    public static Paragraph YourNameIs(string name) =>
        Line($"Your Name is now {name}.").
        DelayInSeconds(2);
}
