using ConsoleHero.Interfaces;
using System.Drawing;

namespace ConsoleHero.StaticExample;
public record struct Fruit(string Name) : IMenuOption
{
    public ColorText Print() => Name.DefaultColor();
}
public record struct ColorFruit(string Name, Color Color) : IMenuOption
{
    public ColorText Print() => Name.Color(Color);
}
public record struct Number(int Value) : IMenuOption
{
    public ColorText Print() => Value.ToString().DefaultColor();
}
public static class Data
{
    public static string Name { get; set; } = "Person";

    public static readonly List<Fruit> Fruit =
    [
        new("Apple"),
        new("Banana"),
        new("Cantaloupe"),
        new("Artichoke"),
    ];

    public static readonly List<ColorFruit> ColoredFruit =
    [
        new("Apple",Color.Red),
        new("Banana",Color.Yellow),
        new("Cantaloupe", GlobalSettings.DefaultTextColor),
        new("Another Cantaloupe", Color.White),
        new("Artichoke", Color.Green),
    ];

    public static readonly List<Number> Numbers =
    [
        new(7000),
        new(8888),
        new(9001),
        new(55),
    ];

    public static readonly List<Player> Players =
    [
        new ("Cathy"),
        new ("Jim"),
        new ("Moose"),
    ];
}

public class Player(string name) : IMenuOption
{
    public string Name { get; set; } = name;
    public int Health { get; set; } = 100;
    public ColorText Print() => Name.DefaultColor();
    public string Review => $"{Name} | Health : {Health}";
}
