namespace ConsoleHero;

/// <summary>
/// The builder class for handling new <see cref="Menu"/>s.
/// </summary>
public static class MenuBuilder
{
    /// <summary>
    /// This menu has no title.
    /// </summary>
    public static IAddOptions NoTitle() => new Builder().NoTitle();
    /// <summary>
    /// The title for this menu.
    /// </summary>
    /// <param name="title">The full string that will be printed.</param>
    /// <param name="color">Can be a custom color from the rest of the options text.</param>
    public static IAddOptions Title(string title, Color? color = null) => new Builder().Title(title, color);

    /// <summary>
    /// Continue building with one of the following:
    /// <br><see cref="NoTitle"/></br>
    /// <br><see cref="Title(string, Color?)"/></br>
    /// </summary>
    public interface ISetTitle
    {
        /// <summary>
        /// This menu has no title.
        /// </summary>
        public IAddOptions NoTitle();
        /// <summary>
        /// The title for this menu.
        /// </summary>
        /// <param name="title">The full string that will be printed.</param>
        /// <param name="color">Can be a custom color from the rest of the options text.</param>
        public IAddOptions Title(string title, Color? color);
    }

    /// <summary>
    /// Continue building with one of the following:
    /// <br><see cref="ClearOnCall"/></br>
    /// <br><see cref="CustomSeperator(string)"/></br>
    /// <br><see cref="Key(string)"/></br>
    /// <br><see cref="Description(string)"/> </br>
    /// <br><see cref="OptionsFromList(IEnumerable{string}, INode, Func{string, bool}?)"/></br>
    /// <br><see cref="Cancel()"/> </br>
    /// <br><see cref="Exit()"/> </br>
    /// <br><see cref="NoRefuse()"/> </br>
    /// </summary>
    public interface IAddOptions
    {
        /// <summary>
        /// Clear the console when this <see cref="INode"/> is called.
        /// </summary>
        public IAddOptions ClearOnCall();
        /// <summary>
        /// Change the default " => " seperator.
        /// </summary>
        /// <param name="seperator">The full string that will be printed between the key and description of a <see cref="MenuOption"/></param>
        public IAddOptions CustomSeperator(string seperator);

        /// <summary>
        /// What the user will need to input to choose this option.
        /// </summary>
        public IOptionDescription Key(string key);
        /// <summary>
        /// What the user will need to input to choose this option.
        /// </summary>
        public IOptionDescription Key(char key);
        /// <summary>
        /// The text that is displayed to explain what this option does when the <see cref="Menu"/> is Called.
        /// </summary>
        /// <param name="description">The full string to be printed after the Key and seperator.</param>
        public IOptionEffect Description(string description);

        /// <summary>
        /// Creates a set of <see cref="MenuOption"/> based of a list.
        /// </summary>
        public IAddOptions OptionsFromList(IEnumerable<ColorText> list, Action<string> effect, Func<string, bool>? condition = null);
        /// <summary>
        /// Creates a set of <see cref="MenuOption"/> based of a list.
        /// </summary>
        public IAddOptions OptionsFromList(IEnumerable<ColorText> list, INode node, Func<string, bool>? condition = null);
        /// <summary>
        /// Creates a set of <see cref="MenuOption"/> based of a list.
        /// </summary>
        public IAddOptions OptionsFromList(IEnumerable<string> list, Action<string> effect, Func<string, bool>? condition = null);
        /// <summary>
        /// Creates a set of <see cref="MenuOption"/> based of a list.
        /// </summary>
        public IAddOptions OptionsFromList(IEnumerable<string> list, INode node, Func<string, bool>? condition = null);
        /// <summary>
        /// Creates a set of <see cref="MenuOption"/> based of a generic list.
        /// </summary>
        public IAddOptions OptionsFromList(IEnumerable<IMenuOption> list, Action<string> effect, Func<string, bool>? condition = null);
        /// <summary>
        /// Creates a set of <see cref="MenuOption"/> based of a generic list.
        /// </summary>
        public IAddOptions OptionsFromList(IEnumerable<IMenuOption> list, INode node, Func<string, bool>? condition = null);

        /// <summary>
        /// Adds an option with Key = "c" and Description = "Cancel" that ends the <see cref="INode"/> chain.
        /// </summary>
        public Menu Cancel();
        /// <summary>
        /// Adds an option with a custom key and Description = "Cancel" that ends the <see cref="INode"/> chain.
        /// </summary>
        public Menu Cancel(string key);
        /// <summary>
        /// Adds an option with a custom key and Description = "Cancel" that ends the <see cref="INode"/> chain.
        /// </summary>
        public Menu Cancel(char key);
        /// <summary>
        /// Adds an option with Key = "x" and Description = "Exit" that closes the application.
        /// </summary>
        public Menu Exit();
        /// <summary>
        /// Adds an option with a custom key and Description = "Exit" that closes the application.
        /// </summary>
        public Menu Exit(string key);
        /// <summary>
        /// Adds an option with a custom key and Description = "Exit" that closes the application.
        /// </summary>
        public Menu Exit(char key);
        /// <summary>
        /// No backing out, the user must give a valid option.
        /// </summary>
        public Menu NoRefuse();
    }

    /// <summary>
    /// Continue building with one of the following:
    /// <br><see cref="IsCaseSensitive"/></br>
    /// <br><see cref="IsHidden()"/></br>
    /// <br><see cref="Description(string)"/></br>
    /// </summary>
    public interface IOptionDescription
    {
        /// <summary>
        /// By default, user inputs are case agnostic. This sets the option to accept a case sensitive key only.
        /// </summary>
        public IOptionDescription IsCaseSensitive();
        /// <summary>
        /// The user can choose this option by inputting the key but it is not displayed when the <see cref="Menu"/> is Called.
        /// </summary>
        public IOptionEffect IsHidden();
        /// <summary>
        /// The text that is displayed to explain what this option does when the <see cref="Menu"/> is Called.
        /// </summary>
        /// <param name="description">The full string to be printed after the Key and seperator.</param>
        public IOptionEffect Description(string description);
    }

    /// <summary>
    /// Continue building with one of the following:
    /// <br><see cref="Color(Color)"/></br>
    /// <br><see cref="If(bool)"/></br>
    /// <br><see cref="GoTo(INode)"/></br>
    /// </summary>
    public interface IOptionEffect
    {
        /// <summary>
        /// Sets the custom color text will be printed for this specific <see cref="MenuOption"/>.
        /// </summary>
        public IOptionEffect Color(Color color);
        /// <summary>
        /// If this function returns false, this <see cref="MenuOption"/> will not be printed or be a choice for user input.
        /// </summary>
        public IOptionEffect If(Func<bool> condition);
        /// <summary>
        /// If this function returns false, this <see cref="MenuOption"/> will not be printed or be a choice for user input.
        /// </summary>
        public IOptionEffect If(bool condition);
        /// <summary>
        /// The next custom action to be called when this <see cref="MenuOption"/> is chosen.
        /// </summary>
        public IAddOptions GoTo(Action action);
        /// <summary>
        /// The next <see cref="INode"/> to be called when this <see cref="MenuOption"/> is chosen.
        /// </summary>
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

        public IAddOptions OptionsFromList(IEnumerable<IMenuOption> list, Action<string> effect, Func<string, bool>? condition = null)
            => OptionsFromList(list.Select(x => x.Print()), effect, condition);
        public IAddOptions OptionsFromList(IEnumerable<IMenuOption> list, INode node, Func<string, bool>? condition = null)
            => OptionsFromList(list.Select(x => x.Print()), node, condition);

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