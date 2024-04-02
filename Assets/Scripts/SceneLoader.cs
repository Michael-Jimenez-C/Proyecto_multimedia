using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadTitleScreen(){
        SceneManager.LoadScene("TitleScreen");
    }
    
    public void LoadMaze(){
        SceneManager.LoadScene("Maze");
    }
}
