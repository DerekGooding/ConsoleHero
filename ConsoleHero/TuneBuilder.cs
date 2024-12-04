using ConsoleHero.Interfaces;

namespace ConsoleHero;
/// <summary>
/// The builder class for handling new <see cref="Tune"/>s.
/// </summary>
public static class TuneBuilder
{
    /// <summary>
    /// Represents musical tones with specific frequencies in hertz (Hz).
    /// </summary>
    public enum Tone
    {
        /// <summary>
        /// No sound or rest.
        /// </summary>
        REST = 0,

        /// <summary>
        /// G below middle C, frequency 196 Hz.
        /// </summary>
        GbelowC = 196,

        /// <summary>
        /// A note, frequency 220 Hz.
        /// </summary>
        A = 220,

        /// <summary>
        /// A-sharp note, frequency 233 Hz.
        /// </summary>
        Asharp = 233,

        /// <summary>
        /// B note, frequency 247 Hz.
        /// </summary>
        B = 247,

        /// <summary>
        /// C note, frequency 262 Hz.
        /// </summary>
        C = 262,

        /// <summary>
        /// C-sharp note, frequency 277 Hz.
        /// </summary>
        Csharp = 277,

        /// <summary>
        /// D note, frequency 294 Hz.
        /// </summary>
        D = 294,

        /// <summary>
        /// D-sharp note, frequency 311 Hz.
        /// </summary>
        Dsharp = 311,

        /// <summary>
        /// E note, frequency 330 Hz.
        /// </summary>
        E = 330,

        /// <summary>
        /// F note, frequency 349 Hz.
        /// </summary>
        F = 349,

        /// <summary>
        /// F-sharp note, frequency 370 Hz.
        /// </summary>
        Fsharp = 370,

        /// <summary>
        /// G note, frequency 392 Hz.
        /// </summary>
        G = 392,

        /// <summary>
        /// G-sharp note, frequency 415 Hz.
        /// </summary>
        Gsharp = 415,
    }

