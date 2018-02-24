using UnityEngine;
using System.Collections.Generic;
using System;

public class DK_SlotsAnatomyLibrary : MonoBehaviour {

    public DK_SlotsAnatomyElement[] dk_SlotsAnatomyElementList = new DK_SlotsAnatomyElement[0];
    public Dictionary<string, DK_SlotsAnatomyData> dk_SlotsAnatomyDictionary = new Dictionary<string, DK_SlotsAnatomyData>();

    void Awake()
    {
        UpdateDictionary();
    }

    public void UpdateDictionary()
    {
        dk_SlotsAnatomyDictionary.Clear();
        for (int i = 0; i < dk_SlotsAnatomyElementList.Length; i++)
        {
            if (dk_SlotsAnatomyElementList[i])
            {
                if (!dk_SlotsAnatomyDictionary.ContainsKey(dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName))
                {
                    dk_SlotsAnatomyDictionary.Add(dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName, dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement);
                }
            }
        }
    }

    public void AddDK_SlotsAnatomy(DK_SlotsAnatomyElement dk_SlotsAnatomy)
    {
        for (int i = 0; i < dk_SlotsAnatomyElementList.Length; i++)
        {
            if (dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName == dk_SlotsAnatomy.dk_SlotsAnatomyElement.dk_SlotsAnatomyName)
            {
                dk_SlotsAnatomyElementList[i] = dk_SlotsAnatomy;
                return;
            }
        }
        var list = new DK_SlotsAnatomyElement[dk_SlotsAnatomyElementList.Length + 1];
        Array.Copy(dk_SlotsAnatomyElementList, list, dk_SlotsAnatomyElementList.Length );
        list[dk_SlotsAnatomyElementList.Length] = dk_SlotsAnatomy;
        dk_SlotsAnatomyElementList = list;
        dk_SlotsAnatomyDictionary.Add(dk_SlotsAnatomy.dk_SlotsAnatomyElement.dk_SlotsAnatomyName, dk_SlotsAnatomy.dk_SlotsAnatomyElement);
    }

    internal DK_SlotsAnatomyData GetDK_SlotsAnatomy(string dk_SlotsAnatomyName)
    {
        return dk_SlotsAnatomyDictionary[dk_SlotsAnatomyName];
    }
}
