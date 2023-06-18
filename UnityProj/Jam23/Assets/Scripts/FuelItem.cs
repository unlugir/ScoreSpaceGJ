using UnityEngine;

public class FuelItem : Item
{
    public override string Name { get; } = "FuelItem";
    public override int Score { get; } = 15;
    [SerializeField] private float fuelToRestore;

    public override void OnItemPickedUp(DebugController plane)
    {
        base.OnItemPickedUp(plane);
        plane.fuel = Mathf.Clamp(plane.fuel + fuelToRestore, 0, plane.startFuel);
    }
}
