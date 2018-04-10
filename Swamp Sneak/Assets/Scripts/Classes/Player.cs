using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Person {
	private GameControl control;
	private Stats player_stats;

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
			player_stats = new Stats ();
			player_stats.level = 1;
			player_stats.experience = 0;
			player_stats.stealth = 1;

		}
		// Otherwise, use the persistent save data
		else {
			// Initialize player_stats
			player_stats = new Stats ();
			player_stats.level = control.persistent_save_data.player_level;
			player_stats.experience = control.persistent_save_data.player_experience;
			player_stats.stealth = control.persistent_save_data.player_stealth;
		}
	}

	// Stealth Functions
	// -----------------------------------------------
	public int getStealth() {
		return player_stats.stealth;
	}

	public void setStealth(int new_stealth) {
		player_stats.stealth = new_stealth;
	}

	public void incrementStealth(int amount) {
		player_stats.stealth += amount;
	}

	// Level Functions
	// -----------------------------------------------
	public int getLevel() {
		return player_stats.level;
	}

	public void incrementLevel(int amount) {
		player_stats.level += amount;
	}

	public void setLevel(int new_level) {
		player_stats.level = new_level;
	}

	// Experience Functions
	// -----------------------------------------------
	public int getExperience() {
		return player_stats.experience;
	}

	public void incrementExperience(int amount) {
		player_stats.experience += amount;
	}

	public void setExperience(int new_experience) {
		player_stats.experience = new_experience;
	}


	// Container class to hold player stats
	// -----------------------------------------------
	public class Stats {
		public int level;
		public int experience;
		public int stealth;
	}

}