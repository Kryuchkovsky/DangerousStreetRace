using DangerousStreetRace.Abilities;

namespace DangerousStreetRace.Cars;

public class JetCar : Car
{
    public override string Name => "Jet";

    protected override Ability? Ability { get; set; } = new Afterburning();

    public override object Clone() => new JetCar();
    
    public override void UpdateTime(float deltaTime)
    {
        base.UpdateTime(deltaTime);
        TryUseAbility();
    }
}