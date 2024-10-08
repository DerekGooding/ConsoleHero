namespace ConsoleHero;

public static class MenuBuilder
{
    public static IAddOptions NoTitle() => new Builder().NoTitle();
    public static IAddOptions Title(string title, ConsoleColor color = ConsoleColor.White) => new Builder().Title(title, color);

    public interface ISetTitle
    {
        public IAddOptions NoTitle();
        public IAddOptions Title(string title, ConsoleColor color);
    }

    public interface IAddOptions
    {
        public IAddOptions CustomSeperator(string seperator);
        public Menu Options(MenuOption[] options);
    }

    private class Builder() : ISetTitle, IAddOptions
    {
        readonly Menu _item = new();

        public IAddOptions NoTitle() => this;
        public IAddOptions Title(string title, ConsoleColor color)
        {
            _item.Title = new ColorLine(title, color);
            return this;
        }

        public IAddOptions CustomSeperator(string seperator)
        {
            _item.Seperator = seperator;
            return this;
        }

        public Menu Options(MenuOption[] options)
        {
            int x = 1;
            foreach (MenuOption option in options)
            {
                if (option.Key.Length == 0)
                    option.Key = $"{x++}";
                _item.Add(option);
            }
            return _item;
        }
    }

    #region OptionBuilder
    /// <summary>
    /// The input a user would need to give to activate this option.
    /// </summary>
    public static ISetDescription Key(string key) => new OptionBuilder().Key(key);

    public static ISetEffect Description(string description) => new OptionBuilder().Key("1").Description(description);

    public static MenuOption Back() => new OptionBuilder().Key('B').Description("Back").GoTo(() => { });
    public static MenuOption Back(string key) => new OptionBuilder().Key(key).Description("Back").GoTo(() => { });

    public static MenuOption Exit() => new OptionBuilder().Key('X').Description("Exit").GoTo(() => Environment.Exit(0));
    public static MenuOption Exit(string key) => new OptionBuilder().Key(key).Description("Exit").GoTo(() => Environment.Exit(0));

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