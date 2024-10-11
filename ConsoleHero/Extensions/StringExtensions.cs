namespace ConsoleHero.Extensions;
public static class StringExtensions
{
    public static ColorLine Color(this string text, Color color) => new(text, color);
    public static ColorLine DefaultColor(this string text) => new(text);
}
