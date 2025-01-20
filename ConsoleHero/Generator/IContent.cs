namespace ConsoleHero.Generator;
/// <summary>
/// Using the IContent interface allows the source generator to pick up this content and
/// automatically generate enums based on the names of each object and
/// Get methods to grab the static data either by index number or the generated Enums.
/// <br>It's recommended to use the [Singleton] <see cref="SingletonAttribute"/>.</br>
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IContent<T> where T : INamed
{
    /// <summary>
    /// For the generator to pick up this property, make sure it follows this format:
    /// <br></br>
    ///<br>public T[] All { get; } =</br>
    /// </summary>
    public abstract T[] All { get; }
}
