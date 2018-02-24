using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class EditorConvertAvatar : MonoBehaviour {

	public static void ConvertAvatar (){
		DKUMAData _DKUMAData = Selection.activeGameObject.GetComponent<DKUMAData>();
		
		#region by string
		string StreamedUMA;
	//	string newStreamedUMA;
		
		// find the StreamedUMA
		StreamedUMA = _DKUMAData.streamedUMA;
		
		// Translate compressions
		StreamedUMA = StreamedUMA.Replace(@"sID","slotID");
		StreamedUMA = StreamedUMA.Replace ( @"oS" , "overlayScale" );
		StreamedUMA = StreamedUMA.Replace ( @"cOI" , "copyOverlayIndex" );
		StreamedUMA = StreamedUMA.Replace ( @"ODL" , "OverlayDataList" );
		StreamedUMA = StreamedUMA.Replace ( @"oID" , "overlayID" );
		StreamedUMA = StreamedUMA.Replace ( @"rL" , "rectList" );
		
		StreamedUMA = StreamedUMA.Replace ( @"cL" , "colorList" );
		StreamedUMA = StreamedUMA.Replace ( @"cML" , "channelMaskList" );
		StreamedUMA = StreamedUMA.Replace ( @"cAML" , "channelAdditiveMaskList" );
		StreamedUMA = StreamedUMA.Replace ( @"DKUMADnaHumanoid" , "UMADnaHumanoid" );
		// About Race
		// is a female
		if ( _DKUMAData.myRenderer.name.Contains("Female")) {
			StreamedUMA = StreamedUMA.Replace ( @""+_DKUMAData.umaRecipe.raceData.raceName+"" , "HumanFemale" );
		}
		else {
			StreamedUMA = StreamedUMA.Replace ( @""+_DKUMAData.umaRecipe.raceData.raceName+"" , "HumanMale" );
		}
		// Modify DNA names
		StreamedUMA = StreamedUMA.Replace ( @"N0\" , "height\\" );
		StreamedUMA = StreamedUMA.Replace ( @"N10" , "legSeparation" );
		StreamedUMA = StreamedUMA.Replace ( @"N11" , "upperMuscle" );
		StreamedUMA = StreamedUMA.Replace ( @"N12" , "lowerMuscle" );
		StreamedUMA = StreamedUMA.Replace ( @"N13" , "upperWeight" );
		StreamedUMA = StreamedUMA.Replace ( @"N14" , "lowerWeight" );
		StreamedUMA = StreamedUMA.Replace ( @"N15" , "legsSize" );
		StreamedUMA = StreamedUMA.Replace ( @"N16" , "belly" );
		StreamedUMA = StreamedUMA.Replace ( @"N17" , "waist" );
		StreamedUMA = StreamedUMA.Replace ( @"N18" , "gluteusSize" );
		StreamedUMA = StreamedUMA.Replace ( @"N19" , "earsSize" );
		
		StreamedUMA = StreamedUMA.Replace ( @"N1\" , "headSize\\" );
		
		StreamedUMA = StreamedUMA.Replace ( @"N20" , "earsPosition" );
		StreamedUMA = StreamedUMA.Replace ( @"N21" , "earsRotation" );
		StreamedUMA = StreamedUMA.Replace ( @"N22" , "noseSize" );
		StreamedUMA = StreamedUMA.Replace ( @"N23" , "noseCurve" );
		StreamedUMA = StreamedUMA.Replace ( @"N24" , "noseWidth" );
		StreamedUMA = StreamedUMA.Replace ( @"N25" , "noseInclination" );
		StreamedUMA = StreamedUMA.Replace ( @"N26" , "nosePosition" );
		StreamedUMA = StreamedUMA.Replace ( @"N27" , "nosePronounced" );
		StreamedUMA = StreamedUMA.Replace ( @"N28" , "noseFlatten" );
		StreamedUMA = StreamedUMA.Replace ( @"N29" , "chinSize" );
		
		StreamedUMA = StreamedUMA.Replace ( @"N2\" , "headWidth\\" );
		
		StreamedUMA = StreamedUMA.Replace ( @"N30" , "chinPronounced" );
		StreamedUMA = StreamedUMA.Replace ( @"N31" , "chinPosition" );
		StreamedUMA = StreamedUMA.Replace ( @"N32" , "mandibleSize" );
		StreamedUMA = StreamedUMA.Replace ( @"N33" , "jawsSize" );
		StreamedUMA = StreamedUMA.Replace ( @"N34" , "jawsPosition" );
		StreamedUMA = StreamedUMA.Replace ( @"N35" , "cheekSize" );
		StreamedUMA = StreamedUMA.Replace ( @"N36" , "cheekPosition" );
		StreamedUMA = StreamedUMA.Replace ( @"N37" , "lowCheekPronounced" );
		StreamedUMA = StreamedUMA.Replace ( @"N38" , "lowCheekPosition" );
		StreamedUMA = StreamedUMA.Replace ( @"N39" , "foreheadSize" );
		
		StreamedUMA = StreamedUMA.Replace ( @"N3\" , "neckThickness\\" );
		
		StreamedUMA = StreamedUMA.Replace ( @"N40" , "foreheadPosition" );
		StreamedUMA = StreamedUMA.Replace ( @"N41" , "lipsSize" );
		StreamedUMA = StreamedUMA.Replace ( @"N42" , "mouthSize" );
		StreamedUMA = StreamedUMA.Replace ( @"N43" , "eyeRotation" );
		StreamedUMA = StreamedUMA.Replace ( @"N44" , "eyeSize" );
		StreamedUMA = StreamedUMA.Replace ( @"N45" , "breastSize" );
		//		StreamedUMA = StreamedUMA.Replace ( "N46" , "" );
		//		StreamedUMA = StreamedUMA.Replace ( "N47" , "" );
		//		StreamedUMA = StreamedUMA.Replace ( "N48" , "" );
		//		StreamedUMA = StreamedUMA.Replace ( "N49" , "" );
		
		StreamedUMA = StreamedUMA.Replace ( @"N4\" , "armLength\\" );
		StreamedUMA = StreamedUMA.Replace ( @"N5\" , "forearmLength\\" );
		StreamedUMA = StreamedUMA.Replace ( @"N6\" , "armWidth\\" );
		StreamedUMA = StreamedUMA.Replace ( @"N7\" , "forearmWidth\\" );
		StreamedUMA = StreamedUMA.Replace ( @"N8\" , "handsSize\\" );
		StreamedUMA = StreamedUMA.Replace ( @"N9\" , "feetSize\\" );
		
		// Open the save window
		var path = EditorUtility.SaveFilePanel("Save serialized Avatar","", _DKUMAData.transform.parent.name+".txt","txt");
		// Save Avatar
		if(path.Length != 0) {
			System.IO.File.WriteAllText(path, StreamedUMA);
		}
		#endregion by string
		
	}

}
