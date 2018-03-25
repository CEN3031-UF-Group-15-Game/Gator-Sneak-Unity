using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MoveToPlayer : MonoBehaviour {

	private Transform playerTransform;	// The player character that the enemy will be chasing
	private float lastTimeOfUpdate;		// The last time the player character's position was updated, measured in seconds since the start of the game
	private float timeInterval;			// The minimum time in seconds required to elapse before the goalPosition will update
	private NavMeshAgent enemy;			// The character that is moving via this script
	private bool playerIsSeen;			// Player is seen by this enemy
	private bool collidedWithPlayer;	// This enemy has collided with the player. Used when we want the enemy to stop chasing the player after they've collided once.

	void Start() {
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		lastTimeOfUpdate = 0;
		timeInterval = 0.1F;
		enemy = GetComponentInParent<NavMeshAgent> ();
		playerIsSeen = false;
		collidedWithPlayer = false;
	}

	bool canUpdatePosition() {
		bool sufficientInterval = ((Time.time - lastTimeOfUpdate) >= timeInterval);
		// If a sufficent time interval has passed and the enemy has not yet collided with the player...
		if (sufficientInterval) {
			lastTimeOfUpdate = Time.time;
		}
		return sufficientInterval && !collidedWithPlayer;
	}

	void OnCollisionEnter(Collision collision) {
		// If the object being collided with is the player character...
		if (collision.gameObject.GetInstanceID() == playerTransform.GetComponent<Collider>().gameObject.GetInstanceID()) {
			collidedWithPlayer = true;
		}
	}

	void Update() {
		if (canUpdatePosition() && playerIsSeen) {
			// Set the destination to the same position as the game object that the collider is attached to (usually the thing that is changing position!)
			enemy.SetDestination (playerTransform.GetComponentInChildren<Collider>().gameObject.GetComponent<Transform>().position);
		}

	}

	public void SetPlayerIsSeen(bool seen) {
		playerIsSeen = seen;
	}
}