using UnityEngine;
using System.Collections;
using UnityEditor;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0649 // private field assigned but not used.

[CustomEditor(typeof(TransposeDK2UMA))]
public class Transpose2UMAInspector : Editor {
	TransposeDK2UMA Transpose;

	void OnEnable (){
		Transpose = (TransposeDK2UMA)target;
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.HelpBox("This component is required by the DK UMA Avatar. " +
			"It is used to transpose the DK UMA avatar for the UMA engine to be able to generate it using the advanced features of the DK UMA Engine.", UnityEditor.MessageType.None);
	}
}
