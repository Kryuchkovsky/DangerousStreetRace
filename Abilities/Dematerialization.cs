using DangerousStreetRace.Cars;
using DangerousStreetRace.Stats;

namespace DangerousStreetRace.Abilities;

public class Dematerialization : Ability
{
    public override float Energy => 50;
    protected override float Duration => 3;

    public override void Apply(Car owner, Car target)
    {
        owner.SetStatModifier(new StatModifier(StatModifierChangeType.Value, float.MaxValue, Duration), StatType.Armor);
        owner.SetStatModifier(new StatModifier(StatModifierChangeType.AbsoluteMultiplier, 0, Duration), StatType.AttackDamage);
    }
}