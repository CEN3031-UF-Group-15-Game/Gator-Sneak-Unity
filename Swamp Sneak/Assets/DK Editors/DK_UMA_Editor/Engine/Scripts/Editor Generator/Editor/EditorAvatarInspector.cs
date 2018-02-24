using UnityEngine;
using System.Collections;
using UnityEditor;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0649 // private field assigned but not used.

[CustomEditor(typeof(EditorAvatarPreview))]
public class EditorAvatarInspector : Editor {
	EditorAvatarPreview preview;

	void OnEnable (){
		preview = (EditorAvatarPreview)target;
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.HelpBox("This Avatar Preview is just a visualisation of the resulting avatar generated in game mode. It will be destroyed while the Runtime mode is started. " +
			"It is useless to add or to refer anything to its bones structure.", UnityEditor.MessageType.Info);
	}
}
