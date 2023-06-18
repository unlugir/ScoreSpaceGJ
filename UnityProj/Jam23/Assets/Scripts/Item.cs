using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public virtual string Name { get; } = "Item";
    public virtual int Score { get; } = 0;

    public virtual void OnItemPickedUp(DebugController plane)
    {
        GameManager.Instance.scoreController.AddNewScore(Score);
    }
}
