using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string Name { get; }
    public float Score { get; }
    public abstract void OnItemPickedUp(DebugController plane);
}
