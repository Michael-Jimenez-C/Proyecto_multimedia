using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    Rigidbody rb;
    public float walkSpeed = 6f;
    public float runSpeed = 8f;
    public float currentSpeed = 0f;
    public float jumpForce = 0.9f;
    private bool grounded = true;
    private float velocidadVertical = 0f;
    public float gravedad = 1.24f;
    public bool climbing = false;

    public CharacterController playerControl;
    
    
    void Start()
    {
	rb = GetComponent<Rigidbody>();
	playerControl = GetComponent<CharacterController>();
	Cursor.lockState = CursorLockMode.Locked;
    }
	
    void Mover(){
	currentSpeed = walkSpeed;
    	float x = Input.GetAxis("Horizontal");
	float z = Input.GetAxis("Vertical");

	Vector3 move = transform.right * x + transform.forward * z + transform.up * velocidadVertical;
	if (Input.GetButton("Fire3")){
		currentSpeed = runSpeed;
	}

	playerControl.Move(move * currentSpeed * Time.deltaTime);
    }
	
    void Escalar(){
    	currentSpeed = walkSpeed*.6f;
	float z = Input.GetAxis("Vertical");

	Vector3 move = transform.up * z;
	playerControl.Move(move * currentSpeed * Time.deltaTime);
    }
    void Jump(){
	    if (grounded && Input.GetButtonDown("Jump")){
		grounded = false;
	    	velocidadVertical = jumpForce;
	    }
	    velocidadVertical -= gravedad * Time.deltaTime;
	    if (velocidadVertical <=-2f){
	    	velocidadVertical = -2f;
	    }
    }

    void OnTriggerEnter(Collider collider){
	if(collider.CompareTag("Floor") && !grounded){
		grounded = true;
		climbing = false;
	}else if(collider.CompareTag("Trampoline")){
		grounded = false;
		velocidadVertical = jumpForce*2;
	}
	
	Debug.Log("a");
	if(collider.CompareTag("MuroEscalable") && Input.GetButtonDown("Fire3")){
		Debug.Log("b");
		climbing = true;
	}

    }
    
    void Update(){
	    if (!climbing){
		Mover();
		Jump();
	    }else{
	    	Escalar();
	    }
    }
}
