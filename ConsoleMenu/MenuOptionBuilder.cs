//namespace ConsoleHero;

//public static class MenuOptionBuilder
//{
//    /// <summary>
//    /// The input a user would need to give to activate this option.
//    /// <br>Keys are not case sensitive by default. Use <see cref="KeyCaseSensitive"/> otherwise.</br>
//    /// </summary>
//    public static ISetDescription Key(string key) => new Builder().Key(key);

//    /// <summary>
//    /// The input a user would need to give to activate this option.
//    /// </summary>
//    public static ISetDescription KeyCaseSensitive(string key) => new Builder().KeyCaseSensitive(key);

//    public interface ISetKey
//    {
//        public ISetDescription Key(string key);
//        public ISetDescription Key(char key);
//        public ISetDescription KeyCaseSensitive(string key);
//        public ISetDescription KeyCaseSensitive(char key);
//    }
//    public interface ISetDescription
//    {
//        public ISetEffect HasDescription(string description);
//    }
//    public interface ISetEffect
//    {
//        public ISetCondition GoTo(Action action);
//    }
//    public interface ISetCondition
//    {
//        public MenuOption OnlyIf(Func<bool> condition);
//        public MenuOption Always();
//    }

//    private class Builder() : ISetKey, ISetDescription, ISetEffect, ISetCondition
//    {
//        readonly MenuOption _item = new();
//        public ISetDescription Key(string key)
//        {
//            _item.Key = key;
//            return this;
//        }
//        public ISetDescription Key(char key) => Key(key.ToString());
//        public ISetDescription KeyCaseSensitive(string key)
//        {
//            _item.IsCaseSensitive = true;
//            _item.Key = key;
//            return this;
//        }

//        public ISetDescription KeyCaseSensitive(char key) => KeyCaseSensitive(key.ToString());

//        public ISetEffect HasDescription(string description)
//        {
//            _item.Description = description;
//            return this;
//        }

//        public ISetCondition GoTo(Action action)
//        {
//            _item.Effect = action;
//            return this;
//        }

//        public MenuOption OnlyIf(Func<bool> condition)
//        {
//            _item.Check = condition;
//            return _item;
//        }

//        public MenuOption Always() => _item;
//    }
//}
