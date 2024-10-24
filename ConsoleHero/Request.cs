namespace ConsoleHero;
/// <summary>
/// Start making a new request with <see cref="RequestBuilder.Ask(string)"/> or <see cref="RequestBuilder.NoMessage"/>.
/// </summary>
public class Request : INode
{
    internal Request() => FailMessage = $"This is not a valid {DataType}.";

    internal RequestBuilder.DataType DataType { get; set; }

    internal Action<string> Apply { get; set; } = (_) => { };

    internal Action<string> Effect { get; set; } = (_) => { };

    internal string FailMessage { get; set; }

    internal string StartingMessage { get; set; } = string.Empty;

    internal string ConfirmMessage { get; set; } = string.Empty;

    internal bool ClearOnCall { get; set; }

    /// <summary>
    /// Asks for a result and will give a failure message if not an appropriate response, asking again.
    /// </summary>
    public void Call(string input = "")
    {
        if (ClearOnCall)
            Clear();

        WriteLine(StartingMessage);
        string? result;
        while (string.IsNullOrEmpty(result = ReadLine()))
        {
            WriteLine(FailMessage);
        }
        Apply.Invoke(result);
        Effect.Invoke(result);
        for (int i = 0; i < GlobalSettings.Spacing; i++)
            WriteLine();
    }
}
