namespace ConsoleHero.Interfaces;
internal interface IPlatformHelper
{
    bool IsWindows { get; }
    bool IsLinux { get; }
    bool IsOSX { get; }
}
