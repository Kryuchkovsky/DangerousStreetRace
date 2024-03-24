using DangerousStreetRace.Cars;
using DangerousStreetRace.Stats;

namespace DangerousStreetRace.Abilities;

public class DemonInvocation : Ability
{
    private Dictionary<StatType, StatModifier> _modifiers;
    
    private readonly float _changeOfSelfDamage = 0.25f;
    private readonly float _damage = 25;
    
    public override float Energy => 25;
    protected override float Duration => 3;

    public DemonInvocation()
    {
        _modifiers = new Dictionary<StatType, StatModifier>();
        _modifiers.Add(StatType.Armor, new StatModifier(StatModifierChangeType.Value, -10, Duration));
        _modifiers.Add(StatType.AttackDamage, new StatModifier(StatModifierChangeType.AbsoluteMultiplier, 0, Duration));
        _modifiers.Add(StatType.AbilityBlockers, new StatModifier(StatModifierChangeType.Value, 1, Duration));
    }

    public override void Apply(Car owner, Car target)
    {
        if (Random.NextSingle() <= _changeOfSelfDamage)
        {
            owner.TakeDamage(_damage);
        }

        var modifierIndex = Random.Next(0, _modifiers.Count - 1);
        var pair = _modifiers.ElementAt(modifierIndex);
        target?.SetStatModifier(pair.Value, pair.Key);
        target?.TakeDamage(_damage);
    }
}