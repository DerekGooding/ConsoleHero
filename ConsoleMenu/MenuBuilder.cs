namespace ConsoleHero;

public static class MenuBuilder
{
    public static IAddOptions NoTitle() => new Builder().NoTitle();
    public static IAddOptions Title(string title) => new Builder().Title(title);

    public interface ISetTitle
    {
        public IAddOptions NoTitle();
        public IAddOptions Title(string title);
    }

    public interface IAddOptions
    {
        public IAddOptions Add(MenuOption option);
        public Menu AddLast(MenuOption option);
    }

    private class Builder() : ISetTitle, IAddOptions
    {
        readonly Menu _item = new();

        public IAddOptions NoTitle() => this;
        public IAddOptions Title(string title)
        {
            _item.Title = title;
            return this;
        }

        public IAddOptions Add(MenuOption option)
        {
            _item.Add(option);
            return this;
        }
        public Menu AddLast(MenuOption option)
        {
            _item.Add(option);
            return _item;
        }
    }
}
