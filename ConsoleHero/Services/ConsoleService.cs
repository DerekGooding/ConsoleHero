using ConsoleHero.Interfaces;
using System.Runtime.InteropServices;

namespace ConsoleHero.Services;

internal class ConsoleService : IConsoleService
{
    void IConsoleService.Clear() => Console.Clear();
    string? IConsoleService.ReadLine() => Console.ReadLine();
    void IConsoleService.Write(string? value) => Console.Write(value);
    void IConsoleService.WriteLine(string? value) => Console.WriteLine(value);
    void IConsoleService.WriteLine() => Console.WriteLine();
    ConsoleKeyInfo IConsoleService.ReadKey() => Console.ReadKey();
    void IConsoleService.Beep(int frequency, int duration)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            Console.Beep(frequency, duration);
    }
}
