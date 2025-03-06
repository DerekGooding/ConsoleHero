using ConsoleHero.Interfaces;
using Moq;

namespace ConsoleHero.Test;

[TestClass]
public class RequestTests
{
    private Mock<IConsoleService> _mockConsoleService;

    [TestInitialize]
    public void Setup()
    {
        _mockConsoleService = new Mock<IConsoleService>();
        GlobalSettings.Service = _mockConsoleService.Object;
    }

    [TestMethod]
    public void Constructor_SetsDefaultFailMessage()
    {
        // Arrange & Act
        var request = new Request();

        // Assert
        Assert.AreEqual("This is not a valid String.", request.FailMessage);
    }

    [TestMethod]
    public void Call_WritesStartingMessageAndSetsListener()
    {
        // Arrange
        var request = new Request { StartingMessage = "Test message" };

        // Act
        request.Call();

        // Assert
        _mockConsoleService.Verify(x => x.WriteLine("Test message"), Times.Once);
        _mockConsoleService.Verify(x => x.SetListener(request), Times.Once);
    }

    [TestMethod]
    public void Call_WithClearOnCall_ClearsConsole()
    {
        // Arrange
        var request = new Request { StartingMessage = "Test message", ClearOnCall = true };

        // Act
        request.Call();

        // Assert
        _mockConsoleService.Verify(x => x.Clear(), Times.Once);
    }

    [TestMethod]
    public void ProcessResult_WithYesResponse_InvokesApplyWithTrue()
    {
        // Arrange
        bool? result = null;
        var request = new Request
        {
            DataType = RequestBuilder.DataType.YesNo,
            Apply = obj => result = (bool)obj
        };
        var listeningNode = (IListeningNode)request;

        // Act
        listeningNode.ProcessResult("yes");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ProcessResult_WithYUppercaseResponse_InvokesApplyWithTrue()
    {
        // Arrange
        bool? result = null;
        var request = new Request
        {
            DataType = RequestBuilder.DataType.YesNo,
            Apply = obj => result = (bool)obj
        };
        var listeningNode = (IListeningNode)request;

        // Act
        listeningNode.ProcessResult("Y");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ProcessResult_WithNoResponse_InvokesApplyWithFalse()
    {
        // Arrange
        bool? result = null;
        var request = new Request
        {
            DataType = RequestBuilder.DataType.YesNo,
            Apply = obj => result = (bool)obj
        };
        var listeningNode = (IListeningNode)request;

        // Act
        listeningNode.ProcessResult("no");

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ProcessResult_WithInvalidYesNoResponse_ShowsFailMessage()
    {
        // Arrange
        var request = new Request
        {
            DataType = RequestBuilder.DataType.YesNo,
            FailMessage = "Invalid response"
        };
        var listeningNode = (IListeningNode)request;

        // Act
        listeningNode.ProcessResult("maybe");

        // Assert
        _mockConsoleService.Verify(x => x.WriteLine("Invalid response"), Times.Once);
        _mockConsoleService.Verify(x => x.SetListener(request), Times.Once);
    }

    [TestMethod]
    public void ProcessResult_WithValidStringResponse_InvokesApplyAndEffect()
    {
        // Arrange
        string applyResult = null;
        string effectResult = null;
        var request = new Request
        {
            DataType = RequestBuilder.DataType.String,
            Apply = obj => applyResult = (string)obj,
            Effect = str => effectResult = str
        };
        var listeningNode = (IListeningNode)request;

        // Act
        listeningNode.ProcessResult("valid input");

        // Assert
        Assert.AreEqual("valid input", applyResult);
        Assert.AreEqual("valid input", effectResult);
    }

    [TestMethod]
    public void ProcessResult_WithEmptyStringResponse_ShowsFailMessage()
    {
        // Arrange
        var request = new Request
        {
            DataType = RequestBuilder.DataType.String,
            FailMessage = "Empty input not allowed"
        };
        var listeningNode = (IListeningNode)request;

        // Act
        listeningNode.ProcessResult("");

        // Assert
        _mockConsoleService.Verify(x => x.WriteLine("Empty input not allowed"), Times.Once);
        _mockConsoleService.Verify(x => x.SetListener(request), Times.Once);
    }

    [TestMethod]
    public void ProcessResult_WithWhitespaceStringResponse_ShowsFailMessage()
    {
        // Arrange
        var request = new Request
        {
            DataType = RequestBuilder.DataType.String,
            FailMessage = "Whitespace input not allowed"
        };
        var listeningNode = (IListeningNode)request;

        // Act
        listeningNode.ProcessResult("   ");

        // Assert
        _mockConsoleService.Verify(x => x.WriteLine("Whitespace input not allowed"), Times.Once);
        _mockConsoleService.Verify(x => x.SetListener(request), Times.Once);
    }
}