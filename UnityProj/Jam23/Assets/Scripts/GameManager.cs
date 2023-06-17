using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] Transform idlePosition;
    [SerializeField] Transform startPosition;
    [SerializeField] DebugController airplane;

    [SerializeField] CinemachineVirtualCamera menuCamera;
    [SerializeField] CinemachineVirtualCamera gameCamera;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        menuCamera.Priority = 10;
        gameCamera.Priority = 0;
    }
    private void SetPlaneStartPosition()
    {
        menuCamera.Priority = 0;
        gameCamera.Priority = 10;
        var seq = DOTween.Sequence();
        seq.Append(airplane.transform.DOMove(startPosition.position, 1f));
        seq.Join(airplane.transform.DORotate(startPosition.rotation.eulerAngles, 1f));
        seq.AppendCallback(() => airplane.enabled = true);

    }
    private void SetPlaneIdlePosition()
    {
        menuCamera.Priority = 10;
        gameCamera.Priority = 0;
        var seq = DOTween.Sequence();
        airplane.enabled = false;
        seq.Append(airplane.transform.DOMove(idlePosition.position, 1f));
        seq.Join(airplane.transform.DORotate(idlePosition.rotation.eulerAngles, 1f));
    }
    public void StartGame()
    {
        SetPlaneStartPosition();
    }
}
