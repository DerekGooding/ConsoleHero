namespace ConsoleHero;
public static class ListExtensions
{
    public static MenuOption[] ToOptions(this IEnumerable<ColorLine> list, Action<string> effect, Func<string, bool>? condition = null)
    => condition == null
    ? [.. list.Select(x => Description(x.Text).Color(x.Color).GoTo(() => effect(x.Text)))]
    : [.. list.Select(x => Description(x.Text).Color(x.Color).If(() => condition(x.Text)).GoTo(() => effect(x.Text)))];

    public static MenuOption[] ToOptions(this IEnumerable<string> list, Action<string> effect, Func<string, bool>? condition = null)
        => list.Select(x => new ColorLine(x, GlobalSettings.DefaultTextColor)).ToOptions(effect, condition);

    public static MenuOption[] ToOptions(this IEnumerable<ColorLine> list, INode node, Func<string, bool>? condition = null)
    => list.ToOptions(node.Call, condition);

    public static MenuOption[] ToOptions(this IEnumerable<string> list, INode node, Func<string, bool>? condition = null)
        => list.ToOptions(node.Call, condition);
}
