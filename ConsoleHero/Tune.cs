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
    internal IBeepHelper _beepHelper = new BeepHelper(new PlatformHelper());
    internal Tune() { }
    internal List<Note> Notes { get; set; } = new();
    internal bool Wait { get; set; } = true;

    internal Action Effect { get; set; } = () => { };

    internal readonly struct Note
    {
        public int Tone { get; }
        public int Duration { get; }

        public Note(int frequency, int time)
        {
            Tone = frequency;
            Duration = time;
        }
    }

    internal void Play()
    {
        if (Notes.Count == 0)
            _beepHelper.Beep();

        foreach (var item in Notes)
        {
            _beepHelper.Beep(item.Tone, item.Duration);
        }
    }

    /// <summary>
    /// Plays the tune using beeps. If it was set to wait, will pause the thread. Otherwise, runs asynconously.
    /// </summary>
    public void Call()
    {
        if (Wait)
            Play();
        else
            new Thread(Play).Start();
        Effect.Invoke();
    }
}
