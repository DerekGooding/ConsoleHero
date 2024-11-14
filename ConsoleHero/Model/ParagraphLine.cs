namespace ConsoleHero.Model;

internal readonly struct ParagraphLine
{
    internal List<ILineComponent> Components { get; } = new();

    public ParagraphLine() { }
}
