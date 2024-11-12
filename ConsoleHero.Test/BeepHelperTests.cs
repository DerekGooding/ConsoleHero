using ConsoleHero.Helpers;
using ConsoleHero.Interfaces;
using Moq;

namespace ConsoleHero.Test;

[TestClass]
public class BeepHelperTests
{
#pragma warning disable CS8618 //Allow nullable. TestInitialize ensures.
    private Mock<IPlatformHelper> _platformHelperMock;
    private BeepHelper _beepHelper;
#pragma warning restore CS8618

    [TestInitialize]
    public void TestInitialize()
    {
        _platformHelperMock = new Mock<IPlatformHelper>();
        _beepHelper = new BeepHelper(_platformHelperMock.Object);
    }

    [TestMethod]
    public void Beep_NoParameters_WritesBellCharacter()
    {
        using ConsoleOutput consoleOutput = new();

        BeepHelper.Beep();

        // Assert
        Assert.AreEqual("\a", consoleOutput.GetOutput());
    }

    [TestMethod]
    public void Beep_LinuxPlatform_StartsProcess()
    {
        _platformHelperMock.Setup(p => p.IsWindows).Returns(false);
        _platformHelperMock.Setup(p => p.IsLinux).Returns(true);

        // Act
        _beepHelper.Beep(1000, 500);

        // Assert
        // Add appropriate assertions for process calls if applicable.
    }

    [TestMethod]
    public void Beep_MacOSPlatform_StartsProcess()
    {
        _platformHelperMock.Setup(p => p.IsWindows).Returns(false);
        _platformHelperMock.Setup(p => p.IsOSX).Returns(true);

        // Act
        _beepHelper.Beep(1000, 500);

        // Assert
        // Add appropriate assertions for process calls if applicable.
    }

    [TestMethod]
    public void Beep_NonWindowsPlatformWithException_WritesBellCharacter()
    {
        _platformHelperMock.Setup(p => p.IsWindows).Returns(false);
        _platformHelperMock.Setup(p => p.IsLinux).Returns(true);
        using ConsoleOutput consoleOutput = new();

        // Act
        _beepHelper.Beep(1000, 500);

        // Assert
        Assert.AreEqual("\a", consoleOutput.GetOutput());
    }
}
