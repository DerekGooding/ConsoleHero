namespace ConsoleHero.Model;

internal readonly struct InputPlaceholder(Color color) : ILineComponent
{
    Color ILineComponent.Color { get; } = color;
}
