using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampItem : Item
{
    public override string Name { get; } = "Stamp";
    public override int Score { get; } = 50;
    public Country country;
    public override void OnItemPickedUp(DebugController plane)
    {
        base.OnItemPickedUp(plane);
        Debug.Log($"Country {Time.time}");
        ItemSpawner.Instance.OnContryItemPickUp(country);
        country.hasItem = false;
        //UI STUFF
    }
}
