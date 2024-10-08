using System;

namespace ConsoleHero;

public static class MenuBuilder
{
    public static MenuOption[] ToOptions<T>(this List<T> list, Action<T> effect, Func<T, bool>? condition = null)
        => condition == null
        ? [.. list.Select(x => Description(x?.ToString() ?? string.Empty).GoTo(() => effect(x)))]
        : [.. list.Select(x => Description(x?.ToString() ?? string.Empty).If(() => condition(x)).GoTo(() => effect(x)))];

    public static IAddOptions NoTitle() => new Builder().NoTitle();
    public static IAddOptions Title(string title, ConsoleColor color = ConsoleColor.White) => new Builder().Title(title, color);

    public interface ISetTitle
    {
        public IAddOptions NoTitle();
        public IAddOptions Title(string title, ConsoleColor color);
    }

    public interface IAddOptions
    {
        public IAddOptions ClearWhenAsk();
        public IAddOptions CustomSeperator(string seperator);
        public IAddOptions Options(params MenuOption[] options);

        public Menu Cancel();
        public Menu Cancel(string key);
        public Menu Cancel(char key);
        public Menu Exit();
        public Menu Exit(string key);
        public Menu Exit(char key);
        public Menu NoRefuse();
    }

    private class Builder() : ISetTitle, IAddOptions
    {
        readonly Menu _item = new();
        private int _x = 1;

        public IAddOptions NoTitle() => this;
        public IAddOptions Title(string title, ConsoleColor color)
        {
            _item.Title = new ColorLine(title, color);
            return this;
        }
        public IAddOptions ClearWhenAsk()
        {
            _item.ClearWhenAsk = true;
            return this;
        }

        public IAddOptions CustomSeperator(string seperator)
        {
            _item.Seperator = seperator;
            return this;
        }
        public IAddOptions Options(params MenuOption[] options)
        {
            foreach (MenuOption option in options)
            {
                Add(option);
            }
            return this;
        }

        public Menu Cancel() => Cancel("C");
        public Menu Cancel(char key) => Cancel(key.ToString());
        public Menu Cancel(string key)
        {
            _item.Add(new OptionBuilder().Key(key).Description("Cancel").GoTo(() => { }));
            return _item;
        }

        public Menu Exit() => Exit("X");
        public Menu Exit(char key) => Exit(key.ToString());
        public Menu Exit(string key)
        {
            _item.Add(new OptionBuilder().Key(key).Description("Exit").GoTo(() => Environment.Exit(0)));
            return _item;
        }

        public Menu NoRefuse() => _item;

        private void Add(MenuOption option)
        {
            if (option.Key.Length == 0)
            {
                option.Key = $"{_x}";
                _x++;
            }
            _item.Add(option);
        }
    }

    #region OptionBuilder
    /// <summary>
    /// The input a user would need to give to activate this option.
    /// </summary>
    public static ISetDescription Key(string key) => new OptionBuilder().Key(key);
    public static ISetDescription Key(char key) => new OptionBuilder().Key(key);

    public static ISetEffect Description(string description) => new OptionBuilder().Key("1").Description(description);

    public interface ISetKey
    {
        public ISetDescription Key(string key);
        public ISetDescription Key(char key);
    }
    public interface ISetDescription
    {
        public ISetDescription IsCaseSensitive();
        public ISetEffect Description(string description);
    }
    public interface ISetEffect
    {
        public ISetEffect If(Func<bool> condition);
        public MenuOption GoTo(Action action);
    }

    private class OptionBuilder() : ISetKey, ISetDescription, ISetEffect
    {
        readonly MenuOption _item = new();
        public ISetDescription Key(string key)
        {
            _item.Key = key;
            return this;
        }
        public ISetDescription Key(char key) => Key(key.ToString());

        public ISetDescription IsCaseSensitive()
        {
            _item.IsCaseSensitive = true;
            return this;
        }
        public ISetEffect Description(string description)
        {
            _item.Description = description;
            return this;
        }
        public ISetEffect If(Func<bool> condition)
        {
            _item.Check = condition;
            return this;
        }
        public MenuOption GoTo(Action action)
        {
            _item.Effect = action;
            return _item;
        }
    }
    #endregion
}