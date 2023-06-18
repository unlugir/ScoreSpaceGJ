using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private string presenterStyle = "0000000000";
    [SerializeField] private TextMeshProUGUI scorePresenter;
    public int score = 0;
    public bool isShowing = false;

    public void AddNewScore(int score)
    {
        this.score += score;
        PulseScore();
        nextScore = this.score;
        UpgradeScore();
    }
    
    private void PulseScore()
    {
        var sequence = DOTween.Sequence().Append(scorePresenter.transform.DOScale(1.1f, 0.1f));
        sequence.SetLoops(2, LoopType.Yoyo).Play();
    }

    private int nextScore = 0;
    int itterator = 1;
    private int displayedScore = 0;
    
    private async void UpgradeScore()
    {
        if (isShowing)
            return;
        
        while (displayedScore != nextScore)
        {
            isShowing = true;
            if(nextScore - displayedScore > itterator )
                displayedScore += itterator;
            else
            {
                displayedScore = nextScore;
                isShowing = false;
            }
                
            await UniTask.WaitForFixedUpdate();
            
            string scoreText = displayedScore.ToString();
            scorePresenter.text = presenterStyle.Remove(presenterStyle.Length - scoreText.Length, scoreText.Length) 
                                  + scoreText;
        }
    }

}
