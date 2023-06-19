using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PostCollector : MonoBehaviour
{
    [SerializeField] private Image postImage;
    [SerializeField] private TextMeshProUGUI textUgui;
    [SerializeField] private float startPos;
    [SerializeField] private float endPos;
    [SerializeField] private GameObject jumpPos;

    Sequence seq;
    public void ShowAnimation(Sprite image)
    {
        if(image == null) return;

        if (seq != null)
            seq.Kill();
        seq = DOTween.Sequence();
        seq.Append(gameObject.transform.DOMoveY(startPos, .25f));
        seq.AppendCallback(()=> ShowPostal(image));
        seq.Append(gameObject.transform.DOMoveY(jumpPos.transform.position.y, .25f));
        seq.AppendInterval(5);
        seq.Append(gameObject.transform.DOMoveY(startPos, .25f));
    }

    public void HideAnimation()
    {
        gameObject.transform.DOMoveY(startPos,
            .25f);
    }

    public void ShowPostal(Sprite image)
    {
        postImage.sprite = image;
        textUgui.text = image.name;
    }
}
