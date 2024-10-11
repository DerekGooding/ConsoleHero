namespace ConsoleHero;
public static class ParagraphBuilder
{
    public static ISetLines Line(string text) => new Builder().Line(text);
    public static ISetLines Line(string text, ConsoleColor color) => new Builder().Line(text, color);

    public interface ISetLines
    {
        public ISetLines Line(string text);
        public ISetLines Line(string text, ConsoleColor color);
        public ISetLines Input();
        public Paragraph Delay(TimeSpan delay);
        public Paragraph PressToContinue();
    }
    private class Builder() : ISetLines
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
        public ISetLines TakesInput(params object[] inputs)
        {
            _item.Arguments = inputs;
            return this;
        }
        public ISetLines Input()
        {
            _item.Outputs[^1].Text += "{0}";
            return this;
        }
    }
}
