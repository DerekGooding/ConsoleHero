namespace ConsoleHero;

public static class MenuBuilder
{
    public static IAddOptions NoTitle() => new Builder().NoTitle();
    public static IAddOptions Title(string title, Color? color = null) => new Builder().Title(title, color);

    public interface ISetTitle
    {
        public IAddOptions NoTitle();
        public IAddOptions Title(string title, Color? color);
    }

    public interface IAddOptions
    {
        public IAddOptions ClearWhenAsk();
        public IAddOptions CustomSeperator(string seperator);

        public IOptionDescription Key(string key);
        public IOptionDescription Key(char key);
        public IOptionEffect Description(string description);

        public IAddOptions OptionsFromList(IEnumerable<ColorText> list, Action<string> effect, Func<string, bool>? condition = null);
        public IAddOptions OptionsFromList(IEnumerable<ColorText> list, INode node, Func<string, bool>? condition = null);
        public IAddOptions OptionsFromList(IEnumerable<string> list, Action<string> effect, Func<string, bool>? condition = null);
        public IAddOptions OptionsFromList(IEnumerable<string> list, INode node, Func<string, bool>? condition = null);

        public Menu Cancel();
        public Menu Cancel(string key);
        public Menu Cancel(char key);
        public Menu Exit();
        public Menu Exit(string key);
        public Menu Exit(char key);
        public Menu NoRefuse();
    }

    public interface IOptionDescription
    {
        public IOptionDescription IsCaseSensitive();
        public IOptionEffect IsHidden();
        public IOptionEffect Description(string description);
    }

    public interface IOptionEffect
    {
        public IOptionEffect Color(Color color);
        public IOptionEffect If(Func<bool> condition);
        public IOptionEffect If(bool condition);
        public IAddOptions GoTo(Action action);
        public IAddOptions GoTo(INode node);
    }

    private class Builder() : ISetTitle, IAddOptions, IOptionDescription, IOptionEffect
    {
        readonly Menu _item = new();
        MenuOption _menuOption = new();

        public IAddOptions NoTitle() => this;
        public IAddOptions Title(string title, Color? color)
        {
            _item.Title = new ColorText(title, color);
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


        public IOptionDescription Key(string key)
        {
            _menuOption.Key = key;
            _menuOption.UsesAutoKey = false;
            return this;
        }
        public IOptionDescription Key(char key) => Key(key.ToString());


        public IOptionDescription IsCaseSensitive()
        {
            _menuOption.IsCaseSensitive = true;
            return this;
        }
        public IOptionEffect IsHidden()
        {
            _menuOption.IsHidden = true;
            return this;
        }
        public IOptionEffect Description(string description)
        {
            _menuOption.Description = description;
            return this;
        }
        public IOptionEffect Color(Color color)
        {
            _menuOption.Color = color;
            return this;
        }
        public IOptionEffect If(Func<bool> condition)
        {
            _menuOption.Check = condition;
            return this;
        }
        public IOptionEffect If(bool condition)
        {
            _menuOption.Check = () => condition;
            return this;
        }
        public IAddOptions GoTo(Action action)
        {
            _menuOption.Effect = action;
            _item.Add(_menuOption);
            _menuOption = new();
            return this;
        }

        public IAddOptions GoTo(INode node)
        {
            _menuOption.Effect = node.Call;
            _item.Add(_menuOption);
            _menuOption = new();
            return this;
        }

        public IAddOptions OptionsFromList(IEnumerable<ColorText> list, Action<string> effect, Func<string, bool>? condition = null)
        {
            foreach (MenuOption option in list.ToOptions(effect, condition))
            {
                _item.Add(option);
            }
            return this;
        }
        public IAddOptions OptionsFromList(IEnumerable<ColorText> list, INode node, Func<string, bool>? condition = null)
        {
            foreach (MenuOption option in list.ToOptions(node, condition))
            {
                _item.Add(option);
            }
            return this;
        }
        public IAddOptions OptionsFromList(IEnumerable<string> list, Action<string> effect, Func<string, bool>? condition = null)
        {
            foreach (MenuOption option in list.ToOptions(effect, condition))
            {
                _item.Add(option);
            }
            return this;
        }
        public IAddOptions OptionsFromList(IEnumerable<string> list, INode node, Func<string, bool>? condition = null)
        {
            foreach (MenuOption option in list.ToOptions(node, condition))
            {
                _item.Add(option);
            }
            return this;
        }

        public Menu Cancel() => Cancel("C");
        public Menu Cancel(char key) => Cancel(key.ToString());
        public Menu Cancel(string key)
        {
            _item.Add(new MenuOption() { Key = key, Description = "Cancel", Effect = () => { } });
            return _item;
        }

        public Menu Exit() => Exit("X");
        public Menu Exit(char key) => Exit(key.ToString());
        public Menu Exit(string key)
        {
            _item.Add(new MenuOption() { Key = key, Description = "Exit", Effect = () => Environment.Exit(0) });
            return _item;
        }

        public Menu NoRefuse() => _item;
    }
}