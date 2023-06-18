using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private string presenterStyle = "0000000000";
    [SerializeField] private TextMeshProUGUI scorePresenter;
    public int score = 0;

    public void AddNewScore(int score)
    {
        this.score += score;
        PulseScore();
        UpgradeScore(this.score);
    }
    
    private void PulseScore()
    {
        var sequence = DOTween.Sequence().Append(scorePresenter.transform.DOScale(1.1f, 0.1f));
        sequence.SetLoops(2, LoopType.Yoyo).Play();
    }

    int itterator = 1;
    private int displayedScore = 0;
    
    private async void UpgradeScore(int nextScore)
    {
        while (displayedScore != nextScore)
        {
            if(nextScore - displayedScore > itterator )
                displayedScore += itterator;
            else
                displayedScore = nextScore;
                
            await UniTask.WaitForFixedUpdate();
            
            string scoreText = displayedScore.ToString();
            scorePresenter.text = presenterStyle.Remove(presenterStyle.Length - scoreText.Length, scoreText.Length) 
                                  + scoreText;
        }
    }

}
