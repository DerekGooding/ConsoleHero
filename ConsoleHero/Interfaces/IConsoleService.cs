namespace ConsoleHero.Interfaces;

public interface IConsoleService
{
    public abstract void WriteLine(string? value);
    public abstract void WriteLine();

    public abstract void Write(string? value);

    public abstract void Clear();

    public abstract string? ReadLine();

    public abstract ConsoleKeyInfo ReadKey();

    public abstract void Beep(int frequency, int duration);
}