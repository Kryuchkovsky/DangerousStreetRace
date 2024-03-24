using DangerousStreetRace.Cars;
using DangerousStreetRace.Stats;

namespace DangerousStreetRace.Abilities;

public class Explosion : Ability
{
    private readonly float _changeToMakeExplosiveAttack = 0.15f;
    
    public override float Energy => 0;
    protected override float Duration => 3;

    public override void Apply(Car owner, Car target)
    {
        if (Random.NextSingle() <= _changeToMakeExplosiveAttack)
        {
            target?.SetStatModifier(new StatModifier(StatModifierChangeType.AbsoluteMultiplier, 0, Duration), StatType.MaxSpeed);
        }
    }
}