using System.Drawing;
using static ConsoleHero.ParagraphBuilder;

namespace ConsoleHero.InjectionExample;
[Singleton]
public class Paragraphs(Data data)
{
    private readonly Data _data = data;

    public Paragraph Eat(ColorFruit fruit) =>
        Line("You just ate a ").Text(fruit.Name, fruit.Color).Text($", {_data.Name}.").
        PressToContinue();

    public Paragraph Eat(Fruit fruit) =>
        Line($"You just ate a {fruit.Name}, {_data.Name}.").
        PressToContinue();

    public Paragraph Crying =>
        Line("You cry and cry!", Color.DarkBlue).
        PressToContinue();

    public Paragraph ReadNumbers(Number n) =>
        Line($"You read the number {n.Value}.").
        Line($"Twice that number is {n.Value * 2}").Text(".").
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
