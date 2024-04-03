using UnityEngine;
using UnityEngine.UI;
using System;

public class playerUIController : MonoBehaviour
{
    public GameObject HUD;
    public GameObject InGameMenu;
    public GameObject SettingsMenu;
    PlayerSingleton playerInstance;
    Camara playerCamara;

    public void Start()
    {
        HUD = GameObject.Find("HUD");
        InGameMenu = GameObject.Find("InGameMenu");
        SettingsMenu = GameObject.Find("SettingsMenu");
        playerCamara = GetComponentInChildren<Camara>();
        playerInstance = GetComponent<PlayerSingleton>();
    }
    public void HideHUD()
    {
        HUD.SetActive(false);
    }
    public void ShowHUD()
    {
        playerCamara.HideCursor();
        HUD.SetActive(true);
    }
    public void HideMenu()
    {
        GameObject.Find("GameManager").GetComponent<SceneLoader>().paused = false;
        SettingsMenu.SetActive(false);
        InGameMenu.SetActive(false);
    }
    public void ShowMenu()
    {
        GameObject.Find("GameManager").GetComponent<SceneLoader>().paused = true;
        playerCamara.ShowCursor();
        InGameMenu.SetActive(true);
    }
    public void ShowSettings()
    {
        if (SettingsMenu.activeSelf)
        {
            SettingsMenu.SetActive(false);
        }
        else
        {
            SettingsMenu.SetActive(true);
        }
    }
    public void HideSettings()
    {
        SettingsMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (playerInstance.currentScene.name != "TitleScreen")
            {
                if (InGameMenu.activeSelf)
                {
                    HideMenu();
                    HideSettings();
                    ShowHUD();
                }
                else
                {
                    ShowMenu();
                    HideHUD();
                }
            }
            else
            {
                HideSettings();
            }
        }
        if (HUD.activeSelf)
        {
            GameObject levelGenerator = GameObject.Find("level_generator(Clone)");
            TimeSpan currentTime = levelGenerator ? TimeSpan.FromSeconds(levelGenerator.GetComponent<levelGenerator>().currentLevelTime) : TimeSpan.FromSeconds(0);
            HUD.transform.Find("tiempo").GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", currentTime.Hours, currentTime.Minutes, currentTime.Seconds);
        }
    }
}
