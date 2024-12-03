using ConsoleHero.Interfaces;
using System.Runtime.InteropServices;

namespace ConsoleHero;

internal class ConsoleService : IConsoleService
{
    public void Clear() => Console.Clear();
    public string? ReadLine() => Console.ReadLine();
    public void Write(string? value) => Console.Write(value);
    public void WriteLine(string? value) => Console.WriteLine(value);
    public void WriteLine() => Console.WriteLine();
    public ConsoleKeyInfo ReadKey() => Console.ReadKey();
    public void Beep(int frequency, int duration)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            Console.Beep(frequency, duration);
    }
}
