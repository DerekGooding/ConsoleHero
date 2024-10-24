﻿using ConsoleHero.Helpers;

namespace ConsoleHero;

/// <summary>
/// A static set of settings that affect all future console text printed with ConsoleHero.
/// </summary>
public static class GlobalSettings
{
    /// <summary>
    /// This is the default color all text will be. If unset, will be White.
    /// </summary>
    public static Color DefaultTextColor { get; set; } = ColorHelper.ConsoleColorToDrawingColor(ConsoleColor.White);

    /// <summary>
    /// How many line breaks between menues or paragraphs. Set to 1 by default.
    /// </summary>
    public static int Spacing { get; set; } = 1;
}
