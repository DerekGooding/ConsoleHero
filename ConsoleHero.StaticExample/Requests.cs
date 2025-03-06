using static ConsoleHero.RequestBuilder;

namespace ConsoleHero.StaticExample;
public static class Requests
{
    public static Request AskForName =>
    Ask("What is your name?").
    For(DataType.String).
    Goto((x) => Paragraphs.YourNameIs(x)).
    Use<string>((x) => Data.Name = x);

    public static Request AskYesOrNo => YesNo(Paragraphs.Part1, Paragraphs.Crying);
}
