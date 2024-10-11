using ConsoleHero.Helpers;

namespace ConsoleHero;

public static class GlobalSettings
{
    public static Color DefaultTextColor { get; set; } = ColorHelper.ConsoleColorToDrawingColor(ConsoleColor.White);

    public static int Spacing { get; set; } = 1;
}
