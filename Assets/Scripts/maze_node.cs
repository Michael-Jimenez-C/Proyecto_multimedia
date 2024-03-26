using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Posibbles estados
public enum NodeState {
    Available,
    Current,
    Completed
}
 
public class maze_node : MonoBehaviour
{
    //Lista de muros del nodo
    [SerializeField] GameObject[] walls;
    //Suelo del nodo
    [SerializeField] MeshRenderer floor;
    //Elimina suelo
    public void RemoveFloor(){
        Destroy(floor.gameObject);
    }
    //Elimina muros del nodo
    public void RemoveWall(int wallToRemove)
    {
        Destroy(walls[wallToRemove].gameObject);
    }
    //Cambio de estado del nodo
    public void SetState(NodeState state) {
        switch (state)
        {
            case NodeState.Available:
                floor.material.color = Color.white;
                break;
            case NodeState.Current:
                floor.material.color = Color.yellow;
                break;
            case NodeState.Completed:
                floor.material.color = Color.blue;
                break;
        }
    }
}
