namespace ConsoleHero;

/// <summary>
/// The builder class for handling new <see cref="Menu"/>s.
/// </summary>
public static class MenuBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IAddOptions NoTitle() => new Builder().NoTitle();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static IAddOptions Title(string title, Color? color = null) => new Builder().Title(title, color);

    /// <summary>
    /// 
    /// </summary>
    public interface ISetTitle
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAddOptions NoTitle();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public IAddOptions Title(string title, Color? color);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IAddOptions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAddOptions ClearOnCall();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public IAddOptions CustomSeperator(string seperator);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IOptionDescription Key(string key);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IOptionDescription Key(char key);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public IOptionEffect Description(string description);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="effect"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IAddOptions OptionsFromList(IEnumerable<ColorText> list, Action<string> effect, Func<string, bool>? condition = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="node"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IAddOptions OptionsFromList(IEnumerable<ColorText> list, INode node, Func<string, bool>? condition = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="effect"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IAddOptions OptionsFromList(IEnumerable<string> list, Action<string> effect, Func<string, bool>? condition = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="node"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IAddOptions OptionsFromList(IEnumerable<string> list, INode node, Func<string, bool>? condition = null);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="effect"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IAddOptions OptionsFromList<T>(IEnumerable<T> list, Action<string> effect, Func<string, bool>? condition = null);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="node"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IAddOptions OptionsFromList<T>(IEnumerable<T> list, INode node, Func<string, bool>? condition = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Menu Cancel();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Menu Cancel(string key);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Menu Cancel(char key);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Menu Exit();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Menu Exit(string key);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Menu Exit(char key);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Menu NoRefuse();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IOptionDescription
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IOptionDescription IsCaseSensitive();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IOptionEffect IsHidden();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public IOptionEffect Description(string description);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IOptionEffect
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public IOptionEffect Color(Color color);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IOptionEffect If(Func<bool> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IOptionEffect If(bool condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IAddOptions GoTo(Action action);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
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
        public IAddOptions ClearOnCall()
        {
            _item.ClearOnCall = true;
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
            _menuOption.Effect = () => node.Call();
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

        public IAddOptions OptionsFromList<T>(IEnumerable<T> list, Action<string> effect, Func<string, bool>? condition = null)
            => OptionsFromList(list.Select(x => x?.ToString() ?? "Null"), effect, condition);
        public IAddOptions OptionsFromList<T>(IEnumerable<T> list, INode node, Func<string, bool>? condition = null)
            => OptionsFromList(list.Select(x => x?.ToString() ?? "Null"), node, condition);

        public Menu Cancel() => Cancel("C");
        public Menu Cancel(char key) => Cancel(key.ToString());
        public Menu Cancel(string key)
        {
            _item.Add(new MenuOption() { Key = key, UsesAutoKey = false, Description = "Cancel", Effect = () => { } });
            return _item;
        }

        public Menu Exit() => Exit("X");
        public Menu Exit(char key) => Exit(key.ToString());
        public Menu Exit(string key)
        {
            _item.Add(new MenuOption() { Key = key, UsesAutoKey = false, Description = "Exit", Effect = () => Environment.Exit(0) });
            return _item;
        }

        public Menu NoRefuse() => _item;
    }
}