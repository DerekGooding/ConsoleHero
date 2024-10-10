namespace ConsoleHero;
public class Paragraph : INode
{
    internal Paragraph() { }

    internal List<ColorLine> Outputs { get; set; } = [];
    internal object[] Arguments { get; set; } = [];
    internal bool PressToContinue { get; set; } = true;
    internal TimeSpan Delay {  get; set; }

    public void Call() => Print();
    public void Call(string input) => Print(input);

    public void Print(string input = "")
    {
        foreach (ColorLine line in Outputs)
        {
            ForegroundColor = line.Color;
            if(line.Text.Contains("{0}"))
                WriteLine(string.Format(line.Text, input));
            else
                WriteLine(line.Text);
        }
        GlobalSettings.SetColorToDefault();

        FinalizeMessage();
    }

    private void FinalizeMessage()
    {
        if(PressToContinue)
        {
            ReadLine();
        }
        else
        {
            //TODO - add delay
        }
    }
}
