using System.Drawing;
using static ConsoleHero.ParagraphBuilder;

namespace ConsoleHero.InjectionExample;
[Singleton]
public class Paragraphs(Data data)
{
    private readonly Data _data = data;

    public Paragraph Eat(ColorText fruit) =>
        Line("You just ate a ").Text(fruit).Text($", {_data.Name}.").
        PressToContinue();

    public Paragraph Eat(string fruit) =>
        Line($"You just ate a {fruit}, {_data.Name}.").
        PressToContinue();

    public Paragraph Crying =>
        Line("You cry and cry!", Color.DarkBlue).
        PressToContinue();

    public Paragraph ReadNumbers(int n) =>
        Line($"You read the number {n}").
        Line($"Twice that number is {n * 2}").Text(".").
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

    public Paragraph YourNameIs(string name) =>
        Line($"Your Name is now {name}.").
        DelayInSeconds(2);
}
