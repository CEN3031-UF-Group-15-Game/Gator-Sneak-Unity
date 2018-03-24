using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MoveToPlayer : MonoBehaviour {

	public Transform playerTransform;	// The player character that the enemy will be chasing
	private Vector3 previousPosition;	// Player character's position when last seen
	private Vector3 goalPosition;		// Enemy will move towards this position
	private float lastTimeOfUpdate;		// The last time the player character's position was updated, measured in seconds since the start of the game
	private float timeInterval;			// The minimum time in seconds required to elapse before the goalPosition will update
	private NavMeshAgent enemy;			// The character that is moving via this script
	public bool playerIsSeen;			// Player is seen by this enemy
	private bool collidedWithPlayer;	// This enemy has collided with the player. Used when we want the enemy to stop chasing the player after they've collided once.

	void Start() {
		lastTimeOfUpdate = 0;
		timeInterval = 0.5F;
		enemy = GetComponent<NavMeshAgent> ();
		playerIsSeen = false;
		collidedWithPlayer = false;
	}

	bool canUpdatePosition() {
		bool sufficientInterval = (Time.time - lastTimeOfUpdate) >= timeInterval;
		// If a sufficent time interval has passed and the enemy has not yet collided with the player...
		return sufficientInterval && !collidedWithPlayer;
	}

	void OnCollisionEnter(Collision collision) {
		// If the object being collided with is the player character...
		if (collision.gameObject.GetInstanceID() == playerTransform.GetInstanceID()) {
			collidedWithPlayer = true;
		}
	}

	void Update() {
		if (canUpdatePosition() && playerIsSeen) {
			enemy.destination = playerTransform.position;
		}

	}
}