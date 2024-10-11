using ConsoleHero.Helpers;

namespace ConsoleHero;

public class Tune : INode
{
    public Tune(List<Note> notes)
    {
        Notes = notes;
    }

    public List<Note> Notes { get; set; } = [];

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

    internal Tune() { }

    public readonly struct Note(Tone frequency, Duration time)
    {
        public Tone NoteTone { get; } = frequency;
        public Duration NoteDuration { get; } = time;
    }

    internal void Play()
    {
        foreach(Note item in Notes)
        {
            BeepHelper.Beep((int)item.NoteTone, (int)item.NoteDuration);
        }
    }

    public void Call() => Play();
    public void Call(string input) => Play();
}
