using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Transform player;
    public Transform vista;
	public GameObject proyectil;
	public GameObject disparador;
	public float tiempo_disparo=2f;
	public float tiempo_espera;


    void Start()
    {
    }

    void Update()
    {
        camara();
        disparo();
    }
    
    public void disparo(){
        tiempo_espera-=Time.deltaTime;
		if(tiempo_espera<1){
			GenerarBala();
			tiempo_espera=tiempo_disparo;
		}       
    }

    public void camara(){
        Vector3 direction = player.position - transform.position;
        direction.y=0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
	public void GenerarBala(){
		GameObject proyectil_inst = Instantiate(proyectil,disparador.transform.position,disparador.transform.rotation);
		proyectil_inst.GetComponent<Bala>().dmg=20;
		proyectil_inst.GetComponent<Rigidbody>().AddForce(disparador.transform.forward * 800);
	}
}
