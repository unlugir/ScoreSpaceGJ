using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Item
{
    public string Name { get; }
    public void OnItemPickedUp(DebugController plane);
}
