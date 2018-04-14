using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autocontrol : MonoBehaviour {
	Vector3 cameraPosition;
	Quaternion cameraRotation;
	//GameObject player;
	//for freezing player when successful
	float normalSpeed;
	bool controlEnable;
	Vector3 direction;
	// Use this for initialization
	void Start() {
		normalSpeed = 10;
		controlEnable = false;
		//	player = GameObject.Find("ThirdPersonController");
	}

	// Update is called once per frame
	void Update() {
		if (controlEnable && Input.GetKeyDown(KeyCode.X))
		{
			Camera.main.transform.rotation = cameraRotation;
			Camera.main.transform.position = cameraPosition;
			controlEnable = false;
		}
		if (!controlEnable && Input.GetKeyDown(KeyCode.X))
		{
			controlEnable = true;
			cameraPosition = Camera.main.transform.position;
			cameraRotation = Camera.main.transform.rotation;
		}
		if (controlEnable && Input.GetKey(KeyCode.T))
		{
			Camera.main.transform.position += Vector3.forward * Time.deltaTime * normalSpeed;
		}
		else if (controlEnable && Input.GetKey(KeyCode.G))
		{
			Camera.main.transform.position += Vector3.back * Time.deltaTime * normalSpeed;
		}
		else if (controlEnable && Input.GetKey(KeyCode.F))
		{
			Camera.main.transform.position += Vector3.left * Time.deltaTime * normalSpeed;
		}
		else if (controlEnable && Input.GetKey(KeyCode.H))
		{
			Camera.main.transform.position += Vector3.right * Time.deltaTime * normalSpeed;
		}

		if (controlEnable && Input.GetKey(KeyCode.R))
		{
			Camera.main.transform.Rotate(0, Time.deltaTime * 20, 0);
		}
		else if (controlEnable && Input.GetKey(KeyCode.Y))
		{
			Camera.main.transform.Rotate(0, Time.deltaTime * -20, 0);
		}
	}
}
