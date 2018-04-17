using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autocontrol : MonoBehaviour {
	
	float normalSpeed;
	bool controlEnable;
	Vector3 direction;
	Vector3 defaultRelativeCameraPosition;
	Quaternion defaultRelativeCameraRotation;

	// Use this for initialization
	void Start() {
		normalSpeed = 10;
		controlEnable = false;
		defaultRelativeCameraPosition = Camera.main.transform.localPosition;
		defaultRelativeCameraRotation = Camera.main.transform.localRotation;
	}

	// Update is called once per frame
	void Update() {
		// Disable camera control and reset camera to default position (relative to player)
		if (controlEnable && Input.GetKeyDown(KeyCode.X))
		{
			Camera.main.transform.localRotation = defaultRelativeCameraRotation;
			Camera.main.transform.localPosition = defaultRelativeCameraPosition;
			controlEnable = false;
		}
		// Enable camera control
		if (!controlEnable && Input.GetKeyDown(KeyCode.X))
		{
			controlEnable = true;
		}
		// Move camera forward
		if (controlEnable && Input.GetKey(KeyCode.T))
		{
			Camera.main.transform.position += Vector3.forward * Time.deltaTime * normalSpeed;
		}
		// Move camera backwards
		else if (controlEnable && Input.GetKey(KeyCode.G))
		{
			Camera.main.transform.position += Vector3.back * Time.deltaTime * normalSpeed;
		}
		// Move camera left
		else if (controlEnable && Input.GetKey(KeyCode.F))
		{
			Camera.main.transform.position += Vector3.left * Time.deltaTime * normalSpeed;
		}
		// Move camera right
		else if (controlEnable && Input.GetKey(KeyCode.H))
		{
			Camera.main.transform.position += Vector3.right * Time.deltaTime * normalSpeed;
		}
		// Rotate camera left
		if (controlEnable && Input.GetKey(KeyCode.R))
		{
			Camera.main.transform.Rotate(0, Time.deltaTime * -20, 0);
		}
		// Rotate camera right
		else if (controlEnable && Input.GetKey(KeyCode.Y))
		{
			Camera.main.transform.Rotate(0, Time.deltaTime * 20, 0);
		}
	}
}
