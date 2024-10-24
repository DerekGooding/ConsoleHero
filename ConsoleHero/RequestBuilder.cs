namespace ConsoleHero;

/// <summary>
/// 
/// </summary>
public static class RequestBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 
        /// </summary>
        String,
        /// <summary>
        /// 
        /// </summary>
        Int,
        /// <summary>
        /// 
        /// </summary>
        Double,
        /// <summary>
        /// 
        /// </summary>
        DateTime,
        /// <summary>
        /// 
        /// </summary>
        FilePath,
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static ISetFail Ask(string message) => new Builder().Ask(message);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static ISetFail NoMessage() => new Builder().NoMessage();

    /// <summary>
    /// 
    /// </summary>
    public interface ISetFail
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ISetFail ClearOnCall();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ISetDataType FailMessage(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public ISetEffect For(DataType dataType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="effect"></param>
        /// <returns></returns>
        public ISetUse Goto(Action<string> effect);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public ISetUse Goto(INode node);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        public Request Use(Action<string> apply);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apply"></param>
        /// <returns></returns>
        public Request Use<T>(Action<T> apply);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ISetDataType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public ISetEffect For(DataType dataType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="effect"></param>
        /// <returns></returns>
        public ISetUse Goto(Action<string> effect);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public ISetUse Goto(INode node);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        public Request Use(Action<string> apply);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apply"></param>
        /// <returns></returns>
        public Request Use<T>(Action<T> apply);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ISetEffect
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="effect"></param>
        /// <returns></returns>
        public ISetUse Goto(Action<string> effect);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public ISetUse Goto(INode node);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        public Request Use(Action<string> apply);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apply"></param>
        /// <returns></returns>
        public Request Use<T>(Action<T> apply);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ISetUse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        public Request Use(Action<string> apply);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apply"></param>
        /// <returns></returns>
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
