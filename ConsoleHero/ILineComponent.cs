namespace ConsoleHero;

public interface ILineComponent
{
    public Color Color { get; }
}

public readonly struct InputPlaceholder(Color? color = null) : ILineComponent
{
    public Color Color { get; } = color ?? GlobalSettings.DefaultTextColor;
}

public readonly struct InputModifier(Func<object, string> modifier, Color? color = null) : ILineComponent
{
    public Func<object, string> Modifier { get; } = modifier;
    public Color Color { get; } = color ?? GlobalSettings.DefaultTextColor;
}