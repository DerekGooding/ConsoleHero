namespace ConsoleHero;
public static class ListExtensions
{
    public static MenuOption[] ToOptions(this IEnumerable<string> list, Action<string> effect, Func<string, bool>? condition = null)
        => condition == null
        ? [.. list.Select(x => Description(x).GoTo(() => effect(x)))]
        : [.. list.Select(x => Description(x).If(() => condition(x)).GoTo(() => effect(x)))];

    public static MenuOption[] ToOptions(this IEnumerable<ColorLine> list, Action<string> effect, Func<string, bool>? condition = null)
        => condition == null
        ? [.. list.Select(x => Description(x.Text).Color(x.Color).GoTo(() => effect(x.Text)))]
        : [.. list.Select(x => Description(x.Text).Color(x.Color).If(() => condition(x.Text)).GoTo(() => effect(x.Text)))];

    public static MenuOption[] ToOptions<T>(this List<T> list, Action<T> effect, Func<T, bool>? condition = null)
    => condition == null
        ? [.. list.Select(x => Description(x?.ToString() ?? string.Empty).GoTo(() => effect(x)))]
        : [.. list.Select(x => Description(x?.ToString() ?? string.Empty).If(() => condition(x)).GoTo(() => effect(x)))];
}
