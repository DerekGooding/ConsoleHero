﻿namespace ConsoleHero;

/// <summary>
/// Extension methods for converting collections of strings and ColorLine objects to MenuOption arrays.
/// </summary>
internal static class ListExtensions
{
    /// <summary>
    /// Converts an IEnumerable of ColorLine objects to an array of MenuOption objects. Each MenuOption includes a description, color, and an optional condition before triggering the effect.
    /// </summary>
    /// <param name="list">The collection of ColorLine objects.</param>
    /// <param name="effect">The action to perform when a menu option is selected.</param>
    /// <param name="condition">Optional condition that must be met to enable the option.</param>
    /// <returns>An array of MenuOption objects based on the provided ColorLine collection.</returns>
    internal static MenuOption[] ToOptions(this IEnumerable<ColorText> list, Action<string> effect, Func<string, bool>? condition = null)
    => condition == null
    ? [.. list.Select(x => new MenuOption() { Description = x.Text, Color = ((ILineComponent)x).Color, Effect = () => effect(x.Text) })]
    : [.. list.Select(x => new MenuOption() { Description = x.Text, Color = ((ILineComponent)x).Color, Effect = () => effect(x.Text), Check = () => condition(x.Text) })];

    /// <summary>
    /// Converts an IEnumerable of strings to an array of MenuOption objects. Each string is wrapped in a ColorLine using a default color.
    /// </summary>
    /// <param name="list">The collection of strings.</param>
    /// <param name="effect">The action to perform when a menu option is selected.</param>
    /// <param name="condition">Optional condition that must be met to enable the option.</param>
    /// <returns>An array of MenuOption objects based on the provided string collection.</returns>
    internal static MenuOption[] ToOptions(this IEnumerable<string> list, Action<string> effect, Func<string, bool>? condition = null)
        => list.Select(x => new ColorText(x, GlobalSettings.DefaultTextColor)).ToOptions(effect, condition);

    /// <summary>
    /// Converts an IEnumerable of ColorLine objects to an array of MenuOption objects, linking the effect to an INode object.
    /// </summary>
    /// <param name="list">The collection of ColorLine objects.</param>
    /// <param name="node">The node that contains the action to perform when a menu option is selected.</param>
    /// <param name="condition">Optional condition that must be met to enable the option.</param>
    /// <returns>An array of MenuOption objects based on the provided ColorLine collection.</returns>
    internal static MenuOption[] ToOptions(this IEnumerable<ColorText> list, INode node, Func<string, bool>? condition = null)
    => list.ToOptions(node.Call, condition);

    /// <summary>
    /// Converts an IEnumerable of strings to an array of MenuOption objects, linking the effect to an INode object. Each string is wrapped in a ColorLine using a default color.
    /// </summary>
    /// <param name="list">The collection of strings.</param>
    /// <param name="node">The node that contains the action to perform when a menu option is selected.</param>
    /// <param name="condition">Optional condition that must be met to enable the option.</param>
    /// <returns>An array of MenuOption objects based on the provided string collection.</returns>
    internal static MenuOption[] ToOptions(this IEnumerable<string> list, INode node, Func<string, bool>? condition = null)
        => list.ToOptions(node.Call, condition);
}
