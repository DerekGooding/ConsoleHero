using ConsoleHero.Interfaces;

namespace ConsoleHero.Services;

internal class ColorService : IColorService
{
    void IColorService.SetTextColor(Color color)
        => GlobalSettings.ColorService.SetTextColor(color.R, color.G, color.B);

    void IColorService.SetTextColor(ConsoleColor consoleColor)
        => GlobalSettings.ColorService.SetTextColor(IColorService.ConsoleColorToDrawingColor(consoleColor));

    void IColorService.SetTextColor(byte r, byte g, byte b)
        => GlobalSettings.Service.Write($"\u001b[38;2;{r};{g};{b}m");

    void IColorService.SetToDefault() =>
        GlobalSettings.ColorService.SetTextColor(GlobalSettings.DefaultTextColor);
}