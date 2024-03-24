namespace DangerousStreetRace.Stats;

public struct StatConfig
{
    public readonly float MaxSpeed;
    public readonly float Weight;
    public readonly float PowerDensity;
    public readonly float MaxHealth;
    public readonly float Armor;
    public readonly float AttackDamage;
    public readonly float AttackInterval;
    public readonly float MaxMana;
    public readonly float EnergyRegenerationRate;
    public readonly float AbilityReloadingRate;

    public StatConfig(float maxSpeed = 300,
        float weight = 1500,
        float powerDensity = 30,
        float maxHealth = 100,
        float armor = 5,
        float attackDamage = 10,
        float attackInterval = 2,
        float maxMana = 100,
        float energyRegenerationRate = 10,
        float abilityReloadingRate = 0)
    {
        MaxSpeed = maxSpeed;
        Weight = weight;
        PowerDensity = powerDensity;
        MaxHealth = maxHealth;
        Armor = armor;
        AttackDamage = attackDamage;
        AttackInterval = attackInterval;
        MaxMana = maxMana;
        EnergyRegenerationRate = energyRegenerationRate;
        AbilityReloadingRate = abilityReloadingRate;
    }
}