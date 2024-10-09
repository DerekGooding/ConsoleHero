namespace ConsoleHero;
public static class ParagraphBuilder
{
    public static ISetLines Line(string text) => new Builder().Line(text);
    public static ISetLines Line(string text, ConsoleColor color) => new Builder().Line(text, color);

    public interface ISetInputs
    {
        
    }
    public interface ISetLines
    {
        public ISetLines Line(string text);
        public ISetLines Line(string text, ConsoleColor color);
        public Paragraph Delay(TimeSpan delay);
        public Paragraph PressToContinue();
    }
    private class Builder() : ISetInputs, ISetLines
    {
        readonly Paragraph _item = new();
        public ISetLines Line(string text)
        {
            _item.Outputs.Add(new ColorLine(text));
            return this;
        }
        public ISetLines Line(string text, ConsoleColor color)
        {
            _item.Outputs.Add(new ColorLine(text, color));
            return this;
        }
        public Paragraph Delay(TimeSpan delay)
        {
            _item.Delay = delay;
            _item.PressToContinue = false;
            return _item;
        }
        public Paragraph PressToContinue() => _item;
    }
}
