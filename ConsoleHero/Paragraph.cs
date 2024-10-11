using ConsoleHero.Helpers;

namespace ConsoleHero;
public class Paragraph : INode
{
    internal Paragraph() { }

    internal List<ParagraphLine> Outputs { get; set; } = [];
    internal object[] Arguments { get; set; } = [];
    internal bool PressToContinue { get; set; } = true;
    internal TimeSpan Delay { get; set; }

    public void Call() => Print();
    public void Call(string input) => Print(input);

    public void Print(string input = "")
    {
        foreach (ParagraphLine line in Outputs)
        {
            foreach(ILineComponent component in line.Components)
            {
                ColorHelper.SetTextColor(component.Color);
                if (component is ColorText c)
                    Write(c.Text);
                else if(component is InputPlaceholder)
                    Write(input);
                else if(component is InputModifier modifier)
                    Write(modifier.Modifier.Invoke(input));
            }
            WriteLine();
        }
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
    }
}
