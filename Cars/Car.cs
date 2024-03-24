using DangerousStreetRace.Abilities;
using DangerousStreetRace.Stats;

namespace DangerousStreetRace.Cars;

public abstract class Car : ICloneable
{
    protected Dictionary<StatType, Stat> Stats;
    
    private Random _random = new();

    public abstract string Name { get; }
    
    protected abstract Ability? Ability { get; set; }
    
    public Car? Rival { get; set; }
    public int Position { get; set; }
    public float AttackSkillFactor { get; private set; } = 1;
    public float DrivingSkillFactor { get; private set; } = 1;
    public float DefenceSkillFactor { get; private set; } = 1;
    public float CurrentHealth { get; private set; }
    public float CurrentEnergy { get; private set; }
    public float AttackCooldown { get; private set; }
    public float PassedDistance { get; private set; }
    public float CurrentSpeed { get; private set; }
    public bool IsDead { get; private set; }

    public Car()
    {
        var config = StatsConfig.GetConfig(GetType());
        Stats = new Dictionary<StatType, Stat>();
        Stats.Add(StatType.MaxSpeed, new Stat(config.MaxSpeed));
        Stats.Add(StatType.Weight, new Stat(config.Weight));
        Stats.Add(StatType.PowerDensity, new Stat(config.PowerDensity));
        Stats.Add(StatType.MaxHealth, new Stat(config.MaxHealth));
        Stats.Add(StatType.Armor, new Stat(config.Armor));
        Stats.Add(StatType.AttackDamage, new Stat(config.AttackDamage));
        Stats.Add(StatType.AttackInterval, new Stat(config.AttackInterval));
        Stats.Add(StatType.MaxEnergy, new Stat(config.MaxMana));
        Stats.Add(StatType.EnergyRegenerationRate, new Stat(config.EnergyRegenerationRate));
        Stats.Add(StatType.AbilityBlockers, new Stat(config.AbilityReloadingRate));
        
        CurrentHealth = config.MaxHealth;
    }

    public abstract object Clone();

    public void RandomizeSkillFactors(float minFactor = 0.6f)
    {
        AttackSkillFactor = Math.Clamp(_random.NextSingle(), minFactor, 1);
        DefenceSkillFactor = Math.Clamp(_random.NextSingle(), minFactor, 1);
        DrivingSkillFactor = Math.Clamp(_random.NextSingle(), minFactor, 1);
    }

    public void SetStatModifier(StatModifier modifier, StatType statType) => Stats[statType].AddModifier(modifier);

    protected virtual void Attack()
    {
        AttackCooldown = Stats[StatType.AttackInterval].CurrentValue;
        Rival?.TakeDamage(Stats[StatType.AttackDamage].CurrentValue * AttackSkillFactor);
    }

    protected virtual bool CanAttack() => Rival != null && AttackCooldown <= 0;

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        var change = Math.Clamp(damage * DefenceSkillFactor - Stats[StatType.Armor].CurrentValue, 0, float.MaxValue);
        CurrentHealth = Math.Clamp(CurrentHealth - change, 0, Stats[StatType.MaxHealth].CurrentValue);

        if (CurrentHealth <= 0)
        {
            IsDead = true;
        }
    }

    public void RestoreHealth(float value)
    {
        CurrentHealth = Math.Clamp(CurrentHealth + value, 0, Stats[StatType.MaxHealth].CurrentValue);
    }

    protected void TryUseAbility()
    {
        if (Ability?.Energy < CurrentEnergy && Math.Round(Stats[StatType.AbilityBlockers].CurrentValue) <= 0)
        {
            Ability.Apply(this, Rival);
            CurrentEnergy -= Ability.Energy;
        }
    }

    public virtual void UpdateTime(float deltaTime)
    {
        if (IsDead) return;
        
        foreach (var stat in Stats)
        {
            stat.Value.UpdateTime(deltaTime);
        }

        AttackCooldown -= deltaTime;
        CurrentEnergy += Stats[StatType.EnergyRegenerationRate].CurrentValue * deltaTime * DrivingSkillFactor;
        CurrentSpeed = Math.Clamp(CurrentSpeed + Stats[StatType.PowerDensity].CurrentValue * deltaTime * AttackSkillFactor, 0, Stats[StatType.MaxSpeed].CurrentValue);
        PassedDistance += CurrentSpeed * deltaTime;

        if (CanAttack())
        {
            Attack();
        }
    }
}