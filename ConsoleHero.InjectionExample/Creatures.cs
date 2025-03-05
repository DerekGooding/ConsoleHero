using ConsoleHero.Generator;

namespace ConsoleHero.InjectionExample;

public readonly record struct Creature(string Name, int Health) : INamed;

[Singleton]
public partial class Creatures : IContent<Creature>
{
    public Creature[] All { get; } =
    [
        new("Slime", 5),
        new("Goblin", 15),
        new("Vampire", 35),
    ];
}
