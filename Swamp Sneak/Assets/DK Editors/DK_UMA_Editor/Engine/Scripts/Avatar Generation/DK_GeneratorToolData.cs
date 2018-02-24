using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class DK_GeneratorToolData {
    public string dk_GeneratorToolName;
    public DK_SlotsAnatomyElement [] SlotList;
    public string ForGender = "";
	public DKDnaConverterBehaviour[] dnaConverterList;
    public Dictionary<Type, System.Action<DKUMAData>> dk_GeneratorToolDictionary = new Dictionary<Type, System.Action<DKUMAData>>();
	public bool ElemAlreadyIn = false;
	
	
    void Awake()
    {
        UpdateDictionary();
    }

    public void UpdateDictionary()
    {
        dk_GeneratorToolDictionary.Clear();
        for (int i = 0; i < dnaConverterList.Length; i++)
        {
            if (dnaConverterList[i])
            {
                if (!dk_GeneratorToolDictionary.ContainsKey(dnaConverterList[i].DNAType))
                {
                    dk_GeneratorToolDictionary.Add(dnaConverterList[i].DNAType, dnaConverterList[i].ApplyDnaAction);
                }
            }
        }
    }
}
