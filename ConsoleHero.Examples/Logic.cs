namespace ConsoleHero.Examples;

public static class Logic
{
    public static void ReviewPlayer(Player player)
    {
        Console.WriteLine(player.Review);
        Console.WriteLine();
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }
}
