using ConsoleHero.Generator;
using System.Drawing;
using static ConsoleHero.Generator.DictionaryExtensions;

namespace ConsoleHero.InjectionExample;

public static class Program
{
    private static void Main()
    {
        GlobalSettings.Spacing = 2;
        GlobalSettings.DefaultTextColor = Color.LightBlue;

        //Source Generation example. Try renaming editing the Creatures data. 
        var goblin = GlobalSettings.Get<Creatures>().Goblin;
        var slime = GlobalSettings.Get<Creatures>().Slime;
        GlobalSettings.Service.WriteLine($"A {goblin.Name} has {goblin.Health} health");
        GlobalSettings.Service.WriteLine($"A {slime.Name} has {slime.Health} health");
        Console.ReadKey();

        //Example that triggers recommended Comparer when using INamed as keys. 
        Dictionary<INamed, int> iNamedHealth = new();
        var creatureHealth = GlobalSettings.Get<Creatures>().All.ToDictionary(x => x, x => x.Health);

        var creatureHealth2 = GlobalSettings.Get<Creatures>().All.ToNamedDictionary(x => x.Health);

        var myList = GlobalSettings.Get<Creatures>().All;

        var dict = myList.ToNamedDictionary(_ => 10);

        while (true)
        {
            GlobalSettings.Get<Menus>().MainMenu.Call();
        }
    }

    public static bool IsOpen { get; private set; } = false;

    public static void ToggleDoor() => IsOpen = !IsOpen;
}