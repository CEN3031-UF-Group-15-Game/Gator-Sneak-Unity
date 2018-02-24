using UnityEngine;
using System.Collections.Generic;
using System;

public class DK_GeneratorPresetLibrary : MonoBehaviour {
	
	public bool IsActivePreset = false;
	public string PresetName = "Write a Name here";
	public string ToGender = "Both";
    public DK_SlotsAnatomyElement[] dk_SlotsAnatomyElementList = new DK_SlotsAnatomyElement[0];
    public Dictionary<string, DK_SlotsAnatomyData> dk_GeneratorPresetDictionary = new Dictionary<string, DK_SlotsAnatomyData>();

    void Awake()
    {
        UpdateDictionary();
    }

    public void UpdateDictionary()
    {
        dk_GeneratorPresetDictionary.Clear();
        for (int i = 0; i < dk_SlotsAnatomyElementList.Length; i++)
        {
            if (dk_SlotsAnatomyElementList[i])
            {
                if (!dk_GeneratorPresetDictionary.ContainsKey(dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName))
                {
                    dk_GeneratorPresetDictionary.Add(dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName, dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement);
                }
            }
        }
    }

    public void AddDK_GeneratorPreset(DK_SlotsAnatomyElement dk_GeneratorPreset)
    {
        for (int i = 0; i < dk_SlotsAnatomyElementList.Length; i++)
        {
            if (dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName == dk_GeneratorPreset.dk_SlotsAnatomyElement.dk_SlotsAnatomyName)
            {
                dk_SlotsAnatomyElementList[i] = dk_GeneratorPreset;
                return;
            }
        }
        var list = new DK_SlotsAnatomyElement[dk_SlotsAnatomyElementList.Length + 1];
        Array.Copy(dk_SlotsAnatomyElementList, list, dk_SlotsAnatomyElementList.Length );
        list[dk_SlotsAnatomyElementList.Length] = dk_GeneratorPreset;
        dk_SlotsAnatomyElementList = list;
        dk_GeneratorPresetDictionary.Add(dk_GeneratorPreset.dk_SlotsAnatomyElement.dk_SlotsAnatomyName, dk_GeneratorPreset.dk_SlotsAnatomyElement);
    }

    internal DK_SlotsAnatomyData GetDK_GeneratorPreset(string dk_SlotsAnatomyName)
    {
        return dk_GeneratorPresetDictionary[dk_SlotsAnatomyName];
    }
}
