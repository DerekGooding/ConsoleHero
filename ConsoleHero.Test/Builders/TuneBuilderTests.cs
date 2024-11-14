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

    private (int tone, int duration) GetSecondNote()
    {
        Assert.IsTrue(_tune.Notes.Count > 1, "No notes were added to the tune.");
        return (_tune.Notes[1].Tone, _tune.Notes[1].Duration);
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
    [DataRow(Tone.A, Duration.SIXTEENTH, 220, 100)]
    [DataRow(Tone.B, Duration.EIGHTH, 247, 200)]
    [DataRow(Tone.C, Duration.QUARTER, 262, 400)]
    [DataRow(Tone.D, Duration.HALF, 294, 800)]
    [DataRow(Tone.E, Duration.WHOLE, 330, 1600)]
    public void Note_WithTone(Tone t, Duration d, int expectedFrequency, int expectedDuration)
    {
        _tune = Note(t, d).Note(t, d).Beep();

        (int tone, int duration) = GetFirstNote();
        Assert.AreEqual(expectedFrequency, tone);
        Assert.AreEqual(expectedDuration, duration);
        (int tone2, int duration2) = GetSecondNote();
        Assert.AreEqual(expectedFrequency, tone2);
        Assert.AreEqual(expectedDuration, duration2);
    }

    [TestMethod]
    [DataRow(Tone.A, 220)]
    [DataRow(Tone.B, 247)]
    [DataRow(Tone.C, 262)]
    [DataRow(Tone.D, 294)]
    [DataRow(Tone.E, 330)]
    [DataRow(Tone.F, 349)]
    [DataRow(Tone.G, 392)]
    public void Sixteenth_WithTone_AddsNoteWithQuarterDuration(Tone t, int expectedFrequency)
    {
        _tune = Sixteeth(t).Sixteeth(t).Beep();

        (int tone, int duration) = GetFirstNote();
        Assert.AreEqual(expectedFrequency, tone);
        Assert.AreEqual(100, duration);
        (int tone2, int duration2) = GetSecondNote();
        Assert.AreEqual(expectedFrequency, tone2);
        Assert.AreEqual(100, duration2);
    }

    [TestMethod]
    [DataRow(Tone.A, 220)]
    [DataRow(Tone.B, 247)]
    [DataRow(Tone.C, 262)]
    [DataRow(Tone.D, 294)]
    [DataRow(Tone.E, 330)]
    [DataRow(Tone.F, 349)]
    [DataRow(Tone.G, 392)]
    public void Eighth_WithTone_AddsNoteWithQuarterDuration(Tone t, int expectedFrequency)
    {
        _tune = Eighth(t).Eighth(t).Beep();

        (int tone, int duration) = GetFirstNote();
        Assert.AreEqual(expectedFrequency, tone);
        Assert.AreEqual(200, duration);
        (int tone2, int duration2) = GetSecondNote();
        Assert.AreEqual(expectedFrequency, tone2);
        Assert.AreEqual(200, duration2);
    }

    [TestMethod]
    [DataRow(Tone.A, 220)]
    [DataRow(Tone.B, 247)]
    [DataRow(Tone.C, 262)]
    [DataRow(Tone.D, 294)]
    [DataRow(Tone.E, 330)]
    [DataRow(Tone.F, 349)]
    [DataRow(Tone.G, 392)]
    public void Quarter_WithTone_AddsNoteWithQuarterDuration(Tone t, int expectedFrequency)
    {
        _tune = Quarter(t).Quarter(t).Beep();

        (int tone, int duration) = GetFirstNote();
        Assert.AreEqual(expectedFrequency, tone);
        Assert.AreEqual(400, duration);
        (int tone2, int duration2) = GetSecondNote();
        Assert.AreEqual(expectedFrequency, tone2);
        Assert.AreEqual(400, duration2);
    }

    [TestMethod]
    [DataRow(Tone.A, 220)]
    [DataRow(Tone.B, 247)]
    [DataRow(Tone.C, 262)]
    [DataRow(Tone.D, 294)]
    [DataRow(Tone.E, 330)]
    [DataRow(Tone.F, 349)]
    [DataRow(Tone.G, 392)]
    public void Half_WithTone_AddsNoteWithWholeDuration(Tone t, int expectedFrequency)
    {
        _tune = Half(t).Half(t).Beep();

        (int tone, int duration) = GetFirstNote();
        Assert.AreEqual(expectedFrequency, tone);
        Assert.AreEqual(800, duration);
        (int tone2, int duration2) = GetSecondNote();
        Assert.AreEqual(expectedFrequency, tone2);
        Assert.AreEqual(800, duration2);
    }

    [TestMethod]
    [DataRow(Tone.A, 220)]
    [DataRow(Tone.B, 247)]
    [DataRow(Tone.C, 262)]
    [DataRow(Tone.D, 294)]
    [DataRow(Tone.E, 330)]
    [DataRow(Tone.F, 349)]
    [DataRow(Tone.G, 392)]
    public void Whole_WithTone_AddsNoteWithWholeDuration(Tone t, int expectedFrequency)
    {
        _tune = Whole(t).Whole(t).Beep();

        (int tone, int duration) = GetFirstNote();
        Assert.AreEqual(expectedFrequency, tone);
        Assert.AreEqual(1600, duration);
        (int tone2, int duration2) = GetSecondNote();
        Assert.AreEqual(expectedFrequency, tone2);
        Assert.AreEqual(1600, duration2);
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
