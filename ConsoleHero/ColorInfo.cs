namespace ConsoleHero;

internal class ColorLine(string text, ConsoleColor color = ConsoleColor.White)
{
    internal string Text { get; set; } = text;
    internal ConsoleColor Color { get; } = color;

    internal void Print()
    {
        ForegroundColor = Color;
        WriteLine(Text);
        ForegroundColor = ConsoleColor.White;
    }
}