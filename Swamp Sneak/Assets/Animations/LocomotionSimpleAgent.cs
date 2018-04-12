using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LocomotionSimpleAgent : MonoBehaviour {
	Animator anim;
    NavMeshAgent agent;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;
    Transform parentTransform; // Parent container transform

	// Use this for initialization
	void Start () {
        agent = GetComponentInParent<NavMeshAgent> ();
        parentTransform = transform.parent;
        // Don’t update position automatically
        agent.updatePosition = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (anim == null) anim = GetComponent<Animator> (); // UMA has to create it
		Vector3 worldDeltaPosition = agent.nextPosition - parentTransform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot (parentTransform.right, worldDeltaPosition);
        float dy = Vector3.Dot (parentTransform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2 (dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime/0.15f);
        smoothDeltaPosition = Vector2.Lerp (smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        // Update animation parameters
        //anim.SetBool("move", shouldMove);
        anim.SetFloat ("Speed", velocity.magnitude);
        }

	void OnAnimatorMove() {
        // Update position to agent position
        parentTransform.position = agent.nextPosition;
	}
}
