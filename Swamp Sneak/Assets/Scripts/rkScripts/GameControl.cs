using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

public class GameControl : MonoBehaviour
{
	// Use singleton design pattern. This variable can be accessed anywhere.
	public static GameControl control;

    //Public Constants and Variables
    public const int LOADINGSCENE = 0;
	public bool gameWin = false;
	public PersistentData persistent_save_data;

    //Private Variables
	private static Player player_character;		// Reference to player character initialized in Awake()
    private bool isCurScnEditable = true;
    private bool loadScn = false;
    private string currentScene, prevScene;
	private GameObject winBox;    
    private int level = 0;
	private string save_file_path { get; set; } // Keep the path of the save file that was loaded / created at game start, to use on subsequent saves during gameplay
	private string save_file_directory = "Assets/SaveData/";

    // Awake is call 0.
	// Use singleton design pattern, so there will only ever be one GameControl.
	// The GameControl can now be placed in every single scene, and the order in 
	// which the scenes are loaded does not matter.
    private void Awake()
    {
		// If control does not exist, initialize it
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
			// If the Player object exists yet...
			if (GameObject.FindWithTag ("Player")) {
				player_character = GameObject.FindWithTag ("Player").GetComponent<Player>();
			}
			//Debug.Log("The " + this.gameObject.ToString() + " is Awake and Will Not Be Destroyed");
		} 
		// If it does already exist, and it is not this, delete it (there can only be one!)
		else if (control != this) {
			Destroy (gameObject);
			return;
		}

    }

    //OnEnable is Call 1
    private void OnEnable()
    {
        //Debug.Log("GameControl onEnable Called");
        SceneManager.sceneLoaded += OnSceneChanged;
    }

	// Called before this object is destroyed
    private void OnDisable()
    {
        //Debug.Log("GameControl onDisable Called");
        SceneManager.sceneLoaded -= OnSceneChanged;

    }

    //OnSceneChanged is Call 3
    void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        prevScene = currentScene;
        currentScene = SceneManager.GetActiveScene().name;

		// If Player character reference has not been set up yet..
		// (This is hacky but it works....... and I don't know how to do it better.....)
		if (player_character == null) {
			try {
				player_character = GameObject.FindWithTag ("Player").GetComponent<Player>();
				//Debug.Log("In scene: " + currentScene);
				//Debug.Log("GameControl: Player reference successfully created.");
			}
			catch {
				//Debug.Log ("In scene: " + currentScene);
				//Debug.Log ("GameControl: Player reference not yet able to be created.");
			}
		}

        //Debug.Log("New Scene Loaded:" + scene.name);

        //Things that happen once at the beginning of scene load go here
        switch (currentScene)
        {
            case "LoadingScreen": //Scene 0 Should Be Mapped to Loading Scene
                //Debug.Log("GameControl is in Loading Screen\n");
                break;
            case "DEMO SCENE":
                //Debug.Log("GameControl is in DemoScene 0\n");
                /*
                 * Put SetUp Code Here
                 */
                break;
            case "DemoTurlington":
                //Debug.Log("GameControl is in DemoScene 0\n");
                //playerStealth = 1 + level;
                /*
                 * Put SetUp Code Here
                 */
                break;
            default:
                //Do Nothing.
                break;
        }
    }

    //Start is call 2
    // Use this for initialization
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;

        //Goto Start
        //SceneManager.LoadScene(startScene);
    }

    /*FixedUpdate is call 4
    void FixedUpdate()
    {

    }
    */

    // Update is call 5
    void Update () {
        manageScene();
	}

    // Scene Management Goes Here
    private void manageScene()
    {
        switch (currentScene)
        {
            case "LoadingScene":
                //Debug.Log("GameControl Loaded");
                break;
            case "start":
                //Debug.Log("In Start");
                break;
            case "DemoTurlington":
                /*
                 * Put Level Code Here
                 */
                break;
            default:
                //Debug.Log("Unknown Scene");
                break;
        }
    }

	// Create initial save file (called in Start menu. Can't save player data here yet, since player does not exist yet.)
	public void CreateSave(string path) {
		
		BinaryFormatter bf = new BinaryFormatter ();
		// Fill out a PersistentData struct
		PersistentData save_data = new PersistentData ();
		save_data.player_level = 1;
		save_data.player_experience = 0;
		save_data.player_stealth = 1;

		// Write save_data to file
		FileStream file;
		if (File.Exists (path)) {
			// If file exists, overwrite it
			file = File.Open (path, FileMode.Truncate, FileAccess.Write);
		} else {
			if (Directory.Exists (save_file_directory)) {
				file = File.Open (path, FileMode.Create, FileAccess.Write);
			} else {
				Directory.CreateDirectory (save_file_directory);
				file = File.Open (path, FileMode.Create, FileAccess.Write);
			}
		}

		bf.Serialize (file, save_data);
		file.Close ();
		save_file_path = path;

		Debug.Log ("Save File Created at: " + path);
		Debug.Log ("Level: " + save_data.player_level);
		Debug.Log ("Experience: " + save_data.player_experience);
		Debug.Log ("Stealth: " + save_data.player_stealth);

	}

	// Saves data out to a binary file. This function can be called anywhere the GameControl 
	// object is accessible (which should be everywhere)
	public void SaveData(string path) {
		
		BinaryFormatter bf = new BinaryFormatter ();

		if (player_character != null) {
			// Fill out a PersistentData struct
			PersistentData save_data = new PersistentData ();
			save_data.player_level = player_character.getLevel ();
			save_data.player_experience = player_character.getExperience ();
			save_data.player_stealth = player_character.getStealth ();

			// Write save_data to file
			FileStream file;
			if (File.Exists (path)) {
				// If file exists, overwrite it
				file = File.Open (path, FileMode.Truncate, FileAccess.Write);
			} else {
				if (Directory.Exists (save_file_directory)) {
					file = File.Open (path, FileMode.Create, FileAccess.Write);
				} else {
					Directory.CreateDirectory (save_file_directory);
					file = File.Open (path, FileMode.Create, FileAccess.Write);
				}
			}

			bf.Serialize (file, save_data);
			file.Close ();
			save_file_path = path;

			Debug.Log ("Data saved to: " + path);
			Debug.Log ("Level: " + persistent_save_data.player_level);
			Debug.Log ("Experience: " + persistent_save_data.player_experience);
			Debug.Log ("Stealth: " + persistent_save_data.player_stealth);
		} 
		else {
			// (Try calling this function before the Player object has been destroyed! Otherwise, the data in the save file will be 
			// overwritten with 0's, since you are accessing a null reference).
			// Or, if the Player object simply has not been created yet, try using CreateSave().
			throw new System.Exception ("Error: SaveData() function has been called, but Player object has already been destroyed.");
		}

	}

	// Read persistent save data from the save file
	public void LoadData(string path) {
		
		if (File.Exists (path)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (path, FileMode.Open, FileAccess.Read);
			persistent_save_data = (PersistentData)bf.Deserialize (file);
			file.Close ();
			save_file_path = path;

			Debug.Log ("Save File Exists.");
			Debug.Log ("Level: " + persistent_save_data.player_level);
			Debug.Log ("Experience: " + persistent_save_data.player_experience);
			Debug.Log ("Stealth: " + persistent_save_data.player_stealth);
		} 
		else {
			persistent_save_data = null;
			Debug.Log ("Save file does not exist. Default values will be used.");
		}

	}


}