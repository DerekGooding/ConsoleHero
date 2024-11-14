namespace ConsoleHero;
/// <summary>
/// Start making a new paragraph with <see cref="ParagraphBuilder.Line(string)"/> or <see cref="ParagraphBuilder.ClearOnCall"/>.
/// </summary>
public record Paragraph : INode
{
    internal Paragraph() { }

    internal List<ParagraphLine> Outputs { get; set; } = new();
    internal object[]? Arguments { get; set; }
    internal bool PressToContinue { get; set; } = true;
    internal bool ClearOnCall { get; set; }
    internal TimeSpan Delay { get; set; }
    internal Action Effect { get; set; } = () => { };

    /// <summary>
    /// Displays a block of text and then either waits for a key to be pressed or delays a number of seconds.
    /// </summary>
    public void Call(string input = "")
    {
        if (ClearOnCall)
            Clear();
        Outputs.Print(input);

        FinalizeMessage();
    }

    private void FinalizeMessage()
    {
        if (PressToContinue)
        {
            ReadKey();
        }
        else
        {
            Thread.Sleep(Delay);
        }
        Effect.Invoke();
    }
}
