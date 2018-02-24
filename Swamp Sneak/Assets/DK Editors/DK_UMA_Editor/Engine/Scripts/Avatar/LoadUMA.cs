using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UMA;
using System;
using System.Reflection;

using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;

[ExecuteInEditMode]
#endif
public class LoadUMA : MonoBehaviour {
	public string _StreamedUMA;
//	UMAData umaData;
	public UMAAvatarBase avatar;
	public UMADnaHumanoid umaDna
//	= new UMADnaHumanoid()
		;
	public UMADnaBase _UMADnaBase;
	public DynamicUMADnaBase DynamicumaDna;

	DK_UMA_GameSettings _DK_UMA_GameSettings;

	[HideInInspector]public bool HideAtStart;

	[HideInInspector]
	public bool UpdateOnly = false;
	[HideInInspector]public bool MeshDirty = false;
	[HideInInspector]public bool ShapeDirty = false;
	[HideInInspector]public bool TextureDirty = false;
	[HideInInspector]public bool AtlasDirty = false;

	[HideInInspector]public bool DestroySelf = true;

	[HideInInspector]public bool AssignLayer = true;

	SlotLibrary SlotLib;
	OverlayLibrary OvLib;
	DK_RPG_UMA DKavatar;
	DKUMA_Variables _DKUMA_Variables;

