namespace ConsoleHero.Helpers;

internal static class ColorHelper
{
    internal static void SetTextColor(Color color) => SetRgbTextColor(color.R, color.G, color.B);

    internal static void SetTextColor(ConsoleColor consoleColor) => SetTextColor(ConsoleColorToDrawingColor(consoleColor));

    internal static Color ConsoleColorToDrawingColor(ConsoleColor consoleColor) => consoleColor switch
    {
        ConsoleColor.Black => Color.Black,
        ConsoleColor.DarkBlue => Color.FromArgb(0, 0, 139),
        ConsoleColor.DarkGreen => Color.FromArgb(0, 100, 0),
        ConsoleColor.DarkCyan => Color.FromArgb(0, 139, 139),
        ConsoleColor.DarkRed => Color.FromArgb(139, 0, 0),
        ConsoleColor.DarkMagenta => Color.FromArgb(139, 0, 139),
        ConsoleColor.DarkYellow => Color.FromArgb(189, 183, 107),
        ConsoleColor.Gray => Color.Gray,
        ConsoleColor.DarkGray => Color.DarkGray,
        ConsoleColor.Blue => Color.Blue,
        ConsoleColor.Green => Color.Green,
        ConsoleColor.Cyan => Color.Cyan,
        ConsoleColor.Red => Color.Red,
        ConsoleColor.Magenta => Color.Magenta,
        ConsoleColor.Yellow => Color.Yellow,
        ConsoleColor.White => Color.White,
        _ => Color.Black,
    };

    private static void SetRgbTextColor(byte r, byte g, byte b) => Write($"\u001b[38;2;{r};{g};{b}m");

    //internal static void ResetColor() => Write("\u001b[0m");

    internal static void SetToDefault() => SetTextColor(GlobalSettings.DefaultTextColor);
}