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

    public interface ISetNotes
    {
        public Tune Beep();
        public ISetNotes Note(Tone tone, Duration duration);
        public ISetNotes Note(int customTone, Duration duration);
        public ISetNotes Note(Tone tone, int customDuration);
        public ISetNotes Note(int customTone, int customDuration);
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
    }
}
