using UnityEngine;
using System.Collections;
using UnityEditor;

public class ChangeSelectionconfirmation : EditorWindow {
//	Color Green = new Color (0.8f, 1f, 0.8f, 1);
	Color Red = new Color (0.9f, 0.5f, 0.5f);

	public static DKOverlayData _DKOverlayData;
	public static DKSlotData _DKSlotData;

	public static string _Action;

	void OnGUI () {

		this.minSize = new Vector2(300, 75);
		this.maxSize = new Vector2(305, 80);
		this.position = new Rect((Screen.currentResolution.width/2)-150, (Screen.currentResolution.height/2)-40, 300, 80);

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


		using (new Horizontal()) {	
			GUI.color = Color.white ;
			GUILayout.Label("Change selection confirmation", "toolbarbutton", GUILayout.ExpandWidth (true));
			GUI.color = Red;
		}
		using (new HorizontalCentered()) {	
			GUI.color = Color.white ;
			GUILayout.Label("Confirm to change the selection", bold, GUILayout.ExpandWidth (true));

		}
		using (new Horizontal()) {

			if ( GUILayout.Button ( "Confirm", GUILayout.ExpandWidth (true))) {
				if ( _Action == "Slot" ) Selection.activeObject = _DKSlotData;
				if ( _Action == "Overlay" ) Selection.activeObject = _DKOverlayData;
				this.Close();

			}

			if ( GUILayout.Button ( "Cancel", GUILayout.ExpandWidth (true))) {
				this.Close();
			}
		}
	}
}
