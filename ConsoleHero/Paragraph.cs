using ConsoleHero.Helpers;

namespace ConsoleHero;
/// <summary>
/// Start making a new paragraph with <see cref="ParagraphBuilder.Line(string)"/> or <see cref="ParagraphBuilder.ClearOnCall"/>.
/// </summary>
public record Paragraph : INode
{
    internal Paragraph() { }

    internal List<ParagraphLine> Outputs { get; set; } = [];
    internal object[] Arguments { get; set; } = [];
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

        foreach (ParagraphLine line in Outputs)
        {
            foreach (ILineComponent component in line.Components)
            {
                ColorHelper.SetTextColor(component.Color);
                if (component is ColorText c)
                    Write(c.Text);
                else if (component is InputPlaceholder)
                    Write(input);
                else if (component is InputModifier modifier)
                    Write(modifier.Modifier.Invoke(input));
            }
            WriteLine();
        }
        for (int i = 0; i < GlobalSettings.Spacing; i++)
            WriteLine();
        ColorHelper.SetToDefault();

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
