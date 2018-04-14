using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class buttonscenechange : MonoBehaviour {

	private GameControl control;

	void Awake() 
	{
		// Get a reference to the GameControl object
		control = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
	}

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

	public void NewGameButtonPress() 
	{
		string save_file_default_name = "SwampSneak" + "_" + DateTime.Now.ToString ("yyyyMMddHHmmss") + ".dat";
		string save_file_path = EditorUtility.SaveFilePanel ("Save Game", Application.dataPath + "/SaveData", save_file_default_name, "dat");
		if (save_file_path != null) {
			control.CreateSave (save_file_path);
			Application.LoadLevel ("Scenes/Sprint-3-Demo");
		}

	}

	public void LoadGameButtonPress()
	{
		string load_file_path = EditorUtility.OpenFilePanel ("Load Game", Application.dataPath + "/SaveData", "dat");
		if (load_file_path != null) {
			control.LoadData (load_file_path);
			Application.LoadLevel ("Scenes/Sprint-3-Demo");
		}

	}

	public void CreditsButtonPress()
	{
		string credits = File.ReadAllText (Application.dataPath + "\\" + "Credits.txt");
		if (credits != null) {
			EditorUtility.DisplayDialog ("Credits", credits, "Close");
		}

	}

}