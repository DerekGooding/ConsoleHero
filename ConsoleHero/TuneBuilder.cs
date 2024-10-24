namespace ConsoleHero;
/// <summary>
/// 
/// </summary>
public static class TuneBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public enum Tone
    {
        /// <summary>
        /// 
        /// </summary>
        REST = 0,
        /// <summary>
        /// 
        /// </summary>
        GbelowC = 196,
        /// <summary>
        /// 
        /// </summary>
        A = 220,
        /// <summary>
        /// 
        /// </summary>
        Asharp = 233,
        /// <summary>
        /// 
        /// </summary>
        B = 247,
        /// <summary>
        /// 
        /// </summary>
        C = 262,
        /// <summary>
        /// 
        /// </summary>
        Csharp = 277,
        /// <summary>
        /// 
        /// </summary>
        D = 294,
        /// <summary>
        /// 
        /// </summary>
        Dsharp = 311,
        /// <summary>
        /// 
        /// </summary>
        E = 330,
        /// <summary>
        /// 
        /// </summary>
        F = 349,
        /// <summary>
        /// 
        /// </summary>
        Fsharp = 370,
        /// <summary>
        /// 
        /// </summary>
        G = 392,
        /// <summary>
        /// 
        /// </summary>
        Gsharp = 415,
    }

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Roslynator",
        "RCS1154:Sort enum members",
        Justification = "Sorted for Human reading")]
    public enum Duration
    {
        /// <summary>
        /// 
        /// </summary>
        WHOLE = 1600,
        /// <summary>
        /// 
        /// </summary>
        HALF = WHOLE / 2,
        /// <summary>
        /// 
        /// </summary>
        QUARTER = HALF / 2,
        /// <summary>
        /// 
        /// </summary>
        EIGHTH = QUARTER / 2,
        /// <summary>
        /// 
        /// </summary>
        SIXTEENTH = EIGHTH / 2,
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static Tune Beep() => new Builder().Beep();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tone"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static ISetNotes Note(Tone tone, Duration duration) => new Builder().Note(tone, duration);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="customTone"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static ISetNotes Note(int customTone, Duration duration) => new Builder().Note(customTone, duration);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tone"></param>
    /// <param name="customDuration"></param>
    /// <returns></returns>
    public static ISetNotes Note(Tone tone, int customDuration) => new Builder().Note(tone, customDuration);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="customTone"></param>
    /// <param name="customDuration"></param>
    /// <returns></returns>
    public static ISetNotes Note(int customTone, int customDuration) => new Builder().Note(customTone, customDuration);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tone"></param>
    /// <returns></returns>
    public static ISetNotes Quarter(Tone tone) => new Builder().Quarter(tone);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="customTone"></param>
    /// <returns></returns>
    public static ISetNotes Quarter(int customTone) => new Builder().Quarter(customTone);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tone"></param>
    /// <returns></returns>
    public static ISetNotes Whole(Tone tone) => new Builder().Whole(tone);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="customTone"></param>
    /// <returns></returns>
    public static ISetNotes Whole(int customTone) => new Builder().Whole(customTone);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tone"></param>
    /// <returns></returns>
    public static ISetNotes Half(Tone tone) => new Builder().Half(tone);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="customTone"></param>
    /// <returns></returns>
    public static ISetNotes Half(int customTone) => new Builder().Half(customTone);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tone"></param>
    /// <returns></returns>
    public static ISetNotes Eighth(Tone tone) => new Builder().Eighth(tone);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="customTone"></param>
    /// <returns></returns>
    public static ISetNotes Eighth(int customTone) => new Builder().Eighth(customTone);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tone"></param>
    /// <returns></returns>
    public static ISetNotes Sixteeth(Tone tone) => new Builder().Sixteeth(tone);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="customTone"></param>
    /// <returns></returns>
    public static ISetNotes Sixteeth(int customTone) => new Builder().Sixteeth(customTone);

    /// <summary>
    /// 
    /// </summary>
    public interface ISetNotes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Tune Beep();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tone"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public ISetNotes Note(Tone tone, Duration duration);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customTone"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public ISetNotes Note(int customTone, Duration duration);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tone"></param>
        /// <param name="customDuration"></param>
        /// <returns></returns>
        public ISetNotes Note(Tone tone, int customDuration);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customTone"></param>
        /// <param name="customDuration"></param>
        /// <returns></returns>
        public ISetNotes Note(int customTone, int customDuration);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tone"></param>
        /// <returns></returns>
        public ISetNotes Quarter(Tone tone);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tone"></param>
        /// <returns></returns>
        public ISetNotes Quarter(int tone);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tone"></param>
        /// <returns></returns>
        public ISetNotes Whole(Tone tone);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tone"></param>
        /// <returns></returns>
        public ISetNotes Whole(int tone);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tone"></param>
        /// <returns></returns>
        public ISetNotes Half(Tone tone);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tone"></param>
        /// <returns></returns>
        public ISetNotes Half(int tone);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tone"></param>
        /// <returns></returns>
        public ISetNotes Eighth(Tone tone);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tone"></param>
        /// <returns></returns>
        public ISetNotes Eighth(int tone);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tone"></param>
        /// <returns></returns>
        public ISetNotes Sixteeth(Tone tone);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tone"></param>
        /// <returns></returns>
        public ISetNotes Sixteeth(int tone);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ISetConfirm GoTo(Action action);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public ISetConfirm GoTo(INode node);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Tune WaitToPlay();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Tune ContinueWhilePlaying();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ISetConfirm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Tune WaitToPlay();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Tune ContinueWhilePlaying();
    }

    private class Builder() : ISetNotes, ISetConfirm
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
