using DangerousStreetRace.Cars;

namespace DangerousStreetRace.Abilities;

public abstract class Ability
{
    protected Random Random = new();
    
    public abstract float Energy { get; }
    protected abstract float Duration { get; }
    public abstract void Apply(Car owner, Car target);
}