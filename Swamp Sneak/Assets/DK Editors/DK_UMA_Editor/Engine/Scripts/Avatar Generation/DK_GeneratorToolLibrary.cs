using UnityEngine;
using System.Collections.Generic;
using System;

public class DK_GeneratorToolLibrary : MonoBehaviour {

    public DK_GeneratorPresetElement[] dk_GeneratorPresetElementList = new DK_GeneratorPresetElement[0];
    public Dictionary<string, DK_GeneratorPresetData> dk_GeneratorToolDictionary = new Dictionary<string, DK_GeneratorPresetData>();

    void Awake()
    {
        UpdateDictionary();
    }

    public void UpdateDictionary()
    {
        dk_GeneratorToolDictionary.Clear();
        for (int i = 0; i < dk_GeneratorPresetElementList.Length; i++)
        {
            if (dk_GeneratorPresetElementList[i])
            {
                if (!dk_GeneratorToolDictionary.ContainsKey(dk_GeneratorPresetElementList[i].dk_GeneratorPresetElement.dk_GeneratorPresetName))
                {
                    dk_GeneratorToolDictionary.Add(dk_GeneratorPresetElementList[i].dk_GeneratorPresetElement.dk_GeneratorPresetName, dk_GeneratorPresetElementList[i].dk_GeneratorPresetElement);
                }
            }
        }
    }

    public void AddDK_GeneratorTool(DK_GeneratorPresetElement dk_GeneratorTool)
    {
        for (int i = 0; i < dk_GeneratorPresetElementList.Length; i++)
        {
            if (dk_GeneratorPresetElementList[i].dk_GeneratorPresetElement.dk_GeneratorPresetName == dk_GeneratorTool.dk_GeneratorPresetElement.dk_GeneratorPresetName)
            {
                dk_GeneratorPresetElementList[i] = dk_GeneratorTool;
                return;
            }
        }
        var list = new DK_GeneratorPresetElement[dk_GeneratorPresetElementList.Length + 1];
        Array.Copy(dk_GeneratorPresetElementList, list, dk_GeneratorPresetElementList.Length );
        list[dk_GeneratorPresetElementList.Length] = dk_GeneratorTool;
        dk_GeneratorPresetElementList = list;
        dk_GeneratorToolDictionary.Add(dk_GeneratorTool.dk_GeneratorPresetElement.dk_GeneratorPresetName, dk_GeneratorTool.dk_GeneratorPresetElement);
    }

    internal DK_GeneratorPresetData GetDK_GeneratorTool(string dk_GeneratorPresetName)
    {
        return dk_GeneratorToolDictionary[dk_GeneratorPresetName];
    }
}
