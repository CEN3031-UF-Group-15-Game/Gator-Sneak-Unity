using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseClcik : MonoBehaviour {
	//pause menu will show on p click
	// Use this for initialization
	CanvasGroup PauseMenu;
	public bool pause;

	void Start()
	{
		PauseMenu = GetComponent<CanvasGroup>();
		pause = false;
		PauseMenu.alpha = 0;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("p")){
			if (pause)
			{
				pause = false;
			}
			else
			{
				pause = true;
			}
		}
		if (pause)
		{
			PauseMenu.alpha = 1;
		}
		else
		{
			PauseMenu.alpha = 0;
		}
	}
}
