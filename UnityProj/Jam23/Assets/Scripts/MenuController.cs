using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; }
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject[] gameHud;

    [SerializeField] Button playBtn;
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
        
        playBtn.onClick.AddListener(OnPlayClick);
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
        menuPanel.SetActive(false);
        foreach (var hud in gameHud)
        {
            hud.SetActive(true);
        }
        GameManager.Instance.StartGame();
    }

}
