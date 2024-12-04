using ConsoleHero.Interfaces;

namespace ConsoleHero;
/// <summary>
/// Start making a new request with <see cref="RequestBuilder.Ask(string)"/> or <see cref="RequestBuilder.NoMessage"/>.
/// </summary>
public record Request : INode, IListeningNode
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
            GlobalSettings.Service.Clear();

        GlobalSettings.Service.WriteLine(StartingMessage);
        GlobalSettings.Service.SetListener(this);
    }

    void IListeningNode.ProcessResult(string response)
    {
        if(string.IsNullOrWhiteSpace(response))
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

}
