using UnityEngine;
# if Editor
using UnityEditor;
# endif
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class DKAutoDetect {
	// Detect Element Types
	public static bool AutoDetLib;
	public static bool DetRace;
	public static bool DetGender;
	public static bool DetPlace;
	public static bool DetOvType;
	public static bool DetWWeight;
	public static bool DetLink;
	public static string TmpName;
	public static bool DetSlots;
	public static bool DetOverlay;
	public static bool DetRaces;
	public static bool ApplyToEmpty;
	public static bool ApplyToSelection;
	public static bool ApplyToAll;
	public static string NewRaceName = "";
	public static bool RemoveRace = false;
	public static List<string> RaceToApplyList = new List<string>();
	public static bool SearchResultsOnly = false;


		
	public static void Prepare ( System.Type type, UnityEngine.Object Element ){
		Debug.Log ( "preparing" );

		TmpName = Element.name;
		if (type.GetType ().ToString () == "DKSlotData") {
			DKAutoDetect.SlotDetection ( Element );	
		}

	}

	public static void SlotDetection ( UnityEngine.Object Element ){
		DKSlotData _Data = Element as DKSlotData;

		Debug.Log ( "detect gender" );

		#region Gender
		if ( Element.name.Contains("Female")){
			_Data.Gender = "Female" ;
			TmpName = TmpName.Replace("Female", "");
		}
		else if ( Element.name.Contains("female")){
			_Data.Gender = "Female" ;
			TmpName = TmpName.Replace("female", "");
		}
		else if ( Element.name.Contains("Male")){
			_Data.Gender = "Male" ;
			TmpName = TmpName.Replace("Male", "");
		}
		else if ( Element.name.Contains("male")){
			_Data.Gender = "Male" ;
			TmpName = TmpName.Replace("male", "");
		}
		#endregion Gender

		#region races
		// Add races
		for (int i = 0; i < RaceToApplyList.Count ; i++)
		{
			if ( _Data.Race.Contains(RaceToApplyList[i]) == false )
				_Data.Race.Add(RaceToApplyList[i]);
		}
		#endregion races

		#region Expressions
		ExpressionLibrary Library = GameObject.Find ("Expressions").GetComponent<ExpressionLibrary>();
		for (int i = 0; i < Library.ExpressionList.Length ; i++)
		{
			if (TmpName.Contains( Library.ExpressionList[i].name )){
				_Data.Elem = Library.ExpressionList[i].Elem;
				_Data.OverlayType = Library.ExpressionList[i].OverlayType;
				_Data.Place = Library.ExpressionList[i].Place.GetComponent<DK_SlotsAnatomyElement>();
				_Data.Replace = Library.ExpressionList[i].Replace;
				_Data.WearWeight = Library.ExpressionList[i].WearWeight;
			}
		}
		#endregion Expressions

		// finishing
		DKAutoDetect.Finish ( Element );
	}

	public static void Finish ( UnityEngine.Object Element ){
		# if Editor
		EditorUtility.SetDirty(Element);
		AssetDatabase.SaveAssets();
		# endif
	}
}
