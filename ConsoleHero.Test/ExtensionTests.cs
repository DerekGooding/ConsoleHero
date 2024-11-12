namespace ConsoleHero.Test;

[TestClass]
public class ExtensionTests
{
    private readonly Action<string> _dummyEffect = _ => { };
    private readonly Func<string, bool> _dummyCondition = s => s.Length > 0;

    [TestMethod]
    public void ToOptions_WithColorTextList_CreatesCorrectMenuOptions()
    {
        List<ColorText> colorTexts =
        [
            new("Option1", Color.Red),
            new("Option2", Color.Blue)
        ];

        MenuOption[] options = colorTexts.ToOptions(_dummyEffect, _dummyCondition);

        Assert.AreEqual(2, options.Length);
        Assert.AreEqual("Option1", options[0].Description);
        Assert.AreEqual(Color.Red, options[0].Color);
        Assert.IsTrue(options[0].Check());
        Assert.AreEqual("Option2", options[1].Description);
        Assert.AreEqual(Color.Blue, options[1].Color);
        Assert.IsTrue(options[1].Check());
    }
    [TestMethod]
    public void ToOptions_WithStringList_CreatesCorrectMenuOptions()
    {
        List<string> strings = ["Option1", "Option2"];

        MenuOption[] options = strings.ToOptions(_dummyEffect, _dummyCondition);

        Assert.AreEqual(2, options.Length);
        Assert.AreEqual("Option1", options[0].Description);
        Assert.IsTrue(options[0].Check());
        Assert.AreEqual("Option2", options[1].Description);
        Assert.IsTrue(options[1].Check());
    }

    [TestMethod]
    public void ToOptions_WithINodeObject_CreatesCorrectMenuOptions()
    {
        List<ColorText> colorTexts =
        [
            new ColorText("Option1", Color.Red),
            new ColorText("Option2", Color.Blue)
        ];
        DummyNode node = new();

        MenuOption[] options = colorTexts.ToOptions(node, _dummyCondition);

        Assert.AreEqual(2, options.Length);
        Assert.AreEqual("Option1", options[0].Description);
        Assert.AreEqual("Option2", options[1].Description);
        Assert.IsTrue(options[0].Check());
        Assert.IsTrue(options[1].Check());
    }

    [TestMethod]
    public void ToOptions_WithIMenuOptionList_CreatesCorrectMenuOptions()
    {
        List<IMenuOption> menuOptions =
        [
            new DummyMenuOption("Option1"),
            new DummyMenuOption("Option2")
        ];

        MenuOption[] options = menuOptions.ToOptions((_) => { }, _dummyCondition);

        Assert.AreEqual(2, options.Length);
        Assert.AreEqual("Option1", options[0].Description);
        Assert.AreEqual("Option2", options[1].Description);
        Assert.IsTrue(options[0].Check());
        Assert.IsTrue(options[1].Check());
    }

    [TestMethod]
    public void Print_MenuOptionList_PrintsCorrectly()
    {
        // Arrange
        List<MenuOption> options =
        [
            new MenuOption { Key = "1", Description = "Option1" },
            new MenuOption { Key = "2", Description = "Option2" }
        ];

        // Act & Assert (assuming Print method outputs to console or logs)
        options.Print("=>"); // Expect output formatted like "1 => Option1" for each option
    }

    [TestMethod]
    public void Print_ParagraphLineList_PrintsCorrectly()
    {
        List<ParagraphLine> paragraphLines = [];
        ParagraphLine newLine1 = new();
        newLine1.Components.Add("Line1".Color(Color.Red));
        ParagraphLine newLine2 = new();
        newLine2.Components.Add("Line1".Color(Color.Blue));

        paragraphLines.Add(newLine1);
        paragraphLines.Add(newLine2);

        paragraphLines.Print(); // Expect output as separate lines for each ParagraphLine component
    }

    // Dummy helper classes for testing (implement or mock these as needed)
    private class DummyNode : INode
    {
        public void Call(string input) { }
    }

    private class DummyMenuOption(string description) : IMenuOption
    {
        private readonly string _description = description;

        public ColorText Print() => new(_description, GlobalSettings.DefaultTextColor);
    }

    [TestMethod]
    public void Color_WithValidStringAndColor_ReturnsCorrectColorText()
    {
        const string text = "Test";
        Color color = Color.Red;

        ColorText result = text.Color(color);

        Assert.AreEqual(text, result.Text);
        Assert.AreEqual(color, result._color);
    }

    [TestMethod]
    public void DefaultColor_WithValidString_ReturnsColorTextWithDefaultColor()
    {
        const string text = "DefaultColorTest";

        ColorText result = text.DefaultColor();

        Assert.AreEqual(text, result.Text);
        Assert.AreEqual(GlobalSettings.DefaultTextColor, result._color);
    }

    [TestMethod]
    public void ListToString_WithObjectList_ReturnsStringRepresentations()
    {
        List<object> objects = [1, "string", null, 3.14];

        IEnumerable<string> result = objects.ListToString();

        CollectionAssert.AreEqual(new List<string> { "1", "string", string.Empty, "3.14" }, result.ToList());
    }

    [TestMethod]
    public void ListToString_WithEmptyList_ReturnsEmptyStringList()
    {
        List<object> objects = [];

        IEnumerable<string> result = objects.ListToString();

        Assert.AreEqual(0, result.Count());
    }
}
