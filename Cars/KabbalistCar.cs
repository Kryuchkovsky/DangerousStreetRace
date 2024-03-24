using DangerousStreetRace.Abilities;

namespace DangerousStreetRace.Cars;

public class KabbalistCar : Car
{
    public override string Name => "Kabbalist";
    
    protected override Ability? Ability { get; set; } = new DemonInvocation();
    
    public override object Clone() => new KabbalistCar();
    
    public override void UpdateTime(float deltaTime)
    {
        base.UpdateTime(deltaTime);
        TryUseAbility();
    }
}