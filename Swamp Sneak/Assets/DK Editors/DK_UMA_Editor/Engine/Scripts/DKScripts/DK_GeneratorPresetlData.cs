using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class DK_GeneratorPresetData {
    public string dk_GeneratorPresetName;
    public DK_SlotsAnatomyElement [] SlotList;
    public string ForGender = "";
	public DKDnaConverterBehaviour[] dnaConverterList;
    public Dictionary<Type, System.Action<DKUMAData>> dk_GeneratorPresetDictionary = new Dictionary<Type, System.Action<DKUMAData>>();
	public bool ElemAlreadyIn = false;
	
	
    void Awake()
    {
        UpdateDictionary();
    }

    public void UpdateDictionary()
    {
        dk_GeneratorPresetDictionary.Clear();
        for (int i = 0; i < dnaConverterList.Length; i++)
        {
            if (dnaConverterList[i])
            {
                if (!dk_GeneratorPresetDictionary.ContainsKey(dnaConverterList[i].DNAType))
                {
                    dk_GeneratorPresetDictionary.Add(dnaConverterList[i].DNAType, dnaConverterList[i].ApplyDnaAction);
                }
            }
        }
    }
}
