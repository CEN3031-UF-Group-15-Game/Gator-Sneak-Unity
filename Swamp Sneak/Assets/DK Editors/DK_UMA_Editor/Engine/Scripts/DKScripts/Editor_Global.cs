using UnityEngine;
//using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
[System.Serializable]
public class Editor_Global : MonoBehaviour {
	[System.Serializable]
	public static class Variables
	{
		public static GameObject UMACrowdObj;
		public static DK_UMACrowd DK_UMACrowd;
		public static GameObject DK_UMA;
		public static GameObject DKUMAGeneratorObj;
//		public static DKUMAGenerator DK_DKUMAGenerator;
		public static GameObject DKUMACustomizationObj;
		public static DKUMACustomization DK_DKUMACustomization;

		public static GameObject DKSlotLibraryObj;
		public static GameObject OverlayLibraryObj;
		public static GameObject RaceLibraryObj;


	
	}

	[System.Serializable]
	public static class Lists
	{
		public static List<ColorPreset> ColorPresetsList = new List<ColorPreset>();
		
	}

	public static int VersionW = 1;
	public static int VersionX = 0;
	public static int VersionY = 0;
	public static int VersionZ = 4;
	public static int UnityVersionW = 4;
	public static int UnityVersionX = 5;


}
