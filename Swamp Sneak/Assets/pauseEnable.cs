using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseEnable : MonoBehaviour {

	bool pause;

	// Use this for initialization
	void Start () {
		pause = false;
		gameObject.GetComponent<Button>().interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
		pause = GameObject.Find("PauseMenu").GetComponent<PauseClcik>().pause;
		if (pause)
		{
			gameObject.GetComponent<Button>().interactable = true;
		}
		else
		{
			gameObject.GetComponent<Button>().interactable = false;
		}
	}
}
