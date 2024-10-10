namespace ConsoleHero.Examples.Bad;
public static class Program
{
    private static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("|---- Main Menu ----|");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine("1 => Approach Door");
        Console.WriteLine("2 => Check Surroundings");
        Console.WriteLine("X => Exit");
        Console.WriteLine();

        string? line;
        while(true)
        {
            line = Console.ReadLine();
            switch (line)
            {
                case "1":
                    ApproachDoor();
                    break;
                case "2":
                    CheckSurroundings();
                    break;
                case "x" or "X":
                    Environment.Exit(0);
                    break;
                default:
                    continue;
            }
        }
    }

    private static void ApproachDoor() { }
    private static void CheckSurroundings() { }
}
