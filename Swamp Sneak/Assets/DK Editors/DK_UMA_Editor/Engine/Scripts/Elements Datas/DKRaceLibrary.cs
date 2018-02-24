using UnityEngine;
using System.Collections.Generic;
using System;

public class DKRaceLibrary : MonoBehaviour {
    public DKRaceData[] raceElementList = new DKRaceData[0];
    public Dictionary<string, DKRaceData> raceDictionary = new Dictionary<string, DKRaceData>();

    public void Awake(){
        UpdateDictionary();
		// detect settings data
		if ( GameObject.Find ("DK_UMA") != null ){
			DKUMA_Variables variables = GameObject.Find ("DK_UMA").GetComponent<DKUMA_Variables>();
			variables.AddToDKRacesLib ();
			variables.AddToUMARaceLib ();
		}
    }

	public void OnEnable (){
		// detect settings data
		if ( GameObject.Find ("DK_UMA") != null ){
			DKUMA_Variables variables = GameObject.Find ("DK_UMA").GetComponent<DKUMA_Variables>();
			variables.AddToDKRacesLib ();
			variables.AddToUMARaceLib ();
		}
	}

    public void UpdateDictionary(){
        raceDictionary.Clear();
        for (int i = 0; i < raceElementList.Length; i++){
            if (raceElementList[i]){
                if (!raceDictionary.ContainsKey(raceElementList[i].raceName)){
                    raceDictionary.Add(raceElementList[i].raceName, raceElementList[i]);
                }
            }
        }
    }

    public void AddRace(DKRaceData race)
    {
        for (int i = 0; i < raceElementList.Length; i++)
        {
			if ( raceElementList[i] != null && raceElementList[i].raceName == race.raceName)
            {
                raceElementList[i] = race;
                return;
            }
        }
        var list = new DKRaceData[raceElementList.Length + 1];
        Array.Copy(raceElementList, list, raceElementList.Length );
        list[raceElementList.Length] = race;
        raceElementList = list;
        raceDictionary.Add(race.raceName, race);
    }

    internal DKRaceData GetRace(string raceName)
    {
//		Debug.Log ( raceName );
        return raceDictionary[raceName];
    }
}
