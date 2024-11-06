namespace ConsoleHero.Model;

internal readonly struct InputModifier(Func<string, string> modifier, Color? color = null) : ILineComponent
{
    internal Func<string, string> Modifier { get; } = modifier;
    Color ILineComponent.Color { get; } = color ?? GlobalSettings.DefaultTextColor;
}