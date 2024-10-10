namespace ConsoleHero;

public interface INode
{
    public abstract void Call();
    public abstract void Call<T>(T item);
}
