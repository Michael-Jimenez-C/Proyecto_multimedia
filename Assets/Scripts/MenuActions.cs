using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    Jugador player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Jugador>();
        player.gravityMultiplier = 0;
        player.GetComponentInChildren<Camera>().GetComponent<Camara>().ShowCursor();
        GameObject.Find("SettingsMenu")?.SetActive(false);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("SettingsMenu")?.SetActive(false);
        }
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

    public void NewGame(){
        SceneManager.LoadScene("Maze3x3");
    }
}
