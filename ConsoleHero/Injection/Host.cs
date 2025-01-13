namespace ConsoleHero.Injection;
public class Host
{
    internal Host() { }

    private readonly List<Singleton> singletons = new();

    internal readonly Dictionary<Type, object> map = new();

    public interface ILoadSingletons
    {
        public ILoadSingletons Singleton<T>();
        public Host Build();
    }

    public static ILoadSingletons Singleton<T>()
        => new Builder().Singleton<T>();

    internal class Builder : ILoadSingletons
    {
        private readonly Host _host = new();

        public ILoadSingletons Singleton<T>()
        {
            _host.singletons.Add(new(typeof(T)));
            return this;
        }

        public Host Build()
        {
            _host.Initialize();
            return _host;
        }
    }

    private void Initialize()
    {
        List<Singleton> toProcess = new(singletons);
        for (int i = 0; i < toProcess.Count; i++)
        {
            Singleton singleton = toProcess[i];
            if (singleton.Dependancies.Count == 0)
            {
                singleton.Initialize();
                map.Add(singleton.Type, singleton.Instance);
                toProcess.RemoveAt(i);
                i--;
            }
        }

        while (toProcess.Count != 0)
        {
            for (int i = 0; i < toProcess.Count; i++)
            {
                Singleton singleton = toProcess[i];
                if (singleton.Dependancies.All(map.ContainsKey))
                {
                    IEnumerable<object> dependancies = singleton.Dependancies.Select(x => map[x]);
                    singleton.Initialize(dependancies.ToArray());
                    map.Add(singleton.Type, singleton.Instance);
                    toProcess.RemoveAt(i);
                    i--;
                }
            }
        }

        if (toProcess.Count != 0)
            throw new Exception("Missing singletons or circular dependancies issue.");
    }

    internal T Get<T>() where T : class
        => map.Count == 0 ? throw new Exception("Host must be initialized") : (T)map[typeof(T)];
}
