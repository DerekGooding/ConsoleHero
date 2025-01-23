using ConsoleHero.Interfaces;
using System.Drawing;

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
    /// Continue building with one of the following:
    /// <br><see cref="Line(string, Color)"/></br>
    /// <br><see cref="Text(string, Color)"/></br>
    /// <br><see cref="DelayInSeconds(double)"/></br>
    /// <br><see cref="Delay(TimeSpan)"/></br>
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
        /// Add additional text to the end of this line. Custom color.
        /// </summary>
        public ISetLines Text(ColorText colorText);
        /// <summary>
        /// After displaying this paragraph, will wait before continuing without input.
        /// </summary>
        public Paragraph Delay(TimeSpan delay);

        /// <summary>
        /// After displaying this paragraph, will wait before continuing without input.
        /// </summary>
        public Paragraph DelayInSeconds(double seconds);
        /// <summary>
        /// After displaying this paragraph, will immediately continue without delay or prompt.
        /// </summary>
        public Paragraph Immediate();
        /// <summary>
        /// After displaying this paragraph, will wait for the user to press a key to continue.
        /// </summary>
        public Paragraph PressToContinue();

        /// <summary>
        /// The next custom action to be called when this <see cref="MenuOption"/> is chosen.
        /// </summary>
        public ISetConfirm GoTo(Action action);
        /// <summary>
        /// The next <see cref="INode"/> to be called when this <see cref="MenuOption"/> is chosen.
        /// </summary>
        public ISetConfirm GoTo(INode node);
    }

    /// <summary>
    /// Continue building with one of the following:
    /// <br><see cref="DelayInSeconds(double)"/></br>
    /// <br><see cref="Delay(TimeSpan)"/></br>
    /// <br><see cref="PressToContinue()"/></br>
    /// </summary>
    public interface ISetConfirm
    {
        /// <summary>
        /// After displaying this paragraph, will wait before continuing without input.
        /// </summary>
        public Paragraph Delay(TimeSpan delay);

        /// <summary>
        /// After displaying this paragraph, will wait before continuing without input.
        /// </summary>
        public Paragraph DelayInSeconds(double seconds);
        /// <summary>
        /// After displaying this paragraph, will immediately continue without delay or prompt.
        /// </summary>
        public Paragraph Immediate();
        /// <summary>
        /// After displaying this paragraph, will wait for the user to press a key to continue.
        /// </summary>
        public Paragraph PressToContinue();
    }
    private class Builder : ISetLines, ISetConfirm
    {
        readonly Paragraph _item = new();

        public ISetLines ClearOnCall()
        {
            _item.ClearOnCall = true;
            return this;
        }

        public ISetLines Line(string text) => Line(new ColorText(text));
        public ISetLines Line(string text, Color color) => Line(new ColorText(text, color));
        public ISetLines Line(ColorText colorText)
        {
            ParagraphLine line = new();
            line.Components.Add(colorText);
            _item.Outputs.Add(line);
            return this;
        }

        public ISetLines Text(string text) => Text(new ColorText(text));
        public ISetLines Text(string text, Color color) => Text(new ColorText(text, color));
        public ISetLines Text(ColorText colorText)
        {
            _item.Outputs[^1].Components.Add(colorText);
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
        public Paragraph Immediate()
        {
            _item.Delay = TimeSpan.FromSeconds(0);
            _item.PressToContinue = false;
            return _item;
        }
        public Paragraph PressToContinue() => _item;

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
