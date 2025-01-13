using System.Drawing;

namespace ConsoleHero.StaticExample;
public static class Program
{
    private static void Main()
    {
        GlobalSettings.Spacing = 2;
        GlobalSettings.DefaultTextColor = Color.LightBlue;

        while (true)
        {
            Menus.MainMenu.Call();
        }
    }

    public static bool IsOpen { get; private set; } = false;

    public static void ToggleDoor() => IsOpen = !IsOpen;
}