using ConsoleHero.Helpers;

namespace ConsoleHero;

public class Tune : INode
{
    internal List<Note> Notes { get; set; } = [];
    internal bool Wait { get; set; } = true;

    internal Tune() { }

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
            BeepHelper.Beep(item.NoteTone, item.NoteDuration);
        }
    }

    public void Call()
    {
        if (Wait)
            Play();
        else
            new Thread(Play).Start();
    }

    public void Call(string input) => Call();
}
