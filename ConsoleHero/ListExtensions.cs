namespace ConsoleHero;
public static class ListExtensions
{
    public static MenuOption[] ToOptions(this IEnumerable<ColorLine> list, Action<string> effect, Func<string, bool>? condition = null)
    => condition == null
    ? [.. list.Select(x => Description(x.Text).Color(x.Color).GoTo(() => effect(x.Text)))]
    : [.. list.Select(x => Description(x.Text).Color(x.Color).If(() => condition(x.Text)).GoTo(() => effect(x.Text)))];

    public static MenuOption[] ToOptions(this IEnumerable<string> list, Action<string> effect, Func<string, bool>? condition = null)
        => list.Select(x => new ColorLine(x, ConsoleColor.White)).ToOptions(effect, condition);

    public static MenuOption[] ToOptions(this IEnumerable<string> list, Menu menu, Func<string, bool>? condition = null)
        => list.ToOptions(menu.PrintOptions, condition);

    public static MenuOption[] ToOptions(this IEnumerable<string> list, Paragraph paragraph, Func<string, bool>? condition = null)
        => list.ToOptions(paragraph.PrintMessage, condition);

    #region Generics
    public static MenuOption[] ToOptions<T>(this IEnumerable<(T, ColorLine)> list, Action<T> effect, Func<T, bool>? condition = null)
    => condition == null
    ? [.. list.Select(x => Description(x.Item2.Text).Color(x.Item2.Color).GoTo(() => effect(x.Item1)))]
    : [.. list.Select(x => Description(x.Item2.Text).Color(x.Item2.Color).If(() => condition(x.Item1)).GoTo(() => effect(x.Item1)))];

    public static MenuOption[] ToOptions<T>(this IEnumerable<T> list, Action<T> effect, Func<T, bool>? condition = null)
    => list.Select(x => (x, new ColorLine(x?.ToString() ?? string.Empty, ConsoleColor.White))).ToOptions(effect, condition);

    public static MenuOption[] ToOptions<T>(this IEnumerable<T> list, Menu menu, Func<T, bool>? condition = null)
    => list.ToOptions(menu.PrintOptions, condition);

    public static MenuOption[] ToOptions<T>(this IEnumerable<T> list, Paragraph paragraph, Func<T, bool>? condition = null)
    => list.ToOptions(paragraph.PrintMessage, condition);
    #endregion
}
