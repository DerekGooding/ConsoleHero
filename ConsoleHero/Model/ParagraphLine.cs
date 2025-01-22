namespace ConsoleHero.Model;

internal readonly struct ParagraphLine
{
    internal List<ColorText> Components { get; } = new();

    public ParagraphLine() { }
}
