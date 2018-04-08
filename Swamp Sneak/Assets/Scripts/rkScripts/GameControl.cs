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
    public string startScene = "start";
	public int playerStealth = 1;
	public bool gameWin = false;
	public string save_file_location = "Assets/SaveData/";
	public string save_file_name = "SaveFile.dat";
	public PersistentData persistent_save_data;

    //Private Variables
	private static Player player_character;		// Reference to player character initialized in Awake()
    private bool isCurScnEditable = true;
    private bool loadScn = false;
    private string currentScene, prevScene;
	private GameObject winBox;    
    private int level = 0;

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
			player_character = GameObject.FindWithTag ("Player").GetComponent<Player>();

			Debug.Log("The " + this.gameObject.ToString() + " is Awake and Will Not Be Destroyed");
		} 
		// If it does already exist, and it is not this, delete it (there can only be one!)
		else if (control != this) {
			Destroy (gameObject);
		}

		// Load save data...
		LoadData();
    }

    //OnEnable is Call 1
    private void OnEnable()
    {
        Debug.Log("GameControl onEnable Called");
        SceneManager.sceneLoaded += OnSceneChanged;
    }

	// Called before this object is destroyed; any final cleanup can go here
    private void OnDisable()
    {
        Debug.Log("GameControl onDisable Called");
        SceneManager.sceneLoaded -= OnSceneChanged;

		// Write persistent_save_data to a file
		SaveData ();
    }

    //OnSceneChanged is Call 3
    void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        prevScene = currentScene;
        currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("New Scene Loaded:" + scene.name);

        //Things that happen once at the beginning of scene load go here
        switch (currentScene)
        {
            case "LoadingScreen": //Scene 0 Should Be Mapped to Loading Scene
                Debug.Log("GameControl is in Loading Screen\n");
                break;
            case "DEMO SCENE":
                Debug.Log("GameControl is in DemoScene 0\n");
                /*
                 * Put SetUp Code Here
                 */
                break;
            case "DemoTurlington":
                Debug.Log("GameControl is in DemoScene 0\n");
                playerStealth = 1 + level;
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
        SceneManager.LoadScene(startScene);
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
                if(gameWin)
                {
                    Debug.Log("You Win");
                    gameWin = false;
                    SceneManager.LoadScene("DemoTurlington");
                    ++level;
                    //updateSaveFile();
					SaveData();


                }
                break;
            default:
                //Debug.Log("Unknown Scene");
                break;
        }
    }

    private void updateSaveFile()
    {
        string path = "Assets/SaveData/Save.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Level: " + level);
        writer.Close();

        //Re-import the file to update the reference in the editor
     	//AssetDatabase.ImportAsset(path);
     	//TextAsset asset = Resources.Load("save.txt");

        //Print the text from the file
      	//Debug.Log(asset.text);

    }

	// Saves data out to a binary file. This function can be called anywhere the GameControl 
	// object is accessible (which should be everywhere)
	public void SaveData() {
		string path = save_file_location + save_file_name;
		BinaryFormatter bf = new BinaryFormatter ();

		// Fill out a PersistentData struct
		PersistentData save_data = new PersistentData();
		save_data.player_level = player_character.getLevel ();
		save_data.player_experience = player_character.getExperience ();
		save_data.player_stealth = player_character.getStealth ();

		// Write save_data to file
		FileStream file;
		if (File.Exists (path)) {
			// If file exists, overwrite it
			file = File.Open (path, FileMode.Truncate, FileAccess.Write);
		} 
		else {
			if (Directory.Exists(save_file_location)) {
				file = File.Open (path, FileMode.Create, FileAccess.Write);
			}
			else {
				Directory.CreateDirectory(save_file_location);
				file = File.Open (path, FileMode.Create, FileAccess.Write);
			}
		}

		bf.Serialize (file, save_data);
		file.Close ();
	}

	// Read persistent save data from the save file
	public void LoadData() {
		string path = save_file_location + save_file_name;
		if (File.Exists (path)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (path, FileMode.Open, FileAccess.Read);
			persistent_save_data = (PersistentData)bf.Deserialize (file);
			file.Close ();

			Debug.Log ("Save File Exists.");
			Debug.Log ("Level: " + persistent_save_data.player_level);
			Debug.Log ("Experience: " + persistent_save_data.player_experience);
			Debug.Log ("Stealth: " + persistent_save_data.player_stealth);
		} 
		else {
			persistent_save_data = null;
			Debug.Log ("Save file does not exist.");
		}

	}


}