using UnityEngine;
using DG.Tweening;
public abstract class Item : MonoBehaviour
{
    public virtual string Name { get; } = "Item";
    public virtual int Score { get; } = 0;
    [SerializeField] float rotateSpeed;
    int direction;
    private void Start()
    {
        var initialScale = this.transform.localScale;
        this.transform.localScale = Vector3.zero;
        this.transform.DOScale(initialScale, 0.4f);

        direction = Random.Range(0, 1) > 0.5f ? 1:-1 ;
    }
    public virtual void OnItemPickedUp(DebugController plane)
    {
        AudioManager.Instance.PlayPickSound();
        GameManager.Instance.scoreController.AddNewScore(Score);
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0,0,rotateSpeed) * direction * Time.deltaTime);
    }
}
