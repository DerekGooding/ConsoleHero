namespace ConsoleHero;
public static class ParagraphBuilder
{
    /// <summary>
    /// Start building a new line of text. Default color.
    /// </summary>
    public static ISetLines Line(string text) => new Builder().Line(text);

    /// <summary>
    /// Start building a new line of text. Custom color.
    /// </summary>
    public static ISetLines Line(string text, ConsoleColor color) => new Builder().Line(text, color);

    public interface ISetLines
    {
        /// <summary>
        /// Start building a new line of text. Default color.
        /// </summary>
        public ISetLines Line(string text);
        /// <summary>
        /// Start building a new line of text. Custom color.
        /// </summary>
        public ISetLines Line(string text, ConsoleColor color);
        /// <summary>
        /// Add additional text to the end of this line.
        /// </summary>
        public ISetLines Text(string text);
        /// <summary>
        /// Add the input variable to this line.
        /// </summary>
        public ISetLines Input();
        /// <summary>
        /// Add the input variable to this line but apply some modifier to it.
        /// </summary>
        public ISetLines ModifiedInput(Func<object, string> modifier);
        /// <summary>
        /// After displaying this paragraph, will wait before continuing without input.
        /// </summary>
        public Paragraph Delay(TimeSpan delay);
        /// <summary>
        /// After displaying this paragraph, will wait for the user to press a key to continue.
        /// </summary>
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
        public ISetLines Text(string text)
        {
            _item.Outputs[^1].Text += text;
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
        public ISetLines ModifiedInput(Func<object, string> modifier)
        {
            _item.Modifiers.Add(modifier);
            int c = _item.Modifiers.Count;
            _item.Outputs[^1].Text += $"{{{c}}}";
            return this;
        }
    }
}
