using System.Runtime.InteropServices;

namespace ConsoleHero.Interfaces;

internal class PlatformHelper : IPlatformHelper
{
    bool IPlatformHelper.IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    bool IPlatformHelper.IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    bool IPlatformHelper.IsOSX => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
}