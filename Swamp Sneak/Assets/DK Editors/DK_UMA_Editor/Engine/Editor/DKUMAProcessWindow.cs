using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class DKUMAProcessWindow : EditorWindow {

	public static string ProcessName = "";
	public static string myPath = "";
	public static float ProcessNumber = 0;
	public static float ProcessCurrent = 0;

	public static bool Processing = false;
	public static bool Launching = false;
	public int Launch = 0;

	void CloseSelf(){
		try{ 
			this.Close();
		}
		catch (NullReferenceException ) {
			
		}

	}
	public static void AddAssetsProcess()
	{
		DKUMAProcessWindow.ProcessName = "Create Prefabs";
		DKUMAProcessWindow.Processing = true;
		DKUMAProcessWindow.ProcessCurrent = 0;
		DKUMAProcessWindow.ProcessNumber = EditorVariables.MFSelectedList.childCount;

		DKUMAProcessWindow.Launching = true;

	}
	  void AddAssets()
	{
		if ( EditorVariables.MFSelectedList != null ) 
			foreach (Transform Model in EditorVariables.MFSelectedList.transform)
		{
			DKUMAProcessWindow.ProcessCurrent = DKUMAProcessWindow.ProcessCurrent +1;
			if ( PrefabUtility.GetPrefabParent( Model.gameObject ) != null )
				myPath = PrefabUtility.GetPrefabParent( Model.gameObject ).ToString() ;
			else myPath = "null";
			// Create Prefab
			try{ if ( myPath == "null" ) 
				{
					if ( Model.GetComponent<DK_Model>() == true ){
						PrefabUtility.CreatePrefab("Assets/DK Editors/DK_UMA_Editor/Prefabs/Models/" + Model.name + ".prefab", Model.gameObject,ReplacePrefabOptions.ConnectToPrefab );
					}
					else
					if ( Model.GetComponent<ModelGrp>() == true ){
						PrefabUtility.CreatePrefab("Assets/DK Editors/DK_UMA_Editor/Prefabs/Models/Groups/" + Model.name + ".prefab", Model.gameObject,ReplacePrefabOptions.ConnectToPrefab );
					}
				}
			}
			catch (NullReferenceException ) {
			}
			if ( DKUMAProcessWindow.ProcessCurrent == EditorVariables.MFSelectedList.childCount ) {
				DKUMAProcessWindow.Processing = false;
			}
		}
	}
	void OnGUI () {
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
		this.Focus();
		if ( Launching ){
			Launch = Launch +1;
		}
	
		if ( Launch >= 25 ) {
			Launching = false;
			Launch = 0;
			AddAssets();
		}
		using (new HorizontalCentered()) {
			GUILayout.Label ( ProcessName, bold, GUILayout.ExpandWidth (true));

		}
		using (new HorizontalCentered()) {
			GUILayout.Label ( ProcessNumber.ToString()+" Actions to do, please wait ...", GUILayout.ExpandWidth (false));
		}

		if ( !Processing ){
			CloseSelf();
		}
		if ( ProcessCurrent == ProcessNumber ){
			CloseSelf();
		}
	}

}
