using ConsoleHero.Helpers;

namespace ConsoleHero;
internal static class GlobalSettings
{
    internal static ConsoleColor DefaultTextColor { get; set; } = ConsoleColor.White;

    internal static int Spacing { get; set; } = 1;

    internal static void SetColorToDefault()
    {
        //ForegroundColor = DefaultTextColor;
        ColorHelper.SetRgbTextColor(100,100,100);
    }
}
