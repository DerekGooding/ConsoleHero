namespace ConsoleHero;
public class Paragraph
{
    internal Paragraph() { }

    internal List<ColorLine> Outputs { get; set; } = [];
    internal bool PressToContinue { get; set; } = true;
    internal TimeSpan Delay {  get; set; }

    public void PrintMessage()
    {
        foreach (ColorLine line in Outputs)
        {
            ForegroundColor = line.Color;
            WriteLine(line.Text);
        }
        ForegroundColor = ConsoleColor.White;

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
