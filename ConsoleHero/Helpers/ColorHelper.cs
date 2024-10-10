namespace ConsoleHero.Helpers;

internal static class ColorHelper
{
    internal static void SetRgbTextColor(byte r, byte g, byte b) => Write($"\u001b[38;2;{r};{g};{b}m");

    internal static void ResetColor() => Write("\u001b[0m");
}