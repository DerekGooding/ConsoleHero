namespace ConsoleHero;

public class ColorLine
{
    public ColorLine(string text, ConsoleColor color = ConsoleColor.White)
    {
        Text = text;
        Color = color;
    }

    internal ColorLine() { }
    internal string Text { get; set; } = string.Empty;
    internal ConsoleColor Color { get; }

    internal void Print()
    {
        ForegroundColor = Color;
        WriteLine(Text);
        ForegroundColor = ConsoleColor.White;
    }
}