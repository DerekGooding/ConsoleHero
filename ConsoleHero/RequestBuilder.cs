namespace ConsoleHero;

public static class RequestBuilder
{
    public enum DataType
    {
        String,
        Int,
        Double,
        DateTime,
        FilePath,
    }

    public static ISetFail Ask(string message) => new Builder().Ask(message);
    public static ISetFail NoMessage() => new Builder().NoMessage();

    public interface ISetOriginalAsk
    {
        public ISetFail Ask(string message);
        public ISetFail NoMessage();
    }

    public interface ISetFail
    {
        public ISetDataType FailMessage(string message);
        public ISetEffect For(DataType dataType);
        public ISetUse Goto(Action effect);
        public ISetUse Goto(INode node);
        public Request Use(Action<object> apply);
    }

    public interface ISetDataType
    {
        public ISetEffect For(DataType dataType);
        public ISetUse Goto(Action effect);
        public ISetUse Goto(INode node);
        public Request Use(Action<object> apply);
    }

    public interface ISetEffect
    {
        public ISetUse Goto(Action effect);
        public ISetUse Goto(INode node);
        public Request Use(Action<object> apply);
    }

    public interface ISetUse
    {
        public Request Use(Action<object> apply);
    }

    internal class Builder : ISetOriginalAsk, ISetFail, ISetDataType, ISetEffect, ISetUse
    {
        private readonly Request _item = new();

        public ISetFail Ask(string message)
        {
            _item.StartingMessage = message;
            return this;
        }
        public ISetFail NoMessage() => this;

        public ISetDataType FailMessage(string message)
        {
            _item.FailMessage = message;
            return this;
        }
        public ISetEffect For(DataType dataType)
        {
            _item.DataType = dataType;
            return this;
        }

        public ISetUse Goto(Action effect)
        {
            _item.Effect = effect;
            return this;
        }
        public ISetUse Goto(INode node)
        {
            _item.Effect = node.Call;
            return this;
        }

        public Request Use(Action<object> effect)
        {
            _item.Apply = effect;
            return _item;
        }
    }
}
