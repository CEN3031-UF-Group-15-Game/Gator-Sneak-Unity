using UnityEngine;
using System.Collections;

public class DKDnaConverterBehaviour : MonoBehaviour 
{
    public System.Type DNAType;
    public System.Action<DKUMAData> ApplyDnaAction;
    public void ApplyDna(DKUMAData data)
    {
        ApplyDnaAction(data);
    }
}