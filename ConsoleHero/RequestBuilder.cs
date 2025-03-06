using ConsoleHero.Interfaces;

namespace ConsoleHero;

/// <summary>
/// The builder class for handling new <see cref="Request"/>s.
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
        /// The requirement of a user input is specifically "Yes" or "No".
        /// </summary>
        YesNo,
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

    public static Request YesNo(Action ifYes, Action ifNo)
        => new Builder()
        .Ask("Yes or No?")
        .For(DataType.YesNo)
        .Use<bool>((s) => { if (s) ifYes(); else ifNo(); });

    public static Request YesNo(INode ifYes, INode ifNo)
        => new Builder()
        .Ask("Yes or No?")
        .For(DataType.YesNo)
        .Use<bool>((s) => { if (s) ifYes.Call(); else ifNo.Call(); });

    public static Request YesNo(Action ifYes, INode ifNo)
        => new Builder()
        .Ask("Yes or No?")
        .For(DataType.YesNo)
        .Use<bool>((s) => { if (s) ifYes(); else ifNo.Call(); });

    public static Request YesNo(INode ifYes, Action ifNo)
        => new Builder()
        .Ask("Yes or No?")
        .For(DataType.YesNo)
        .Use<bool>((s) => { if (s) ifYes.Call(); else ifNo(); });

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
    /// <br><see cref="Use(Action{object})"/></br>
    /// </summary>
    public interface ISetFail
    {
        /// <summary>
        /// Clear the console when this <see cref="INode"/> is called.
        /// </summary>
        public ISetFail ClearOnCall();
        /// <summary>
        /// Set a custom message to inform the user an erronous response has been given.
        /// </summary>
        /// <param name="message">The full string message to be printed when a failed user input is given.</param>
        public ISetDataType FailMessage(string message);
        /// <summary>
        /// Set a restriction for the type of response the user can give as a valid response.
        /// </summary>
        public ISetEffect For(DataType dataType);
        /// <summary>
        /// The next custom action to be called when this <see cref="MenuOption"/> is chosen.
        /// </summary>
        public ISetUse Goto(Action<string> effect);
        /// <summary>
        /// The next <see cref="INode"/> to be called when this <see cref="MenuOption"/> is chosen.
        /// </summary>
        public ISetUse Goto(INode node);
        /// <summary>
        /// What to do with the user's result. Usually this is applied to a static property.
        /// </summary>
        public Request Use(Action<object> apply);
        /// <summary>
        /// What to do with the user's result. Usually this is applied to a static property.
        /// </summary>
        public Request Use<T>(Action<T> apply);
    }

    /// <summary>
    /// Continue building with one of the following:
    /// <br><see cref="For(DataType)"/></br>
    /// <br><see cref="Goto(INode)"/> </br>
    /// <br><see cref="Use(Action{object})"/></br>
    /// </summary>
    public interface ISetDataType
    {
        /// <summary>
        /// Set a restriction for the type of response the user can give as a valid response.
        /// </summary>
        public ISetEffect For(DataType dataType);
        /// <summary>
        /// The next custom action to be called when this <see cref="MenuOption"/> is chosen.
        /// </summary>
        public ISetUse Goto(Action<string> effect);
        /// <summary>
        /// The next <see cref="INode"/> to be called when this <see cref="MenuOption"/> is chosen.
        /// </summary>
        public ISetUse Goto(INode node);
        /// <summary>
        /// What to do with the user's result. Usually this is applied to a static property.
        /// </summary>
        public Request Use(Action<object> apply);
        /// <summary>
        /// What to do with the user's result. Usually this is applied to a static property.
        /// </summary>
        public Request Use<T>(Action<T> apply);
    }

    /// <summary>
    /// Continue building with one of the following:
    /// <br><see cref="Goto(INode)"/> </br>
    /// <br><see cref="Use(Action{object})"/></br>
    /// </summary>
    public interface ISetEffect
    {
        /// <summary>
        /// The next custom action to be called when this <see cref="MenuOption"/> is chosen.
        /// </summary>
        public ISetUse Goto(Action<string> effect);
        /// <summary>
        /// The next <see cref="INode"/> to be called when this <see cref="MenuOption"/> is chosen.
        /// </summary>
        public ISetUse Goto(INode node);
        /// <summary>
        /// What to do with the user's result. Usually this is applied to a static property.
        /// </summary>
        public Request Use(Action<object> apply);
        /// <summary>
        /// What to do with the user's result. Usually this is applied to a static property.
        /// </summary>
        public Request Use<T>(Action<T> apply);
    }

    /// <summary>
    /// Finish the build with a <see cref="Use(Action{object})"/>.
    /// </summary>
    public interface ISetUse
    {
        /// <summary>
        /// What to do with the user's result. Usually this is applied to a static property.
        /// </summary>
        public Request Use(Action<object> apply);
        /// <summary>
        /// What to do with the user's result. Usually this is applied to a static property.
        /// </summary>
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
            _item.Effect = (_) => node.Call();
            return this;
        }

        public Request Use(Action<object> effect)
        {
            _item.Apply = effect;
            return _item;
        }
        public Request Use<T>(Action<T> effect)
        {
            _item.Apply = (object s) => effect((T)s);
            return _item;
        }
    }
}
