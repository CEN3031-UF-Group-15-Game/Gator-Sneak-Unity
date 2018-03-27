using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonscenechange : MonoBehaviour {

	public void MainGameChange()
	{
		Application.LoadLevel("Start");
	}
	public void HighScoreGameChange()
	{
		Application.LoadLevel("highscore");
	}
	public void Level1GameChange()
	{
		Application.LoadLevel("Scenes/DEMO SCENE");
	}
}
