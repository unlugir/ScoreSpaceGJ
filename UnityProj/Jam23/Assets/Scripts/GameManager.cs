using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using Coherence;
using Coherence.Toolkit;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] CoherenceMonoBridge bridge;
    [SerializeField] Transform idlePosition;
    [SerializeField] Transform startPosition;
    [SerializeField] DebugController airplane;
    public ScoreController scoreController;
    [SerializeField] ItemSpawner itemSpawner;

    [SerializeField] CinemachineVirtualCamera menuCamera;
    [SerializeField] CinemachineVirtualCamera gameCamera;

    public DebugController localAirplane;

    private void Awake()
    {
        Instance = this;
    }
    public void OnConnected(CoherenceMonoBridge bridge)
    {
        StartCoroutine(ConnectedCoroutine());
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
    }

    private void SetPlaneStartPosition()
    {
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
        //add delay here
        localAirplane.GetComponent<CoherenceSync>().SendCommand(typeof(DebugController), nameof(localAirplane.ResetPlane), MessageTarget.All);
        MenuController.Instance.ShowMenu();
    }
}
