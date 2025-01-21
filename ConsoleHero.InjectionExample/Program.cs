using System.Drawing;

namespace ConsoleHero.InjectionExample;

public static class Program
{
    private static void Main()
    {
        GlobalSettings.Spacing = 2;
        GlobalSettings.DefaultTextColor = Color.LightBlue;

        //Source Generation example. Try renaming editing the Creatures data. 
        Creature goblin = GlobalSettings.Get<Creatures>()[ContentEnums.CreaturesType.Goblin];
        GlobalSettings.Service.Write($"A Goblin has {goblin.Health} health");
        Console.ReadKey();

        while (true)
        {
            GlobalSettings.Get<Menus>().MainMenu.Call();
        }
    }

    public static bool IsOpen { get; private set; } = false;

    public static void ToggleDoor() => IsOpen = !IsOpen;
}