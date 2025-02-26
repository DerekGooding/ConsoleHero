using static ConsoleHero.MenuBuilder;

namespace ConsoleHero.Test.Builders;

[TestClass]
public class MenuBuilderTests
{
#pragma warning disable CS8618 //Allow nullable. TestInitialize ensures.
    private Menu _menu;
#pragma warning restore CS8618

    [TestInitialize]
    public void SetUp() => _menu = NoTitle().NoRefuse(); // Base menu for each test

    private MenuOption GetFirstOption()
    {
        Assert.IsTrue(_menu.Count > 0, "No options were added to the menu.");
        return _menu.Options[0];
    }

    [TestMethod]
    public void NoTitle_SetsTitleToEmpty()
    {
        _menu = NoTitle().NoRefuse();
        Assert.AreEqual(string.Empty, _menu.Title.Text);
    }

    [TestMethod]
    public void Title_WithTextAndColor_SetsTitleCorrectly()
    {
        Color expectedColor = Color.Red;
        _menu = Title("Main Menu", expectedColor).NoRefuse();

        Assert.AreEqual("Main Menu", _menu.Title.Text);
        Assert.AreEqual(expectedColor, _menu.Title._color);
    }

    [TestMethod]
    public void ClearOnCall_SetsClearOnCallToTrue()
    {
        _menu = NoTitle().ClearOnCall().NoRefuse();
        Assert.IsTrue(_menu.ClearOnCall, "ClearOnCall should be set to true.");
    }

    [TestMethod]
    public void CustomSeperator_SetsSeperatorCorrectly()
    {
        _menu = NoTitle().CustomSeperator("--").NoRefuse();
        Assert.AreEqual("--", _menu.Separator, "Custom separator should be set correctly.");
    }

    [TestMethod]
    public void Key_WithString_AddsOptionWithCorrectKey()
    {
        _menu = NoTitle().Key("A").Description("Option A").GoTo(() => { }).NoRefuse();

        MenuOption option = GetFirstOption();
        Assert.AreEqual("A", option.Key);
        Assert.AreEqual("Option A", option.Description);
    }

    [TestMethod]
    public void Key_WithChar_AddsOptionWithCorrectKey()
    {
        _menu = NoTitle().Key('B').Description("Option B").GoTo(() => { }).NoRefuse();

        MenuOption option = GetFirstOption();
        Assert.AreEqual("B", option.Key);
        Assert.AreEqual("Option B", option.Description);
    }

    [TestMethod]
    public void Description_SetsOptionDescription()
    {
        _menu = NoTitle().Key("C").Description("Cancel Option").GoTo(() => { }).NoRefuse();

        MenuOption option = GetFirstOption();
        Assert.AreEqual("Cancel Option", option.Description, "Description should match input.");
    }
    [TestMethod]
    public void IsCaseSensitive_SetsCaseSensitiveToTrue()
    {
        _menu = NoTitle().Key("A").IsCaseSensitive().Description("").GoTo(() => { }).NoRefuse();

        MenuOption option = GetFirstOption();
        Assert.IsTrue(option.IsCaseSensitive, "Option should be case sensitive.");
    }

    [TestMethod]
    public void IsHidden_SetsOptionToHidden()
    {
        _menu = NoTitle().Key("B").IsHidden().GoTo(() => { }).NoRefuse();

        MenuOption option = GetFirstOption();
        Assert.IsTrue(option.IsHidden, "Option should be hidden.");
    }

    [TestMethod]
    public void If_WithCondition_SetsOptionCondition()
    {
        bool condition = true;
        _menu = NoTitle().Key("D").Description("").If(() => condition).GoTo(() => { }).NoRefuse();

        MenuOption option = GetFirstOption();
        Assert.IsTrue(option.Check(), "Condition should return true.");
    }
    [TestMethod]
    public void GoTo_WithAction_SetsEffectCorrectly()
    {
        bool actionCalled = false;
        Action testAction = () => actionCalled = true;

        _menu = NoTitle().Key("A").Description("Test Action").GoTo(testAction).NoRefuse();

        MenuOption option = GetFirstOption();
        option.Effect?.Invoke();
        Assert.IsTrue(actionCalled, "Action should be invoked when Effect is called.");
    }
    [TestMethod]
    public void Cancel_AddsCancelOption()
    {
        _menu = NoTitle().Cancel('C');

        MenuOption option = GetFirstOption();
        Assert.AreEqual("C", option.Key);
        Assert.AreEqual("Cancel", option.Description);
    }

    [TestMethod]
    public void Exit_AddsExitOption()
    {
        _menu = NoTitle().Exit('X');

        MenuOption option = GetFirstOption();
        Assert.AreEqual("X", option.Key);
        Assert.AreEqual("Exit", option.Description);
    }
}
