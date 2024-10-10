using ConsoleHero.Helpers;

namespace ConsoleHero;

public class Tune : INode
{
    internal enum Tone
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

    internal enum Duration
    {
        WHOLE = 1600,
        HALF = WHOLE / 2,
        QUARTER = HALF / 2,
        EIGHTH = QUARTER / 2,
        SIXTEENTH = EIGHTH / 2,
    }

    internal static Note[] Mary =
    [
        new(Tone.B, Duration.QUARTER),
        new(Tone.A, Duration.QUARTER),
        new(Tone.GbelowC, Duration.QUARTER),
        new(Tone.A, Duration.QUARTER),
        new(Tone.B, Duration.QUARTER),
        new(Tone.B, Duration.QUARTER),
        new(Tone.B, Duration.HALF),
        new(Tone.A, Duration.QUARTER),
        new(Tone.A, Duration.QUARTER),
        new(Tone.A, Duration.HALF),
        new(Tone.B, Duration.QUARTER),
        new(Tone.D, Duration.QUARTER),
        new(Tone.D, Duration.HALF)
    ];

    internal Tune() { }

    internal readonly struct Note(Tone frequency, Duration time)
    {
        public Tone NoteTone { get; } = frequency;
        public Duration NoteDuration { get; } = time;
    }

    public static void PlayMary()
    {
        new Thread(() =>
        {
            foreach (Note item in Mary)
            {
                BeepHelper.Beep((int)item.NoteTone, (int)item.NoteDuration);
            }
            BeepHelper.Beep();
            BeepHelper.Beep();
            BeepHelper.Beep();
        }).Start();
    }
    internal void Play()
    {
        foreach(Note item in  Mary)
        {
            BeepHelper.Beep((int)item.NoteTone, (int)item.NoteDuration);
        }
    }

    public void Call() => Play();
    public void Call(string input) => Play();
}
