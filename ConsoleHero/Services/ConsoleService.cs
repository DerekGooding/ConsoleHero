using ConsoleHero.Interfaces;
using System.Runtime.InteropServices;

namespace ConsoleHero.Services;

internal class ConsoleService : IConsoleService
{
    private IListeningNode? CurrentListener;

    void IConsoleService.SetListener(IListeningNode listener)
    {
        CurrentListener = listener;
        if (listener is Paragraph)
            ((IConsoleService)this).ReadKey();
        else
            ((IConsoleService)this).ReadLine();
    }

    void IConsoleService.Clear() => Console.Clear();
    void IConsoleService.ReadLine() => CurrentListener?.ProcessResult(Console.ReadLine() ?? string.Empty);
    void IConsoleService.Write(string? value) => Console.Write(value);
    void IConsoleService.WriteLine(string? value) => Console.WriteLine(value);
    void IConsoleService.WriteLine() => Console.WriteLine();
    void IConsoleService.ReadKey()
    {
        Console.ReadKey();
        CurrentListener?.ProcessResult("");
    }

    void IConsoleService.Beep(int frequency, int duration)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            Console.Beep(frequency, duration);
    }
}
