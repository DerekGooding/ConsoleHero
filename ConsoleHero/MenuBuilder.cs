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
                _item.Add(option);
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
    }

    #region OptionBuilder
    /// <summary>
    /// The input a user would need to give to activate this option.
    /// </summary>
    public static ISetDescription Key(string key) => new OptionBuilder().Key(key);
    public static ISetDescription Key(char key) => new OptionBuilder().Key(key);

    public static ISetEffect Description(string description) => new OptionBuilder().Description(description);

    public interface ISetKey
    {
        public ISetDescription Key(string key);
        public ISetDescription Key(char key);
    }
    public interface ISetDescription
    {
        public ISetDescription IsCaseSensitive();
        public ISetEffect IsHidden();
        public ISetEffect Description(string description);
    }
    public interface ISetEffect
    {
        public ISetEffect Color(ConsoleColor color);
        public ISetEffect If(Func<bool> condition);
        public ISetEffect If(bool condition);
        public MenuOption GoTo(Action action);
        public MenuOption GoTo(Menu menu);
        public MenuOption GoTo(Paragraph paragraph);
    }

    private class OptionBuilder() : ISetKey, ISetDescription, ISetEffect
    {
        readonly MenuOption _item = new();
        public ISetDescription Key(string key)
        {
            _item.Key = key;
            _item.UsesAutoKey = false;
            return this;
        }
        public ISetDescription Key(char key) => Key(key.ToString());

        public ISetDescription IsCaseSensitive()
        {
            _item.IsCaseSensitive = true;
            return this;
        }
        public ISetEffect IsHidden()
        {
            _item.IsHidden = true;
            return this;
        }
        public ISetEffect Description(string description)
        {
            _item.Description = description;
            return this;
        }
        public ISetEffect Color(ConsoleColor color)
        {
            _item.Color = color;
            return this;
        }
        public ISetEffect If(Func<bool> condition)
        {
            _item.Check = condition;
            return this;
        }
        public ISetEffect If(bool condition)
        {
            _item.Check = () => condition;
            return this;
        }
        public MenuOption GoTo(Action action)
        {
            _item.Effect = action;
            return _item;
        }

        public MenuOption GoTo(Menu menu)
        {
            _item.Effect = menu.Ask;
            return _item;
        }
        public MenuOption GoTo(Paragraph paragraph)
        {
            _item.Effect = paragraph.PrintMessage;
            return _item;
        }


    }
    #endregion
}