using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonscenechange : MonoBehaviour {

	public void MainGameChange()
	{
		SceneManager.LoadScene("Start");
	}
	public void HighScoreGameChange()
	{
        SceneManager.LoadScene("highscore");
	}
	public void Level1GameChange()
	{
        SceneManager.LoadScene("Scenes/DEMO SCENE");
	}
}
