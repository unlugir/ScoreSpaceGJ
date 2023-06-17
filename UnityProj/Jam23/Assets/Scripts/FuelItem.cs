using UnityEngine;

public class FuelItem : MonoBehaviour, Item
{
    [SerializeField] private float fuelToRestore;
    public string Name { get; } = "Fuel";

    public void OnItemPickedUp(DebugController plane)
    {
        plane.fuel = Mathf.Clamp(plane.fuel + fuelToRestore, 0, plane.startFuel);
    }
}
