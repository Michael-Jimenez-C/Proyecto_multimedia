using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject generator;
    public GameObject loadingScreen;
    public bool paused = false;
    public Text nombre_jugador;
    void Start(){
        loadingScreen = transform.Find("LoadingScreen").GameObject();
	nombre_jugador.text=PlayerPrefs.GetString("PlayerName");
    }
    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadMaze(Vector2Int size, int layers)
    {
        //loadingScreen.SetActive(true);
        SceneManager.LoadScene("Maze");
        StartCoroutine(mazeLoadRoutine(size, layers));
    }
    IEnumerator mazeLoadRoutine(Vector2Int size, int layers)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject currentGenerator = Instantiate(generator, new Vector3(0, 0, 0), Quaternion.identity);
        currentGenerator.GetComponent<levelGenerator>().mazeSize = size;
        currentGenerator.GetComponent<levelGenerator>().numberLayers = layers;
        currentGenerator.GetComponent<levelGenerator>().StartLevel();
        //loadingScreen.SetActive(false);
    }
}