	void OnEnable (){
		if ( GameObject.Find ("DK_UMA" ) == null ){
			InstallDKUMA ();
			InstallUMA ();
		}

		string racesPath = "Assets/DK Editors/DK_UMA_Content/Elements/Default/Races";
		_DKUMA_Variables = FindObjectOfType<DKUMA_Variables>();
		if ( _DK_UMA_GameSettings == null ) _DK_UMA_GameSettings = _DKUMA_Variables._DK_UMA_GameSettings;

		// Correct Avatar definition
		DKavatar = GetComponentInParent<DK_RPG_UMA>();
		if ( DKavatar == null ) Debug.LogError ("DKavatar == null");
		else if ( DKavatar.RaceData == null ) {
			#if UNITY_EDITOR
			if ( AssetDatabase.IsValidFolder (racesPath) == true ) {
				if ( _DKUMA_Variables != null ){
					Debug.Log ("DK UMA : Verifying the Libraries.");
					_DKUMA_Variables.VerifyLibraries ();
				}
			}
			#endif
			// verify RaceLibrary of the scene
			DKRaceLibrary racelib = FindObjectOfType<DKRaceLibrary>();
			if ( racelib.raceElementList.Length == 0 ) {
				#if UNITY_EDITOR
				if ( AssetDatabase.IsValidFolder (racesPath) == true ) {
					Debug.LogError ("DK UMA Error : The RaceLibrary of the scene is umpty. Open the 'Elements Manager' and click on the 'Add to Libraries' button.");
				
				}
				#endif
			}
			// verify if the list have some missing entry
			else {
				foreach ( DKRaceData race in racelib.raceElementList ){
					if ( race != null && race.Race == DKavatar.Race && race.Gender == DKavatar.Gender ){
						DKavatar.RaceData = race;
					}
				}
			}
			if ( DKavatar.RaceData == null ) {
				#if UNITY_EDITOR
				if ( AssetDatabase.IsValidFolder (racesPath) == true ) {
					Debug.LogError ("DK UMA Error : The RaceLibrary of the scene does not contain the race of this avatar '"+DKavatar.Race+"' of gender '"+DKavatar.Gender+"'. Trying to find it in the Race database of the project...");
				}
				#endif
				// verify RaceLibrary of the project
				if ( _DK_UMA_GameSettings._GameLibraries.DkRacesLibrary.Count == 0 ) {
					#if UNITY_EDITOR
					if ( AssetDatabase.IsValidFolder (racesPath) == true ) {
						Debug.LogError ("DK UMA Error : The RaceLibrary of the project is umpty. Open the 'Elements Manager' and click on the 'Add to Libraries' button.");
					}
					#endif
				}
				// verify if the list have some missing entry
				else {
					foreach ( DKRaceData race in _DK_UMA_GameSettings._GameLibraries.DkRacesLibrary ){
						if ( race != null && race.Race == DKavatar.Race && race.Gender == DKavatar.Gender ){
								DKavatar.RaceData = race;							
						}
					}
				}
			}
			if ( DKavatar.RaceData == null ) {
				#if UNITY_EDITOR
				if ( AssetDatabase.IsValidFolder (racesPath) == true ) {
					Debug.LogError ("DK UMA Error : The RaceLibrary of the project and the RaceLibrary of the scene does not" +
						"contain the race of this avatar. Trying to find the DKUMARace in the project...");
					TryToFindDKRaceInProject ();
				}
				#endif
			}
			if ( DKavatar.RaceData == null ) {
				#if UNITY_EDITOR
				if ( AssetDatabase.IsValidFolder (racesPath) == true ) {
					Debug.LogError ("DK UMA Error : The race of this avatar '"+DKavatar.Race+"' of gender '"+DKavatar.Gender+"' is not present in the project. " +
						"You have to import it.");
				}
				#endif
			}
		}
		#if UNITY_EDITOR
		if ( AssetDatabase.IsValidFolder (racesPath) == false ) {
			Debug.LogError ("DK UMA Install : No DK UMA Content detected ! Import the Default Content or your own Packed DK UMA Content using the Install window.");
		}
		#endif
		if ( DKavatar.RaceData != null ) {
			if ( DKavatar.Race != DKavatar.RaceData.Race ) DKavatar.Race = DKavatar.RaceData.Race;
			if ( DKavatar.Gender != DKavatar.RaceData.Gender ) DKavatar.Gender = DKavatar.RaceData.Gender;
			if ( DKavatar.DkUmaStreaming.Contains ("packedSlotDataList") == false ) DKavatar.Rebuild ();

			// Preview avatar
			#if UNITY_EDITOR
			// Load from Avatar Data if no Saved file present
			if ( DKavatar != null ){
				if ( DKavatar.LoadFromFile == false || DKavatar.FileName == "" ){
					if (DKavatar.AvatarFromDB != null ){
						// get definition
						DKavatar.SavedRPGStreamed = DKavatar.AvatarFromDB.StreamedAvatar;
						// load the avatar
						DKavatar.LoadRPGStreamed ();
					}
				}
				// load from file
				else {
					if ( DKavatar.FileName.Contains(".uml") )
						DKavatar.LoadFile ( DKavatar.FileName );
					else
						DKavatar.LoadFile ( DKavatar.FileName+".uml" );
				}
			}

			if ( GetComponent<PreviewAvatar>() == null )
				gameObject.AddComponent<PreviewAvatar>();
			if ( GetComponent<PreviewAvatar>() != null
				&& GetComponent<PreviewAvatar>().NewPreview != null 
				&& EditorApplication.isPlaying == true
				&& GetComponent<PreviewAvatar>().DontDestroyAtStart == false ) 
				DestroyImmediate ( GetComponent<PreviewAvatar>().NewPreview );
			else if ( GetComponent<PreviewAvatar>().NewPreview != null
				&& GetComponent<PreviewAvatar>().DontDestroyAtStart == false ){ 
				DestroyImmediate ( GetComponent<PreviewAvatar>().NewPreview );
				GetComponent<PreviewAvatar>().GeneratePreview ();

			}
			if ( EditorApplication.isPlaying == true ){
				Invoke ( "Loading" , 0.05f );
				InvokeRepeating ( "Testing" , 0.055f, 0.05f );
			}

			#else
			if ( GetComponent<PreviewAvatar>() != null 
			&& GetComponent<PreviewAvatar>().NewPreview != null
			&& GetComponent<PreviewAvatar>().DontDestroyAtStart == false ) {
				DestroyImmediate ( GetComponent<PreviewAvatar>().NewPreview );

				Invoke ( "Loading" , 0.05f );
				InvokeRepeating ( "Testing" , 0.055f, 0.05f );
			}
			else if ( GetComponent<PreviewAvatar>() == null ) {				
			Invoke ( "Loading" , 0.05f );
			InvokeRepeating ( "Testing" , 0.055f, 0.05f );
			}
			#endif

			// delete DK UMA1 old bones
			if ( transform.parent.FindChildIncludingDeactivated ( "Male_Unified" ) )
				DestroyImmediate (transform.parent.FindChildIncludingDeactivated ( "Male_Unified" ).gameObject);
			else if ( transform.parent.FindChildIncludingDeactivated ( "UMA_Human_Female" ) )
				DestroyImmediate (transform.parent.FindChildIncludingDeactivated ( "UMA_Human_Female" ).gameObject);
			else if ( transform.parent.FindChildIncludingDeactivated ( "" ) )
				DestroyImmediate (transform.parent.FindChildIncludingDeactivated ( "" ).gameObject);
		}
	}

