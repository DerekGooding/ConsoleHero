﻿namespace ConsoleHero.Interfaces;

public interface IConsoleService
{
    public abstract ValueTask WriteLineAsync(string? value);
    public abstract ValueTask WriteLineAsync();
    public abstract ValueTask WriteAsync(string? value);

    public abstract void WriteLine(string? value);
    public abstract void WriteLine();

    public abstract void Write(string? value);

    public abstract void Clear();

    public abstract void Beep(int frequency, int duration);

    public abstract void SetListener(IListeningNode listener);

    public abstract void Cancel();
    public abstract void Exit();
}