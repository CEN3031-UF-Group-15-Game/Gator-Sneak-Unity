using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class DK_SlotsAnatomyData {
    public string dk_SlotsAnatomyName;
    public string ForGender;
	public DKDnaConverterBehaviour[] dnaConverterList;
	public List<string> Race = new List<string>();
    public Dictionary<Type, System.Action<DKUMAData>> dk_SlotsAnatomyDictionary = new Dictionary<Type, System.Action<DKUMAData>>();
	public bool Selected = false;
	public bool ByDefault = false;
	public bool ElemAlreadyIn = false;
	public int SpawnPerct = 50;

	public DK_SlotsAnatomyElement Place;

	[System.Serializable]
	public class DefSlotOvTypeData{
		public String OverlayType = "";
		public bool Elem = false;
	}
	public DefSlotOvTypeData DefaultSlotOverlayType = new DefSlotOvTypeData();

    void Awake()
    {
        UpdateDictionary();
    }

    public void UpdateDictionary()
    {
        dk_SlotsAnatomyDictionary.Clear();
        for (int i = 0; i < dnaConverterList.Length; i++)
        {
            if (dnaConverterList[i])
            {
                if (!dk_SlotsAnatomyDictionary.ContainsKey(dnaConverterList[i].DNAType))
                {
                    dk_SlotsAnatomyDictionary.Add(dnaConverterList[i].DNAType, dnaConverterList[i].ApplyDnaAction);
                }
            }
        }
    }
}
