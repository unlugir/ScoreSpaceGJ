using TMPro;
using UnityEngine;

public class PlayerScorePresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetData(string position, string name, string score)
    {
        positionText.text = position;
        nameText.text = name;
        scoreText.text = score;
    }
}
