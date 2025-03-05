using Moq;

namespace ConsoleHero.Test;

[TestClass]
public class MenuTests
{
    [TestMethod]
    public void ShouldCreate()
    {
        Menu menu = new();

        Assert.IsNotNull(menu);
    }

    [TestMethod]
    public void ShouldAddOptions()
    {
        Menu menu = new();
        MenuOption menuOption = new()
        {
            Key = "1",
            Description = "Open Door"
        };

        menu.Add(menuOption);

        Assert.AreEqual(1, menu.Count);
    }

    [TestMethod]
    public void ShouldNot_AddOption_IfReturnsFalse()
    {
        Menu menu = new();
        MenuOption menuOption = new()
        {
            Key = "1",
            Description = "Open Door",
            Check = () => false,
        };
        MenuOption menuOption2 = new()
        {
            Key = "2",
            Description = "Close Door",
            Check = () => true,
        };

        IEnumerable<MenuOption> expected = [menuOption2];

        menu.Add(menuOption);
        menu.Add(menuOption2);

        var actual = menu.OuputOptions;
        Assert.AreEqual(2, menu.Count);
        Assert.AreEqual(expected.Count(), actual.Count());
        Assert.AreEqual(expected.First(), actual.First());
    }

    [TestMethod]
    public void ShouldPrint()
    {
        Menu menu = new();
        MenuOption menuOption = new()
        {
            Key = "1",
            Description = "Open Door",
        };
        MenuOption menuOption2 = new()
        {
            Key = "2",
            Description = "Close Door",
        };
        IEnumerable<MenuOption> expected = [menuOption, menuOption2];

        menu.Add(menuOption);
        menu.Add(menuOption2);

        var actual = menu.OuputOptions;
        Assert.AreEqual(expected.Count(), actual.Count());
        Assert.AreEqual(expected.First(), actual.First());
    }

    [TestMethod]
    public void ShouldInvoke()
    {
        Mock<Action> mockService = new();
        Action myClass = new(mockService.Object);
        Action action = new(() => { });

        MenuOption menuOption = new()
        {
            Key = "1",
            Description = "Open Door",
            Effect = myClass
        };

        menuOption.Invoke();

        mockService.Verify(service => service.Invoke(), Times.Once);
    }

    [TestMethod]
    public void DefaultSeperator()
    {
        Menu menu = new();

        var actual = menu.Separator;

        const string expected = " => ";

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void NoTitle()
    {
        var menu = MenuBuilder.NoTitle().Cancel();

        ColorText expected = new(string.Empty);

        Assert.AreEqual(expected, menu.Title);
    }

    [TestMethod]
    public void Title_Set()
    {
        var menu = MenuBuilder.Title("Main", Color.Red).Cancel();

        ColorText expected = new("Main", ConsoleColor.Red);

        Assert.AreEqual(expected.Text, menu.Title.Text);
        Assert.AreEqual(expected._color, menu.Title._color);
    }

    [TestMethod]
    public void ClearOnCall_Default()
    {
        var menu = MenuBuilder.NoTitle().Cancel();

        Assert.AreEqual(false, menu.ClearOnCall);
    }

    [TestMethod]
    public void ClearOnCall_Set()
    {
        var menu = MenuBuilder.NoTitle().ClearOnCall().Cancel();

        Assert.AreEqual(true, menu.ClearOnCall);
    }
}