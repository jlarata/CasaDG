using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour

{
	public float mouseSensitivity = 100.0f;
	public float clampAngle = 80.0f;
	public float speed;
	public GameObject player;
	public CharacterController controller;

	private float rotY = 0.0f; // rotation around the up/y axis
	private float rotX = 0.0f; // rotation around the right/x axis


	void Start()
	{
    	
	//Screen.lockCursor = true;
	Cursor.lockState = CursorLockMode.Locked;


    	Vector3 rot = transform.localRotation.eulerAngles;
    	rotY = rot.y;
    	rotX = rot.x;
	speed = 100f;

	player = this.transform.parent.gameObject; // Getting the parent object.
	controller = player.GetComponent<CharacterController>(); // You can
	}
	//
	void Update()
	{
    	float mouseX = Input.GetAxis("Mouse X");
    	float mouseY = -Input.GetAxis("Mouse Y");

	rotY += mouseX * mouseSensitivity * Time.deltaTime;
    	rotX += mouseY * mouseSensitivity * Time.deltaTime;

	rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

	Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
    	transform.rotation = localRotation;
    	transform.parent.transform.rotation = Quaternion.Euler(0.0f, rotY, 0.0f);

	float horizontal = Input.GetAxisRaw("Horizontal");
    	float vertical = Input.GetAxisRaw("Vertical");

	Vector3 Direction = (player.transform.forward * vertical + player.transform.right * horizontal).normalized;

	controller.Move(Direction * speed * Time.deltaTime);
	
	if(Input.GetKey(KeyCode.E))
	{
	player.transform.Translate(Vector3.up * speed * Time.deltaTime);
	}
	if(Input.GetKey(KeyCode.Q))
	{
	player.transform.Translate(Vector3.down * speed * Time.deltaTime);
	}

	}
}

//cada piso tiene 8 de altura y 40 de ancho