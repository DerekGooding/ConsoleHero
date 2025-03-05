using ConsoleHero.Interfaces;
using Moq;

namespace ConsoleHero.Test;

[TestClass]
public class TuneTests
{
#pragma warning disable CS8618 //Allow nullable. TestInitialize ensures.
    private Mock<IBeepHelper> _mockBeepHelper;
    private Tune _tune;
#pragma warning restore CS8618

    [TestInitialize]
    public void Setup()
    {
        _mockBeepHelper = new Mock<IBeepHelper>();
        _tune = new Tune
        {
            _beepHelper = _mockBeepHelper.Object,
            Wait = true
        };
    }

    [TestMethod]
    public void Play_WhenNotesAreEmpty_CallsBeepOnce()
    {
        _tune.Play();

        _mockBeepHelper.Verify(beep => beep.Beep(), Times.Once, "Beep should be called once when Notes is empty");
    }

    [TestMethod]
    public void Play_WhenNotesArePresent_CallsBeepForEachNote()
    {
        _tune.Notes =
        [
            new(440, 500),
            new(880, 300)
        ];

        _tune.Play();

        _mockBeepHelper.Verify(beep => beep.Beep(440, 500), Times.Once, "Beep should be called with frequency 440 and duration 500");
        _mockBeepHelper.Verify(beep => beep.Beep(880, 300), Times.Once, "Beep should be called with frequency 880 and duration 300");
    }

    [TestMethod]
    public void Call_WhenWaitIsTrue_CallsPlaySynchronously()
    {
        var playCalled = false;
        _tune = new Tune
        {
            Wait = true,
            Effect = () => playCalled = true
        };

        _tune.Call();

        Assert.IsTrue(playCalled, "Effect should be called after synchronous Play");
    }

    [TestMethod]
    public void Call_WhenWaitIsFalse_RunsPlayAsynchronously()
    {
        _tune.Wait = false;

        _tune.Call();

        Thread.Sleep(100); // Allow time for async Play to run
        _mockBeepHelper.Verify(beep => beep.Beep(), Times.AtLeastOnce, "Beep should be called in asynchronous Play");
    }
}