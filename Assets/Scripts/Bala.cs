using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
	public float dmg;
	
	public void Start(){
		Destroy(gameObject,3f);
	}

	public void OnCollisionEnter(Collision collision){
		
		//if(collision.collider.CompareTag("Player")){
		//}
    }
}
