using static ConsoleHero.TuneBuilder;

namespace ConsoleHero.Test.Builders;

[TestClass]
public class TuneBuilderTests
{
#pragma warning disable CS8618 //Allow nullable. TestInitialize ensures.
    private Tune _tune;
#pragma warning restore CS8618

    [TestInitialize]
    public void SetUp() => _tune = Beep();

    private (int tone, int duration) GetFirstNote()
    {
        Assert.IsTrue(_tune.Notes.Count > 0, "No notes were added to the tune.");
        return (_tune.Notes[0].Tone, _tune.Notes[0].Duration);
    }

    [TestMethod]
    public void Note_WithToneAndDuration_AddsCorrectNote()
    {
        _tune = Note(Tone.A, Duration.QUARTER).Beep();

        (int tone, int duration) = GetFirstNote();
        Assert.AreEqual(220, tone);
        Assert.AreEqual(400, duration);
    }

    [TestMethod]
    public void Note_WithCustomToneAndDuration_AddsCorrectNote()
    {
        _tune = Note(440, Duration.WHOLE).Beep();

        (int tone, int duration) = GetFirstNote();
        Assert.AreEqual(440, tone);
        Assert.AreEqual(1600, duration);
    }

    [TestMethod]
    public void Note_WithToneAndCustomDuration_AddsCorrectNote()
    {
        _tune = Note(Tone.B, 500).Beep();

        (int tone, int duration) = GetFirstNote();
        Assert.AreEqual(247, tone);
        Assert.AreEqual(500, duration);
    }

    [TestMethod]
    public void Quarter_WithTone_AddsNoteWithQuarterDuration()
    {
        _tune = Quarter(Tone.G).Beep();

        (int tone, int duration) = GetFirstNote();
        Assert.AreEqual(392, tone);
        Assert.AreEqual(400, duration);
    }

    [TestMethod]
    public void Whole_WithTone_AddsNoteWithWholeDuration()
    {
        _tune = Whole(Tone.C).Beep();

        (int tone, int duration) = GetFirstNote();
        Assert.AreEqual(262, tone);
        Assert.AreEqual(1600, duration);
    }

    [TestMethod]
    public void GoTo_WithAction_SetsEffectOnTune()
    {
        bool effectCalled = false;
        Action action = () => effectCalled = true;

        Tune tune = Note(Tone.A, Duration.QUARTER).GoTo(action).WaitToPlay();
        Assert.IsNotNull(tune.Effect);

        tune.Effect?.Invoke();
        Assert.IsTrue(effectCalled, "Action was not called as expected.");
    }

    [TestMethod]
    public void WaitToPlay_Default_ReturnsTuneWithWaitEnabled()
    {
        _tune = Quarter(Tone.E).WaitToPlay();
        Assert.IsTrue(_tune.Wait, "Wait should be enabled by default.");
    }

    [TestMethod]
    public void ContinueWhilePlaying_SetsWaitToFalse()
    {
        _tune = Quarter(Tone.E).ContinueWhilePlaying();
        Assert.IsFalse(_tune.Wait, "Wait should be disabled.");
    }

    [TestMethod]
    public void MultipleNotes_AddedInSequence()
    {
        _tune = Note(Tone.A, Duration.HALF)
                            .Quarter(Tone.C)
                            .Eighth(Tone.G)
                            .Beep();

        Assert.AreEqual(3, _tune.Notes.Count, "Tune should contain three notes.");

        Tune.Note firstNote = _tune.Notes[0];
        Tune.Note secondNote = _tune.Notes[1];
        Tune.Note thirdNote = _tune.Notes[2];

        Assert.AreEqual(220, firstNote.Tone);
        Assert.AreEqual(800, firstNote.Duration);
        Assert.AreEqual(262, secondNote.Tone);
        Assert.AreEqual(400, secondNote.Duration);
        Assert.AreEqual(392, thirdNote.Tone);
        Assert.AreEqual(200, thirdNote.Duration);
    }
}
