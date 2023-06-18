using System;
using Cysharp.Threading.Tasks;
using LootLocker.Requests;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardDisplayer : MonoBehaviour
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private int maxOnPage;
    [SerializeField] private GameObject parentSpawner;
    [SerializeField] private PlayerScorePresenter playerScorePresenter;

    public void Awake()
    {
        closeBtn.onClick.AddListener(CloseLeaderBoard);
    }

    public void CloseLeaderBoard()
    {
        gameObject.SetActive(false);
        for (int index = 0; index < parentSpawner.transform.childCount; index++)
        {
            Destroy(parentSpawner.transform.GetChild(index).gameObject);
        }
    }

    public async void ShowLeaderBoard()
    {
        gameObject.SetActive(true);
        PlayerSession.Instance.GetLeaderBoardInfo(maxOnPage);
        
        await UniTask.WaitUntil(()=> PlayerSession.Instance.members != null 
                                     && PlayerSession.Instance.members.Length > 0);
        SpawnPlayerScoresPage(PlayerSession.Instance.members);
    }

    public void SpawnPlayerScoresPage(LootLockerLeaderboardMember[] members)
    {
        foreach (var member in members)
        {
            var spawnedPresenter = Instantiate(playerScorePresenter, parentSpawner.transform);
            var psPresenter = spawnedPresenter.GetComponent<PlayerScorePresenter>();
            psPresenter.SetData(member.rank.ToString(), member.player.name, member.score.ToString());
        }
        
    }
}
