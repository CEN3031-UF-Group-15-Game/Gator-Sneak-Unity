using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class ChooseAnatomy_Win : EditorWindow {

	public static string Action = "";
	GameObject SlotsAnatomyLibraryObj;
	DK_SlotsAnatomyLibrary _SlotsAnatomyLibrary;
	Vector2 scroll;

	void OnEnable (){
		DetectPlaces ();
	}

	void DetectPlaces (){
		DKUMA_Variables _DKUMA_Variables = FindObjectOfType<DKUMA_Variables>();
		_SlotsAnatomyLibrary =  FindObjectOfType<DK_SlotsAnatomyLibrary>();

		// clear list
		if (_SlotsAnatomyLibrary != null ){
			_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.ToList ().Clear();
		}
		// detect all places
	//	List<GameObject> PlacesList = new List<GameObject>();
		List<DK_SlotsAnatomyElement> tmpPlacesList = new List<DK_SlotsAnatomyElement>();
	//	PlacesList.Clear();
		_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.ToList().Clear();
		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:GameObject", lookFor);
		foreach (string guid in guids2) {
			string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			GameObject element = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
			if ( element.GetComponent<DK_SlotsAnatomyElement>() != null ){
				tmpPlacesList.Add ( element.GetComponent<DK_SlotsAnatomyElement>() );
				if ( _DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.PlacesList.Contains ( element.GetComponent<DK_SlotsAnatomyElement>() ) == false )
					_DKUMA_Variables._DK_UMA_GameSettings._GameLibraries.PlacesList.Add ( element.GetComponent<DK_SlotsAnatomyElement>() );
			}
		}
		Debug.Log ("tmpPlaces count = "+tmpPlacesList.Count);
		_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList = tmpPlacesList.ToArray();
	}

	void OnGUI () {
		#region fonts variables
		var bold = new GUIStyle ("label");
		var boldFold = new GUIStyle ("foldout");
		bold.fontStyle = FontStyle.Bold;
		bold.fontSize = 14;
		boldFold.fontStyle = FontStyle.Bold;
		//	var someMatched = false;
		
		var Slim = new GUIStyle ("label");
		Slim.fontStyle = FontStyle.Normal;
		Slim.fontSize = 10;	
		
		var style = new GUIStyle ("label");
		style.wordWrap = true;
		#endregion fonts variables

		#region choose Places List
		if ( Action == "ChoosePlace" ) using (new Horizontal()) {
			GUILayout.Label("choose a Place for the Element", "toolbarbutton", GUILayout.ExpandWidth (true));
			GUI.color = new Color (0.9f, 0.5f, 0.5f);
			if ( GUILayout.Button ( "X", "toolbarbutton", GUILayout.ExpandWidth (false))) {
				Action = "";
				this.Close();
			}
		}

		GUI.color = Color.white;
		GUILayout.Space(5);
		using (new Horizontal()) {
			GUI.color = Color.white ;
			GUILayout.Label("Anatomy Slot Name", "toolbarbutton", GUILayout.Width (140));
			GUILayout.Label("Race", "toolbarbutton", GUILayout.Width (60));
			GUILayout.Label("Gender", "toolbarbutton", GUILayout.Width (55));
			GUILayout.Label("Overlay Type", "toolbarbutton", GUILayout.Width (90));
			GUILayout.Label("", "toolbarbutton", GUILayout.ExpandWidth (true));
		}

		using (new Horizontal()) {
			using (new ScrollView(ref scroll)) 	{
			//	SlotsAnatomyLibraryObj = GameObject.Find("DK_SlotsAnatomyLibrary");
			//	DK_SlotsAnatomyLibrary _SlotsAnatomyLibrary =  SlotsAnatomyLibraryObj.GetComponent<DK_SlotsAnatomyLibrary>();
				
				for(int i = 0; i < _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList.Length; i ++){
					DK_Race DK_Race = new DK_Race();
					if ( _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i] != null ) DK_Race = _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].GetComponent("DK_Race") as DK_Race;

					using (new Horizontal(GUILayout.Width (80))) {
						// Element
						if ( EditorVariables.SelectedElemPlace ==  _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i] ) GUI.color = Color.yellow ;
						else GUI.color = Color.white ;
						if (GUILayout.Button ( _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].dk_SlotsAnatomyElement.dk_SlotsAnatomyName , "toolbarbutton", GUILayout.Width (140))) {
							EditorVariables.SelectedElemPlace = _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i];
							DKSlotData SelectedSlotElement =  Selection.activeObject as DKSlotData;
							if ( SelectedSlotElement != null ) 
							{
								if ( Action == "ChoosePlace" ) SelectedSlotElement.Place = EditorVariables.SelectedElemPlace;
								AutoOverlayType.AutoAssignOverlayType (SelectedSlotElement.Place, SelectedSlotElement, null);

							}
							DKOverlayData SelectedOverlayElement =  Selection.activeObject as DKOverlayData;
							if ( SelectedOverlayElement != null ) 
							{
								if ( Action == "ChoosePlace" ) SelectedOverlayElement.Place = EditorVariables.SelectedElemPlace;
								AutoOverlayType.AutoAssignOverlayType (SelectedOverlayElement.Place, null, SelectedOverlayElement);

							}


							EditorUtility.SetDirty(Selection.activeObject);
							AssetDatabase.SaveAssets();
							this.Close();
						}
						// Race
						if ( _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.GetComponent<DK_Race>() as DK_Race == null ) {
							_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.AddComponent<DK_Race>();	
						}
						if ( DK_Race.Race.Count == 0 ) GUI.color = new Color (0.9f, 0.5f, 0.5f);
						if ( DK_Race.Race.Count == 0 && GUILayout.Button ( "No Race" , Slim, GUILayout.Width (50))) {
							
						}
						GUI.color = new Color (0.8f, 1f, 0.8f, 1);
						if ( DK_Race.Race.Count == 1 && GUILayout.Button ( DK_Race.Race[0] , Slim, GUILayout.Width (50))) {
							
						}
						if ( DK_Race.Race.Count > 1 && GUILayout.Button ( "Multi" , Slim, GUILayout.Width (50))) {
							
						}
						// Gender
						if ( DK_Race.Gender == "" ) GUI.color = new Color (0.9f, 0.5f, 0.5f);
						if ( DK_Race.Gender == "" ) GUILayout.Label ( "N" , "Button") ;
						GUI.color = new Color (0.8f, 1f, 0.8f, 1);
						if ( DK_Race.Gender != "" && DK_Race.Gender == "Female" ) GUILayout.Label ( "Female" , Slim, GUILayout.Width (50) );
						if ( DK_Race.Gender != "" && DK_Race.Gender == "Male" ) GUILayout.Label ( "Male" , Slim, GUILayout.Width (50) );
						if ( DK_Race.Gender != "" && DK_Race.Gender == "Both" ) GUILayout.Label ( "Both" , Slim, GUILayout.Width (50) );
						
						// OverlayType
						if ( _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.GetComponent<DK_Race>() as DK_Race == null ) {
							_SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].gameObject.AddComponent<DK_Race>();	
						}
						DK_Race = _SlotsAnatomyLibrary.dk_SlotsAnatomyElementList[i].GetComponent("DK_Race") as DK_Race;
						if ( DK_Race.OverlayType == "" ) GUI.color = new Color (0.9f, 0.5f, 0.5f);
						if ( DK_Race.OverlayType == "" && GUILayout.Button ( "No Race" , Slim, GUILayout.Width (50))) {
							
						}
						GUI.color = new Color (0.8f, 1f, 0.8f, 1);
						if ( DK_Race.OverlayType != "" && GUILayout.Button ( DK_Race.OverlayType , Slim, GUILayout.Width (50))) {
							
						}
					}
				}
			}
		}
		#endregion choose Places List

	}

}
