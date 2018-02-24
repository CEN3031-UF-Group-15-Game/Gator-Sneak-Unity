using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DKUMA;

[CustomEditor(typeof(SetAllToMediumDNATest))]
public class SetAllDNAToMediumInspector : Editor {
	SetAllToMediumDNATest SetAllDNA;

	void OnEnable (){
		SetAllDNA = (SetAllToMediumDNATest)target;
	}

	public override void OnInspectorGUI()
	{
		if (GUILayout.Button ( "Set All DNA to 0.5", GUILayout.ExpandWidth (true))) {
			SetAllToMediumDNA.SetAllDNA ( SetAllDNA.transform.GetComponent<DK_RPG_UMA>(), true, false );
		}
	}
}
