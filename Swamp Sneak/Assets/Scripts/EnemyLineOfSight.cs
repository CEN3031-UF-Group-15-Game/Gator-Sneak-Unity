using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineOfSight : MonoBehaviour {

	public Transform playerTransform;
	public float degreeOfSight;
	float roty = 20;

	bool PlayerIsSeenByEnemy() {
		Vector3 playerDir = playerTransform.position - transform.position;
		float angle = Vector3.Angle (playerDir, transform.forward);

		if (angle <= degreeOfSight) {
			return true;
		}
		else {
			return false;
		}
	}

	void Update() {
		if(PlayerIsSeenByEnemy()) {
			Debug.Log ("Seen!");
		}
		else {
			Debug.Log ("Not seen.");
		}
		if (Time.deltaTime%30<=15)
			transform.Rotate (0, roty * Time.deltaTime, 0);
		else
			transform.Rotate (0, -roty * Time.deltaTime, 0);
	
	}
}
