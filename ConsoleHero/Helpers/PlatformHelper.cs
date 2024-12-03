using ConsoleHero.Interfaces;
using System.Runtime.InteropServices;

namespace ConsoleHero.Helpers;

internal class PlatformHelper : IPlatformHelper
{
    internal PlatformHelper()
    {
        IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        IsLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        IsOSX = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }

    internal bool IsWindows { get; }
    internal bool IsLinux { get; }
    internal bool IsOSX { get; }

    bool IPlatformHelper.IsWindows => IsWindows;

    bool IPlatformHelper.IsLinux => IsLinux;

    bool IPlatformHelper.IsOSX => IsOSX;
}