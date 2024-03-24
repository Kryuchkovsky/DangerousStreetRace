using DangerousStreetRace.Cars;
using DangerousStreetRace.Stats;

namespace DangerousStreetRace.Abilities;

public class Afterburning : Ability
{
    public override float Energy => 50;
    protected override float Duration => 2;

    public override void Apply(Car owner, Car target)
    {
        owner.SetStatModifier(new StatModifier(StatModifierChangeType.Value, 200, Duration), StatType.MaxSpeed);
        owner.SetStatModifier(new StatModifier(StatModifierChangeType.Value, 100, Duration), StatType.PowerDensity);
    }
}