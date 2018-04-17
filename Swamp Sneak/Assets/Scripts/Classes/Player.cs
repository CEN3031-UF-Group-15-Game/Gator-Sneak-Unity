using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Person {
	
	private int level;
	private int experience;
	private int stealth;

	public const int default_level = 1;
	public const int default_experience = 0;
	public const int default_stealth = 5;

	/* From documentation: 
	 * "Each GameObject's Awake is called in a random order between objects. Because of this, 
	 * you should use Awake to set up references between scripts, and use Start to pass any 
	 * information back and forth. Awake is always called before any Start functions."
	 * 
	 * "Use Awake instead of the constructor for initialization, as the serialized 
	 * state of the component is undefined at construction time. Awake is called once, just like 
	 * the constructor. 
	 * Link: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
	*/
	void Awake() {
		// Get a reference to the GameControl object
		control = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
	}

	void Start() {
		// If the save file does not exist, initialize player_stats to default values.
		// If it does exist, the save file will be loaded and stored to a PersistentData
		// object in GameControl's Awake function.
		if (control.persistent_save_data == null) {
			Debug.Log ("No save data; using default values.");
			level = default_level;
			experience = default_experience;
			stealth = default_stealth;

		}
		// Otherwise, use the persistent save data
		else {
			// Initialize player_stats
			Debug.Log("Save data found; using saved values.");
			level = control.persistent_save_data.player_level;
			experience = control.persistent_save_data.player_experience;
			stealth = control.persistent_save_data.player_stealth;
		}

		Debug.Log ("Player level = " + level);
		Debug.Log ("Player experience = " + experience);
		Debug.Log ("Player stealth = " + stealth);
	}

	// Stealth Functions
	// -----------------------------------------------
	public int getStealthStat() {
		return stealth;
	}

	public void setStealthStat(int new_stealth) {
		stealth = new_stealth;
	}

	public void incrementStealthStat(int amount) {
		stealth += amount;
	}

	// Level Functions
	// -----------------------------------------------
	public int getLevelStat() {
		return level;
	}

	public void incrementLevelStat(int amount) {
		level += amount;
	}

	public void setLevelStat(int new_level) {
		level = new_level;
	}

	// Experience Functions
	// -----------------------------------------------
	public int getExperienceStat() {
		return experience;
	}

	public void incrementExperienceStat(int amount) {
		experience += amount;
	}

	public void setExperienceStat(int new_experience) {
		experience = new_experience;
	}

}