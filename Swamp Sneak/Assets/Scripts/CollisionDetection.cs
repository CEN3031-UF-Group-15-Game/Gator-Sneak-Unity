using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour {

	public Text collisionText;
	public float textTime;

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Enemy") {
			// NOTE(anthony): Put game over routine in here later
			SetCollisionText (gameObject.name + " collided with " + col.collider.name + "\n" + "Game Over!");
		}
		else {
			SetCollisionText (gameObject.name + " collided with " + col.collider.name);	
		}
		StartCoroutine (WaitForText (textTime));
	}

	void OnCollisionExit(Collision col) {
		SetCollisionText (gameObject.name + " is no longer colliding with " + col.collider.name);
		StartCoroutine ( WaitForText(textTime));
	}

	void OnCollisionStay(Collision col) {
		// Do stuff while in a collision
	}

	void SetCollisionText(string s) {
		collisionText.text = s;
	}

	IEnumerator WaitForText(float time) {
		yield return new WaitForSeconds (time);
		SetCollisionText ("");
	}
}