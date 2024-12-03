using ConsoleHero.Helpers;

namespace ConsoleHero;

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
    {
        List<MenuOption> options = new();
        foreach (ColorText x in list)
        {
            Color color = ((ILineComponent)x).Color;
            MenuOption menuOption = new()
            {
                Description = x.Text,
                Color = color,
                Effect = () => effect(x.Text)
            };
            if (condition != null)
            {
                menuOption.Check = () => condition(x.Text);
            }
            options.Add(menuOption);
        }
        return options.ToArray();
    }

    /// <summary>
    /// Converts an IEnumerable of strings to an array of MenuOption objects. Each string is wrapped in a ColorLine using a default color.
    /// </summary>
    /// <param name="list">The collection of strings.</param>
    /// <param name="effect">The action to perform when a menu option is selected.</param>
    /// <param name="condition">Optional condition that must be met to enable the option.</param>
    /// <returns>An array of MenuOption objects based on the provided string collection.</returns>
    internal static MenuOption[] ToOptions(this IEnumerable<string> list, Action<string> effect, Func<string, bool>? condition = null)
    {
        IEnumerable<ColorText> colorTextList = list.Select(x => new ColorText(x, GlobalSettings.DefaultTextColor));
        return colorTextList.ToOptions(effect, condition);
    }

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

    internal static MenuOption[] ToOptions<T>(this IEnumerable<T> list, Action<T> effect, Func<string, bool>? condition = null) where T : IMenuOption
    {
        List<MenuOption> options = new();
        foreach (IMenuOption x in list)
        {
            ColorText colorText = x.Print();
            Color color = ((ILineComponent)colorText).Color;
            MenuOption menuOption = new()
            {
                Description = colorText.Text,
                Color = color,
                Effect = () => effect((T)x)
            };
            if (condition != null)
            {
                menuOption.Check = () => condition(colorText.Text);
            }
            options.Add(menuOption);
        }
        return options.ToArray();
    }

    internal static void Print(this IEnumerable<MenuOption> list, string seperator)
    {
        List<ParagraphLine> lines = new();
        foreach (ColorText item in list.Select(x => $"{x.Key} {seperator} {x.Description}".DefaultColor()))
        {
            ParagraphLine paragraphLine = new();
            paragraphLine.Components.Add(item);
            lines.Add(paragraphLine);
        }
        lines.Print();
    }

    internal static void Print(this IList<ParagraphLine> list, string input = "")
    {
        foreach (ParagraphLine line in list)
        {
            foreach (ILineComponent component in line.Components)
            {
                ColorHelper.SetTextColor(component.Color);
                if (component is ColorText c)
                    GlobalSettings.Service.Write(c.Text);
                else if (component is InputPlaceholder)
                    GlobalSettings.Service.Write(input);
                else if (component is InputModifier modifier)
                    GlobalSettings.Service.Write(modifier.Modifier.Invoke(input));
            }
            GlobalSettings.Service.WriteLine();
        }
        for (int i = 0; i < GlobalSettings.Spacing; i++)
            GlobalSettings.Service.WriteLine();

        ColorHelper.SetToDefault();
    }
}
