using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; }
    [SerializeField] GameObject menuPanel;
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
        menuPanel.SetActive(true);
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
        menuPanel.SetActive(false);
        foreach (var hud in gameHud)
        {
            hud.SetActive(false);
        }
    }
    public void ShowHUD()
    {
        menuPanel.SetActive(false);
        foreach (var hud in gameHud)
        {
            hud.SetActive(true);
        }
    }

}