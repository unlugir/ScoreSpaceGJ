using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; }
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject gameName;
    [SerializeField] LeaderBoardDisplayer leaderBoardDisplayer;
    [SerializeField] GameObject[] gameHud;

    [SerializeField] Button playBtn;
    [SerializeField] Button leaderBoardBtn;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        menuPanel.SetActive(true);
        gameName.SetActive(true);
        foreach (var hud in gameHud)
        {
            hud.SetActive(false);
        }
        
        HideAll();
        playBtn.onClick.AddListener(OnPlayClick);
        leaderBoardBtn.onClick.AddListener(leaderBoardDisplayer.ShowLeaderBoard);
    }
    public void ShowMenu()
    {
        gameName.transform.localScale = Vector3.zero;
        gameName.SetActive(true);
        gameName.transform.DOScale(Vector3.one, 0.2f);
        
        menuPanel.transform.localScale = Vector3.zero;
        menuPanel.SetActive(true);
        menuPanel.transform.DOScale(Vector3.one, 0.2f);
        
        foreach (var hud in gameHud)
        {
            hud.SetActive(false);
        }

    }
    private void OnPlayClick()
    {
        GameManager.Instance.StartGame();
    }
    public void HideAll()
    {
        leaderBoardDisplayer.CloseLeaderBoard();
        menuPanel.SetActive(false);
        gameName.SetActive(false);
        foreach (var hud in gameHud)
        {
            hud.SetActive(false);
        }
    }
    public void ShowHUD()
    {
        menuPanel.SetActive(false);
        gameName.SetActive(false);
        foreach (var hud in gameHud)
        {
            hud.SetActive(true);
        }
    }

}