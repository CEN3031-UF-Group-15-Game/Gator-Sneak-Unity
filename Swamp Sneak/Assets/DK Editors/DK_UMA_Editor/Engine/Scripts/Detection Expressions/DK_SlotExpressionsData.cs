using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class DK_SlotExpressionsData {
    public string dk_SlotExpressionsName;
    public GameObject AnatomyPart;
	public string OverlayType;
	public string WearWeight;
	public bool Selected;
	public bool Elem;
	public DKDnaConverterBehaviour[] dnaConverterList;
    public Dictionary<Type, System.Action<DKUMAData>> dk_SlotExpressionsDictionary = new Dictionary<Type, System.Action<DKUMAData>>();
	public bool ElemAlreadyIn = false;
	
    void Awake()
    {
        UpdateDictionary();
    }

    public void UpdateDictionary()
    {
        dk_SlotExpressionsDictionary.Clear();
        for (int i = 0; i < dnaConverterList.Length; i++)
        {
            if (dnaConverterList[i])
            {
                if (!dk_SlotExpressionsDictionary.ContainsKey(dnaConverterList[i].DNAType))
                {
                    dk_SlotExpressionsDictionary.Add(dnaConverterList[i].DNAType, dnaConverterList[i].ApplyDnaAction);
                }
            }
        }
    }
}