	void InstallDKUMA (){
		DKUMACorrectors.InstallDKUMA ();
	}

	void InstallUMA (){
		DKUMACorrectors.InstallUMA ();
	}

	#if UNITY_EDITOR
	void TryToFindDKRaceInProject (){
		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:DKRaceData", lookFor);

		foreach (string guid in guids2) {
			string path =  AssetDatabase.GUIDToAssetPath(guid).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			//	Debug.Log (path);
			DKRaceData race = (DKRaceData)AssetDatabase.LoadAssetAtPath(path, typeof(DKRaceData));
				
			if ( race.Race == DKavatar.Race && race.Gender == DKavatar.Gender ) {
				DKavatar.RaceData = race;
				Debug.LogError ("DK UMA Fix : The race of the avatar have been found in the project and assigned to the avatar." +
					" But the RaceLibrary of the scene and the project does not contain the race of this avatar, so you still have to "+
					"open the 'Elements Manager' and click on the 'Add to Libraries' button to populate them.");
			}
		}
	}
	#endif

	void Loading (){
		if (  Application.isPlaying ){
			avatar = null;

			var selectedTransform = transform;
			avatar = selectedTransform.GetComponent<UMAAvatarBase>();
			
			while (avatar == null && selectedTransform.parent != null){
				selectedTransform = selectedTransform.parent;
				avatar = selectedTransform.GetComponent<UMAAvatarBase>();
			}
			if (avatar != null ){
				var asset = ScriptableObject.CreateInstance<UMATextRecipe>();
				asset.recipeString = _StreamedUMA;
			//	Debug.Log (_StreamedUMA);
				if ( avatar.umaData == null ) 
					avatar.umaData = gameObject.AddComponent<UMAData>();
				if ( avatar.umaData.umaRecipe == null ) 
					avatar.umaData.umaRecipe = new UMAData.UMARecipe();


				if ( _DK_UMA_GameSettings.Debugger ) Debug.Log ("DK UMA LoadUMA : Generating UMA Avatar.");
				avatar.Load(asset);
			}
		}
	}

	void Testing (){
		if (  avatar.umaData != null ){

			ApplyDNA();

			if ( !AssignLayer ) CancelInvoke();

			if ( AssignLayer && avatar.GetComponentInChildren<SkinnedMeshRenderer>() != null ){
				if ( HideAtStart ) avatar.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
				else avatar.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
				avatar.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.layer = gameObject.layer;
				CancelInvoke();
				Finish ();
			}
		}
	}

	public void ApplyDNA (){
	//	if ( _DK_UMA_GameSettings.Debugger ) Debug.Log ("DK UMA LoadUMA : Apply DNA : launch.");

		avatar.umaData.umaRecipe.ClearDna();
		avatar.umaData.umaRecipe.AddDna(umaDna);

	//	ApplyDNAOnebyOne (avatar.umaData);

		// pack UMA recipe
		if ( _DK_UMA_GameSettings.Debugger ) Debug.Log ("DK UMA LoadUMA : Apply DNA.");
		avatar.umaRecipe.Save( avatar.umaData.umaRecipe, avatar.context );

		if ( !AssignLayer ) Finish ();

	}

	public void ApplyPreviewDNA ( UMAData umaData ){
	//	Debug.Log ("ApplyPreviewDNA");


		umaData.umaRecipe.ClearDna();
		umaData.umaRecipe.AddDna(umaDna);
	
		umaData.umaRecipe.ApplyDNA (umaData);

		// pack UMA recipe
	//	avatar.umaRecipe.Save( avatar.umaData.umaRecipe, avatar.context );

	//	if ( !AssignLayer ) Finish ();

	//	ApplyDNAOnebyOne (umaData);

	}

