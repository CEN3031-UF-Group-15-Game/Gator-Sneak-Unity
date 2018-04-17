using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyLineOfSight : MonoBehaviour {

	private Transform playerTransform;
	private GameObject playerGameObject;
	private float degreeOfSight;
	private float sightDistance;
	private float stealthCheckDelay = 0.5F; // Wait before running another stealth check
	private float lastStealthCheck;
	private MoveToPlayer moveToPlayerComponent; 
	/** 
		Variable for enemy movement component (defined in MoveToPlayer.cs), so we can 
		access the variable bool playerIsSeen. I update it here so we don't need to perform the 
		line of sight calculations more than necessary.
	*/

		
	void Awake() {
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		playerGameObject = GameObject.FindGameObjectWithTag ("Player");
		moveToPlayerComponent = gameObject.GetComponent<MoveToPlayer> ();
		lastStealthCheck = 0;
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
					if(hitPositive.collider.gameObject.name == playerTransform.GetComponentInChildren<Collider>().gameObject.name) {
						if (stealthCheck()) {
							return true;
						}
					}
				}

				if(Physics.Raycast(transform.position, vecNegative, out hitNegative, sightDistance)) {
					if(hitNegative.collider.gameObject.name == playerTransform.GetComponentInChildren<Collider>().gameObject.name) {
						if (stealthCheck()) {
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	void FixedUpdate() {
		if(PlayerIsSeenByEnemy()) {
			moveToPlayerComponent.SetPlayerIsSeen(true);	
			//Debug.Log ("Seen!");
		}
		else {
			moveToPlayerComponent.SetPlayerIsSeen(false);
			//Debug.Log ("Not seen.");
		}
	}

	public void SetDegreeOfSight(int dos) {
		degreeOfSight = dos;
	}
	public void SetSightDistance(int sd) {
		sightDistance = sd;
	}

	//Function to check if player is stealthed and also handle RNG factor
	bool stealthCheck()
	{
		if (playerGameObject == null) {
			playerGameObject = GameObject.FindGameObjectWithTag ("Player");
		}

		// If player is not crouching...
		if(playerGameObject.GetComponent<ThirdPersonCharacter>().getStealth() == false)
		{
			return true;
		}

		// If enough time has passed, do a stealth check
		if ((Time.time - lastStealthCheck) >= stealthCheckDelay) {
			lastStealthCheck = Time.time;
			int chance = Random.Range(1,9);
			int playerStealthStat = playerGameObject.GetComponent<Player> ().getStealthStat ();

			if (playerStealthStat < chance) {
				return true;
			} 
			else {
				return false;
			}
		}

		return false;
	}
}
