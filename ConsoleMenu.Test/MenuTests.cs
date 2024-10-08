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
        MenuOption menuOption = new("1", "Open Door");

        menu.Add(menuOption);

        Assert.AreEqual(1, menu.Count);
    }

    [TestMethod]
    public void Menu_ShouldNot_AddOption_IfReturnsFalse()
    {
        Menu menu = new();
        MenuOption menuOption = new("1", "Open Door", check: () => false);
        MenuOption menuOption2 = new("2", "Close Door", check: () => true);

        const string expected = "2 => Close Door";

        menu.Add(menuOption);
        menu.Add(menuOption2);

        Assert.AreEqual(2, menu.Count);
        Assert.AreEqual(expected, menu.Print());
    }

    [TestMethod]
    public void Menu_ShouldPrint()
    {
        Menu menu = new([new("1", "Open Door"), new("2", "Close Door")]);

        const string expected = "1 => Open Door\n2 => Close Door";

        Assert.AreEqual(expected, menu.Print());
    }

    [TestMethod]
    public void Menu_ShouldPrint_DifferentSeperator()
    {
        Menu menu = new([new("1", "Open Door"), new("2", "Close Door")])
        {
            Seperator = " | "
        };

        const string expected = "1 | Open Door\n2 | Close Door";

        Assert.AreEqual(expected, menu.Print());
    }

    [TestMethod]
    public void MenuOption_ShouldPrint()
    {
        MenuOption menuOption = new("1", "Open Door");

        const string expected = "1 => Open Door";

        Assert.AreEqual(expected, menuOption.Print());
    }

    [TestMethod]
    public void Menu_ShouldInvoke()
    {
        Mock<Action> mockService = new();
        Action myClass = new(mockService.Object);
        Action action = new(() => { });

        MenuOption menuOption = new("1", "Open Door", myClass);

        menuOption.Invoke();

        mockService.Verify(service => service.Invoke(), Times.Once);
    }
}