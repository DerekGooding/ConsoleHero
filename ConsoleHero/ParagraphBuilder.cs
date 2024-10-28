namespace ConsoleHero;
/// <summary>
/// The builder class for handling new <see cref="Paragraph"/>s.
/// </summary>
public static class ParagraphBuilder
{
    /// <summary>
    /// Clear the console when this <see cref="INode"/> is called.
    /// </summary>
    public static ISetLines ClearOnCall() => new Builder().ClearOnCall();

    /// <summary>
    /// Start building a new line of text. Default color.
    /// </summary>
    public static ISetLines Line(string text) => new Builder().Line(text);

    /// <summary>
    /// Start building a new line of text. Custom color.
    /// </summary>
    public static ISetLines Line(string text, Color color) => new Builder().Line(text, color);

    /// <summary>
    /// 
    /// </summary>
    public interface ISetLines
    {
        /// <summary>
        /// Start building a new line of text. Default color.
        /// </summary>
        public ISetLines Line(string text);
        /// <summary>
        /// Start building a new line of text. Custom color.
        /// </summary>
        public ISetLines Line(string text, Color color);
        /// <summary>
        /// Add additional text to the end of this line. Default color.
        /// </summary>
        public ISetLines Text(string text);
        /// <summary>
        /// Add additional text to the end of this line. Custom color.
        /// </summary>
        public ISetLines Text(string text, Color color);
        /// <summary>
        /// Add the input variable to this line. Default Color.
        /// </summary>
        public ISetLines Input();
        /// <summary>
        /// Add the input variable to this line. Custom color.
        /// </summary>
        public ISetLines Input(Color color);
        /// <summary>
        /// Add the input variable to this line but apply some modifier to it.
        /// </summary>
        public ISetLines ModifiedInput(Func<object, string> modifier);
        /// <summary>
        /// After displaying this paragraph, will wait before continuing without input.
        /// </summary>
        public Paragraph Delay(TimeSpan delay);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seconds"></param>
        public Paragraph DelayInSeconds(double seconds);
        /// <summary>
        /// After displaying this paragraph, will wait for the user to press a key to continue.
        /// </summary>
        public Paragraph PressToContinue();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ISetConfirm GoTo(Action action);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public ISetConfirm GoTo(INode node);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ISetConfirm
    {
        /// <summary>
        /// After displaying this paragraph, will wait before continuing without input.
        /// </summary>
        public Paragraph Delay(TimeSpan delay);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public Paragraph DelayInSeconds(double seconds);
        /// <summary>
        /// After displaying this paragraph, will wait for the user to press a key to continue.
        /// </summary>
        public Paragraph PressToContinue();
    }
    private class Builder() : ISetLines, ISetConfirm
    {
        readonly Paragraph _item = new();

        public ISetLines ClearOnCall()
        {
            _item.ClearOnCall = true;
            return this;
        }

        public ISetLines Line(string text)
        {
            ParagraphLine line = new();
            line.Components.Add(new ColorText(text));
            _item.Outputs.Add(line);
            return this;
        }
        public ISetLines Line(string text, Color color)
        {
            ParagraphLine line = new();
            line.Components.Add(new ColorText(text, color));
            _item.Outputs.Add(line);
            return this;
        }
        public ISetLines Text(string text)
        {
            _item.Outputs[^1].Components.Add(new ColorText(text));
            return this;
        }
        public ISetLines Text(string text, Color color)
        {
            _item.Outputs[^1].Components.Add(new ColorText(text, color));
            return this;
        }
        public Paragraph Delay(TimeSpan delay)
        {
            _item.Delay = delay;
            _item.PressToContinue = false;
            return _item;
        }
        public Paragraph DelayInSeconds(double seconds)
        {
            _item.Delay = TimeSpan.FromSeconds(seconds);
            _item.PressToContinue = false;
            return _item;
        }
        public Paragraph PressToContinue() => _item;

        public ISetLines Input()
        {
            _item.Outputs[^1].Components.Add(new InputPlaceholder(GlobalSettings.DefaultTextColor));
            return this;
        }
        public ISetLines Input(Color color)
        {
            _item.Outputs[^1].Components.Add(new InputPlaceholder(color));
            return this;
        }
        public ISetLines ModifiedInput(Func<object, string> modifier)
        {
            _item.Outputs[^1].Components.Add(new InputModifier(modifier));
            return this;
        }

        public ISetConfirm GoTo(Action action)
        {
            _item.Effect = action;
            return this;
        }

        public ISetConfirm GoTo(INode node)
        {
            _item.Effect = () => node.Call();
            return this;
        }
    }
}
