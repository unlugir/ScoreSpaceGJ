using Cysharp.Threading.Tasks;
using LootLocker.Requests;
using UnityEngine;

public class LeaderBoardDisplayer : MonoBehaviour
{
    [SerializeField] private int maxOnPage;
    [SerializeField] private GameObject parentSpawner;
    [SerializeField] private PlayerScorePresenter playerScorePresenter;

    public async void ShowLeaderBoard()
    {
        gameObject.SetActive(true);
        PlayerSession.Instance.GetLeaderBoardInfo();
        
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
