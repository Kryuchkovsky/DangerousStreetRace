using DangerousStreetRace.Cars;

namespace DangerousStreetRace.Abilities;

public class RapidRepair : Ability
{
    private readonly float _changeOfCriticalRepair = 0.25f;
    private float _repairValue = 30;
    private float _criticalRepairValue = 60;
    
    public override float Energy => 25;
    protected override float Duration => 0;
    public override void Apply(Car owner, Car target)
    {
        var value = Random.NextSingle() <= _changeOfCriticalRepair ? _criticalRepairValue : _repairValue;
        owner.RestoreHealth(value);
    }
}