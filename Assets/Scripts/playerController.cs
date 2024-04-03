using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jugador : MonoBehaviour
{
	public CharacterController playerCharacterController;
	KeyBindings keyBidings;
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
		keyBidings = GameObject.Find("GameManager").GetComponent<KeyBindings>();
	}
	void Update()
	{
		//Movimiento Horizontal
		float movementAxisX = 0f;
		float movementAxisZ = 0f;
		if (Input.GetKey(keyBidings.forward))
		{
			movementAxisZ = 1f;
		}
		if (Input.GetKey(keyBidings.backward))
		{
			movementAxisZ = -1f;
		}
		if (Input.GetKey(keyBidings.right))
		{
			movementAxisX = 1f;
		}
		if (Input.GetKey(keyBidings.left))
		{
			movementAxisX = -1f;
		}
		if (Input.GetKey(keyBidings.run) && movementAxisZ > 0)
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
			if (Input.GetKey(keyBidings.jump))
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
		Physics.SyncTransforms();
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
				if (groundHit.transform.tag == "Floor")
				{
					grounded = true;
				}
				else
				{
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
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.parent.CompareTag("Goal"))
		{
			Destroy(other.transform.parent.gameObject);
			gravityMultiplier = 0f;
			transform.position = transform.position+Vector3.up*20f;
			playerVelocity = Vector3.zero;
			Vector2Int oldSize = GameObject.Find("level_generator(Clone)").GetComponent<levelGenerator>().mazeSize;
			int oldLayers = GameObject.Find("level_generator(Clone)").GetComponent<levelGenerator>().numberLayers;
			GameObject.Find("GameManager").GetComponent<SceneLoader>().LoadMaze(new Vector2Int(oldSize.x+1, oldSize.y+1), oldLayers+1);
		}
	}
	public void Teleport(Vector3 position){
		transform.position = position;
	}
}
