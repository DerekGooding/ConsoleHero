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

    #region ColorLine color defaults
    public static ColorLine Black(string text) => new(text, ConsoleColor.Black);
    public static ColorLine DarkBlue(string text) => new(text, ConsoleColor.DarkBlue);
    public static ColorLine DarkGreen(string text) => new(text, ConsoleColor.DarkGreen);
    public static ColorLine DarkCyan(string text) => new(text, ConsoleColor.DarkCyan);
    public static ColorLine DarkRed(string text) => new(text, ConsoleColor.DarkRed);
    public static ColorLine DarkMagenta(string text) => new(text, ConsoleColor.DarkMagenta);
    public static ColorLine DarkYellow(string text) => new(text, ConsoleColor.DarkYellow);
    public static ColorLine Gray(string text) => new(text, ConsoleColor.Gray);
    public static ColorLine DarkGray(string text) => new(text, ConsoleColor.DarkGray);
    public static ColorLine Blue(string text) => new(text, ConsoleColor.Blue);
    public static ColorLine Green(string text) => new(text, ConsoleColor.Green);
    public static ColorLine Cyan(string text) => new(text, ConsoleColor.Cyan);
    public static ColorLine Red(string text) => new(text, ConsoleColor.Red);
    public static ColorLine Magenta(string text) => new(text, ConsoleColor.Magenta);
    public static ColorLine Yellow(string text) => new(text, ConsoleColor.Yellow);
    public static ColorLine White(string text) => new(text, ConsoleColor.White);
    public static ColorLine NoColor(string text) => new(text, ConsoleColor.White);
    #endregion
}
