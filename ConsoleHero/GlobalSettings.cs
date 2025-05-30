using ConsoleHero.Injection;

namespace ConsoleHero
{
    /// <summary>
    /// A static set of settings that affect all future console text printed with ConsoleHero.
    /// </summary>
    public static class GlobalSettings
    {
        public static Host Content { get; set; } = Host.Initialize();

        public static T Get<T>() where T : class => Content.Get<T>();
    }
}
