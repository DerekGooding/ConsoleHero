using Moq;

namespace ConsoleHero.Test;

[TestClass]
public class MenuTests
{
    [TestMethod]
    public void Menu_ShouldCreate()
    {
        Menu menu = new();

        Assert.IsNotNull(menu);
    }

    [TestMethod]
    public void Menu_ShouldAddOptions()
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
    public void Menu_ShouldNot_AddOption_IfReturnsFalse()
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

        IEnumerable<MenuOption> actual = menu.OuputOptions;
        Assert.AreEqual(2, menu.Count);
        Assert.AreEqual(expected.Count(), actual.Count());
        Assert.AreEqual(expected.First(), actual.First());
    }

    [TestMethod]
    public void Menu_ShouldPrint()
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

        IEnumerable<MenuOption> actual = menu.OuputOptions;
        Assert.AreEqual(expected.Count(), actual.Count());
        Assert.AreEqual(expected.First(), actual.First());
    }

    [TestMethod]
    public void Menu_ShouldInvoke()
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
}