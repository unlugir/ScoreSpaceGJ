using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; }
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject hud;

    [SerializeField] Button playBtn;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        HideAll();
        playBtn.onClick.AddListener(OnPlayClick);
    }
    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        hud.SetActive(false);
    }
    public void HideAll() 
    {
        menuPanel.SetActive(false);
        hud.SetActive(false);
    }
    public void ShowHUD()
    {
        menuPanel.SetActive(false);
        hud.SetActive(true);
    }
    private void OnPlayClick()
    {
        GameManager.Instance.StartGame();
    }

}
