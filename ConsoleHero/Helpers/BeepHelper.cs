﻿using ConsoleHero.Interfaces;

namespace ConsoleHero.Helpers;

internal class BeepHelper : IBeepHelper
{
    private readonly IPlatformHelper _platformHelper;

    public BeepHelper(IPlatformHelper plateformHelper) => _platformHelper = plateformHelper;

    void IBeepHelper.Beep() => GlobalSettings.Service.Write("\a");

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Ensured by _platformHelper")]
    void IBeepHelper.Beep(int frequency, int duration)
    {
        if (_platformHelper.IsWindows)
        {
            GlobalSettings.Service.Beep(frequency, duration);
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
                var process = System.Diagnostics.Process.Start(psi);
                process?.WaitForExit();
            }
            catch
            {
                GlobalSettings.Service.Write("\a");
            }
        }
    }
}