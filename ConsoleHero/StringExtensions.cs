namespace ConsoleHero;
/// <summary>
/// Extension methods for converting strings to ColorLine objects with specific or default colors.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Converts a string to a ColorLine object with the specified color.
    /// </summary>
    /// <param name="text">The text to be converted into a ColorLine.</param>
    /// <param name="color">The color to apply to the text.</param>
    /// <returns>A ColorLine object with the specified color.</returns>
    public static ColorText Color(this string text, Color color) => new(text, color);

    /// <summary>
    /// Converts a string to a ColorLine object with the default color.
    /// </summary>
    /// <param name="text">The text to be converted into a ColorLine.</param>
    /// <returns>A ColorLine object with the default color.</returns>
    public static ColorText DefaultColor(this string text) => new(text);
}
