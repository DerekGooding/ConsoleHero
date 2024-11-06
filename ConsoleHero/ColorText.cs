using ConsoleHero.Helpers;

namespace ConsoleHero;

/// <summary>
/// A holder for a string and a color. Is consumed by ConsoleHero and it's implimented <see cref="INode"/>.
/// <br>To create a ColorText object, use the <see cref="StringExtensions.Color"/> or <see cref="StringExtensions.DefaultColor"/> with any string.</br>
/// <br>Example: "ExampleText123".DefaultColor();</br>
/// <br/>
/// <br><see cref="Menu"/></br>
/// <br><see cref="Paragraph"/></br>
/// <br><see cref="Request"/></br>
/// </summary>
public record struct ColorText : ILineComponent
{
    private readonly Color _color;

    internal ColorText(string text, Color? color = null)
    {
        Text = text;
        _color = color ?? GlobalSettings.DefaultTextColor;
    }

    internal ColorText(string text, ConsoleColor color)
    {
        Text = text;
        _color = ColorHelper.ConsoleColorToDrawingColor(color);
    }

    internal string Text { get; set; } = string.Empty;

    readonly Color ILineComponent.Color => _color;

    internal readonly void Print()
    {
        ColorHelper.SetTextColor(_color);
        WriteLine(Text);
        ColorHelper.SetToDefault();
    }
}