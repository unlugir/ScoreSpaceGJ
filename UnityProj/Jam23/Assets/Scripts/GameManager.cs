using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using LootLocker.Requests;

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
        
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");

                return;
            }

            Debug.Log("successfully started LootLocker session");
        });
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
        airplane.ResetPlane();
        seq.Append(airplane.transform.DOMove(startPosition.position, 1f));
        seq.Join(airplane.transform.DORotate(startPosition.rotation.eulerAngles, 1f));
        seq.AppendCallback(() => airplane.enabled = true);

    }
    private void SetPlaneIdlePosition()
    {
        menuCamera.Priority = 10;
        gameCamera.Priority = 0;
        airplane.enabled = false;
        airplane.transform.position = idlePosition.position;
        airplane.transform.rotation = idlePosition.rotation;
    }
    public void StartGame()
    {
        SetPlaneStartPosition();
    }
    public void StartMenu()
    {
        SetPlaneIdlePosition();
        airplane.ResetPlane();
        MenuController.Instance.ShowMenu();
    }
}
