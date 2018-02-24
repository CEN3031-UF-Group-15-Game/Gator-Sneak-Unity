using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
#if UNITY_5_5_OR_NEWER
#else
//using LitJson;
#endif
using UMA;

public class UMAConvAvatarWin : EditorWindow {
	#region Variables
	Vector2 scroll;
	Vector2 scroll1;
	Vector2 scroll2;
	Color Green = new Color (0.8f, 1f, 0.8f, 1);
	Color Red = new Color (0.9f, 0.5f, 0.5f);
	string tmpStreamedUMA = "";
	string StreamedUMA = "";
	string DKRace = "";
	string raceName = "";
	string Gender = "";
	bool Helper = false;
	bool ShowUMA = false;
	bool ShowDKUMA = false;
	float DNAValue;
	GameObject NewAvatarGo;

	#endregion Variables

	void OnGUI () {
		this.minSize = new Vector2(340, 500);
		
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
		
		#region Menu
		using (new Horizontal()) {
			GUILayout.Label("Convert UMA Avatar", "toolbarbutton", GUILayout.ExpandWidth (true));
			GUI.color = Red;
			if ( Helper ) GUI.color = Green;
			else GUI.color = Color.yellow;
			if ( GUILayout.Button ( "?", "toolbarbutton", GUILayout.ExpandWidth (false))) {
				if ( Helper ) Helper = false;
				else Helper = true;
			}
		}
		if ((Selection.activeGameObject 
		     && (Selection.activeGameObject.GetComponentInParent<UMA.UMAData> () != null
		    || Selection.activeGameObject.GetComponentInParent<UMADynamicAvatar> () != null
		    || Selection.activeGameObject.GetComponent<UMADynamicAvatar> () != null )
		     && Selection.activeGameObject.GetComponentInParent<DKUMAData> () == null ))
		{
			GUI.color = Color.white;
			if ( tmpStreamedUMA == "" ) GUILayout.TextField("First the converter gets the definition of the UMA avatar." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			if ( Selection.activeGameObject.GetComponentInParent<UMADynamicAvatar> () != null ){
				UMADynamicAvatar umaDynamicAvatar = Selection.activeGameObject.GetComponentInParent<UMADynamicAvatar> ();
				GUI.color = Green;
				if( tmpStreamedUMA == "" && GUILayout.Button("1- Find Avatar Streamed")){
					StreamedUMA = "";
					raceName = "";
					var asset = ScriptableObject.CreateInstance<UMATextRecipe>();
					try{
						asset.Save(umaDynamicAvatar.umaData.umaRecipe, umaDynamicAvatar.context);
						tmpStreamedUMA = asset.recipeString;
						ScriptableObject.Destroy(asset);
					}
					catch(NullReferenceException){
						Debug.LogError ("The UMA Avatar has no Values. You have to be in playmode to convert a UMA Avatar to DK UMA.");
					
					}
				}
				GUI.color = Color.white;
				if ( tmpStreamedUMA != "" && StreamedUMA == "" &&  DKRace == "" && Helper ) 
					GUILayout.TextField("select the basic settings for DK UMA to know the gender and the race of the avatar." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));


				if( tmpStreamedUMA != "" && StreamedUMA == "" && raceName == "" ){
					DKRaceLibrary _RaceLibrary = GameObject.Find("DKUMACrowd").GetComponent<DK_UMACrowd>().raceLibrary;
					GUI.color = Color.yellow;
					GUILayout.TextField("2- Select a gender and a race for the new DK Avatar :" , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					using (new Horizontal()) {
						if ( Gender == "Male" ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Male", GUILayout.ExpandWidth (true))) {
							Gender = "Male";
						}
						if ( Gender == "Female" ) GUI.color = Green;
						else GUI.color = Color.gray;
						if ( GUILayout.Button ( "Female", GUILayout.ExpandWidth (true))) {
							Gender = "Female";
						}
					}

					if ( DKRace != "" && Gender != "" ) 
					using (new HorizontalCentered()) GUILayout.TextField( DKRace , 50, bold, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					using (new Horizontal()) {
						GUI.color = Green;
						if ( DKRace != "" && Gender != "" && GUILayout.Button ( "3- Accept", GUILayout.ExpandWidth (true))) {

							for(int i2 = 0; i2 < _RaceLibrary.raceElementList.Length; i2 ++){
								if ( _RaceLibrary.raceElementList[i2].Race == DKRace 
								    && _RaceLibrary.raceElementList[i2].Gender == Gender ) 
								{
									raceName = _RaceLibrary.raceElementList[i2].raceName;
								}
							}
						}
					}

					GUI.color = Color.yellow;
					if ( Gender != "" ) GUILayout.TextField("Remember : the selected race applies its DNA to the converted avatar." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

					GUILayout.Space(5);
					if ( _RaceLibrary && Gender != "" ) using (new ScrollView(ref scroll)) 
					{
						List<string> _RacesList = new List<string>();
						for(int i = 0; i < _RaceLibrary.raceElementList.Length; i ++){
							try{
								if ( _RacesList.Contains(_RaceLibrary.raceElementList[i].Race) == false 
								    && _RaceLibrary.raceElementList[i].Active ){
									_RacesList.Add(_RaceLibrary.raceElementList[i].Race);
								}
							}catch(NullReferenceException){}
						}
						for(int i = 0; i < _RacesList.Count; i ++){
							using (new Horizontal()) {
								if ( DKRace == _RacesList[i] ) GUI.color = Color.yellow;
								else GUI.color = Color.white;
								if (GUILayout.Button ( _RacesList[i], "toolbarbutton", GUILayout.ExpandWidth (true))) {
									DKRace = _RacesList[i];
								}
							}
						}
					}

				}

				if( tmpStreamedUMA != "" && StreamedUMA == "" && raceName != "" ){
					if ( tmpStreamedUMA != "" && StreamedUMA == "" && Helper ) 
						GUI.color = Color.white;
						GUILayout.TextField("Now the converter is ready to modify the definition of the avatar. In this first version of the converter, it reads the UMA streamed of the avatar, then it changes the necessary fields and the new definition is created for DK UMA. " +
							"In the next version, the process will get the avatar information then generate the new avatar using the DK UMA tools." , 350, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					

					using (new Horizontal()) {
						GUILayout.Label("Race :", GUILayout.ExpandWidth (false));
						GUILayout.TextField( raceName , 50, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

					}
					GUI.color = Green;
					if ( GUILayout.Button("4- Convert Avatar")){
						ConvertStreamedUMA();
					}
				}



				if( tmpStreamedUMA != "" && StreamedUMA != "" && raceName != "" ){
					GUI.color = Color.white;
					if ( Helper ) GUILayout.TextField("You can save the new definition to a text file." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

					using (new Horizontal()) {
						GUI.color = Green;
						if ( GUILayout.Button("5a- Save to a text file")){
							var path = EditorUtility.SaveFilePanel("Save serialized Avatar", "Assets", Selection.activeGameObject.name + ".txt", "txt");
							System.IO.File.WriteAllText(path, StreamedUMA);
						}

					}

					GUI.color = Color.white;
					if ( NewAvatarGo == null && Helper ) GUILayout.TextField("Now the converted DK avatar can be generated. The DNA convertion may have some issues, you can correct the avatar proportions in the prepare panel of the Editor." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
					
					using (new Horizontal()) {
						GUI.color = Green;
						if ( NewAvatarGo == null && GUILayout.Button("5b- Generate the new Avatar")){

							// instantiate the prefab
							GameObject NewAvatar = Instantiate( Resources.Load("NewDKAvatar", typeof(GameObject)) ) as GameObject;
							NewAvatar.name = "NewDKAvatar";

							// add the definition
							DKUMAData _DKUMAData = NewAvatar.GetComponentInChildren<DKUMAData>();
							_DKUMAData.streamedUMA = StreamedUMA;

							// Launch the generation process
							ApplyDNA(_DKUMAData, StreamedUMA, NewAvatar);
						//	_DKUMAData.enabled = true;
						}
					}
					GUI.color = Color.white;
					if ( NewAvatarGo != null && Helper ){
						GUILayout.TextField("Create a prefab to be able to use your new avatar in the Editor mode and to get it back in the Game mode." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
						GUILayout.TextField("The new prefab is stored in the Assets/DK Editors/DK_UMA_Editor/Prefabs/Models/ folder." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
						GUI.color = Green;
						GUILayout.TextField("You can manipulate the prefabs of the avatars using the DK Browser." , 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

					}


					using (new Horizontal()) {
						GUI.color = Green;
						if ( NewAvatarGo != null && GUILayout.Button("5c- Create a prefab")){
							CreatePrefab();

						}
					}
				}

				using (new Horizontal()) {
					if ( ShowUMA == true ) GUI.color = Color.yellow;
					else GUI.color = Color.gray;
					if ( tmpStreamedUMA != "" && GUILayout.Button ( "Show UMA Streamed", "toolbarbutton", GUILayout.ExpandWidth (true))) {
						if ( ShowUMA == true ) ShowUMA = false;
						else ShowUMA = true;
					}
				}
				if ( ShowUMA == true ) using (new ScrollView(ref scroll1)) 
					GUILayout.TextField(tmpStreamedUMA , 6000, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

				using (new Horizontal()) {
					if ( ShowDKUMA == true ) GUI.color = Color.yellow;
					else GUI.color = Color.gray;
					if ( StreamedUMA != "" && GUILayout.Button ( "Show DK Streamed", "toolbarbutton", GUILayout.ExpandWidth (true))) {
						if ( ShowDKUMA == true ) ShowDKUMA = false;
						else ShowDKUMA = true;
					}
				}
				if ( ShowDKUMA == true ) using (new ScrollView(ref scroll2)) 
					GUILayout.TextField(StreamedUMA , 6000, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			}
		}
		#endregion Menu
	}

	void ConvertStreamedUMA(){
		StreamedUMA = tmpStreamedUMA;
		// Translate compressions
		StreamedUMA = StreamedUMA.Replace ( @"slotID","sID");
		StreamedUMA = StreamedUMA.Replace ( @"overlayScale","oS");
		StreamedUMA = StreamedUMA.Replace ( @"copyOverlayIndex","cOI" );
		StreamedUMA = StreamedUMA.Replace ( @"OverlayDataList","ODL" );
		StreamedUMA = StreamedUMA.Replace ( @"overlayID","oID" );
		StreamedUMA = StreamedUMA.Replace ( @"colorList","cL" );
		StreamedUMA = StreamedUMA.Replace ( @"rectList","rL" );

		StreamedUMA = StreamedUMA.Replace ( @"channelMaskList","cML" );
		StreamedUMA = StreamedUMA.Replace ( @"channelAdditiveMaskList","cAML" );
		StreamedUMA = StreamedUMA.Replace ( @"UMADnaHumanoid","DKUMADnaHumanoid" );

		// About Race TODO
		// is a female
	//	if ( _DKUMAData.myRenderer.name.Contains("Female")) {
		StreamedUMA = StreamedUMA.Replace ( @"HumanFemale",""+raceName+"" );
	//		Debug.Log ( "Female" );
	//	}
	//	else {
		StreamedUMA = StreamedUMA.Replace ( @"HumanMale",""+raceName+"" );
	//		Debug.Log ( "Male" );
			
	//	}

		// Modify DNA names
		StreamedUMA = StreamedUMA.Replace ( @"height","N0"  );
		StreamedUMA = StreamedUMA.Replace ( @"legSeparation","N10" );
		StreamedUMA = StreamedUMA.Replace ( @"upperMuscle","N11" );
		StreamedUMA = StreamedUMA.Replace ( @"lowerMuscle","N12" );
		StreamedUMA = StreamedUMA.Replace ( @"upperWeight","N13" );
		StreamedUMA = StreamedUMA.Replace ( @"lowerWeight","N14" );
		StreamedUMA = StreamedUMA.Replace ( @"legsSize","N15" );
		StreamedUMA = StreamedUMA.Replace ( @"belly","N16" );
		StreamedUMA = StreamedUMA.Replace ( @"waist","N17" );
		StreamedUMA = StreamedUMA.Replace ( @"gluteusSize","N18" );
		StreamedUMA = StreamedUMA.Replace ( @"earsSize","N19" );
		
		StreamedUMA = StreamedUMA.Replace ( @"headSize","N1" );
		
		StreamedUMA = StreamedUMA.Replace ( @"earsPosition","N20" );
		StreamedUMA = StreamedUMA.Replace ( @"earsRotation","N21" );
		StreamedUMA = StreamedUMA.Replace ( @"noseSize","N22" );
		StreamedUMA = StreamedUMA.Replace ( @"noseCurve","N23" );
		StreamedUMA = StreamedUMA.Replace ( @"noseWidth","N24" );
		StreamedUMA = StreamedUMA.Replace ( @"noseInclination","N25" );
		StreamedUMA = StreamedUMA.Replace ( @"nosePosition","N26" );
		StreamedUMA = StreamedUMA.Replace ( @"nosePronounced","N27" );
		StreamedUMA = StreamedUMA.Replace ( @"noseFlatten","N28" );
		StreamedUMA = StreamedUMA.Replace ( @"chinSize","N29" );
		
		StreamedUMA = StreamedUMA.Replace ( @"headWidth","N2" );
		
		StreamedUMA = StreamedUMA.Replace ( @"chinPronounced","N30" );
		StreamedUMA = StreamedUMA.Replace ( @"chinPosition","N31" );
		StreamedUMA = StreamedUMA.Replace ( @"mandibleSize","N32" );
		StreamedUMA = StreamedUMA.Replace ( @"jawsSize","N33" );
		StreamedUMA = StreamedUMA.Replace ( @"jawsPosition","N34" );
		StreamedUMA = StreamedUMA.Replace ( @"cheekSize","N35" );
		StreamedUMA = StreamedUMA.Replace ( @"cheekPosition","N36" );
		StreamedUMA = StreamedUMA.Replace ( @"lowCheekPronounced","N37" );
		StreamedUMA = StreamedUMA.Replace ( @"lowCheekPosition","N38" );
		StreamedUMA = StreamedUMA.Replace ( @"foreheadSize","N39" );
		
		StreamedUMA = StreamedUMA.Replace ( @"neckThickness","N3" );
		
		StreamedUMA = StreamedUMA.Replace ( @"foreheadPosition","N40" );
		StreamedUMA = StreamedUMA.Replace ( @"lipsSize","N41" );
		StreamedUMA = StreamedUMA.Replace ( @"mouthSize","N42" );
		StreamedUMA = StreamedUMA.Replace ( @"eyeRotation","N43" );
		StreamedUMA = StreamedUMA.Replace ( @"eyeSize","N44" );
		StreamedUMA = StreamedUMA.Replace ( @"breastSize","N45" );
		//		StreamedUMA = StreamedUMA.Replace ( "N46" , "" );
		//		StreamedUMA = StreamedUMA.Replace ( "N47" , "" );
		//		StreamedUMA = StreamedUMA.Replace ( "N48" , "" );
		//		StreamedUMA = StreamedUMA.Replace ( "N49" , "" );
		
		StreamedUMA = StreamedUMA.Replace ( @"armLength","N4" );
		StreamedUMA = StreamedUMA.Replace ( @"forearmLength","N5" );
		StreamedUMA = StreamedUMA.Replace ( @"armWidth","N6" );
		StreamedUMA = StreamedUMA.Replace ( @"forearmWidth","N7" );
		StreamedUMA = StreamedUMA.Replace ( @"handsSize","N8" );
		StreamedUMA = StreamedUMA.Replace ( @"feetSize","N9" );

	}

	void ApplyDNA ( DKUMAData _DKUMAData , string streamedUMA, GameObject CreatedModel ){
		// dk uma DNA
//		Debug.Log ("Instantiating My model");
			

	}

	public void LoadFromString (DKUMAData _DKUMAData , string IncStreamedUMA ){
//		Debug.Log ("LoadFromString Start ");
	//	streamedUMA = IncStreamedUMA;
		
		DKUMAData umaData = _DKUMAData;
		umaData.Loading = true;
		
		if(umaData){
			DKUMAData.UMARecipe umaRecipe = new DKUMAData.UMARecipe();
			DKUMAData.UMAPackRecipe umaPackRecipe = new DKUMAData.UMAPackRecipe();
			DK_UMACrowd _DK_UMACrowd = GameObject.Find("DKUMACrowd").GetComponent<DK_UMACrowd>();
		//	#if UNITY_5_5_OR_NEWER
			umaPackRecipe = JsonUtility.FromJson<DKUMAData.UMAPackRecipe>(IncStreamedUMA);
		//	#else
		//	umaPackRecipe = JsonMapper.ToObject<DKUMAData.UMAPackRecipe>(IncStreamedUMA);
		//	#endif
			foreach ( DKRaceData _Race in _DK_UMACrowd.raceLibrary.raceElementList ){
				if ( _Race.raceName == umaPackRecipe.race ){
					umaRecipe.raceData =  _Race;
			//		Debug.Log ("Race Loaded "+umaPackRecipe.race);
				}
			}
			
			// New
			DKUMADnaHumanoid _UMADnaHumanoid = new DKUMADnaHumanoid();
			Dictionary<Type,DKUMADna> umaDna = new Dictionary<Type,DKUMADna>();
			
			// DNA
			// load DK_UMAdnaHumanoid
			for(int dna = 0; dna < umaPackRecipe.packedDna.Count; dna++){
				Type dnaType = DKUMADna.GetType(umaPackRecipe.packedDna[dna].dnaType);
				if ( dna != 0 && dnaType != null ) umaRecipe.umaDna.Add(dnaType, DKUMADna.LoadInstance(dnaType, umaPackRecipe.packedDna[dna].packedDna));
				umaDna = umaRecipe.umaDna;
			}

			Transform tempUMA = (Instantiate(umaRecipe.raceData.racePrefab ,umaData.transform.position,umaData.transform.rotation) as GameObject).transform;
			DKUMAData newUMA = tempUMA.gameObject.GetComponentInChildren<DKUMAData>();
			newUMA.umaRecipe = umaRecipe;
			newUMA.umaPackRecipe = umaPackRecipe;
			newUMA.streamedUMA = IncStreamedUMA;
			
			// Modifyers
			for (int i = 0; i <  umaRecipe.raceData.DNAConverterDataList.Count; i ++) {
				// create new DK DNA
				DKRaceData.DNAConverterData _newDNA = new DKRaceData.DNAConverterData();

				// add to DK_UMAdnaHumanoid
				DKUMADna temp = null;
				if ( umaDna.TryGetValue(_UMADnaHumanoid.GetType(), out temp) ){
					// there it is, apply the value to the modifyer
					DNAValue = float.Parse( umaDna[_UMADnaHumanoid.GetType()].Values.GetValue(i).ToString() );
					if ( DNAValue == 0 ) DNAValue = 0.5f;
				}
				
				_newDNA.Name = umaRecipe.raceData.DNAConverterDataList[i].Name;
				_newDNA.Value = DNAValue;
				_newDNA.Part = umaRecipe.raceData.DNAConverterDataList[i].Part;
				_newDNA.Part2 = umaRecipe.raceData.DNAConverterDataList[i].Part2;
				newUMA.DNAList2.Add(_newDNA);
				
			}
			newUMA.LoadFromMemoryStream();
			newUMA.Dirty(true, true, true);
			Transform Parent = umaData.transform.parent;
			newUMA.transform.parent = Parent ;
			Transform  ZeroPoint = _DK_UMACrowd.zeroPoint;
			if (ZeroPoint != null)
			//	tempUMA.transform.position = _DK_UMACrowd.zeroPoint.transform.position;
				Parent.transform.position = _DK_UMACrowd.zeroPoint.transform.position;
			else {
				ZeroPoint = GameObject.Find ( "ZeroPoint").transform;
			//	tempUMA.transform.position = ZeroPoint.transform.position;
				Parent.transform.position = ZeroPoint.transform.position;

			}
		//	tempUMA.transform.position = Parent.position;
		//	Parent.name = Selection.activeGameObject.GetComponentInParent(UMA.UMAData).transform.parent.name;
			NewAvatarGo = Parent.gameObject;


			// Finishing by destroying the previous model
			#if UNITY_EDITOR
		//	DestroyImmediate(umaData.transform.parent.gameObject);
			DestroyImmediate(umaData.transform.gameObject);

			#endif
		//	if (  Application.isPlaying && umaData ) Destroy(umaData.transform.parent.gameObject);
			if (  Application.isPlaying && umaData ) Destroy(umaData.transform.gameObject);
		}
	}

	void CreatePrefab(){
		PrefabUtility.CreatePrefab("Assets/DK Editors/DK_UMA_Editor/Prefabs/Models/" + NewAvatarGo.name + ".prefab", NewAvatarGo.gameObject,ReplacePrefabOptions.ConnectToPrefab  );

	}

	void OnSelectionChange() {
		this.Close();

	}
}
