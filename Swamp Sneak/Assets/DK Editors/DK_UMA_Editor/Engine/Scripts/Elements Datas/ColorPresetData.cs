using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class ColorPresetData : ScriptableObject{

//	[System.Serializable]
	public string ColorPresetName;
	public Color PresetColor;
	public string OverlayType;
	public float Mini;
	public float Maxi;
	public List<DKRaceData> RacesList = new List<DKRaceData>();


}
