using ConsoleHero.Helpers;

namespace ConsoleHero;

public class ColorLine
{
    internal ColorLine(string text, Color? color = null)
    {
        Text = text;
        Color = color ?? GlobalSettings.DefaultTextColor;
    }

    internal ColorLine(string text, ConsoleColor color)
    {
        Text = text;
        Color = ColorHelper.ConsoleColorToDrawingColor(color);
    }

    internal ColorLine() { }
    internal string Text { get; set; } = string.Empty;
    internal Color Color { get; }

    internal void Print()
    {
        ColorHelper.SetTextColor(Color);
        WriteLine(Text);
        ColorHelper.SetToDefault();
    }
}