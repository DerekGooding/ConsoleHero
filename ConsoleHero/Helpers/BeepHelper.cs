using ConsoleHero.Interfaces;

namespace ConsoleHero.Helpers;

internal class BeepHelper(IPlatformHelper plateformHelper) : IBeepHelper
{
    private readonly IPlatformHelper _platformHelper = plateformHelper;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Required for unit testing")]
    void IBeepHelper.Beep() => Write("\a");

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Ensured by _platformHelper")]
    void IBeepHelper.Beep(int frequency, int duration)
    {
        if (_platformHelper.IsWindows)
        {
            Beep(frequency, duration);
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