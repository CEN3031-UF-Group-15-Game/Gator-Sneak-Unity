using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class PBRTutoWin : EditorWindow {
	Vector2 scroll;
	Color Green = new Color (0.8f, 1f, 0.8f, 1);
	Color Red = new Color (0.9f, 0.5f, 0.5f);




	public static void OpenAutoDetectWin(){
		GetWindow(typeof(AutoDetect_Editor), false, "Manager");
	}

	void OnGUI () {
		this.minSize = new Vector2(300, 570);
		this.maxSize = new Vector2(310, 580);

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

		using (new ScrollView(ref scroll)) {
			using (new Horizontal()) {
				GUI.color = Color.white ;
				GUILayout.Label ( "Deleting the basic elements", "toolbarbutton", GUILayout.ExpandWidth (true));
			}
			GUI.color = Color.yellow ;
			GUILayout.TextField("Save your UMA and DK UMA project as a new package to be able to restore it in case of problem." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			GUILayout.TextField("UMA and DK UMA have to be installed to your scene." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			GUI.color = Color.white ;
			GUILayout.TextField("To prepare your DK Project to be able to use the PRB elements, "+
				"first you need to delete the basic UMA Elements." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			GUILayout.TextField("Open the folder from your assets : UMA/Content/UMA/Legacy." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			GUI.color = Red ;
			GUILayout.TextField("Delete the Slots and Overlays folders ONLY." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			GUILayout.TextField("If you have no additionnal content installed in this folder, " +
			                    "select the Overlays and Slots Folders and delete them." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			GUI.color = Color.white ;
			GUILayout.Label ( "Preparing the DK elements", "toolbarbutton", GUILayout.ExpandWidth (true));

			GUI.color = Green ;
			if ( GUILayout.Button ( "Open the 'Element Manager'", GUILayout.ExpandWidth (true))) {
				OpenAutoDetectWin();
			}

			GUILayout.Label ( "1- Click on 'Detect all Elements'", GUILayout.ExpandWidth (true));
			GUILayout.Label ( "2- Click on 'Fix Elements'", GUILayout.ExpandWidth (true));
			GUILayout.Label ( "3- Click on 'Add to libraries", GUILayout.ExpandWidth (true));

			GUI.color = Color.white ;
			GUILayout.Label ( "Result", "toolbarbutton", GUILayout.ExpandWidth (true));
			GUILayout.TextField(" The PBR UMA elements are now linked to DK UMA." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			GUILayout.TextField(" Also they have taken the place of the previous basic elements in the Slots" +
				" and Overlay libraries of your scene." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			GUILayout.TextField(" Finally the PBR Overlays have been corrected." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			GUI.color = Color.yellow ;
			GUILayout.TextField("If you encounter any problem, please let us know by reporting to our forum." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			GUI.color = Green ;
			if ( GUILayout.Button ( "Go to the DK Forum", GUILayout.ExpandWidth (true))) {
				Application.OpenURL ("http://unity3d-dk-tools.boards.net/");
			}
		}
	}
}
