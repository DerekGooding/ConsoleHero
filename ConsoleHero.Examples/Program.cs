using static ConsoleHero.MenuBuilder;

namespace ConsoleHero.Examples;
public static class Program
{
    public static readonly Menu MainMenu =
    Title("|---- Main Menu ----|", ConsoleColor.Red)
    .Options
    ([
        Description("Approach Door").GoTo(ApproachDoor),
        Exit(),
    ]);

    public static readonly Menu OtherMenu =
    NoTitle()
    .Options
    ([
        Description("Open Door").If(() => !_isOpen).GoTo(()=> _isOpen = true),
        Description("Close Door").If(() => _isOpen).GoTo(()=> _isOpen = false),
        Back(),
    ]);

    private static bool _isOpen = false;

    private static void Main()
    {
        while (true)
        {
            Console.Clear();
            MainMenu.Ask();
        }
    }

    private static void ApproachDoor()
    {
        OtherMenu.Ask();
    }
}