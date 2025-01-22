namespace ConsoleHero.Test;

[TestClass]
public class ModelTests
{
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
