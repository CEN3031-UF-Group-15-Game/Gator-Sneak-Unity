using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

[Serializable]
public class DKUMACustomization : MonoBehaviour {
	
	public static Transform sliderPrefab;
	public static GameObject UI;

	public bool DisplayUI;
	public DKUMAData umaData;
	public static DKUMADnaHumanoid umaDna;
	public static Transform TempSlider;
	public DKUMAData tempUMA;
	public Transform EditedModel;
	public static List<SliderControl> sliderList = new List<SliderControl>();
	public static SliderControl[] sliderControlList;
	public DKRaceData _RaceData;
	public static string Type;

	public DKSlotLibrary myDKSlotLibrary;
    public DKOverlayLibrary myOverlayLibrary;

	public static int _Index = 0;


	public DKUMACustomization ()
	{
		#if UNITY_EDITOR
		EditorApplication.update += Update;

		#endif
	}

	void Start () {
		sliderList.Clear();
		SetSliders();
	}
	public void CloseSliders () {
		UI = GameObject.Find("UMA UI");
		Destroy(UI);
	}

	public void SetSliders () {
		if ( DisplayUI ){
			#if UNITY_EDITOR
			Selection.activeGameObject = null;
			Selection.activeObject = null;
			#endif

			umaData = null;
			umaDna = null;
			tempUMA = null;
			if ( UI == null ){
				UI = GameObject.Find("UMA UI");
				if ( UI == null ) {
					UI = new GameObject();
					GameObject DKUMACustomization =  GameObject.Find("DKUMACustomization");
					UI.transform.parent = DKUMACustomization.transform;
					UI.name = "UMA UI";
				}
			}
			if ( Editor_Global.VersionW >= 1 && Editor_Global.VersionX >= 1 && Editor_Global.VersionY >= 0 && Editor_Global.VersionZ >= 0 ) {

				//Changed slider order
				GameObject DNALibraries = GameObject.Find("DNALibraries");
				ConverterLibrary ConverterLibrary = DNALibraries.GetComponent<ConverterLibrary>();

				// Face
				int Lines = 0;
				if ( Type != null ) {
					for(int i = 0; i < ConverterLibrary.ConverterList.Count; i ++){
						if (i == 0) { 
							_Index = 0;
							Lines = 1;
						}
						if ( _Index >= 2 && Lines == 1 ) { 
							_Index = 0; 
							Lines = 2;
						}
						if ( _Index >= 2 && Lines == 2 ) { 
							_Index = 0; 
							Lines = 3;
						}
						if ( _Index >= 2 && Lines == 3 ) { 
							_Index = 0; 
							Lines = 4;
						}
						if ( _Index >= 2 && Lines == 4 ) { 
							_Index = 0; 
							Lines = 5;
						}
						if ( _Index >= 2 && Lines == 5 ) { 
							_Index = 0; 
							Lines = 6;
						}
						if ( _Index >= 2 && Lines == 6 ) { 
							_Index = 0; 
							Lines = 7;
						}
						if ( _Index >= 2 && Lines == 7 ) { 
							_Index = 0; 
							Lines = 8;
						}
						if ( _Index >= 2 && Lines == 8 ) { 
							_Index = 0; 
							Lines = 9;
						}
						if ( _Index >= 2 && Lines == 9 ) { 
							_Index = 0; 
							Lines = 10;
						}
						if ( ConverterLibrary.ConverterList[i].Part == Type ){
							//	Debug.Log ( _Index.ToString()+" "+ ConverterLibrary.ConverterList[i].Name);
							_Index = _Index +1;
							try{
								if (Lines == 1 ) sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,0,ConverterLibrary.ConverterList[i].Part));
								if (Lines == 2 ) sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,1,ConverterLibrary.ConverterList[i].Part));
								if (Lines == 3 ) sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,2,ConverterLibrary.ConverterList[i].Part));
								if (Lines == 4 ) sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,3,ConverterLibrary.ConverterList[i].Part));
								if (Lines == 5 ) sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,4,ConverterLibrary.ConverterList[i].Part));
								if (Lines == 6 ) sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,5,ConverterLibrary.ConverterList[i].Part));
								if (Lines == 7 ) sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,6,ConverterLibrary.ConverterList[i].Part));
								if (Lines == 8 ) sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,7,ConverterLibrary.ConverterList[i].Part));
								if (Lines == 9 ) sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,8,ConverterLibrary.ConverterList[i].Part));
								if (Lines == 10 ) sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,9,ConverterLibrary.ConverterList[i].Part));
								//	sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,2,ConverterLibrary.ConverterList[i].Part));
							}catch( NullReferenceException ){}
						}
					}
				}else{

					// General
					for(int i = 0; i < ConverterLibrary.ConverterList.Count; i ++){
						if (i == 0) _Index = 0;
						if ( ConverterLibrary.ConverterList[i].Part == "" ){
							//	Debug.Log ( _Index.ToString()+" "+ ConverterLibrary.ConverterList[i].Name);
							_Index = _Index +1;
							try{
								sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,0,ConverterLibrary.ConverterList[i].Part));
								//	this.sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,0,ConverterLibrary.ConverterList[i].Part));
							}catch( NullReferenceException ){}
						}
					}
					
					// Head
					for(int i = 0; i < ConverterLibrary.ConverterList.Count; i ++){
						if (i == 0) _Index = 0;
						if ( ConverterLibrary.ConverterList[i].Part == "Head" ){
							//	Debug.Log ( _Index.ToString()+" "+ ConverterLibrary.ConverterList[i].Name);
							_Index = _Index +1;
							try{
								sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,1,ConverterLibrary.ConverterList[i].Part));
								//	sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,1,ConverterLibrary.ConverterList[i].Part));
							}catch( NullReferenceException ){}
						}
					}
					// Face
					int Lines2 = 0;
					for(int i = 0; i < ConverterLibrary.ConverterList.Count; i ++){
						if (i == 0) { 
							_Index = 0;
							Lines2 = 1;
						}
						if ( _Index >= 7 && Lines2 == 1 ) { 
								_Index = 0; 
								Lines2 = 2;
						}
						if ( _Index >= 7 && Lines == 2 ) { 
							_Index = 0; 
							Lines2 = 3;
						}
						if ( _Index >= 7 && Lines == 3 ) { 
							_Index = 0; 
							Lines2 = 4;
						}
						if ( ConverterLibrary.ConverterList[i].Part == "Face" ){
							//	Debug.Log ( _Index.ToString()+" "+ ConverterLibrary.ConverterList[i].Name);
							_Index = _Index +1;
							try{
								if (Lines2 == 1 ) sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,2,ConverterLibrary.ConverterList[i].Part));
								if (Lines2 == 2 ) sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,3,ConverterLibrary.ConverterList[i].Part));

								//	sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,2,ConverterLibrary.ConverterList[i].Part));
							}catch( NullReferenceException ){}
						}
					}
					// Arms
					for(int i = 0; i < ConverterLibrary.ConverterList.Count; i ++){
						if (i == 0) _Index = 0;
						if ( ConverterLibrary.ConverterList[i].Part == "Arms" ){
							//	Debug.Log ( _Index.ToString()+" "+ ConverterLibrary.ConverterList[i].Name);
							_Index = _Index +1;
							try{
								sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,4,ConverterLibrary.ConverterList[i].Part));
								//	sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,3,ConverterLibrary.ConverterList[i].Part));
							}catch( NullReferenceException ){}
						}
					}
					// Torso
					for(int i = 0; i < ConverterLibrary.ConverterList.Count; i ++){
						if (i == 0) _Index = 0;
						if ( ConverterLibrary.ConverterList[i].Part == "Torso" ){
							//	Debug.Log ( _Index.ToString()+" "+ ConverterLibrary.ConverterList[i].Name);
							_Index = _Index +1;
							try{
								sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,5,ConverterLibrary.ConverterList[i].Part));
								//	sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,4,ConverterLibrary.ConverterList[i].Part));
							}catch( NullReferenceException ){}
						}
					}
					// Legs
					for(int i = 0; i < ConverterLibrary.ConverterList.Count; i ++){
						if (i == 0) _Index = 0;
						if ( ConverterLibrary.ConverterList[i].Part == "Legs" ){
							//	Debug.Log ( _Index.ToString()+" "+ ConverterLibrary.ConverterList[i].Name);
							_Index = _Index +1;
							try{
								sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,6,ConverterLibrary.ConverterList[i].Part));
								//	sliderList.Add(InstantiateSlider2(ConverterLibrary.ConverterList[i].Name,_Index,5,ConverterLibrary.ConverterList[i].Part));
							}catch( NullReferenceException ){}
						}
					}
				}
			}
			else{
				sliderControlList = new SliderControl[46];	
				//Changed slider order

				sliderControlList[0] = InstantiateSlider("height",0,0);
				sliderControlList[1] = InstantiateSlider("headSize",1,0);
				sliderControlList[43] = InstantiateSlider("headWidth",2,0);
				sliderControlList[32] = InstantiateSlider("forehead size",3,0);
				sliderControlList[33] = InstantiateSlider("forehead position",4,0);
				
				sliderControlList[12] = InstantiateSlider("ears size",0,1);
				sliderControlList[13] = InstantiateSlider("ears position",1,1);
				sliderControlList[14] = InstantiateSlider("ears rotation",2,1);
				
				sliderControlList[28] = InstantiateSlider("cheek size",0,2);
				sliderControlList[29] = InstantiateSlider("cheek position",1,2);
				sliderControlList[30] = InstantiateSlider("lowCheek pronounced",2,2);
				sliderControlList[31] = InstantiateSlider("lowCheek position",3,2);
				
				sliderControlList[15] = InstantiateSlider("nose size",0,3);
				sliderControlList[16] = InstantiateSlider("nose curve",1,3);
				sliderControlList[17] = InstantiateSlider("nose width",2,3);
				
				sliderControlList[18] = InstantiateSlider("nose inclination",0,4);
				sliderControlList[19] = InstantiateSlider("nose position",1,4);
				sliderControlList[20] = InstantiateSlider("nose pronounced",2,4);
				sliderControlList[21] = InstantiateSlider("nose flatten",3,4);
				
				sliderControlList[44] = InstantiateSlider("eye Size",0,5);
				sliderControlList[45] = InstantiateSlider("eye Rotation",1,5);
				sliderControlList[34] = InstantiateSlider("lips size",2,5);
				sliderControlList[35] = InstantiateSlider("mouth size",3,5);
				sliderControlList[25] = InstantiateSlider("mandible size",4,5);
				
				sliderControlList[26] = InstantiateSlider("jaw Size",0,6);
				sliderControlList[27] = InstantiateSlider("jaw Position",1,6);
				sliderControlList[2] = InstantiateSlider("neck",2,6);
				
				sliderControlList[22] = InstantiateSlider("chinSize",0,7);
				sliderControlList[23] = InstantiateSlider("chinPronounced",1,7);
				sliderControlList[24] = InstantiateSlider("chinPosition",2,7);
				
				sliderControlList[7] = InstantiateSlider("upper muscle",0,8);
				sliderControlList[8] = InstantiateSlider("lower muscle",1,8);
				sliderControlList[9] = InstantiateSlider("upper weight",2,8);
				sliderControlList[10] = InstantiateSlider("lower weight",3,8);	
				
				sliderControlList[3] = InstantiateSlider("arm Length",0,9);
				sliderControlList[38] = InstantiateSlider("arm Width",1,9);
				sliderControlList[39] = InstantiateSlider("forearm Length",2,9);
				sliderControlList[40] = InstantiateSlider("forearm Width",3,9);
				sliderControlList[4] = InstantiateSlider("hands Size",4,9);
				
				sliderControlList[5] = InstantiateSlider("feet Size",0,10);
				sliderControlList[6] = InstantiateSlider("leg Separation",1,10);
				sliderControlList[11] = InstantiateSlider("legsSize",2,10);
				sliderControlList[37] = InstantiateSlider("Gluteus Size",3,10);
				
				sliderControlList[36] = InstantiateSlider("breatsSize",0,11);
				sliderControlList[41] = InstantiateSlider("belly",1,11);
				sliderControlList[42] = InstantiateSlider("waist",2,11);
			}
		}
	}

	public void SelectModel (){
		try{
			EditedModel = tempUMA.transform.parent;
			#if UNITY_EDITOR
			Selection.activeGameObject = tempUMA.transform.parent.gameObject;
			Selection.activeGameObject = tempUMA.transform.gameObject;
			#endif
			
			_RaceData = tempUMA.umaRecipe.raceData;
		}catch(NullReferenceException){}
		
		
		if(tempUMA){
		/*	if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				if (umaData)
				{
					tempUMA.SaveToMemoryStream();
					umaData.streamedUMA = tempUMA.streamedUMA;
					umaData.LoadFromMemoryStream();
					umaData.Dirty(true, true, true);
					
				}
				return;
			}*/
			
			//Clear saved data of old UMA
			if(umaData){
				umaData.streamedUMA = null;
			}
			umaData = tempUMA;
	
			umaDna = new DKUMADnaHumanoid();

			ReceiveValues();
			
			//Save functionality
			umaData.SaveToMemoryStream();
		}
	}


	void Update () {
		if (  Application.isPlaying && Camera.main ) {
		//	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//	RaycastHit hit;
			
		/*	if(Input.GetMouseButtonDown(1)){
				if (Physics.Raycast(ray, out hit, 100)){
					tempUMA = hit.collider.GetComponent("DKUMAData") as DKUMAData;
					try{
						EditedModel = tempUMA.transform.parent;
						#if UNITY_EDITOR
						Selection.activeGameObject = tempUMA.transform.parent.gameObject;
						Selection.activeGameObject = tempUMA.transform.gameObject;
						#endif

						_RaceData = tempUMA.umaRecipe.raceData;
					}catch(NullReferenceException){}
					if ( tempUMA == null ) {
						tempUMA = hit.collider.transform.GetComponentInChildren<DKUMAData>();
					}
					
					if(tempUMA){
	                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
	                    {
	                        if (umaData)
	                        {
	                            tempUMA.SaveToMemoryStream();
	                            umaData.streamedUMA = tempUMA.streamedUMA;
	                            umaData.LoadFromMemoryStream();
	                            umaData.Dirty(true, true, true);

	                        }
	                        return;
	                    }
	
						//Clear saved data of old UMA
						if(umaData){
							umaData.streamedUMA = null;
						}
//						Debug.Log ( tempUMA.transform.parent.name + " selected" );
						umaData = tempUMA;
						if(cameraTrack){
							cameraTrack.target = umaData.transform;
						}
						
						umaDna = new DKUMADnaHumanoid();
				
						ReceiveValues();
						
						//Save functionality
						umaData.SaveToMemoryStream();
					}
				}
			}*/
			
		if( umaData && EditedModel != null )
			#if UNITY_EDITOR
			if( Selection.activeGameObject == umaData.gameObject )
			#endif
			{
				TransferValues();
		
			//	for(int i = 0; i < sliderControlList.Length; i++){
			//		if(sliderControlList[i].pressed == true){
						UpdateUMAShape();
						umaData.UpdateCollider();
			//		}
			//	}
			}
			//Load functionality
		/*	if( Input.GetKeyDown(KeyCode.Z) ){
				if(umaData){	
					umaData.LoadFromMemoryStream();
					umaData.isShapeDirty = true;
					umaData.Dirty();
					
					umaDna = new DKUMADnaHumanoid();
					ReceiveValues();
					
					umaData.SaveToMemoryStream();
				}
			}
			 */
		}
		
		if ( EditedModel != null )
			#if UNITY_EDITOR
			if (Selection.activeGameObject == EditedModel ) 
			#endif
			{
				tempUMA = EditedModel.GetComponent("DKUMAData") as DKUMAData;
				if(tempUMA)	umaData = tempUMA;

			//	try{ 
			if( EditedModel != null && umaData){umaDna = new DKUMADnaHumanoid();}
					
					if(umaData){
						UpdateUMAShape();
						umaData.UpdateCollider();
						ReceiveValues();
					}
			//	}catch (System.ArgumentException ) {}
			}
	}
	
	public  static SliderControl InstantiateSlider2(string name, int X, int Y, string Part){
		if (  Application.isPlaying ) TempSlider = Instantiate(Resources.Load("SliderModule" , typeof(Transform))  ) as Transform;
		#if UNITY_EDITOR
		else TempSlider =  PrefabUtility.InstantiatePrefab(Resources.Load("SliderModule" , typeof(Transform))  ) as Transform;
		#endif
		TempSlider.parent = UI.transform;
	//	TempSlider.gameObject.SetActive(true);
		if ( Part == "" ){
			GameObject SlidersGrp = GameObject.Find("SlidersGeneral");
			if ( SlidersGrp == null ) {
				SlidersGrp = new GameObject();
				SlidersGrp.name = "SlidersGeneral";
				SlidersGrp.transform.parent = UI.transform;
			}
			TempSlider.parent = SlidersGrp.transform;
		}
		else if ( Part == "Head" ){
			GameObject SlidersGrp = GameObject.Find("SlidersHead");
			if ( SlidersGrp == null ) {
				SlidersGrp = new GameObject();
				SlidersGrp.name = "SlidersHead";
				SlidersGrp.transform.parent = UI.transform;
			}
			TempSlider.parent = SlidersGrp.transform;
		}
		else if ( Part == "Face" ){
			GameObject SlidersGrp = GameObject.Find("SlidersFace");
			if ( SlidersGrp == null ) {
				SlidersGrp = new GameObject();
				SlidersGrp.name = "SlidersFace";
				SlidersGrp.transform.parent = UI.transform;
			}
			TempSlider.parent = SlidersGrp.transform;
		}
		else if ( Part == "Arms" ){
			GameObject SlidersGrp = GameObject.Find("SlidersArms");
			if ( SlidersGrp == null ) {
				SlidersGrp = new GameObject();
				SlidersGrp.name = "SlidersArms";
				SlidersGrp.transform.parent = UI.transform;
			}
			TempSlider.parent = SlidersGrp.transform;
		}
		else if ( Part == "Torso" ){
			GameObject SlidersGrp = GameObject.Find("SlidersTorso");
			if ( SlidersGrp == null ) {
				SlidersGrp = new GameObject();
				SlidersGrp.name = "SlidersTorso";
				SlidersGrp.transform.parent = UI.transform;
			}
			TempSlider.parent = SlidersGrp.transform;
		}
		else if ( Part == "Legs" ){
			GameObject SlidersGrp = GameObject.Find("SlidersLegs");
			if ( SlidersGrp == null ) {
				SlidersGrp = new GameObject();
				SlidersGrp.name = "SlidersLegs";
				SlidersGrp.transform.parent = UI.transform;
			}
			TempSlider.parent = SlidersGrp.transform;
		}

		TempSlider.name = name;
		SliderControl tempSliderControl = TempSlider.GetComponent("SliderControl") as SliderControl;
		tempSliderControl.enabled = true;
		tempSliderControl.actualValue = 0;
		tempSliderControl.descriptionText.text = name;
		tempSliderControl.sliderOffset.x = 20 + X*100;
		tempSliderControl.sliderOffset.y = -20 - Y*60;
		sliderList.Add(tempSliderControl);
//		Debug.Log (sliderList.Count.ToString() );
		return tempSliderControl;
	}
	public  static SliderControl InstantiateStepSlider2(string name, int X, int Y, string Part){
		SliderControl tempSlider = InstantiateSlider2(name,X,Y, Part);
		tempSlider.stepSlider = true;
		
		return tempSlider;
	}
	public  static SliderControl InstantiateSlider(string name, int X, int Y){
		if (  Application.isPlaying ) TempSlider = Instantiate(Resources.Load("SliderModule" , typeof(Transform))  ) as Transform;
		#if UNITY_EDITOR
		else TempSlider =  PrefabUtility.InstantiatePrefab(Resources.Load("SliderModule" , typeof(Transform))  ) as Transform;
		#endif

		TempSlider.parent = UI.transform;

		TempSlider.name = name;
		SliderControl tempSliderControl = TempSlider.GetComponent("SliderControl") as SliderControl;
		tempSliderControl.enabled = true;
		tempSliderControl.actualValue = 0;
		tempSliderControl.descriptionText.text = name;
		tempSliderControl.sliderOffset.x = 20 + X*100;
		tempSliderControl.sliderOffset.y = -20 - Y*60;

		return tempSliderControl;
	}
	
	public  static SliderControl InstantiateStepSlider(string name, int X, int Y){
		SliderControl tempSlider = InstantiateSlider(name,X,Y);
		tempSlider.stepSlider = true;
		
		return tempSlider;
	}

	public void UpdateUMAAtlas(){
		umaData.isTextureDirty = true;
		umaData.Dirty();	
	}
	
	public void UpdateUMAShape(){
		umaData.isShapeDirty = true;
		umaData.Dirty();
	}

	public void ReceiveValues(){
		if (  Application.isPlaying ) {
			if (  umaDna == null){
				umaDna = new DKUMADnaHumanoid();
			}
			if ( umaDna != null){

				if ( Editor_Global.VersionW >= 1 
				    && Editor_Global.VersionX >= 1 
				    && Editor_Global.VersionY >= 0 
				    && Editor_Global.VersionZ >= 0 ) 
				{
					SliderControl[] SliderControlList = FindObjectsOfType(typeof(SliderControl)) as SliderControl[];
					_RaceData = umaData.umaRecipe.raceData;
					for (int i2 = 0; i2 <  SliderControlList.Length; i2 ++) {
						for (int i = 0; i <  umaData.DNAList2.Count; i ++) {
							if ( umaData.DNAList2[i].Name == SliderControlList[i2].name
							 && umaData.DNAList2[i].Value != SliderControlList[i2].actualValue)
							{
								SliderControlList[i2].name = umaData.DNAList2[i].Name;
								SliderControlList[i2].actualValue = umaData.DNAList2[i].Value;
						
							}
						}
					}
				}
			}
		}
	}
	
	
	public void TransferValues(){
		if ( umaDna == null){
			umaDna = new DKUMADnaHumanoid();
		}
		if ( umaDna != null){
			if ( Editor_Global.VersionW >= 1 
			    && Editor_Global.VersionX >= 1 
			    && Editor_Global.VersionY >= 0 
			    && Editor_Global.VersionZ >= 0 ) 
			{

				SliderControl[] SliderControlList = FindObjectsOfType(typeof(SliderControl)) as SliderControl[];
				for (int i2 = 0; i2 <  SliderControlList.Length; i2 ++) {
					for (int i = 0; i <  umaData.DNAList2.Count; i ++) {
						if ( umaData.DNAList2[i].Name == SliderControlList[i2].name 
						    && umaData.DNAList2[i].Value != SliderControlList[i2].actualValue )
						{
							umaData.DNAList2[i].Value = SliderControlList[i2].actualValue;
							float tmpValue = umaData.DNAList2[i].Value;
							if ( i == 0 ) umaDna.N0 = tmpValue;if ( i == 1 ) umaDna.N1 = tmpValue;if ( i == 2 ) umaDna.N2 = tmpValue;
							if ( i == 3 ) umaDna.N3 = tmpValue;if ( i == 4 ) umaDna.N4 = tmpValue;if ( i == 5 ) umaDna.N5 = tmpValue;
							if ( i == 6 ) umaDna.N6 = tmpValue;if ( i == 7 ) umaDna.N7 = tmpValue;if ( i == 8 ) umaDna.N8 = tmpValue;
							if ( i == 9 ) umaDna.N9 = tmpValue;if ( i == 10 ) umaDna.N10 = tmpValue;if ( i == 11 ) umaDna.N11 = tmpValue;
							if ( i == 12 ) umaDna.N12 = tmpValue;if ( i == 13 ) umaDna.N13 = tmpValue;if ( i == 14 ) umaDna.N14 = tmpValue;
							if ( i == 15 ) umaDna.N15 = tmpValue;if ( i == 16 ) umaDna.N16 = tmpValue;if ( i == 17 ) umaDna.N17 = tmpValue;
							if ( i == 18 ) umaDna.N18 = tmpValue;if ( i == 19 ) umaDna.N19 = tmpValue;if ( i == 20 ) umaDna.N20 = tmpValue;
							if ( i == 21 ) umaDna.N21 = tmpValue;if ( i == 22 ) umaDna.N22 = tmpValue;if ( i == 23 ) umaDna.N23 = tmpValue;
							if ( i == 24 ) umaDna.N24 = tmpValue;if ( i == 25 ) umaDna.N25 = tmpValue;if ( i == 26 ) umaDna.N26 = tmpValue;
							if ( i == 27 ) umaDna.N27 = tmpValue;if ( i == 28 ) umaDna.N28 = tmpValue;if ( i == 29 ) umaDna.N29 = tmpValue;
							if ( i == 30 ) umaDna.N30 = tmpValue;if ( i == 31 ) umaDna.N31 = tmpValue;if ( i == 32 ) umaDna.N32 = tmpValue;
							if ( i == 33 ) umaDna.N33 = tmpValue;if ( i == 34 ) umaDna.N34 = tmpValue;if ( i == 35 ) umaDna.N35 = tmpValue;
							if ( i == 36 ) umaDna.N36 = tmpValue;if ( i == 37 ) umaDna.N37 = tmpValue;if ( i == 38 ) umaDna.N38 = tmpValue;
							if ( i == 39 ) umaDna.N39 = tmpValue;if ( i == 40 ) umaDna.N40 = tmpValue;if ( i == 41 ) umaDna.N41 = tmpValue;
							if ( i == 42 ) umaDna.N42 = tmpValue;if ( i == 43 ) umaDna.N43 = tmpValue;if ( i == 44 ) umaDna.N44 = tmpValue;
							if ( i == 45 ) umaDna.N45 = tmpValue;if ( i == 46 ) umaDna.N46 = tmpValue;if ( i == 47 ) umaDna.N47 = tmpValue;
							if ( i == 48 ) umaDna.N48 = tmpValue;if ( i == 49 ) umaDna.N49 = tmpValue;if ( i == 50 ) umaDna.N50 = tmpValue;
							if ( i == 51 ) umaDna.N51 = tmpValue;if ( i == 52 ) umaDna.N52 = tmpValue;
							if ( i == 53 ) umaDna.N53 = tmpValue;if ( i == 54 ) umaDna.N54 = tmpValue;if ( i == 55 ) umaDna.N55 = tmpValue;
							if ( i == 56 ) umaDna.N56 = tmpValue;if ( i == 57 ) umaDna.N57 = tmpValue;if ( i == 58 ) umaDna.N58 = tmpValue;
							if ( i == 59 ) umaDna.N59 = tmpValue;
							if ( i == 60 ) umaDna.N60 = tmpValue;if ( i == 61 ) umaDna.N61 = tmpValue;if ( i == 62 ) umaDna.N62 = tmpValue;
							if ( i == 63 ) umaDna.N63 = tmpValue;if ( i == 64 ) umaDna.N64 = tmpValue;if ( i == 65 ) umaDna.N65 = tmpValue;
							if ( i == 66 ) umaDna.N66 = tmpValue;if ( i == 67 ) umaDna.N67 = tmpValue;if ( i == 68 ) umaDna.N68 = tmpValue;
							if ( i == 69 ) umaDna.N69 = tmpValue;
							//	Debug.Log ("TransferValues");
							umaData.Dirty(true, true, true);
						}
					}
				}
			}
			else{

			}
		}
	}
}
