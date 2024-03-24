namespace DangerousStreetRace.Stats
{
    public struct StatModifier
    {
        public readonly StatModifierChangeType ChangeType;
        public readonly float ModifierValue;
        public readonly float Duration;

        public float TimeBeforeRemoving { get; set; }

        public StatModifier(StatModifierChangeType changeType, float modifierValue, float duration)
        {
            ChangeType = changeType;
            ModifierValue = modifierValue;
            Duration = duration;
            TimeBeforeRemoving = Duration;
        }
    }
}