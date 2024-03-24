using DangerousStreetRace.Abilities;

namespace DangerousStreetRace.Cars;

public class GhostCar : Car
{
    public override string Name => "Ghost";

    protected override Ability? Ability { get; set; } = new Dematerialization();

    public override object Clone() => new GhostCar();

    public override void UpdateTime(float deltaTime)
    {
        base.UpdateTime(deltaTime);
        TryUseAbility();
    }
}