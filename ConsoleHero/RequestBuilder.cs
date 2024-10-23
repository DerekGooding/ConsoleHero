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

    public interface ISetFail
    {
        public ISetFail ClearOnCall();
        public ISetDataType FailMessage(string message);
        public ISetEffect For(DataType dataType);
        public ISetUse Goto(Action<string> effect);
        public ISetUse Goto(INode node);
        public Request Use(Action<string> apply);
        public Request Use<T>(Action<T> apply);
    }

    public interface ISetDataType
    {
        public ISetEffect For(DataType dataType);
        public ISetUse Goto(Action<string> effect);
        public ISetUse Goto(INode node);
        public Request Use(Action<string> apply);
        public Request Use<T>(Action<T> apply);
    }

    public interface ISetEffect
    {
        public ISetUse Goto(Action<string> effect);
        public ISetUse Goto(INode node);
        public Request Use(Action<string> apply);
        public Request Use<T>(Action<T> apply);
    }

    public interface ISetUse
    {
        public Request Use(Action<string> apply);
        public Request Use<T>(Action<T> apply);
    }

    internal class Builder : ISetFail, ISetDataType, ISetEffect, ISetUse
    {
        private readonly Request _item = new();

        public ISetFail ClearOnCall()
        {
            _item.ClearOnCall = true;
            return this;
        }
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

        public ISetUse Goto(Action<string> effect)
        {
            _item.Effect = effect;
            return this;
        }
        public ISetUse Goto(INode node)
        {
            _item.Effect = node.Call;
            return this;
        }

        public Request Use(Action<string> effect)
        {
            _item.Apply = effect;
            return _item;
        }
        public Request Use<T>(Action<T> effect)
        {
            _item.Apply = (string s) => effect((T)(object)s);
            return _item;
        }
    }
}
