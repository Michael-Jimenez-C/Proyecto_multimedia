using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    public static PlayerSingleton Instance;
    public Scene currentScene;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Instance.currentScene = SceneManager.GetActiveScene();
    }
    public void GoToScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
