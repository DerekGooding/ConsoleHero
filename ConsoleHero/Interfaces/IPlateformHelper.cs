namespace ConsoleHero.Interfaces;
internal interface IPlatformHelper
{
    internal bool IsWindows { get; }
    internal bool IsLinux { get; }
    internal bool IsOSX { get; }
}
