using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jugador : MonoBehaviour
{
	public CharacterController playerCharacterController;
	float groundOffset = 0.1f;
	float baseSpeed = 6f;
	public float runSpeedMultiplier = 1.8f;
	float baseJumpSpeed = 10f;
	public float jumpSpeedMultiplier = 1f;
	public float gravityMultiplier = 1f;
	public bool grounded = false;

	Vector3 playerVelocity;
	void Start()
	{
		playerCharacterController = GetComponent<CharacterController>();
		StartCoroutine(GroundDetection());
		playerVelocity = Vector3.zero;
	}
	void Update()
	{
		//Movimiento Horizontal
		float movementAxisX = Input.GetAxis("Horizontal");
		float movementAxisZ = Input.GetAxis("Vertical");
		if (Input.GetKey(KeyCode.LeftShift) && movementAxisZ > 0)
		{
			movementAxisZ *= runSpeedMultiplier;
		}
		float vSpeed = playerVelocity.y;
		playerVelocity = transform.right * movementAxisX + transform.forward * movementAxisZ;
		playerVelocity *= baseSpeed;
		playerVelocity.y = vSpeed;

		//Movimiento Vertical
		if (grounded)
		{
			if (Input.GetKey(KeyCode.Space))
			{
				transform.position += Vector3.up * (groundOffset + 0.1f);
				grounded = false;
				playerVelocity.y = baseJumpSpeed * jumpSpeedMultiplier;
			}
			else
			{
				playerVelocity.y = 0;
			}
		}
		else
		{
			playerVelocity += Physics.gravity * gravityMultiplier * 0.05f;
		}

		//Aplica movimiento
		playerCharacterController.Move(playerVelocity * Time.deltaTime);
	}
	IEnumerator GroundDetection()
	{
		while (true)
		{
			Vector3 footPosition = transform.position - Vector3.up * (playerCharacterController.height / 2);
			RaycastHit groundHit;
			if (Physics.Raycast(footPosition, -Vector3.up, out groundHit, groundOffset))
			{
				if (groundHit.transform.tag == "Floor"){
					grounded = true;
				} else{
					//Debug.Log(groundHit.transform.tag);
				}
			}
			else
			{
				grounded = false;
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
}
