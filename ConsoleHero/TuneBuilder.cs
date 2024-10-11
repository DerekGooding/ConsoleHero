using static ConsoleHero.TuneBuilder;

namespace ConsoleHero;
public static class TuneBuilder
{
    public enum Tone
    {
        REST = 0,
        GbelowC = 196,
        A = 220,
        Asharp = 233,
        B = 247,
        C = 262,
        Csharp = 277,
        D = 294,
        Dsharp = 311,
        E = 330,
        F = 349,
        Fsharp = 370,
        G = 392,
        Gsharp = 415,
    }

    public enum Duration
    {
        WHOLE = 1600,
        HALF = WHOLE / 2,
        QUARTER = HALF / 2,
        EIGHTH = QUARTER / 2,
        SIXTEENTH = EIGHTH / 2,
    }

    public static Tune Beep() => new Builder().Beep();
    public static ISetNotes Note(Tone tone, Duration duration) => new Builder().Note(tone, duration);
    public static ISetNotes Note(int customTone, Duration duration) => new Builder().Note(customTone, duration);
    public static ISetNotes Note(Tone tone, int customDuration) => new Builder().Note(tone, customDuration);
    public static ISetNotes Note(int customTone, int customDuration) => new Builder().Note(customTone, customDuration);

    public static ISetNotes Quarter(Tone tone) => new Builder().Quarter(tone);
    public static ISetNotes Quarter(int customTone) => new Builder().Quarter(customTone);
    public static ISetNotes Whole(Tone tone) => new Builder().Whole(tone);
    public static ISetNotes Whole(int customTone) => new Builder().Whole(customTone);
    public static ISetNotes Half(Tone tone) => new Builder().Half(tone);
    public static ISetNotes Half(int customTone) => new Builder().Half(customTone);
    public static ISetNotes Eighth(Tone tone) => new Builder().Eighth(tone);
    public static ISetNotes Eighth(int customTone) => new Builder().Eighth(customTone);
    public static ISetNotes Sixteeth(Tone tone) => new Builder().Sixteeth(tone);
    public static ISetNotes Sixteeth(int customTone) => new Builder().Sixteeth(customTone);

    public interface ISetNotes
    {
        public Tune Beep();
        public ISetNotes Note(Tone tone, Duration duration);
        public ISetNotes Note(int customTone, Duration duration);
        public ISetNotes Note(Tone tone, int customDuration);
        public ISetNotes Note(int customTone, int customDuration);

        public ISetNotes Quarter(Tone tone);
        public ISetNotes Quarter(int tone);
        public ISetNotes Whole(Tone tone);
        public ISetNotes Whole(int tone);
        public ISetNotes Half(Tone tone);
        public ISetNotes Half(int tone);
        public ISetNotes Eighth(Tone tone);
        public ISetNotes Eighth(int tone);
        public ISetNotes Sixteeth(Tone tone);
        public ISetNotes Sixteeth(int tone);

        public Tune WaitToPlay();
        public Tune ContinueWhilePlaying();
    }

    private class Builder() : ISetNotes
    {
        readonly Tune _item = new();

        public Tune Beep() => _item;

        public ISetNotes Note(Tone tone, Duration duration)
        {
            _item.Notes.Add(new((int)tone, (int)duration));
            return this;
        }
        public ISetNotes Note(int tone, Duration duration)
        {
            _item.Notes.Add(new(tone, (int)duration));
            return this;
        }
        public ISetNotes Note(Tone tone, int duration)
        {
            _item.Notes.Add(new((int)tone, duration));
            return this;
        }
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

        public ISetNotes Quarter(Tone tone)
        {
            _item.Notes.Add(new((int)tone, (int)Duration.QUARTER));
            return this;
        }
        public ISetNotes Quarter(int tone)
        {
            _item.Notes.Add(new(tone, (int)Duration.QUARTER));
            return this;
        }
        public ISetNotes Whole(Tone tone)
        {
            _item.Notes.Add(new((int)tone, (int)Duration.WHOLE));
            return this;
        }
        public ISetNotes Whole(int tone)
        {
            _item.Notes.Add(new(tone, (int)Duration.WHOLE));
            return this;
        }
        public ISetNotes Half(Tone tone)
        {
            _item.Notes.Add(new((int)tone, (int)Duration.HALF));
            return this;
        }
        public ISetNotes Half(int tone)
        {
            _item.Notes.Add(new(tone, (int)Duration.HALF));
            return this;
        }
        public ISetNotes Eighth(Tone tone)
        {
            _item.Notes.Add(new((int)tone, (int)Duration.EIGHTH));
            return this;
        }
        public ISetNotes Eighth(int tone)
        {
            _item.Notes.Add(new(tone, (int)Duration.EIGHTH));
            return this;
        }
        public ISetNotes Sixteeth(Tone tone)
        {
            _item.Notes.Add(new((int)tone, (int)Duration.SIXTEENTH));
            return this;
        }
        public ISetNotes Sixteeth(int tone)
        {
            _item.Notes.Add(new(tone, (int)Duration.SIXTEENTH));
            return this;
        }
    }
}
