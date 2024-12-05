using ConsoleHero.Interfaces;
using System.Runtime.InteropServices;

namespace ConsoleHero.Services;

internal class ConsoleService : IConsoleService
{
    private readonly List<IListeningNode> ListenerQueue = new();

    void IConsoleService.SetListener(IListeningNode listener)
    {
        ListenerQueue.Add(listener);
        if (listener is Paragraph)
            ListenerQueue[^1].ProcessResult(Console.ReadKey().Key.ToString());
        else
            ListenerQueue[^1].ProcessResult(Console.ReadLine() ?? string.Empty);
    }

    void IConsoleService.Clear() => Console.Clear();
    void IConsoleService.Write(string? value) => Console.Write(value);
    void IConsoleService.WriteLine(string? value) => Console.WriteLine(value);
    void IConsoleService.WriteLine() => Console.WriteLine();

    void IConsoleService.Beep(int frequency, int duration)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            Console.Beep(frequency, duration);
    }

    void IConsoleService.Cancel() => ListenerQueue.RemoveAt(ListenerQueue.Count - 1);
    void IConsoleService.Exit() => Environment.Exit(0);
}
