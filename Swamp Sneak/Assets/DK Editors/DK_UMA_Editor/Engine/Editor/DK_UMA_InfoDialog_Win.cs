using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;



public class DK_UMA_InfoDialog_Win : EditorWindow {
	
	#region Variables
	public static string DialogType = "";
	public static bool EquipmentSetLoaded;
	public static bool AvatarLoaded;
	Vector2 scroll;

//	Color Green = new Color (0.8f, 1f, 0.8f, 1);
//	Color Red = new Color (0.9f, 0.5f, 0.5f);
	
//	bool Helper = false;
	#endregion Variables


	void OnGUI () {
		this.minSize = new Vector2(350, 500);
		
		#region fonts variables
		var bold = new GUIStyle ("label");
		var boldFold = new GUIStyle ("foldout");
		bold.fontStyle = FontStyle.Bold;
		bold.fontSize = 14;
		boldFold.fontStyle = FontStyle.Bold;
		
		var Slim = new GUIStyle ("label");
		Slim.fontStyle = FontStyle.Normal;
		Slim.fontSize = 10;	
		
		var style = new GUIStyle ("label");
		style.wordWrap = true;
		
		#endregion fonts variables

		if ( EquipmentSetLoaded ){
			GUI.color = Color.white;
			GUILayout.Label("Equipment Set Loaded", "toolbarbutton", GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("The selected Equipment Set is loaded to the DK UMA Avatar. " +
				"You can now clear the Equipment Set field of the avatar to prevent the Avatar to reload the Set in runtime.", UnityEditor.MessageType.Info);
			if ( GUILayout.Button ( "Clear the Equipment Set field", GUILayout.ExpandWidth (true))) {
				if ( Selection.activeObject != null && (Selection.activeObject as GameObject).GetComponent<DK_RPG_UMA>() != null ){
					DK_RPG_UMA avatar = (Selection.activeObject as GameObject).GetComponent<DK_RPG_UMA>();
					avatar.EquipmentSet.DKEquipmentSet = null;
					avatar.EquipmentSet.SetLoaded = false;
					EquipmentSetLoaded = false;
					this.Close ();
				}

			}
		}	
		if ( AvatarLoaded ){
			GUI.color = Color.white;
			GUILayout.Label("Avatar Loaded", "toolbarbutton", GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("The saved avatar is loaded to the DK UMA Avatar. " +
				"You can now clear saved avatar field of the avatar to prevent the Avatar to reload the save in runtime.", UnityEditor.MessageType.Info);
			if ( GUILayout.Button ( "Clear the Load Avatar field", GUILayout.ExpandWidth (true))) {
				if ( Selection.activeObject != null && (Selection.activeObject as GameObject).GetComponent<DK_RPG_UMA>() != null ){
					DK_RPG_UMA avatar = (Selection.activeObject as GameObject).GetComponent<DK_RPG_UMA>();
					avatar.AvatarFromDB = null;
					avatar.AvatarLoaded = false;
					AvatarLoaded = false;
					this.Close ();
				}

			}
		}	
	}
		
}
