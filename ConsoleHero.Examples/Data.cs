using System.Drawing;
using System.Xml.Linq;

namespace ConsoleHero.Examples;
public static class Data
{
    public static string Name { get; set; } = "Person";

    public readonly static List<string> Fruit =
    [
        "Apple",
        "Banana",
        "Cantaloupe",
        "Artichoke"
    ];

    public readonly static List<ColorText> ColoredFruit =
    [
        "Apple".             Color(Color.Red),
        "Banana".            Color(Color.Yellow),
        "Cantaloupe".        DefaultColor(),
        "Another Cantaloupe".Color(Color.White),
        "Artichoke".         Color(Color.Green)
    ];

    public readonly static List<Number> Numbers =
    [
        new(7000),
        new(8888),
        new(9001),
        new(55),
    ];

    public readonly static List<Player> Players =
    [
        new("Cathy"),
        new("Jim"),
        new("Moose")
    ];
}

public class Player(string name) : IMenuOption
{
    public string Name { get; set; } = name;
    public int Health { get; set; } = 100;
    public ColorText Print() => Name.DefaultColor();
    public string Review() => $"{Name} | Health : {Health}";
}

public class Number(int value) : IMenuOption
{
    public int Value { get; } = value;
    public ColorText Print() => $"{Value}".DefaultColor();
}
