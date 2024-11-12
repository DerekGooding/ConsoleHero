namespace ConsoleHero.Model;

internal readonly struct InputPlaceholder(Color? color = null) : ILineComponent
{
    Color ILineComponent.Color { get; } = color ?? GlobalSettings.DefaultTextColor;
}
