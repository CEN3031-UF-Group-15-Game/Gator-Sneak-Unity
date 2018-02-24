using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {
	public bool _AutoRotate = false;
	public float _X = 0f;
	public float _Y = 0f;
	public float _Z = 0f;


	// Update is called once per frame
	void LateUpdate () {
		transform.Rotate (_X,_Y,_Z);
	}
}
