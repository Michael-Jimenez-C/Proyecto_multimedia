using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuActions : MonoBehaviour
{
    GameObject player;

    SceneLoader sceneLoader;
    void Start()
    {
        GameObject.Find("GameManager").GetComponent<SceneLoader>().loadingScreen.SetActive(true);
        player = GameObject.FindWithTag("Player");
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart(){
        yield return new WaitForSeconds(0.2f);
        //Set player settings for title screen
        player.GetComponent<Jugador>().gravityMultiplier = 0;
        player.GetComponent<Jugador>().grounded = true;
        player.GetComponent<playerUIController>().HideHUD();
        player.GetComponent<playerUIController>().HideMenu();
        //Shows cursor and hides settings menu
        player.GetComponentInChildren<Camera>().GetComponent<Camara>().ShowCursor();
        GameObject.Find("SettingsMenu")?.SetActive(false);
        //Scene Loader Instance
        sceneLoader = GameObject.Find("GameManager").GetComponent<SceneLoader>();
        //Ends Loading Screen
        GameObject.Find("GameManager").GetComponent<SceneLoader>().loadingScreen.SetActive(false);
    }

    public void NewGame()
    {
        sceneLoader.LoadMaze(new Vector2Int(4,4),1);
        player.GetComponentInChildren<Camera>().GetComponent<Camara>().HideCursor();
        player.GetComponent<playerUIController>().ShowHUD();
        player.GetComponent<playerUIController>().HideMenu();
    }

    public void ViewSettings()
    {
        player.GetComponent<playerUIController>().ShowSettings();
    }

    public void CloseGame()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit(0);
        }
    }
}
