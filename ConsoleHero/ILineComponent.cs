namespace ConsoleHero;

public interface ILineComponent;

public struct ColoredCharacter : ILineComponent
{
    public char Character { get; set; }

    public Color Color { get; set; }
}

public struct InputPlaceholder : ILineComponent
{
    public Color Color { get; set; }
}

public struct InputModifier : ILineComponent
{
    public Func<object, string>? Modifier { get; set; }
    public Color Color { get; set; }
}
