namespace ConsoleHero;
public class Paragraph : INode
{
    internal Paragraph() { }

    internal List<ColorLine> Outputs { get; set; } = [];
    internal bool PressToContinue { get; set; } = true;
    internal TimeSpan Delay {  get; set; }

    public void Call() => Print();
    public void Call<T>(T item) => Print();

    public void Print()
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
