using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PostCollector : MonoBehaviour
{
    [SerializeField] private Image postImage;
    [SerializeField] private TextMeshProUGUI textUgui;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;

    public void ShowAnimation(Sprite image)
    {
        if(image == null) return;
        
        
        gameObject.transform.DOLocalMove(endPos,
            .25f).OnComplete(() => { ShowPostal(image);});
    }

    public void HideAnimation()
    {
        gameObject.transform.DOLocalMove(startPos,
            .25f);
    }

    public void ShowPostal(Sprite image)
    {
        postImage.sprite = image;
        textUgui.text = image.name;
    }
}