using static ConsoleHero.RequestBuilder;

namespace ConsoleHero.InjectionExample;
[Singleton]
public class Requests(Paragraphs paragraphs, Data data)
{
    private readonly Paragraphs _paragraphs = paragraphs;
    private readonly Data _data = data;

    public Request AskForName =>
    Ask("What is your name?").
    For(DataType.String).
    Goto((x) => _paragraphs.YourNameIs(x)).
    Use<string>((x) => _data.Name = x);

    public Request AskYesOrNo => YesNo(_paragraphs.Part1, _paragraphs.Crying);
}
