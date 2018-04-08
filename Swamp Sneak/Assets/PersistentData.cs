using System;

// PersistentData is a container class used to hold information we want to read/write to/from a file.
// This data gets written to a file in the GameControl.SaveData() function.

// Keyword necessary to tell Unity that this class can be serialized to a file
[System.Serializable]
public class PersistentData {
	
	public int player_stealth;
	public int player_experience;
	public int player_level;

}


