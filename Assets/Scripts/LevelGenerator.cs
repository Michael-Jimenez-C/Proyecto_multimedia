using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGenerator : MonoBehaviour
{
    GameObject player;
    //Obtiene el prefab del nodo
    public mazeGenerator generatorPrefab;
    //Prefab de la meta
    public MazeGoal mazeGoal;
    //Prefab del trampolin
    public GameObject trampolin;
    //Prefab Torretas
    public GameObject turret;
    //Tamaño
    public Vector2Int mazeSize;
    //Capas
    public int numberLayers;
    public int currentLevelTime = 0;
    List<mazeGenerator> layers = new List<mazeGenerator>();

    // Start is called before the first frame update
    public void StartLevel()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < numberLayers; i++)
        {
            StartCoroutine(CreateLayer(i, mazeSize));
            StartCoroutine(CurrentTimer());
        }
    }

    IEnumerator CreateLayer(int yIndex, Vector2Int layerSize)
    {
        //Crea un generador de laberinto
        Vector3 generatorPosition = new Vector3(0, yIndex * 4, 0);
        mazeGenerator layer = Instantiate(generatorPrefab, generatorPosition, Quaternion.identity);
        List<Vector3> removedPositions = new();
        layers.Add(layer);
        layer.mazeSize = layerSize;
        //Espera a que termine de generarse
        while (!layer.MazeGenerationCompleted)
        {
            yield return null;
        }
        //Spawnea al jugador
        if (yIndex == numberLayers - 1)
        {
            Vector3 spawnPosition;
            do
            {
                spawnPosition = layer.GetRandomNode().transform.position;
            } while (spawnPosition == layer.goalNode.transform.position);
            spawnPosition += new Vector3(0, 20, 0);
            player.GetComponent<Jugador>().Teleport(spawnPosition);
            player.GetComponent<Jugador>().gravityMultiplier = 1f;
            //StartCoroutine(removeLayer());
        }
        //Elimina el suelo de la meta para pasar a la siguiente capa
        if (yIndex > 0)
        {
            layer.goalNode.RemoveFloor();
            removedPositions.Add(layer.goalNode.transform.position);
            //TODO: comporobar que haya suelo para instanciar y comprobar que no sea la meta
            Instantiate(trampolin, layer.goalNode.transform.position - Vector3.up * 6, Quaternion.identity);
            for (int i = 0; i < (layerSize.x + layerSize.y) / 4; i++)
            {
                mazeNode accesNode = layer.GetRandomNode();
                Vector3 accessPosition = accesNode.transform.position;
                if (!removedPositions.Contains(accessPosition))
                {
                    accesNode.RemoveFloor();
                    Instantiate(trampolin, accesNode.transform.position - Vector3.up * 6, Quaternion.identity);
                }
            }
        }
        else
        {
            MazeGoal goal = Instantiate(mazeGoal, layer.goalNode.transform.position, Quaternion.identity);
        }
        //Spawn Torretas
        for (int i = 0; i < (layerSize.x + layerSize.y) / 4; i++)
        {
            Vector3 turretPosition = layer.GetRandomNode().transform.position;
            if (!removedPositions.Contains(turretPosition) || turretPosition == layer.goalNode.transform.position)
            {
                GameObject turretInstance = Instantiate(turret, turretPosition-Vector3.up * 1f, Quaternion.identity);
                turretInstance.GetComponent<Enemigo>().player = player.transform;
            }
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
                    if (layers[i])
                    {
                        Destroy(layers[i].gameObject);
                    }
                    layers[i] = null;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator CurrentTimer()
    {
        SceneLoader sceneLoader = GameObject.Find("GameManager").GetComponent<SceneLoader>();
        currentLevelTime = 0;
        while (true)
        {
            if (!sceneLoader.paused)
            {
                currentLevelTime += 1;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
