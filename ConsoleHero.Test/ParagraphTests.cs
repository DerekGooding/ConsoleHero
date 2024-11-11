using static ConsoleHero.ParagraphBuilder;

namespace ConsoleHero.Test;

[TestClass]
public class ParagraphTests
{
    [TestMethod]
    public void ShouldCreate()
    {
        Paragraph paragraph = new();

        Assert.IsNotNull(paragraph);
    }

    [TestMethod]
    public void Outputs_Default()
    {
        Paragraph paragraph = new();
        List<ParagraphLine> expected = [];

        Assert.IsNotNull(paragraph.Outputs);
        Assert.AreEqual(expected.Count, paragraph.Outputs.Count);
    }

    [TestMethod]
    public void Outputs_Set()
    {
        const string anything = "Anything";
        Paragraph paragraph = Line(anything).PressToContinue();
        ParagraphLine expected = new();
        expected.Components.Add(anything.DefaultColor());

        Assert.AreEqual(1, paragraph.Outputs.Count);
        Assert.AreEqual(expected.Components.Count, paragraph.Outputs[0].Components.Count);
        Assert.AreEqual(expected.Components[0], paragraph.Outputs[0].Components[0]);
    }

    [TestMethod]
    public void Arguments_Default()
    {
        Paragraph paragraph = new();
        object[] expected = [];

        Assert.IsNotNull(paragraph.Arguments);
        Assert.AreEqual(expected.Length, paragraph.Arguments.Length);
    }
}
