using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    GameObject player;
    //Obtiene el prefab del nodo
    public maze_generator generatorPrefab;
    //Prefab de la meta
    public maze_goal mazeGoal;
    //Tamaño
    public Vector2Int mazeSize;
    //Capas
    public int numberLayers;
    List<maze_generator> layers = new List<maze_generator>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < numberLayers; i++)
        {
            StartCoroutine(CreateLayer(i , mazeSize));
        }
    }

    IEnumerator CreateLayer(int yIndex, Vector2Int layerSize)
    {
        //Crea un generador de laberinto
        Vector3 generatorPosition = new Vector3(0, yIndex*4, 0);
        maze_generator layer = Instantiate(generatorPrefab, generatorPosition, Quaternion.identity);
        layers.Add(layer);
        layer.mazeSize = layerSize;
        //Espera a que termine de generarse
        while (!layer.MazeGenerationCompleted)
        {
            yield return null;
        }
        //Spawnea al jugador
        if (yIndex == numberLayers-1)
        {
            player.transform.position = layer.GetRandomNode().transform.position;
            StartCoroutine(removeLayer());
        }
        //Elimina el suelo de la meta para pasar al ultimo
        if (yIndex > 0)
        {
            layer.goalNode.RemoveFloor();
        }
        else
        {
            maze_goal goal = Instantiate(mazeGoal, layer.goalNode.transform.position, Quaternion.identity);
        }
    }
    IEnumerator removeLayer()
    {
        while (player.transform.position.y > -2)
        {
            for (int i = 0; i < numberLayers; i++)
            {
                bool under = player.transform.position.y < (i * 4) - 1;
                if (under)
                {
                    if(layers[i]){
                        Destroy(layers[i].gameObject);
                    }
                    layers[i]=null;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
