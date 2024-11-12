namespace ConsoleHero.Test;

[TestClass]
internal class ModelTesting
{
    static string ExpectedModifier(string s) => s.ToUpper();

    [TestMethod]
    public void InputModifier_ShouldUseDefaultColor()
    {
        ILineComponent inputModifier = new InputModifier(ExpectedModifier);

        Assert.AreEqual(GlobalSettings.DefaultTextColor, inputModifier.Color);
    }

    [TestMethod]
    public void InputModifier_ShouldInitializeModifierCorrectly()
    {
        InputModifier inputModifier = new(ExpectedModifier);

        Assert.AreEqual(ExpectedModifier, inputModifier.Modifier);
        string result = inputModifier.Modifier("test");
        Assert.AreEqual("TEST", result);
    }

    [TestMethod]
    public void InputModifier_ShouldInitializeColorCorrectly()
    {
        Color expected = Color.Red;
        ILineComponent inputPlaceholder = new InputModifier(ExpectedModifier, expected);

        Assert.AreEqual(expected, inputPlaceholder.Color);
    }

    [TestMethod]
    public void InputPlaceholder_ShouldUseDefaultColor()
    {
        ILineComponent inputPlaceholder = new InputPlaceholder();

        Assert.AreEqual(GlobalSettings.DefaultTextColor, inputPlaceholder.Color);
    }

    [TestMethod]
    public void InputPlaceholder_ShouldInitializeColorCorrectly()
    {
        Color expected = Color.Red;
        ILineComponent inputPlaceholder = new InputPlaceholder(expected);

        Assert.AreEqual(expected, inputPlaceholder.Color);
    }

    [TestMethod]
    public void MenuOption_DefaultBools()
    {
        MenuOption menuOption = new();

        Assert.IsFalse(menuOption.IsCaseSensitive);
        Assert.IsTrue(menuOption.UsesAutoKey);
        Assert.IsFalse(menuOption.IsHidden);
        Assert.AreEqual(GlobalSettings.DefaultTextColor, menuOption.Color);
    }
}
