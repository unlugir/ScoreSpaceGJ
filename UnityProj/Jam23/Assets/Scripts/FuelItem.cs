using UnityEngine;

public class FuelItem : Item
{
    [SerializeField] private float fuelToRestore;
    public string Name { get; } = "Fuel";
    public float Score { get; } = 15f;

    public override void OnItemPickedUp(DebugController plane)
    {
        plane.fuel = Mathf.Clamp(plane.fuel + fuelToRestore, 0, plane.startFuel);
    }
}
