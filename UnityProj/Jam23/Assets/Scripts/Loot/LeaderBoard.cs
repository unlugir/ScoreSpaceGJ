using LootLocker.Requests;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private Button pressBtn;
    // Start is called before the first frame update
    void Start()
    {
        pressBtn.onClick.AddListener(() =>
        {
            string memberID = "20";
            int leaderboardID = 123;
            int score = 1000;
        
            LootLockerSDKManager.SubmitScore(memberID, score, leaderboardID, (response) =>
            {
                if (response.statusCode == 200) {
                    Debug.Log("Successful");
                } else {
                    Debug.Log("failed: " + response.Error);
                }
            });
        });
    }

}