    /// <summary>
    /// Represents the duration of musical notes in milliseconds.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Roslynator",
        "RCS1154:Sort enum members",
        Justification = "Sorted for Human reading")]
    public enum Duration
    {
        /// <summary>
        /// Whole note duration, 1600 milliseconds.
        /// </summary>
        WHOLE = 1600,

        /// <summary>
        /// Half note duration, half of a whole note (800 milliseconds).
        /// </summary>
        HALF = WHOLE / 2,

        /// <summary>
        /// Quarter note duration, half of a half note (400 milliseconds).
        /// </summary>
        QUARTER = HALF / 2,

        /// <summary>
        /// Eighth note duration, half of a quarter note (200 milliseconds).
        /// </summary>
        EIGHTH = QUARTER / 2,

        /// <summary>
        /// Sixteenth note duration, half of an eighth note (100 milliseconds).
        /// </summary>
        SIXTEENTH = EIGHTH / 2,
    }

    /// <summary>
    /// Plays a single beep tone with default settings.
    /// </summary>
    public static Tune Beep() => new Builder().Beep();
    /// <summary>
    /// Adds a note with a specified tone and duration.
    /// </summary>
    public static ISetNotes Note(Tone tone, Duration duration) => new Builder().Note(tone, duration);
    /// <summary>
    /// Adds a note with a custom frequency (Hz) and a predefined duration.
    /// </summary>
    public static ISetNotes Note(int customTone, Duration duration) => new Builder().Note(customTone, duration);
    /// <summary>
    /// Adds a note with a specified tone and custom duration.
    /// </summary>
    public static ISetNotes Note(Tone tone, int customDuration) => new Builder().Note(tone, customDuration);
    /// <summary>
    /// Adds a note with custom frequency and custom duration.
    /// </summary>
    public static ISetNotes Note(int customTone, int customDuration) => new Builder().Note(customTone, customDuration);

    /// <summary>
    /// Adds a quarter note with the specified tone.
    /// </summary>
    public static ISetNotes Quarter(Tone tone) => new Builder().Quarter(tone);
    /// <summary>
    /// Adds a quarter note with a custom frequency.
    /// </summary>
    public static ISetNotes Quarter(int customTone) => new Builder().Quarter(customTone);
    /// <summary>
    /// Adds a whole note with the specified tone.
    /// </summary>
    public static ISetNotes Whole(Tone tone) => new Builder().Whole(tone);
    /// <summary>
    /// Adds a whole note with a custom frequency.
    /// </summary>
    public static ISetNotes Whole(int customTone) => new Builder().Whole(customTone);
    /// <summary>
    /// Adds a half note with the specified tone.
    /// </summary>
    public static ISetNotes Half(Tone tone) => new Builder().Half(tone);
    /// <summary>
    /// Adds a half note with a custom frequency.
    /// </summary>
    public static ISetNotes Half(int customTone) => new Builder().Half(customTone);
    /// <summary>
    /// Adds a eighth note with the specified tone.
    /// </summary>
    public static ISetNotes Eighth(Tone tone) => new Builder().Eighth(tone);
    /// <summary>
    /// Adds a eighth note with a custom frequency.
    /// </summary>
    public static ISetNotes Eighth(int customTone) => new Builder().Eighth(customTone);
    /// <summary>
    /// Adds a sixteenth note with the specified tone.
    /// </summary>
    public static ISetNotes Sixteeth(Tone tone) => new Builder().Sixteeth(tone);
    /// <summary>
    /// Adds a sixteenth note with a custom frequency.
    /// </summary>
    public static ISetNotes Sixteeth(int customTone) => new Builder().Sixteeth(customTone);

    /// <summary>
    /// Continue building with one of the following:
    /// <br><see cref="Note(Tone, Duration)"/></br>
    /// <br><see cref="Beep()"/></br>
    /// <br>Any <see cref="Duration"/> is also an option. Ex <see cref="Quarter(Tone)"/></br>
    /// <br><see cref="GoTo(INode)"/></br>
    /// <br><see cref="WaitToPlay()"/></br>
    /// <br><see cref="ContinueWhilePlaying()"/></br>
    /// </summary>
    public interface ISetNotes
    {
        /// <summary>
        /// Plays a single beep tone with default settings.
        /// </summary>
        public Tune Beep();
        /// <summary>
        /// Adds a note with a specified tone and duration.
        /// </summary>
        public ISetNotes Note(Tone tone, Duration duration);
        /// <summary>
        /// Adds a note with a custom frequency (Hz) and a predefined duration.
        /// </summary>
        public ISetNotes Note(int customTone, Duration duration);
        /// <summary>
        /// Adds a note with a specified tone and custom duration.
        /// </summary>
        public ISetNotes Note(Tone tone, int customDuration);
        /// <summary>
        /// Adds a note with custom frequency and custom duration.
        /// </summary>
        public ISetNotes Note(int customTone, int customDuration);

        /// <summary>
        /// Adds a quarter note with the specified tone.
        /// </summary>
        public ISetNotes Quarter(Tone tone);
        /// <summary>
        /// Adds a quarter note with a custom frequency.
        /// </summary>
        public ISetNotes Quarter(int tone);
        /// <summary>
        /// Adds a whole note with the specified tone.
        /// </summary>
        public ISetNotes Whole(Tone tone);
        /// <summary>
        /// Adds a whole note with a custom frequency.
        /// </summary>
        public ISetNotes Whole(int tone);
        /// <summary>
        /// Adds a half note with the specified tone.
        /// </summary>
        public ISetNotes Half(Tone tone);
        /// <summary>
        /// Adds a half note with a custom frequency.
        /// </summary>
        public ISetNotes Half(int tone);
        /// <summary>
        /// Adds a eighth note with the specified tone.
        /// </summary>
        public ISetNotes Eighth(Tone tone);
        /// <summary>
        /// Adds a eighth note with a custom frequency.
        /// </summary>
        public ISetNotes Eighth(int tone);
        /// <summary>
        /// Adds a sixteenth note with the specified tone.
        /// </summary>
        public ISetNotes Sixteeth(Tone tone);
        /// <summary>
        /// Adds a sixteenth note with a custom frequency.
        /// </summary>
        public ISetNotes Sixteeth(int tone);

        /// <summary>
        /// Specifies an action to execute when the sequence is reached.
        /// </summary>
        public ISetConfirm GoTo(Action action);
        /// <summary>
        /// Specifies a node to transition to when the sequence is reached.
        /// </summary>
        public ISetConfirm GoTo(INode node);

        /// <summary>
        /// Waits for the tune to complete before continuing.
        /// </summary>
        public Tune WaitToPlay();

        /// <summary>
        /// The tune will be played asynconously. If thie node has a GoTo, it will call it immediately.
        /// </summary>
        public Tune ContinueWhilePlaying();
    }

    /// <summary>
    /// Continue building with one of the following:
    /// <br><see cref="WaitToPlay"/></br>
    /// <br><see cref="ContinueWhilePlaying()"/></br>
    /// </summary>
    public interface ISetConfirm
    {
        /// <summary>
        /// Waits for the tune to complete before continuing.
        /// </summary>
        public Tune WaitToPlay();

        /// <summary>
        /// The tune will be played asynconously. If thie node has a GoTo, it will call it immediately.
        /// </summary>
        public Tune ContinueWhilePlaying();
    }

    private class Builder : ISetNotes, ISetConfirm
    {
        readonly Tune _item = new();

        public Tune Beep() => _item;

        public ISetNotes Note(Tone tone, Duration duration) => Note((int)tone, (int)duration);
        public ISetNotes Note(int tone, Duration duration) => Note(tone, (int)duration);
        public ISetNotes Note(Tone tone, int duration) => Note((int)tone, duration);
        public ISetNotes Note(int tone, int duration)
        {
            _item.Notes.Add(new(tone, duration));
            return this;
        }
        public Tune WaitToPlay() => _item;
        public Tune ContinueWhilePlaying()
        {
            _item.Wait = false;
            return _item;
        }

        public ISetNotes Quarter(Tone tone) => Note((int)tone, (int)Duration.QUARTER);
        public ISetNotes Quarter(int tone) => Note(tone, (int)Duration.QUARTER);
        public ISetNotes Whole(Tone tone) => Note((int)tone, (int)Duration.WHOLE);
        public ISetNotes Whole(int tone) => Note(tone, (int)Duration.WHOLE);
        public ISetNotes Half(Tone tone) => Note((int)tone, (int)Duration.HALF);
        public ISetNotes Half(int tone) => Note(tone, (int)Duration.HALF);
        public ISetNotes Eighth(Tone tone) => Note((int)tone, (int)Duration.EIGHTH);
        public ISetNotes Eighth(int tone) => Note(tone, (int)Duration.EIGHTH);
        public ISetNotes Sixteeth(Tone tone) => Note((int)tone, (int)Duration.SIXTEENTH);
        public ISetNotes Sixteeth(int tone) => Note(tone, (int)Duration.SIXTEENTH);

        public ISetConfirm GoTo(Action action)
        {
            _item.Effect = action;
            return this;
        }

        public ISetConfirm GoTo(INode node)
        {
            _item.Effect = () => node.Call();
            return this;
        }
    }
}
