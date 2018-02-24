using UnityEngine;
using System.Collections.Generic;
using System;

public class DK_SlotExpressionsLibrary : MonoBehaviour {

    public DK_SlotExpressionsElement[] dk_SlotExpressionsElementList = new DK_SlotExpressionsElement[0];
    public Dictionary<string, DK_SlotExpressionsData> dk_SlotExpressionsDictionary = new Dictionary<string, DK_SlotExpressionsData>();
	
	
    void Awake()
    {
        UpdateDictionary();
    }

    public void UpdateDictionary()
    {
        dk_SlotExpressionsDictionary.Clear();
        for (int i = 0; i < dk_SlotExpressionsElementList.Length; i++)
        {
            if (dk_SlotExpressionsElementList[i])
            {
                if (!dk_SlotExpressionsDictionary.ContainsKey(dk_SlotExpressionsElementList[i].dk_SlotExpressionsElement.dk_SlotExpressionsName))
                {
                    dk_SlotExpressionsDictionary.Add(dk_SlotExpressionsElementList[i].dk_SlotExpressionsElement.dk_SlotExpressionsName, dk_SlotExpressionsElementList[i].dk_SlotExpressionsElement);
                }
            }
        }
    }

    public void AddDK_SlotExpressions(DK_SlotExpressionsElement dk_SlotExpressions)
    {
        for (int i = 0; i < dk_SlotExpressionsElementList.Length; i++)
        {
            if (dk_SlotExpressionsElementList[i].dk_SlotExpressionsElement.dk_SlotExpressionsName == dk_SlotExpressions.dk_SlotExpressionsElement.dk_SlotExpressionsName)
            {
                dk_SlotExpressionsElementList[i] = dk_SlotExpressions;
                return;
            }
        }
        var list = new DK_SlotExpressionsElement[dk_SlotExpressionsElementList.Length + 1];
        Array.Copy(dk_SlotExpressionsElementList, list, dk_SlotExpressionsElementList.Length );
        list[dk_SlotExpressionsElementList.Length] = dk_SlotExpressions;
        dk_SlotExpressionsElementList = list;
        dk_SlotExpressionsDictionary.Add(dk_SlotExpressions.dk_SlotExpressionsElement.dk_SlotExpressionsName, dk_SlotExpressions.dk_SlotExpressionsElement);
    }

    internal DK_SlotExpressionsData GetDK_SlotExpressions(string dk_SlotExpressionsName)
    {
        return dk_SlotExpressionsDictionary[dk_SlotExpressionsName];
    }
}
