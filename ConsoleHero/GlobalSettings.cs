using ConsoleHero.Interfaces;
using ConsoleHero.Services;

namespace ConsoleHero;

/// <summary>
/// A static set of settings that affect all future console text printed with ConsoleHero.
/// </summary>
public static class GlobalSettings
{
    /// <summary>
    /// This is the default color all text will be. If unset, will be White.
    /// </summary>
    public static Color DefaultTextColor { get; set; } = IColorService.ConsoleColorToDrawingColor(ConsoleColor.White);

    /// <summary>
    /// How many line breaks between menues or paragraphs. Set to 1 by default.
    /// </summary>
    public static int Spacing { get; set; } = 1;

    /// <summary>
    /// By default will use the standard Console. If you want to adapt this to other output sources, 
    /// create a new <see cref="IConsoleService"/> and set this GlobalSetting.
    /// </summary>
    public static IConsoleService Service
    {
        get
        {
            _service ??= new ConsoleService();
            return _service;
        }
        set => _service = value;
    }

    public static IColorService ColorService
    {
        get
        {
            _colorService ??= new ColorService();
            return _colorService;
        }
        set => _colorService = value;
    }

    private static IConsoleService? _service;
    private static IColorService? _colorService;
}
