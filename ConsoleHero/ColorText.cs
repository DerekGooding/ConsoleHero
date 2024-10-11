using ConsoleHero.Helpers;

namespace ConsoleHero;

public class ColorText : ILineComponent
{
    internal ColorText(string text, Color? color = null)
    {
        Text = text;
        Color = color ?? GlobalSettings.DefaultTextColor;
    }

    internal ColorText(string text, ConsoleColor color)
    {
        Text = text;
        Color = ColorHelper.ConsoleColorToDrawingColor(color);
    }

    internal string Text { get; set; } = string.Empty;
    public Color Color { get; }

    internal void Print()
    {
        ColorHelper.SetTextColor(Color);
        WriteLine(Text);
        ColorHelper.SetToDefault();
    }
}