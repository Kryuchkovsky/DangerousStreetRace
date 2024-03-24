using DangerousStreetRace.Abilities;

namespace DangerousStreetRace.Cars;

public class MinerCar : Car
{
    public override string Name => "Miner";

    protected override Ability? Ability { get; set; } = new Explosion();

    public override object Clone() => new MinerCar();
    
    protected override bool CanAttack() => base.CanAttack() && Position == 1;

    protected override void Attack()
    {
        base.Attack();
        TryUseAbility();
    }
}