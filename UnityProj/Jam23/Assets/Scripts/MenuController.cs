using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject hud;


    [SerializeField] Button playBtn;
    void Start()
    {
        menuPanel.SetActive(true);
        hud.SetActive(false);
        playBtn.onClick.AddListener(OnPlayClick);
    }

    private void OnPlayClick()
    {
        menuPanel.SetActive(false);
        hud.SetActive(true);
        GameManager.Instance.StartGame();
    }

}
