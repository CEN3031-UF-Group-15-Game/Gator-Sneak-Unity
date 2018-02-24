using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

[System.Serializable]
public class PresetRaceAssign : ScriptableObject {
	
	public DKRaceData RacePreset;
	public List<Transform> RacePresetList = new List<Transform>();
	
}
