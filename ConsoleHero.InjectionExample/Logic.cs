namespace ConsoleHero.InjectionExample;

[Singleton]
public class Logic
{
    public void ReviewPlayer(Player player)
    {
        Console.WriteLine(player.Review);
        Console.WriteLine();
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }
}
