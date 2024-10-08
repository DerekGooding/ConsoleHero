namespace ConsoleHero;

public class ColorInfo(string text, ConsoleColor color = ConsoleColor.White)
{
    public string Text { get; } = text;
    public ConsoleColor Color { get; } = color;
}