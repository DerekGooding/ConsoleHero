using ConsoleHero.Interfaces;

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
    internal static MenuOption[] ToOptions(this IEnumerable<ColorText> list, Action effect, Func<string, bool>? condition = null)
    {
        int capacity = list is ICollection<ColorText> collection ? collection.Count : 4;
        List<MenuOption> options = new(capacity);
        foreach (ColorText x in list)
        {
            MenuOption menuOption = new()
            {
                Description = x.Text,
                Color = x.Color,
                Effect = effect
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
    internal static MenuOption[] ToOptions(this IEnumerable<string> list, Action effect, Func<string, bool>? condition = null)
    {
        int capacity = list is ICollection<string> collection ? collection.Count : 4;
        List<MenuOption> options = new(capacity);
        Color defaultColor = GlobalSettings.DefaultTextColor;

        foreach (string text in list)
        {
            MenuOption menuOption = new()
            {
                Description = text,
                Color = defaultColor,
                Effect = effect
            };

            if (condition != null)
            {
                menuOption.Check = () => condition(text);
            }

            options.Add(menuOption);
        }

        return options.ToArray();
    }

    /// <summary>
    /// Converts an IEnumerable of ColorLine objects to an array of MenuOption objects, linking the effect to an INode object.
    /// </summary>
    /// <param name="list">The collection of ColorLine objects.</param>
    /// <param name="node">The node that contains the action to perform when a menu option is selected.</param>
    /// <param name="condition">Optional condition that must be met to enable the option.</param>
    /// <returns>An array of MenuOption objects based on the provided ColorLine collection.</returns>
    internal static MenuOption[] ToOptions(this IEnumerable<ColorText> list, INode node, Func<string, bool>? condition = null)
    {
        Action nodeAction = node.Call;
        return list.ToOptions(nodeAction, condition);
    }

    /// <summary>
    /// Converts an IEnumerable of strings to an array of MenuOption objects, linking the effect to an INode object. Each string is wrapped in a ColorLine using a default color.
    /// </summary>
    /// <param name="list">The collection of strings.</param>
    /// <param name="node">The node that contains the action to perform when a menu option is selected.</param>
    /// <param name="condition">Optional condition that must be met to enable the option.</param>
    /// <returns>An array of MenuOption objects based on the provided string collection.</returns>
    internal static MenuOption[] ToOptions(this IEnumerable<string> list, INode node, Func<string, bool>? condition = null)
    {
        Action nodeAction = node.Call;
        return list.ToOptions(nodeAction, condition);
    }

    internal static MenuOption[] ToOptions<T>(this IEnumerable<T> list, Action<T> effect, Func<string, bool>? condition = null)
    {
        int capacity = list is ICollection<T> collection ? collection.Count : 4;
        List<MenuOption> options = new(capacity);

        foreach (T x in list)
        {
            ColorText colorText = x is IMenuOption iMenuOption ? iMenuOption.Print() : (x?.ToString() ?? string.Empty).DefaultColor();

            MenuOption menuOption = new()
            {
                Description = colorText.Text,
                Color = colorText.Color,
                Effect = () => effect(x)
            };
            if (condition != null)
            {
                menuOption.Check = () => condition(colorText.Text);
            }

            options.Add(menuOption);
        }

        return options.ToArray();
    }

    internal static MenuOption[] ToOptions<T>(this IEnumerable<T> list, Func<T, INode> effect, Func<string, bool>? condition = null)
    {
        int capacity = list is ICollection<T> collection ? collection.Count : 4;
        List<MenuOption> options = new(capacity);

        foreach (T x in list)
        {
            ColorText colorText = x is IMenuOption iMenuOption ? iMenuOption.Print() : (x?.ToString() ?? string.Empty).DefaultColor();

            MenuOption menuOption = new()
            {
                Description = colorText.Text,
                Color = colorText.Color,
                Effect = () => effect(x).Call()
            };
            if (condition != null)
            {
                menuOption.Check = () => condition(colorText.Text);
            }
            options.Add(menuOption);
        }
        return options.ToArray();
    }

    internal static void Print(this IEnumerable<MenuOption> list, string separator)
    {
        var service = GlobalSettings.Service;
        var colorService = GlobalSettings.ColorService;

        foreach (MenuOption option in list)
        {
            string text = $"{option.Key} {separator} {option.Description}";
            colorService.SetTextColor(option.Color);
            service.WriteLine(text);
        }

        for (int i = 0; i < GlobalSettings.Spacing; i++)
            service.WriteLine();

        colorService.SetToDefault();
    }

    internal static void Print(this IList<ParagraphLine> list)
    {
        var service = GlobalSettings.Service;
        var colorService = GlobalSettings.ColorService;

        foreach (ParagraphLine line in list)
        {
            foreach (ColorText component in line.Components)
            {
                colorService.SetTextColor(component.Color);

                service.Write(component.Text);
            }
            service.WriteLine();
        }
        for (int i = 0; i < GlobalSettings.Spacing; i++)
            service.WriteLine();

        colorService.SetToDefault();
    }
}
