namespace ConsoleHero.Test;
public class ConsoleOutput : IDisposable
{
    private readonly StringWriter _stringWriter;
    private readonly TextWriter _originalOutput;

    public ConsoleOutput()
    {
        _stringWriter = new StringWriter();
        _originalOutput = Console.Out;
        Console.SetOut(_stringWriter);
    }

    public string GetOutput() => _stringWriter.ToString();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Console.SetOut(_originalOutput);
        _stringWriter.Dispose();
    }
}
