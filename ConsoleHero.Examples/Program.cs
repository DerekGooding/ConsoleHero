using static ConsoleHero.MenuBuilder;

namespace ConsoleHero.Examples;
public static class Program
{
    private static void Main()
    {
        Menu mainMenu = Title("|---- Main Menu ----|", ConsoleColor.Red)
        .Options
        ([
            Key("1").Description("Open Door").GoTo(OtherMenu).Always(),
            Exit("X"),
        ]);

        mainMenu.Ask();
    }

    private static void OtherMenu()
    {
        Menu mainMenu = NoTitle()
        .Options
        ([
            Key("1").Description("Open Door").GoTo(OtherMenu).Always(),
            Back("C"),
        ]);
    }
}