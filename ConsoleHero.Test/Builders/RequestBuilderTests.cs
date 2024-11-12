namespace ConsoleHero.Test.Builders;

[TestClass]
public class RequestBuilderTests
{
    // Test that Ask initializes the Request with the given message
    [TestMethod]
    public void Ask_ShouldSetStartingMessage()
    {
        Request request = RequestBuilder.Ask("Test Message").Use(_ => { });

        Assert.AreEqual("Test Message", request.StartingMessage);
    }

    // Test that NoMessage does not set a message
    [TestMethod]
    public void NoMessage_ShouldNotSetStartingMessage()
    {
        Request request = RequestBuilder.NoMessage().Use(_ => { });

        Assert.AreEqual(string.Empty, request.StartingMessage);
    }

    // Test that ClearOnCall sets the ClearOnCall flag on the Request
    [TestMethod]
    public void ClearOnCall_ShouldSetClearOnCallFlag()
    {
        Request request = RequestBuilder.Ask("Test").ClearOnCall().Use(_ => { });

        Assert.IsTrue(request.ClearOnCall);
    }

    // Test that FailMessage sets the FailMessage in the Request
    [TestMethod]
    public void FailMessage_ShouldSetFailMessage()
    {
        Request request = RequestBuilder.Ask("Test").FailMessage("Failure message").Use(_ => { });

        Assert.AreEqual("Failure message", request.FailMessage);
    }

    // Test that For sets the DataType correctly
    [DataTestMethod]
    [DataRow(RequestBuilder.DataType.String)]
    [DataRow(RequestBuilder.DataType.Int)]
    [DataRow(RequestBuilder.DataType.Double)]
    public void For_ShouldSetDataType(RequestBuilder.DataType dataType)
    {
        Request request = RequestBuilder.Ask("Test").For(dataType).Use(_ => { });

        Assert.AreEqual(dataType, request.DataType);
    }

    // Test that Goto(Action<string>) sets the Effect in the Request
    [TestMethod]
    public void Goto_Action_ShouldSetEffect()
    {
        Action<string> effect = s => { /* Effect Logic */ };
        Request request = RequestBuilder.Ask("Test").Goto(effect).Use(_ => { });

        Assert.AreEqual(effect, request.Effect);
    }

    // Test that Goto(INode) sets the Effect in the Request
    [TestMethod]
    public void Goto_Node_ShouldSetEffectToNodeCall()
    {
        MockNode mockNode = new();
        Request request = RequestBuilder.Ask("Test").Goto(mockNode).Use(s => { });

        Assert.AreEqual(mockNode.Call, request.Effect);
    }

    // Test that Use(Action<string>) sets the Apply function correctly
    [TestMethod]
    public void Use_Action_ShouldSetApply()
    {
        Action<string> applyAction = _ => { /* Apply Logic */ };
        Request request = RequestBuilder.Ask("Test").Use(applyAction);

        Assert.AreEqual(applyAction, request.Apply);
    }

    // Test that Use<T>(Action<T>) sets the Apply function with the correct type
    [TestMethod]
    public void Use_GenericAction_ShouldSetApplyWithCorrectType()
    {
        int result = 0;
        Action<string> applyAction = i => result = int.Parse(i) * 2;
        Request request = RequestBuilder.Ask("Test").Use(applyAction);

        Assert.IsNotNull(request.Apply);
        request.Apply("123");
        Assert.AreEqual(246, result);
    }

    // Test class for Goto(INode)
    private class MockNode : INode
    {
        public void Call(string input = "") { }
    }
}
