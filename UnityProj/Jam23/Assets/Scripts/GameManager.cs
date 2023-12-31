using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using LootLocker.Requests;
using Coherence;
using Coherence.Toolkit;
using Coherence.UI;
using Cysharp.Threading.Tasks;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] CoherenceMonoBridge bridge;
    [SerializeField] Transform idlePosition;
    [SerializeField] Transform startPosition;
    public ScoreController scoreController;
    [SerializeField] ItemSpawner itemSpawner;
    [SerializeField] FuelPresenter fuelPresenter;

    [SerializeField] CinemachineVirtualCamera menuCamera;
    [SerializeField] CinemachineVirtualCamera gameCamera;

    [SerializeField] TMP_Text playerInGameText;
    public DebugController localAirplane;

    public async void ShakeCamera()
    {
        var perlin = gameCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 6;
        perlin.m_FrequencyGain = 6;
        await UniTask.Delay(1000);
        
        perlin.m_AmplitudeGain = 0;
        perlin.m_FrequencyGain = 0;
    }

    private void Awake()
    {
        Instance = this;
        bridge.ClientConnections.OnCreated += RefreshPlayersList;
        bridge.ClientConnections.OnDestroyed += RefreshPlayersList;
    }
    private void RefreshPlayersList(CoherenceClientConnection connection)
    {
        StartCoroutine(RefreshList());
    }
    IEnumerator RefreshList()
    {
        yield return new WaitForSeconds(1f);
        playerInGameText.text = "";
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        foreach (var conn in bridge.ClientConnections.GetAll())
        {
            if (conn.GameObject == null) continue;
            if (!conn.GameObject.TryGetComponent<DebugController>(out var controller)) continue;
            builder.Append(controller.playerName);
            builder.Append("\n");
        }
        playerInGameText.text = builder.ToString();
    }
    public void OnConnected(CoherenceMonoBridge bridge)
    {
        StartCoroutine(ConnectedCoroutine());
        PlayerSession.Instance.StartSession(NetworkDialog.PlayerName);
    }

    IEnumerator ConnectedCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        var connection = bridge.ClientConnections.GetMine();

        localAirplane = connection.GameObject.GetComponent<DebugController>();
        gameCamera.Follow = localAirplane.cameraFollow;
        gameCamera.LookAt = localAirplane.transform;
        StartMenu();
    }
    public void OnDisconnected(CoherenceMonoBridge bridge)
    {
        MenuController.Instance.HideAll();
        Destroy(localAirplane.gameObject);
        SaveScore();
    }

    public void SaveScore()
    {
        PlayerSession.Instance.SendRecordToLeaderBoard(NetworkDialog.PlayerName, scoreController.score);
        scoreController.score = 0;
        scoreController.AddNewScore(0);
    }

    private void SetPlaneStartPosition()
    {
        AudioManager.Instance.PlayTakeOffSound();
        menuCamera.Priority = 0;
        gameCamera.Priority = 10;

        var seq = DOTween.Sequence();
        localAirplane.GetComponent<CoherenceSync>().SendCommand(typeof(DebugController), nameof(localAirplane.ResetPlane), MessageTarget.All);

        seq.Append(localAirplane.transform.DOMove(startPosition.position, 1f));
        seq.Join(localAirplane.transform.DORotate(startPosition.rotation.eulerAngles, 1f));
        seq.AppendCallback(() => localAirplane.enabled = true);

    }
    private void SetPlaneIdlePosition()
    {
        menuCamera.Priority = 10;
        gameCamera.Priority = 0;
        localAirplane.enabled = false;
        localAirplane.transform.position = idlePosition.position;
        localAirplane.transform.rotation = idlePosition.rotation;
    }
    public void StartGame()
    {
        SetPlaneStartPosition();
        MenuController.Instance.ShowHUD();
    }
    public void StartMenu()
    {
        SetPlaneIdlePosition();
        fuelPresenter.DisplayFuelData();
        localAirplane.GetComponent<CoherenceSync>().SendCommand(typeof(DebugController), nameof(localAirplane.ResetPlane), MessageTarget.All);
        StartCoroutine(Delay(1f, MenuController.Instance.ShowMenu));
    }
    IEnumerator Delay(float time, System.Action callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }
}
