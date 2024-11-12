using ConsoleHero.Interfaces;

namespace ConsoleHero.Helpers;

internal class BeepHelper(IPlatformHelper plateformHelper)
{
    private readonly IPlatformHelper _platformHelper = plateformHelper;

    internal static void Beep() => Write("\a");

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Ensured by _platformHelper")]
    internal void Beep(int frequency, int duration)
    {
        if (_platformHelper.IsWindows)
        {
            Console.Beep(frequency, duration);
        }
        else if (_platformHelper.IsLinux || _platformHelper.IsOSX)
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