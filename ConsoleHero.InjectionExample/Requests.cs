using static ConsoleHero.RequestBuilder;

namespace ConsoleHero.InjectionExample;

public class Requests(Paragraphs paragraphs, Data data)
{
    private readonly Paragraphs _paragraphs = paragraphs;
    private readonly Data _data = data;

    public Request AskForName =>
    Ask("What is your name?").
    For(DataType.String).
    Goto(_paragraphs.YourNameIs).
    Use<string>((x) => _data.Name = x);
}
