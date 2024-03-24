using DangerousStreetRace.Abilities;

namespace DangerousStreetRace.Cars;

public class MechanicCar : Car
{
    public override string Name => "Mechanic";
    
    protected override Ability? Ability { get; set; } = new RapidRepair();

    public override object Clone() => new MechanicCar();

    public override void UpdateTime(float deltaTime)
    {
        base.UpdateTime(deltaTime);
        TryUseAbility();
    }
}