namespace ConsoleHero;

public static class MenuBuilder
{
    public static IAddOptions NoTitle() => new Builder().NoTitle();
    public static IAddOptions Title(string title) => new Builder().Title(title);

    public static IAddOptions Title(string title, ConsoleColor color) => new Builder().Title(title, color);

    public interface ISetTitle
    {
        public IAddOptions NoTitle();
        public IAddOptions Title(string title);
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
        public IAddOptions Title(string title)
        {
            _item.Title.Text = title;
            return this;
        }
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
            foreach (MenuOption option in options)
                _item.Add(option);
            return _item;
        }
    }

    #region OptionBuilder
    /// <summary>
    /// The input a user would need to give to activate this option.
    /// </summary>
    public static ISetDescription Key(string key) => new OptionBuilder().Key(key);

    public static MenuOption Back(string key) => new OptionBuilder().Key(key).Description("Back").GoTo(() => { }).Always();

    public static MenuOption Exit(string key) => new OptionBuilder().Key(key).Description("Exit").GoTo(() => Environment.Exit(0)).Always();

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
        public ISetCondition GoTo(Action action);
    }
    public interface ISetCondition
    {
        public MenuOption OnlyIf(Func<bool> condition);
        public MenuOption Always();
    }

    private class OptionBuilder() : ISetKey, ISetDescription, ISetEffect, ISetCondition
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

        public ISetCondition GoTo(Action action)
        {
            _item.Effect = action;
            return this;
        }

        public MenuOption OnlyIf(Func<bool> condition)
        {
            _item.Check = condition;
            return _item;
        }

        public MenuOption Always() => _item;
    }
    #endregion

}