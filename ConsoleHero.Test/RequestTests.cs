namespace ConsoleHero.Test;

[TestClass]
public class RequestTests
{
    [TestMethod]
    public void ShouldCreate()
    {
        Request request = new();

        Assert.IsNotNull(request);
    }

    [TestMethod]
    public void Outputs_Default()
    {
        Request request = new();

        Assert.IsNotNull(request);
    }

    [TestMethod]
    public void Outputs_Set()
    {
        Request request = new();

        Assert.IsNotNull(request);
    }
}