namespace ConsoleHero;

/// <summary>
/// 
/// </summary>
public static class RequestBuilder
{
    /// <summary>
    /// The requirement datatype for the user input. By setting this to something specific, a check is done during input.
    /// If the response fails to meet this reqirement, the user is informed and requested again. 
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// By default. The requirement of a user input is string which covers everything except a null response.
        /// </summary>
        String,
        /// <summary>
        /// Not yet implimented.
        /// </summary>
        Int,
        /// <summary>
        /// Not yet implimented.
        /// </summary>
        Double,
        /// <summary>
        /// Not yet implimented.
        /// </summary>
        DateTime,
        /// <summary>
        /// Not yet implimented.
        /// </summary>
        FilePath,
    }

    /// <summary>
    /// The message shown to the user before waiting on a response.
    /// </summary>
    public static ISetFail Ask(string message) => new Builder().Ask(message);
    /// <summary>
    /// There is no additional message. 
    /// Useful if you connect a request right after a paragraph, allowing for more complicated, multi-line messages.
    /// </summary>
    public static ISetFail NoMessage() => new Builder().NoMessage();

    /// <summary>
    /// Continue building with one of the following:
    /// <br><see cref="ClearOnCall"/></br>
    /// <br><see cref="FailMessage(string)"/></br>
    /// <br><see cref="For(DataType)"/></br>
    /// <br><see cref="Goto(INode)"/> </br>
    /// <br><see cref="Use(Action{string})"/></br>
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
    /// Continue building with one of the following:
    /// <br><see cref="For(DataType)"/></br>
    /// <br><see cref="Goto(INode)"/> </br>
    /// <br><see cref="Use(Action{string})"/></br>
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
    /// Continue building with one of the following:
    /// <br><see cref="Goto(INode)"/> </br>
    /// <br><see cref="Use(Action{string})"/></br>
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
    /// Finish the build with a <see cref="Use(Action{string})"/>.
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
