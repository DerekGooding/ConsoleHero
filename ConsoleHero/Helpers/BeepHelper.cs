using System.Runtime.InteropServices;

namespace ConsoleHero.Helpers;

internal static class BeepHelper
{
    internal static void Beep() => Write("\a");

    internal static void Beep(int frequency, int duration)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.Beep(frequency, duration);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            try
            {
                System.Diagnostics.ProcessStartInfo psi = new()
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"beep -f {frequency} -l {duration}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                System.Diagnostics.Process? process = System.Diagnostics.Process.Start(psi);
                process?.WaitForExit();
            }
            catch
            {
                Write("\a");
            }
        }
    }
}