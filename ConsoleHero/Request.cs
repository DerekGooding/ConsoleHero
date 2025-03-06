using ConsoleHero.Interfaces;

namespace ConsoleHero;
/// <summary>
/// Start making a new request with <see cref="RequestBuilder.Ask(string)"/> or <see cref="RequestBuilder.NoMessage"/>.
/// </summary>
public record Request : INode, IListeningNode
{
    internal Request() => FailMessage = $"This is not a valid {DataType}.";

    internal RequestBuilder.DataType DataType { get; set; }

    internal Action<object> Apply { get; set; } = (_) => { };

    internal Action<string> Effect { get; set; } = (_) => { };

    internal string FailMessage { get; set; }

    internal string StartingMessage { get; set; } = string.Empty;

    internal string ConfirmMessage { get; set; } = string.Empty;

    internal bool ClearOnCall { get; set; }

    /// <summary>
    /// Asks for a result and will give a failure message if not an appropriate response, asking again.
    /// </summary>
    public void Call()
    {
        if (ClearOnCall)
            GlobalSettings.Service.Clear();

        GlobalSettings.Service.WriteLine(StartingMessage);
        GlobalSettings.Service.SetListener(this);
    }

    void IListeningNode.ProcessResult(string response)
    {
        switch (DataType)
        {
            case RequestBuilder.DataType.YesNo:
                ProcessYesNo(response);
                break;
            default:
                ProcessString(response);
                break;
        }
    }

    private void ProcessString(string response)
    {
        if (string.IsNullOrWhiteSpace(response))
        {
            GlobalSettings.Service.WriteLine(FailMessage);
            GlobalSettings.Service.SetListener(this);
        }
        else
        {
            Apply.Invoke(response);
            Effect.Invoke(response);
        }
    }

    private void ProcessYesNo(string response)
    {
        string[] affirmative = { "yes", "y" };
        string[] negative = { "no", "n" };
        var lower = response.ToLower();
        if (affirmative.Contains(lower))
        {
            Apply.Invoke(true);
            Effect.Invoke(response);
        }
        else if (negative.Contains(lower))
        {
            Apply.Invoke(false);
            Effect.Invoke(response);
        }
        else
        {
            GlobalSettings.Service.WriteLine(FailMessage);
            GlobalSettings.Service.SetListener(this);
        }
    }
}
