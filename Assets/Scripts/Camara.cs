using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    
    public float sensitivityx = 50;
    public float sensitivityy = 120;
    public float xRotacion;
    public Camera playerCamera;
    private void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")*sensitivityx;
        float mouseY = Input.GetAxis("Mouse Y")*sensitivityy;

        xRotacion -= mouseY * Time.deltaTime;
        xRotacion = Mathf.Clamp(xRotacion, -90, 90);
        
        transform.Rotate(Vector3.up * mouseX * Time.deltaTime);
        playerCamera.transform.localEulerAngles = Vector3.right * xRotacion;
    }
}
