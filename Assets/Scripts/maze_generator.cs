using System.Collections.Generic;
using UnityEngine;
 
public class maze_generator : MonoBehaviour
{
    //Obtiene el prefab del nodo
    [SerializeField] maze_node nodePrefab;
    //Tamano del laberinto
    public Vector2Int mazeSize;
    //Nodo meta
    public maze_node goalNode;
    //Evento para detecctar cuando el laberinto se ha generado
    public bool MazeGenerationCompleted = false;
    private List<maze_node> nodes;

    private void Start()
    {
        //Generacion del laberinto
        GenerateMaze(mazeSize);
    }

    void GenerateMaze(Vector2Int size)
    {
        //Lista de nodos
        nodes = new List<maze_node>();
        //Genera una matriz de nodos de tamano nxn
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                //Posicion del nodo
                Vector3 nodePos = new Vector3( 4*x - (size.x / 2f), transform.position.y, 4*y - (size.y / 2f));
                //Instancia del nodo
                maze_node newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                //Agrega el nodo a la lista
                nodes.Add(newNode);
            }
        }

        //Lista de nodos que componen el camino actual
        List<maze_node> currentPath = new List<maze_node>();
        //Lista de nodos ya visitados
        List<maze_node> completedNodes = new List<maze_node>();
 
        //El camino empieza desde un nodo al azar
        currentPath.Add(nodes[Random.Range(0, nodes.Count)]);
        goalNode = currentPath[0];
 
        //Mientras todos los nodos no hayan sido visitados sigue generando camino:
        while (completedNodes.Count < nodes.Count)
        {

            List<int> posibleNextNodes = new List<int>();
            List<int> posibleDirections = new List<int>();
            
            //Obtiene el nodo actual y sus coordenadas
            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;
            
            //Valida que haya un nodo a la derecha
            if (currentNodeX < size.x - 1)
            {
                //Si dicho nodo no ha sidovisitaddo y no hace parte del camino actual lo agrega como posible
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) &&
                !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    posibleDirections.Add(1);
                    posibleNextNodes.Add(currentNodeIndex + size.y);
                }
 
            }

            //Validacion nodo a la izquierda
            if (currentNodeX > 0)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    posibleDirections.Add(2);
                    posibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            
            //Validacion nodo por encima
            if (currentNodeY < size.y - 1)
            {
                if(!completedNodes.Contains(nodes[currentNodeIndex + 1]) &&
                !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    posibleDirections.Add(3);
                    posibleNextNodes.Add(currentNodeIndex + 1);
                }
            }

            //Validacion nodo por debajo
            if (currentNodeY > 0)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex-1]) &&
                !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    posibleDirections.Add(4);
                    posibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            //Elige proxima direccion
            if (posibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, posibleDirections.Count);
                maze_node chosenNode = nodes[posibleNextNodes[chosenDirection]];
                
                //Elimina los muros entre los nodos actual y siguiente
                //Direcciones: 1=derecha, 2=izquierda, 3=arriba, 4=abajo
                //Muros: 0=derecho, 1=izquierdo, 2=superior, 3=inferior
                switch(posibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }
                //Agrega el nodo elgido al camino actual
                currentPath.Add(chosenNode);
            }
            //Si no quedan caminos posibles retrocede
            else
            {
                completedNodes.Add(currentPath[currentPath.Count-1]);
                currentPath.RemoveAt(currentPath.Count-1);
            }
        }
        MazeGenerationCompleted = true;
    }
    public maze_node GetRandomNode (){
        return nodes[Random.Range(0, nodes.Count)];
    }
}
