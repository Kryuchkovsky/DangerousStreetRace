using DangerousStreetRace.Cars;

namespace DangerousStreetRace.Stats;

public static class StatsConfig
{
    private static Dictionary<Type, StatConfig> _configs = new ();
    
    private static bool IsInitialized;
    
    public static StatConfig GetConfig(Type type)
    {
        if (!IsInitialized)
        {
            _configs = new Dictionary<Type, StatConfig>
            {
                {typeof(GhostCar), new StatConfig(maxSpeed: 420, attackDamage: 15, armor: 0, maxHealth: 80)},
                {typeof(JetCar), new StatConfig(armor: 0, maxHealth: 80, maxSpeed: 480, powerDensity: 35)},
                {typeof(KabbalistCar), new StatConfig(maxSpeed: 240, attackDamage: 20)},
                {typeof(MechanicCar), new StatConfig(armor: 10, maxHealth: 120)},
                {typeof(MinerCar), new StatConfig(maxSpeed: 360, powerDensity: 40)},
            };
            
            IsInitialized = true;
        }

        return _configs[type];
    }
}