namespace ConsoleHero;
/// <summary>
/// Start making a new request with <see cref="RequestBuilder.Ask(string)"> or <see cref="RequestBuilder.NoMessage"/>.
/// </summary>
public class Request : INode
{
    public void Call() => Ask();
    public void Call(string input) => Call();

    internal Request() => FailMessage = $"This is not a valid {DataType}.";

    internal RequestBuilder.DataType DataType { get; set; }

    internal Action<object> Apply { get; set; } = (_) => { };

    internal Action Effect { get; set; } = () => { };

    internal string FailMessage { get; set; }

    internal string StartingMessage { get; set; } = string.Empty;

    internal string ConfirmMessage { get; set; } = string.Empty;

    internal string Ask()
    {
        WriteLine(StartingMessage);
        string? result;
        while (string.IsNullOrEmpty(result = ReadLine()))
        {
            WriteLine(FailMessage);
        }
        Apply.Invoke(result);
        Effect.Invoke();
        return result;
    }
}
