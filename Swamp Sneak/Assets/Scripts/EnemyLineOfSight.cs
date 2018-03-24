using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineOfSight : MonoBehaviour {

	public Transform playerTransform;
	public float degreeOfSight;
	public float sightDistance;
	private MoveToPlayer moveToPlayerComponent; 	// Variable for enemy movement component (defined in MoveToPlayer.cs), so we can 
													// access the variable bool playerIsSeen. I update it here so we don't need to perform the 
													// line of sight calculations more than necessary.

	void Start() {
		moveToPlayerComponent = GetComponent<MoveToPlayer> ();
	}

	bool PlayerIsSeenByEnemy() {
		
		Vector3 playerDir = (playerTransform.transform.position - transform.position).normalized;
		float dot = Vector3.Dot (playerDir, transform.forward);
		if(dot > Mathf.Abs(0.5F)) {

			for(int i = 0; i <= degreeOfSight; i += 5) {
				RaycastHit hitPositive;
				RaycastHit hitNegative;
				Quaternion anglePositive = Quaternion.AngleAxis (i, new Vector3 (0, 1, 0));
				Quaternion angleNegative = Quaternion.AngleAxis (-i, new Vector3 (0, 1, 0));
				Vector3 vecPositive = anglePositive * transform.forward;
				Vector3 vecNegative = angleNegative * transform.forward;

				if(Physics.Raycast(transform.position, vecPositive, out hitPositive, sightDistance)) {
					if(hitPositive.collider.gameObject.name == "ThirdPersonController") {
						return true;
					}
				}

				if(Physics.Raycast(transform.position, vecNegative, out hitNegative, sightDistance)) {
					if(hitNegative.collider.gameObject.name == "ThirdPersonController") {
						return true;
					}
				}
			}
		}
		return false;
	}

	void FixedUpdate() {
		if(PlayerIsSeenByEnemy()) {
			moveToPlayerComponent.playerIsSeen = true;	
			Debug.Log ("Seen!");
		}
		else {
			moveToPlayerComponent.playerIsSeen = false;
			Debug.Log ("Not seen.");
		}


	}
}
