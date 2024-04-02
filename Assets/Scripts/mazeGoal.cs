using UnityEngine;

public class MazeGoal : MonoBehaviour
{
    float rotationSpeed = 50f;
    void Update()
    {
        transform.GetChild(0).Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
