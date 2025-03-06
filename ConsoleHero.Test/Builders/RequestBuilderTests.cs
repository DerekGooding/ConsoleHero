using ConsoleHero.Interfaces;
using Moq;

namespace ConsoleHero.Test.Builders;

[TestClass]
public class RequestBuilderTests
{
    private Mock<IConsoleService> _mockConsoleService;

    [TestInitialize]
    public void Setup()
    {
        _mockConsoleService = new Mock<IConsoleService>();
        GlobalSettings.Service = _mockConsoleService.Object;
    }

    [TestMethod]
    public void YesNo_WithActionParameters_CreatesRequestCorrectly()
    {
        // Arrange
        var yesActionCalled = false;
        var noActionCalled = false;
        Action yesAction = () => yesActionCalled = true;
        Action noAction = () => noActionCalled = true;

        // Act
        var request = RequestBuilder.YesNo(yesAction, noAction);

        // Assert
        Assert.IsNotNull(request);
        Assert.AreEqual(RequestBuilder.DataType.YesNo, request.DataType);
        Assert.AreEqual("Yes or No?", request.StartingMessage);

        // Test yes response
        request.Apply.Invoke(true);
        Assert.IsTrue(yesActionCalled);
        Assert.IsFalse(noActionCalled);

        // Reset and test no response
        yesActionCalled = false;
        request.Apply.Invoke(false);
        Assert.IsFalse(yesActionCalled);
        Assert.IsTrue(noActionCalled);
    }

    [TestMethod]
    public void YesNo_WithINodeParameters_CreatesRequestCorrectly()
    {
        // Arrange
        var mockYesNode = new Mock<INode>();
        var mockNoNode = new Mock<INode>();

        // Act
        var request = RequestBuilder.YesNo(mockYesNode.Object, mockNoNode.Object);

        // Assert
        Assert.IsNotNull(request);
        Assert.AreEqual(RequestBuilder.DataType.YesNo, request.DataType);

        // Test yes response
        request.Apply.Invoke(true);
        mockYesNode.Verify(x => x.Call(), Times.Once);
        mockNoNode.Verify(x => x.Call(), Times.Never);

        // Test no response
        request.Apply.Invoke(false);
        mockYesNode.Verify(x => x.Call(), Times.Once);
        mockNoNode.Verify(x => x.Call(), Times.Once);
    }

    [TestMethod]
    public void YesNo_WithActionAndINodeParameters_CreatesRequestCorrectly()
    {
        // Arrange
        var actionCalled = false;
        Action yesAction = () => actionCalled = true;
        var mockNoNode = new Mock<INode>();

        // Act
        var request = RequestBuilder.YesNo(yesAction, mockNoNode.Object);

        // Assert
        Assert.IsNotNull(request);

        // Test yes response
        request.Apply.Invoke(true);
        Assert.IsTrue(actionCalled);
        mockNoNode.Verify(x => x.Call(), Times.Never);

        // Test no response
        actionCalled = false;
        request.Apply.Invoke(false);
        Assert.IsFalse(actionCalled);
        mockNoNode.Verify(x => x.Call(), Times.Once);
    }

    [TestMethod]
    public void YesNo_WithINodeAndActionParameters_CreatesRequestCorrectly()
    {
        // Arrange
        var actionCalled = false;
        Action noAction = () => actionCalled = true;
        var mockYesNode = new Mock<INode>();

        // Act
        var request = RequestBuilder.YesNo(mockYesNode.Object, noAction);

        // Assert
        Assert.IsNotNull(request);

        // Test yes response
        request.Apply.Invoke(true);
        Assert.IsFalse(actionCalled);
        mockYesNode.Verify(x => x.Call(), Times.Once);

        // Test no response
        request.Apply.Invoke(false);
        Assert.IsTrue(actionCalled);
        mockYesNode.Verify(x => x.Call(), Times.Once);
    }

    [TestMethod]
    public void Ask_SetsStartingMessage()
    {
        // Arrange & Act
        var builder = RequestBuilder.Ask("Test message");
        var request = builder.Use(_ => { });

        // Assert
        Assert.AreEqual("Test message", request.StartingMessage);
    }

    [TestMethod]
    public void NoMessage_LeavesStartingMessageEmpty()
    {
        // Arrange & Act
        var builder = RequestBuilder.NoMessage();
        var request = builder.Use(_ => { });

        // Assert
        Assert.AreEqual(string.Empty, request.StartingMessage);
    }

    [TestMethod]
    public void ClearOnCall_SetsFlagCorrectly()
    {
        // Arrange & Act
        var builder = RequestBuilder.Ask("Test").ClearOnCall();
        var request = builder.Use(_ => { });

        // Assert
        Assert.IsTrue(request.ClearOnCall);
    }

    [TestMethod]
    public void FailMessage_SetsCustomMessage()
    {
        // Arrange & Act
        var builder = RequestBuilder.Ask("Test").FailMessage("Custom fail message");
        var request = builder.Use(_ => { });

        // Assert
        Assert.AreEqual("Custom fail message", request.FailMessage);
    }

    [TestMethod]
    public void For_SetsDataType()
    {
        // Arrange & Act
        var builder = RequestBuilder.Ask("Test").For(RequestBuilder.DataType.YesNo);
        var request = builder.Use(_ => { });

        // Assert
        Assert.AreEqual(RequestBuilder.DataType.YesNo, request.DataType);
    }

    [TestMethod]
    public void Goto_WithAction_SetsEffectCorrectly()
    {
        // Arrange
        string receivedInput = null;
        Action<string> effect = input => receivedInput = input;

        // Act
        var builder = RequestBuilder.Ask("Test").Goto(effect);
        var request = builder.Use(_ => { });
        request.Effect.Invoke("test input");

        // Assert
        Assert.AreEqual("test input", receivedInput);
    }

    [TestMethod]
    public void Goto_WithINode_SetsEffectCorrectly()
    {
        // Arrange
        var mockNode = new Mock<INode>();

        // Act
        var builder = RequestBuilder.Ask("Test").Goto(mockNode.Object);
        var request = builder.Use(_ => { });
        request.Effect.Invoke("test input");

        // Assert
        mockNode.Verify(x => x.Call(), Times.Once);
    }

    [TestMethod]
    public void Use_SetsApplyCorrectly()
    {
        // Arrange
        object receivedObject = null;
        Action<object> apply = obj => receivedObject = obj;

        // Act
        var request = RequestBuilder.Ask("Test").Use(apply);
        request.Apply.Invoke("test object");

        // Assert
        Assert.AreEqual("test object", receivedObject);
    }

    [TestMethod]
    public void UseGeneric_SetsApplyCorrectly()
    {
        // Arrange
        string receivedString = null;
        Action<string> apply = str => receivedString = str;

        // Act
        var request = RequestBuilder.Ask("Test").Use(apply);
        request.Apply.Invoke("test string");

        // Assert
        Assert.AreEqual("test string", receivedString);
    }
}
