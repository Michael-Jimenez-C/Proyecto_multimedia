using UnityEngine;
using UnityEngine.SceneManagement;

public class menuActions : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<Jugador>().gravityMultiplier = 0;
        player.GetComponent<playerUIController>().HideHUD();
        player.GetComponent<playerUIController>().HideMenu();
        player.GetComponentInChildren<Camera>().GetComponent<Camara>().ShowCursor();
        GameObject.Find("SettingsMenu")?.SetActive(false);

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

    public void NewGame()
    {
        SceneManager.LoadScene("Maze3x3");
        player.GetComponent<Jugador>().gravityMultiplier = 1;
        player.GetComponentInChildren<Camera>().GetComponent<Camara>().HideCursor();
        player.GetComponent<playerUIController>().ShowHUD();
    }
    public void ViewSettings()
    {
        player.GetComponent<playerUIController>().ShowSettings();
    }
}
