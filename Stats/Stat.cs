namespace DangerousStreetRace.Stats;

public class Stat(float baseValue)
{
    public float BaseValue { get; private set; } = baseValue;
    public float CurrentValue { get; private set; } = baseValue;

    public List<StatModifier> Modifiers { get; private set; } = new();

    public void AddModifier(StatModifier modifier)
    {
        Modifiers.Add(modifier);
        RecalculateStatValue();
    }

    public void RemoveModifier(int index)
    {
        Modifiers.RemoveAt(index);
        RecalculateStatValue();
    }

    public void RecalculateStatValue()
    {
        CurrentValue = BaseValue;
            
        foreach (var modifier in Modifiers)
        {
            switch (modifier.ChangeType)
            {
                case StatModifierChangeType.Value:
                    CurrentValue += modifier.ModifierValue;
                    break;
                case StatModifierChangeType.Percentage:
                    CurrentValue += BaseValue * modifier.ModifierValue;
                    break;
                case StatModifierChangeType.AbsoluteMultiplier:
                    CurrentValue *= modifier.ModifierValue;
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void UpdateTime(float deltaTime)
    {
        for (int i = 0; i < Modifiers.Count;)
        {
            var modifier = Modifiers[i];
            modifier.TimeBeforeRemoving -= deltaTime;
            Modifiers[i] = modifier;
            
            if (Modifiers[i].TimeBeforeRemoving <= 0)
            {
                RemoveModifier(i);
            }
            else
            {
                i++;
            }
        }
    }
}