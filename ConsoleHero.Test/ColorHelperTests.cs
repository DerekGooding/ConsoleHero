using ConsoleHero.Interfaces;
using ConsoleHero.Services;

namespace ConsoleHero.Test;

[TestClass]
public class ColorHelperTests
{
    private readonly StringWriter _consoleOutput = new();

    [TestInitialize]
    public void SetUp() => Console.SetOut(_consoleOutput);

    [TestCleanup]
    public void TearDown()
    {
        Console.SetOut(Console.Out);
        _consoleOutput.Dispose();
    }

    [TestMethod]
    public void SetTextColor_WithColor_WritesExpectedAnsiCode()
    {
        IColorService colorService = new ColorService();
        var color = Color.FromArgb(255, 0, 0);

        colorService.SetTextColor(color);

        const string expectedOutput = "\u001b[38;2;255;0;0m";
        Assert.AreEqual(expectedOutput, _consoleOutput.ToString());
    }

    [DataTestMethod]
    [DataRow(ConsoleColor.Black, 0, 0, 0)]
    [DataRow(ConsoleColor.DarkBlue, 0, 0, 139)]
    [DataRow(ConsoleColor.DarkGreen, 0, 100, 0)]
    [DataRow(ConsoleColor.DarkCyan, 0, 139, 139)]
    [DataRow(ConsoleColor.DarkRed, 139, 0, 0)]
    [DataRow(ConsoleColor.DarkMagenta, 139, 0, 139)]
    [DataRow(ConsoleColor.DarkYellow, 189, 183, 107)]
    [DataRow(ConsoleColor.Gray, 128, 128, 128)]
    [DataRow(ConsoleColor.DarkGray, 169, 169, 169)]
    [DataRow(ConsoleColor.Blue, 0, 0, 255)]
    [DataRow(ConsoleColor.Green, 0, 128, 0)]
    [DataRow(ConsoleColor.Cyan, 0, 255, 255)]
    [DataRow(ConsoleColor.Red, 255, 0, 0)]
    [DataRow(ConsoleColor.Magenta, 255, 0, 255)]
    [DataRow(ConsoleColor.Yellow, 255, 255, 0)]
    [DataRow(ConsoleColor.White, 255, 255, 255)]
    public void SetTextColor_WithConsoleColor_WritesExpectedAnsiCode(
        ConsoleColor consoleColor,
        int expectedR,
        int expectedG,
        int expectedB)
    {
        IColorService colorService = new ColorService();

        colorService.SetTextColor(consoleColor);

        var expectedOutput = $"\u001b[38;2;{expectedR};{expectedG};{expectedB}m";
        Assert.AreEqual(expectedOutput, _consoleOutput.ToString());
    }

    [TestMethod]
    public void ConsoleColorToDrawingColor_ReturnsExpectedColor()
    {
        const ConsoleColor consoleColor = ConsoleColor.Green;

        var color = IColorService.ConsoleColorToDrawingColor(consoleColor);

        Assert.AreEqual(Color.Green, color);
    }

    [TestMethod]
    public void SetToDefault_SetsGlobalDefaultTextColor()
    {
        IColorService colorService = new ColorService();
        GlobalSettings.DefaultTextColor = Color.FromArgb(0, 255, 255);

        colorService.SetToDefault();

        const string expectedOutput = "\u001b[38;2;0;255;255m";
        Assert.AreEqual(expectedOutput, _consoleOutput.ToString());
    }
}
