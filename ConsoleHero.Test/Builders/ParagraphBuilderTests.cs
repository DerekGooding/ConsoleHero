using static ConsoleHero.ParagraphBuilder;

namespace ConsoleHero.Test.Builders;

[TestClass]
public class ParagraphBuilderTests
{
#pragma warning disable CS8618 //Allow nullable. TestInitialize ensures.
    private Paragraph _paragraph;
#pragma warning restore CS8618

    [TestInitialize]
    public void SetUp() => _paragraph = ClearOnCall().PressToContinue();

    private ILineComponent GetFirstLineTextComponent()
    {
        Assert.IsTrue(_paragraph.Outputs.Count > 0, "No lines were added to the paragraph.");
        return _paragraph.Outputs[0].Components[0];
    }
    [TestMethod]
    public void ClearOnCall_SetsClearOnCallToTrue()
    {
        _paragraph = ClearOnCall().PressToContinue();
        Assert.IsTrue(_paragraph.ClearOnCall, "ClearOnCall should be true.");
    }
    [TestMethod]
    public void Line_AddsTextLineWithoutColor()
    {
        _paragraph = Line("Hello World").PressToContinue();
        ILineComponent component = GetFirstLineTextComponent();
        if (component is ColorText colorText)
            Assert.AreEqual("Hello World", colorText.Text, "Text should match input.");
        else
            throw new Exception();
        Assert.AreEqual(GlobalSettings.DefaultTextColor, component.Color);
    }

    [TestMethod]
    public void Line_WithColor_AddsColoredTextLine()
    {
        Color expectedColor = Color.Green;
        _paragraph = Line("Hello Green World", expectedColor).PressToContinue();
        ILineComponent component = GetFirstLineTextComponent();

        Assert.IsTrue(component is ColorText colorText && colorText.Text == "Hello Green World");
        Assert.AreEqual(expectedColor, component.Color, "Color should match the input color.");
    }
    [TestMethod]
    public void Text_AppendsTextToLastLine()
    {
        const string firstPart = "First Part";
        const string secondPart = " Second Part";
        _paragraph = Line(firstPart).Text(secondPart).PressToContinue();
        ParagraphLine paragraphLine = _paragraph.Outputs[0];

        Assert.AreEqual(2, paragraphLine.Components.Count);
        Assert.IsTrue(paragraphLine.Components[0] is ColorText colorText && colorText.Text == firstPart);
        Assert.IsTrue(paragraphLine.Components[1] is ColorText colorText2 && colorText2.Text == secondPart);
    }
    [TestMethod]
    public void Delay_SetsDelayCorrectly()
    {
        TimeSpan delay = TimeSpan.FromMilliseconds(500);
        _paragraph = ClearOnCall().Delay(delay);

        Assert.AreEqual(delay, _paragraph.Delay, "Delay should match the input timespan.");
    }

    [TestMethod]
    public void DelayInSeconds_SetsDelayCorrectly()
    {
        const double seconds = 2.5;
        _paragraph = ClearOnCall().DelayInSeconds(seconds);

        Assert.AreEqual(TimeSpan.FromSeconds(seconds), _paragraph.Delay, "Delay should match the input seconds.");
    }
    [TestMethod]
    public void PressToContinue_SetsPressToContinueTrue()
    {
        _paragraph = Line("Press Enter to Continue").PressToContinue();
        Assert.IsTrue(_paragraph.PressToContinue, "PressToContinue should be true.");
    }
    [TestMethod]
    public void Input_AddsInputPlaceholderWithDefaultColor()
    {
        _paragraph = Line("Your name:").Input().PressToContinue();
        ILineComponent input = _paragraph.Outputs[0].Components[1];

        Assert.IsNotNull(input, "Input should be of type InputPlaceholder.");
        Assert.AreEqual(GlobalSettings.DefaultTextColor, input.Color, "Input color should be default.");
    }

    [TestMethod]
    public void ModifiedInput_WithModifierFunction_AddsInputModifier()
    {
        _paragraph = Line("Input (modified):").ModifiedInput(s => s.ToUpper()).PressToContinue();
        ILineComponent input = _paragraph.Outputs[0].Components[1];

        Assert.IsNotNull(input, "Input should be of type InputModifier.");
        Assert.IsTrue(input is InputModifier modifier && modifier.Modifier("test") == "TEST");
    }
    [TestMethod]
    public void GoTo_WithAction_SetsEffectAction()
    {
        bool actionCalled = false;
        Action testAction = () => actionCalled = true;

        _paragraph = Line("Click to proceed").GoTo(testAction).PressToContinue();

        _paragraph.Effect?.Invoke();
        Assert.IsTrue(actionCalled, "Effect action should be called.");
    }
    [TestMethod]
    public void MultipleLinesAndInputs_BuildsComplexParagraph()
    {
        _paragraph =
            Line("Enter your name:").Input()
            .Line("Confirm your input:").ModifiedInput(s => s.Trim())
            .PressToContinue();

        Assert.AreEqual(2, _paragraph.Outputs.Count, "Paragraph should contain two lines.");
        Assert.IsInstanceOfType(_paragraph.Outputs[0].Components[1], typeof(InputPlaceholder), "First line should contain InputPlaceholder.");
        Assert.IsInstanceOfType(_paragraph.Outputs[1].Components[1], typeof(InputModifier), "Second line should contain InputModifier.");
    }
}