	public void ApplyDNAOnebyOne ( UMAData umaData ){
		// get DNA type
	/*	UMADnaBase[] allDna = umaData.GetAllDna();
		int[] _dnaTypeHashes;
		Type[] _dnaTypes;
		string[] _dnaTypeNames;
		int viewDna = 0;
		UMAData.UMARecipe recipe;

		_dnaTypes = new Type[allDna.Length];
		//DynamicUMADna:: we need the hashes here too
		_dnaTypeHashes = new int[allDna.Length];
		_dnaTypeNames = new string[allDna.Length];

		for (int i = 0; i < allDna.Length; i++)
		{
			var entry = allDna[i];
			var entryType = entry.GetType();

			_dnaTypes[i] = entryType;
			//DynamicUMADna:: we need to use typehashes now
			_dnaTypeHashes[i] = entry.DNATypeHash;
			if (entry is DynamicUMADnaBase)
			{
				var dynamicDna = entry as DynamicUMADnaBase;
				if (dynamicDna.dnaAsset != null)
				{
					_dnaTypeNames[i] = dynamicDna.dnaAsset.name + " (DynamicUMADna)";
				}
			}
			else
			{
				_dnaTypeNames[i] = entryType.Name;
			}
			_dnaValues[entry.DNATypeHash] = new DNASingleEditor(entry);
		}
*/


	//	UMADynamicAvatar avatar = 
		if ( umaData.umaRecipe.dnaValues.Count == 0 ) Debug.LogError ("umaData.umaRecipe.dnaValues.Count = 0");
		for (int i = 0; i <  umaData.umaRecipe.dnaValues[0].Names.Length; i ++) {
		//	Debug.Log ( umaData.umaRecipe.dnaValues[0].Names[i]+" : "+umaData.umaRecipe.dnaValues[0].Values[i].ToString() );
			if ( umaData.umaRecipe.dnaValues[0].Names[i] == "height" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.height);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "headSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.headSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "headWidth" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.headWidth);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "neckThickness" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.neckThickness);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "handsSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.handsSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "feetSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.feetSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "legSeparation" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.legSeparation);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "waist" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.waist);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "upperMuscle" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.upperMuscle);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "upperWeight" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.upperWeight);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "lowerMuscle" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.lowerMuscle);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "lowerWeight" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.lowerWeight);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "belly" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.belly);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "legsSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.legsSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "gluteusSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.gluteusSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "earsSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.earsSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "earsPosition" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.earsPosition);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "earsRotation" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.earsRotation);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "noseSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.noseSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "noseCurve" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.noseCurve);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "noseWidth" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.noseWidth);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "noseInclination" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.noseInclination);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "nosePosition" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.nosePosition);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "nosePronounced" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.nosePronounced);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "noseFlatten" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.noseFlatten);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "chinSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.chinSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "chinPronounced" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.chinPronounced);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "chinPosition" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.chinPosition);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "mandibleSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.mandibleSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "jawsSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.jawsSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "jawsPosition" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.jawsPosition);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "cheekSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.cheekSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "cheekPosition" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.cheekPosition);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "lowCheekPronounced" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.lowCheekPronounced);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "lowCheekPosition" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.lowCheekPosition);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "foreheadSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.foreheadSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "foreheadPosition" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.foreheadPosition);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "lipsSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.lipsSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "mouthSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.mouthSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "eyeRotation" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.eyeRotation);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "eyeSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.eyeSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "breastSize" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.breastSize);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "armLength" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.armLength);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "forearmLength" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.forearmLength);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "armWidth" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.armWidth);
			else if ( umaData.umaRecipe.dnaValues[0].Names[i] == "forearmWidth" ) umaData.umaRecipe.dnaValues[0].SetValue(i, umaDna.forearmWidth);

		//	umaData.ApplyDNA ();
		
			//	umaData.umaRecipe.ApplyDNA (umaData);
		
			//	umaData.umaRecipe.ApplyDNA (umaData, true);
		
			//	umaData.Dirty(true, false, false);

		/*	var selectedTransform = transform;
			avatar = selectedTransform.GetComponent<UMAAvatarBase>();
			avatar.umaData = umaData;

			avatar.umaRecipe.Save( avatar.umaData.umaRecipe, avatar.context );
			*/
		}

		//	foreach ( string name in umaData.umaRecipe.dnaValues[0] )
		//	Debug.Log ( name );
	
	}

	void Finish (){
	//	Debug.Log ("DK UMA LoadUMA : Avatar Finished.");
		if ( DestroySelf ) Destroy(this);
		else this.enabled = false;
	}
}
