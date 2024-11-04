using System.Drawing;

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

    public readonly static List<string> Numbers =
    [
        "7000",
        "8888",
        "9001",
        "55",
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
    public ColorText Print() => $"{Name} | Health : {Health}".DefaultColor();
}
