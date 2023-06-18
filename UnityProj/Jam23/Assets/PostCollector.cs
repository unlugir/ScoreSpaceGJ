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

    public async void ShowAnimation(Sprite image)
    {
        if(image == null) return;

        gameObject.transform.DOMoveY(startPos,
            .25f).OnComplete(() =>
        {
            ShowPostal(image);
            gameObject.transform.DOMoveY(endPos,
                .25f).OnComplete(() => { });
        }); 
        await UniTask.Delay(5000);
        HideAnimation();
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
