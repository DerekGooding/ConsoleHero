using System.Drawing;

namespace ConsoleHero.InjectionExample;

public static class Program
{
    private static void Main()
    {
        GlobalSettings.Spacing = 2;
        GlobalSettings.DefaultTextColor = Color.LightBlue;

        while (true)
        {
            GlobalSettings.Get<Menus>().MainMenu.Call();
        }
    }

    public static bool IsOpen { get; private set; } = false;

    public static void ToggleDoor() => IsOpen = !IsOpen;
}