﻿namespace ConsoleHero;

internal interface ILineComponent
{
    internal Color Color { get; }
}

internal readonly struct InputPlaceholder(Color? color = null) : ILineComponent
{
    Color ILineComponent.Color { get; } = color ?? GlobalSettings.DefaultTextColor;
}

internal readonly struct InputModifier(Func<string, string> modifier, Color? color = null) : ILineComponent
{
    internal Func<string, string> Modifier { get; } = modifier;
    Color ILineComponent.Color { get; } = color ?? GlobalSettings.DefaultTextColor;
}