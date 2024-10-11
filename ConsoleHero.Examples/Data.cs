using System.Drawing;

namespace ConsoleHero.Examples;
public static class Data
{
    public readonly static List<string> Fruit =
    [
        "Apple",
        "Banana",
        "Cantaloupe",
        "Artichoke"
    ];

    public readonly static List<ColorLine> ColoredFruit =
    [
        "Apple".             Color(Color.Red),
        "Banana".            Color(Color.Yellow),
        "Cantaloupe".        DefaultColor(),
        "Another Cantaloupe".Color(Color.White),
        "Artichoke".         Color(Color.Green)
    ];

    public readonly static List<int> Numbers =
    [
        7000,
        8888,
        9001,
        55,
    ];
}
