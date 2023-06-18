using System.Collections;
using System.Collections.Generic;
using Coherence.UI;
using LootLocker.Requests;
using UnityEngine;

public class PlayerSession : MonoBehaviour
{
    public static PlayerSession Instance;
    const int leaderboardID = 15225;

    private void Awake()
    {
        Instance = this;
    }

    public void SendRecordToLeaderBoard(string playerName, int score)
    {
        LootLockerSDKManager.SubmitScore(playerName, score, leaderboardID, (response) =>
        {
            if (response.statusCode == 200) {
                Debug.Log("Successful");
            } else {
                Debug.Log("failed: " + response.Error);
            }
        });
    }

    public void StartSession(string playerName)
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");

                return;
            }
            
            LootLockerSDKManager.SetPlayerName(playerName, response =>
            {
                if (!response.success)
                {
                    Debug.Log("error starting LootLocker session");
                }
            });

            Debug.Log("successfully started LootLocker session");
        });
    }
}
