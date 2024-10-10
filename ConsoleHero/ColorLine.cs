namespace ConsoleHero;

public class ColorLine
{
    public ColorLine(string text, ConsoleColor? color = null)
    {
        Text = text;
        Color = color ?? GlobalSettings.DefaultTextColor;
    }

    internal ColorLine() { }
    internal string Text { get; set; } = string.Empty;
    internal ConsoleColor Color { get; }

    internal void Print()
    {
        ForegroundColor = Color;
        WriteLine(Text);
        ForegroundColor = GlobalSettings.DefaultTextColor;
    }
}