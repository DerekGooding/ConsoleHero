namespace ConsoleHero.Interfaces;

public interface IConsoleService
{
    public abstract void WriteLine(string? value);
    public abstract void WriteLine();

    public abstract void Write(string? value);

    public abstract void Clear();

    public abstract void ReadLine();

    public abstract void ReadKey();

    public abstract void Beep(int frequency, int duration);

    public abstract void SetListener(IListeningNode listener);
}