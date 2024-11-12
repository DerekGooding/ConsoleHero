using ConsoleHero.Helpers;
using ConsoleHero.Interfaces;

namespace ConsoleHero;

/// <summary>
/// Start make a new Tune with any of the <see cref="TuneBuilder.Note(TuneBuilder.Tone, TuneBuilder.Duration)"/> overloads.
/// Or use a methhod named after the note duration eg. <see cref="TuneBuilder.Quarter(TuneBuilder.Tone)"/>.
/// <br><see cref="TuneBuilder.Beep"/> for a simple single beep.</br>
/// </summary>
public record Tune : INode
{
    internal BeepHelper _beepHelper = new(new PlatformHelper());
    internal Tune() { }
    internal List<Note> Notes { get; set; } = [];
    internal bool Wait { get; set; } = true;

    internal Action Effect { get; set; } = () => { };

    internal readonly struct Note(int frequency, int time)
    {
        public int NoteTone { get; } = frequency;
        public int NoteDuration { get; } = time;
    }

    internal void Play()
    {
        if (Notes.Count == 0)
            BeepHelper.Beep();

        foreach (Note item in Notes)
        {
            _beepHelper.Beep(item.NoteTone, item.NoteDuration);
        }
    }

    /// <summary>
    /// Plays the tune using beeps. If it was set to wait, will pause the thread. Otherwise, runs asynconously.
    /// </summary>
    public void Call(string input = "")
    {
        if (Wait)
            Play();
        else
            new Thread(Play).Start();
        Effect.Invoke();
    }
}
