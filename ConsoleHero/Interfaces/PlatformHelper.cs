using System.Runtime.InteropServices;

namespace ConsoleHero.Interfaces;

internal class PlatformHelper : IPlatformHelper
{
    public bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    public bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    public bool IsOSX => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
}