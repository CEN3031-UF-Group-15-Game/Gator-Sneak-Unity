using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
#if UNITY_5_5_OR_NEWER
#else
//using LitJson;
#endif
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif



[System.Serializable]
public partial class DKUMADna{
	
}
[ExecuteInEditMode]
public class DKUMAData : MonoBehaviour {	
	[HideInInspector]public SkinnedMeshRenderer myRenderer;
	[HideInInspector]public SkinnedMeshRenderer TMPmyRenderer;

	[HideInInspector]public bool Creating = false;
	
	[System.Serializable]
	public class AtlasList
	{
		public List<AtlasElement> atlas;
	}

	[System.Serializable]
	public class AtlasElement
	{
		public List<AtlasMaterialDefinition> atlasMaterialDefinitions;
		public Material materialSample;
		public Shader shader;
		public Texture[] resultingAtlasList;
		public Vector2 cropResolution;
		public float resolutionScale;
		public int mipmap;
	}
	
	[System.Serializable]
	public class AtlasMaterialDefinition
	{
		public MaterialDefinition source;
		public Rect atlasRegion;
		public bool isRectShared;
	}
	
	public class MaterialDefinition
	{
		public Texture2D[] baseTexture;
		public Color32 baseColor;
        public Material materialSample;
		public Rect[] rects;
		public textureData[] overlays;
		public Color32[] overlayColors;
        public Color32[][] channelMask;
        public Color32[][] channelAdditiveMask;
		public DKSlotData slotData;

        internal Color32 GetMultiplier(int overlay, int textureType)
        {
			
            if (channelMask[overlay] != null && channelMask[overlay].Length > 0)
            {
                return channelMask[overlay][textureType];
            }
            else
            {
                if (textureType > 0) return new Color32(255, 255, 255, 255);
                if (overlay == 0) return baseColor;
                return overlayColors[overlay - 1];
            }
        }
        internal Color32 GetAdditive(int overlay, int textureType)
        {
            if (channelAdditiveMask[overlay] != null && channelAdditiveMask[overlay].Length > 0)
            {
                return channelAdditiveMask[overlay][textureType];
            }
            else
            {
                return new Color32(0, 0, 0, 0);
            }
        }
    }
	
	[System.Serializable]
	public class packedSlotData{
	//	public string sID;
		public string sID;
		//	public int oS = 1;
		public int oS = 1;
	//	public int cOI = -1;
		public int cOI = -1;
	//	public packedOverlayData[] ODL;
		public packedOverlayData[] ODL;
	}
	
	[System.Serializable]
	public class packedOverlayData{
	//	public string oID;
		public string oID;
	//	public int[] cL;
		public int[] cL;
	//	public int[][] cML;
		public int[][] cML;
	//	public int[][] cAML;
		public int[][] cAML;

	//	public int[] rL;
		public int[] rL;
	}
	[HideInInspector]public List<DKSlotData> tmpRecipeList = new List<DKSlotData>();

	
	[System.Serializable]
	public class textureData{
		public Texture2D[] textureList;
	}
	
	[System.Serializable]
	public class resultAtlasTexture{
		public Texture[] textureList;
	}

    [System.Serializable]
    public class UMAPackedDna
    {
        public string dnaType;
        public string packedDna;
    }

/*	[System.Serializable]
	public class DK_UMAPackedDna
	{
		public string dnaType;
	//	public string packedDna;
	//	public string packedConverter;
	//	public List<DKRaceData.DNAConverterData> tmpDNAList = new List<DKRaceData.DNAConverterData>();
	}
*/
	[HideInInspector]public string packedSlots = "";

	[System.Serializable]
	public class UMAPackRecipe{
		public packedSlotData[] packedSlotDataList;
		public string race;

		public Dictionary<Type,DKUMADna> umaDna = new Dictionary<Type,DKUMADna>();
		public List<UMAPackedDna> packedDna = new List<UMAPackedDna>();
	//	public DK_UMAPackedDna DKpackedDna;
		public DKRaceData.DNAConverterData[] DNAList;
	}


	
	[System.Serializable]
	public class UMARecipe{
		public DKRaceData raceData;
		public Dictionary<Type,DKUMADna> umaDna = new Dictionary<Type,DKUMADna>();
		protected Dictionary<Type, Action<DKUMAData>> umaDnaConverter = new Dictionary<Type, Action<DKUMAData>>();
		public DKSlotData[] slotDataList;

        internal T GetDna<T>()
			where T : DKUMADna
        {
			DKUMADna dna;
            if(umaDna.TryGetValue(typeof(T), out dna))
            {
                return dna as T;               
            }
            return null;
        }

        internal void SetRace(DKRaceData raceData)
        {
            this.raceData = raceData;
            ClearDNAConverters();
        }

	/*	public void ExternalApplyDNA(DKUMAData umaData)
		{

		}
*/
		internal void ApplyDNA(DKUMAData umaData)
        {
            foreach (var dnaEntry in umaDna)
            {            
                Action<DKUMAData> dnaConverter;
                if (umaDnaConverter.TryGetValue(dnaEntry.Key, out dnaConverter))
                {
                    dnaConverter(umaData);
                }
                else
                {
                    Debug.LogWarning("Cannot apply dna: " + dnaEntry.Key);
                }
            }
        }

        internal void ClearDNAConverters()
        {
            umaDnaConverter.Clear();
            foreach (var converter in raceData.dnaConverterList)
            {
                umaDnaConverter.Add(converter.DNAType, converter.ApplyDnaAction);
            }
        }

		internal void AddDNAUpdater(DKDnaConverterBehaviour dnaConverter)
        {
            if( dnaConverter == null ) return;
            if (!umaDnaConverter.ContainsKey(dnaConverter.DNAType))
            {
                umaDnaConverter.Add(dnaConverter.DNAType, dnaConverter.ApplyDnaAction);
            }
        }
    }
	
	[System.Serializable]
	public class BoneData{
		public Transform boneTransform;
		public Vector3 actualBoneScale;
		public Vector3 originalBoneScale;
		public Vector3 actualBonePosition;
		public Quaternion actualBoneRotation;
        public Vector3 originalBonePosition;
		public Quaternion originalBoneRotation;
	}

	[HideInInspector]public bool dirty = false;
	[HideInInspector]public bool isMeshDirty;
	[HideInInspector]public bool isShapeDirty;
	[HideInInspector]public bool isTextureDirty;
	[HideInInspector]public bool Loading;

    private bool _hasUpdatedBefore = false;
    public event Action<bool> OnUpdated;



    public void FireUpdatedEvent()
    {
        if (OnUpdated != null)
        {
            OnUpdated(!_hasUpdatedBefore);
            _hasUpdatedBefore = true;
        }
		dirty = false;
    }
	
    public void ApplyDNA()
    {
        umaRecipe.ApplyDNA(this);
    }



    public virtual void Dirty()
    {
    /*    if (dirty) return;
        dirty = true;
	//	DK_UMACrowd _DK_UMACrowd = GameObject.Find ( "DKUMACrowd" ).GetComponent<DK_UMACrowd>();
		if ( DK_UMACrowd.GeneratorMode == "Preset" ){
	        if (!DKumaGenerator)
	        {
				DKumaGenerator = GameObject.Find("DKUMAGenerator").GetComponent("DKUMAGenerator") as DKUMAGenerator;
			}
	        if (DKumaGenerator)
	        {
	            DKumaGenerator.addDirtyUMA(this);
	        }
		}
		else if ( DK_UMACrowd.GeneratorMode == "RPG" ){
			if ( !umaGenerator ){
				umaGenerator = transform.gameObject.AddComponent <DK_RPG_SelfGenerator>() as DK_RPG_SelfGenerator;
			//	Debug.Log ("creating DK_RPG_SelfGenerator umaGenerator 2");
			}
			umaGenerator.addDirtyUMA(this);
			// launch generator
			umaGenerator.Awake();
			#if UNITY_EDITOR
			umaGenerator.UpdateEditor();
			#endif
		}*/
    }
	

	[HideInInspector]public bool firstBake;
	[HideInInspector]public bool UseNaturalBehaviour = true;

	[HideInInspector]public string DKSlotLibraryObj;
	[HideInInspector]public string OverlayLibraryObj;
	[HideInInspector]public string RaceLibraryObj;

	[HideInInspector]public DKRaceLibrary raceLibrary;
	[HideInInspector]public DKSlotLibrary slotLibrary;
	[HideInInspector]public DKOverlayLibrary overlayLibrary;
	
//Original
//	[HideInInspector]public DKUMAGenerator DKumaGenerator;
//	[HideInInspector]public DK_RPG_SelfGenerator umaGenerator;
	[HideInInspector]public string path;
	[Tooltip("The Streamed UMA is the packed recipe of the avatar. " +
		"It is unpacked by the 'Transpose2UMA' component and sent to the UMADynamicAvatar component of the avatar and stored in its UmaRecipe. " +
		"It is also the stream to use for a network.")]
	public string streamedUMA;
	[HideInInspector]public UMARecipe umaRecipe;
	[HideInInspector]public UMAPackRecipe umaPackRecipe;

	[Tooltip("The DNA List 2 is the specific DK UMA DNA of the avatar. " +
		"It is Different from the UMA DNA, unlocking the mini and maxi values for a better result." +
		"It is unpacked by the 'Transpose2UMA' component and is applied to the avatar just after its generation by the 'LoadUMA' component. " +
		"This component is deleted after the generation of the avatar.")]
	public List<DKRaceData.DNAConverterData> DNAList2 = new List<DKRaceData.DNAConverterData>();

	[System.Serializable]
	public class WearWeightData{
		public string Name;
		public List<string> Weights = new List<string>();
	}
	[HideInInspector]public List<WearWeightData> WearWeightList = new List<WearWeightData>();

	[HideInInspector]public AtlasList atlasList;
	
	[HideInInspector]public float atlasResolutionScale;
	
	[HideInInspector]public CapsuleCollider capsuleCollider;
	int iTmp;
	
	public Dictionary<string,BoneData> boneList = new Dictionary<string,BoneData>();
	[HideInInspector]public BoneData[] updateBoneList = new BoneData[0];
	
	[HideInInspector]public BoneData[] tempBoneData; //Only while Dictionary can't be serialized
// Added by DK	
	public DKUMAData ()
	{
		#if UNITY_EDITOR
		EditorApplication.update += Update;

		#endif
	}
	// DK	
	public void OnEnable () {
	//	Debug.Log ("OnEnable");
		Awake ();
		Creating = true;


	}	

	DK_RPG_ReBuild _DK_RPG_ReBuild;
	void Start (){
	//	_DK_RPG_ReBuild = gameObject.GetComponent<DK_RPG_ReBuild>();
/*		if ( _DK_RPG_ReBuild == null ) _DK_RPG_ReBuild = gameObject.AddComponent<DK_RPG_ReBuild>();
		_DK_RPG_ReBuild.Launch(this);
		if (  Application.isPlaying ) Destroy(_DK_RPG_ReBuild);
		else DestroyImmediate(_DK_RPG_ReBuild);
		*/
	}

	GameObject UMACrowdObj;
	DK_UMACrowd _DK_UMACrowd;

	public void DetectAll () {
		if ( _DK_UMACrowd == null ) {
			UMACrowdObj = GameObject.Find("DKUMACrowd");	
			if ( UMACrowdObj == null ) {
				UMACrowdObj = (GameObject) Instantiate(Resources.Load("DKUMACrowd"), Vector3.zero, Quaternion.identity);
				UMACrowdObj.name = "DKUMACrowd";
				#if UNITY_EDITOR
				PrefabUtility.ReconnectToLastPrefab(UMACrowdObj);
				#endif
			}
			_DK_UMACrowd = UMACrowdObj.GetComponent<DK_UMACrowd>();
		}

		if(!slotLibrary) slotLibrary = _DK_UMACrowd.slotLibrary;
		if(!raceLibrary ) raceLibrary = _DK_UMACrowd.raceLibrary;
		if(!overlayLibrary ) overlayLibrary = _DK_UMACrowd.overlayLibrary;
	}

	void InstallDKUMA (){
		DKUMACorrectors.InstallDKUMA ();
	}

	void InstallUMA (){
		DKUMACorrectors.InstallUMA ();
	}

	public void Awake () {
		if ( GameObject.Find ("DK_UMA" ) == null ){
			InstallDKUMA ();
			InstallUMA ();
		}

		Awaking ();
		if ( gameObject.GetComponent<DK_RPG_UMA>() != null ){
		//	DK_RPG_ReBuild _DK_RPG_ReBuild = gameObject.AddComponent<DK_RPG_ReBuild>();
		//	_DK_RPG_ReBuild.Launch(this);
		}
	/*	else
		if ( streamedUMA != "" && streamedUMA != null ){
			isShapeDirty = true;
			isTextureDirty = true;
			isMeshDirty = true;
			Dirty();
		}*/
	}

	public void Awaking () {
	
	//	Debug.Log ("Awaking");
		firstBake = true;
		DetectAll ();

		if( this.transform.parent && this.transform.parent.GetComponentInChildren<DK_Model>() == true ){
		//	Debug.Log ( this.transform.parent.name);
		//	DKSlotLibraryObj = this.transform.parent.GetComponent<DK_Model>().DKSlotLibraryObj;
			if(!slotLibrary ){
				GameObject slotLibraryObj =  GameObject.Find(DKSlotLibraryObj);
				if ( slotLibraryObj ) slotLibrary = slotLibraryObj.GetComponent("DKSlotLibrary") as DKSlotLibrary;
				else {
					Debug.LogError ( "The model '"+this.transform.parent.name+"' has not been generated : The Uma model does not found the needed Slot Library. You need to get the Library used during its creation: '"+DKSlotLibraryObj
					                +"'. Try to contact the creator of the model's Asset to get: '"+DKSlotLibraryObj+"'"
					                +" Or create a new Library named : '"+DKSlotLibraryObj+"', be sure to append all the recquired Elements needed by the model.");
					if ( this.transform.parent.parent.childCount == 1 ){
						if (  Application.isPlaying ) Destroy(this.transform.parent.parent.gameObject);
						else DestroyImmediate(this.transform.parent.parent.gameObject);
					}
					else {
						if (  Application.isPlaying ) Destroy(this.transform.parent.gameObject);
						else DestroyImmediate(this.transform.parent.gameObject);
					}

				}
			}
			else
		//	RaceLibraryObj = this.transform.parent.GetComponent<DK_Model>().RaceLibraryObj;
			if(!raceLibrary){
				GameObject raceLibraryObj =  GameObject.Find(RaceLibraryObj);
				if ( raceLibraryObj ) raceLibrary = raceLibraryObj.GetComponent("DKRaceLibrary") as DKRaceLibrary;
				else {
					Debug.LogError ( "The model '"+this.transform.parent.name+"' has not been generated : The Uma model does not found the needed Race Library. You need to get the Library used during its creation: '"+DKSlotLibraryObj
					                +"'. Try to contact the creator of the model's Asset to get: '"+RaceLibraryObj+"'"
					                +" Or create a new Library named : '"+RaceLibraryObj+"', be sure to append all the recquired Elements needed by the model.");
					if ( this.transform.parent.parent.childCount == 1 ){
						if (  Application.isPlaying ) Destroy(this.transform.parent.parent.gameObject);
						else DestroyImmediate(this.transform.parent.parent.gameObject);
					}
					else {
						if (  Application.isPlaying ) Destroy(this.transform.parent.gameObject);
						else DestroyImmediate(this.transform.parent.gameObject);
					}
				}
			}
			else
		//	OverlayLibraryObj = this.transform.parent.GetComponent<DK_Model>().OverlayLibraryObj;
			if(!overlayLibrary){
				GameObject overlayLibraryObj =  GameObject.Find(OverlayLibraryObj);
				if ( overlayLibraryObj ) overlayLibrary = overlayLibraryObj.GetComponent("DKOverlayLibrary") as DKOverlayLibrary;
				else {
					Debug.LogError ( "The model '"+this.transform.parent.name+"' has not been generated : The Uma model does not found the needed Overlay Library. You need to get the Library used during its creation: '"+DKSlotLibraryObj
					                +"'. Try to contact the creator of the model's Asset to get: '"+OverlayLibraryObj+"'"
					                +" Or create a new Library named : '"+OverlayLibraryObj+"', be sure to append all the recquired Elements needed by the model.");
					if ( this.transform.parent.parent.childCount == 1 ){
						if (  Application.isPlaying ) Destroy(this.transform.parent.parent.gameObject);
						else DestroyImmediate(this.transform.parent.parent.gameObject);
					}
					else {
						if (  Application.isPlaying ) Destroy(this.transform.parent.gameObject);
						else DestroyImmediate(this.transform.parent.gameObject);
					}				
				}
			}
		}


		boneList.Clear();
		if ( !Loading && boneList.Count == 0 ) {
			UpdateBoneData();
		}
		else
		if ( boneList.Count == 0 ) {
			UpdateBoneData();
		}
		UseNaturalBehaviour = _DK_UMACrowd.UseNaturalBehaviour;
	//	if ( UseNaturalBehaviour && this.gameObject.GetComponent<NaturalLauncher>() == null ) AddNaturalLauncher ();
	}

	public void AddNaturalLauncher (){
		//	NaturalLauncher
	//	this.gameObject.AddComponent<NaturalLauncher>();
	}




    void Update()
    {
        if (ownedRenderTextures != null)
        {
            foreach (var rt in ownedRenderTextures)
            {
                if (!rt.IsCreated())
                {
					if ( DK_UMACrowd.GeneratorMode == "Preset" ){
	                    isTextureDirty = true;
			//	DKumaGenerator.addDirtyUMA(this);
					}
					         }
            }
        }
    }

	public void UpdateBoneData(){
	/*	for(int i = 0; i < tempBoneData.Length; i++){			
			if ( tempBoneData[i].boneTransform != null ) boneList.Add(tempBoneData[i].boneTransform.gameObject.name,tempBoneData[i]);
		}*/
	}
	
    //void LateUpdate () {
    //    //foreach (BoneData bone in updateBoneList)
    //    //{
    //    //    bone.boneTransform.localPosition = bone.actualBonePosition;
    //    //    bone.boneTransform.localScale = bone.actualBoneScale;
    //    //}
    //}

    public void ChangeBone(string boneName, Vector3 positionToChange, Vector3 scaleToChange)
    {
        BoneData tempBoneData;
        if (boneList.TryGetValue(boneName, out tempBoneData))
        {
            tempBoneData.actualBoneScale = scaleToChange;
            tempBoneData.actualBonePosition = positionToChange;
            tempBoneData.boneTransform.localPosition = positionToChange;
            tempBoneData.boneTransform.localScale = scaleToChange;
        }
    }
	
	public void ChangeBonePosition(string boneName,Vector3 positionToChange) {
        BoneData tempBoneData;
        if (boneList.TryGetValue(boneName, out tempBoneData))
        {
            tempBoneData.actualBonePosition = positionToChange;
            tempBoneData.boneTransform.localPosition = positionToChange;
        }
    }

    public void ChangeBoneScale(string boneName, Vector3 scaleToChange)
    {
        BoneData tempBoneData;
        if (boneList.TryGetValue(boneName, out tempBoneData))
        {
            tempBoneData.actualBoneScale = scaleToChange;
            tempBoneData.boneTransform.localScale = scaleToChange;
        }
    }

    internal void ChangeBoneMoveRelative(string boneName, Vector3 positionToChange)
    {
        BoneData tempBoneData;
        if (boneList.TryGetValue(boneName, out tempBoneData))
        {
            tempBoneData.actualBonePosition = tempBoneData.originalBonePosition + positionToChange;
            tempBoneData.boneTransform.localPosition = tempBoneData.actualBonePosition;
        }
    }

	public virtual void PackRecipe() {
		umaPackRecipe.packedSlotDataList = new packedSlotData[umaRecipe.slotDataList.Length];
		umaPackRecipe.race = umaRecipe.raceData.raceName;

		// DNA
		umaPackRecipe.packedDna.Clear();
		foreach(var dna in umaRecipe.umaDna.Values)
		{
			// DK UMA
            UMAPackedDna packedDna = new UMAPackedDna();
            packedDna.dnaType = dna.GetType().Name;

			packedDna.packedDna = DKUMADna.SaveInstance(dna);
            umaPackRecipe.packedDna.Add(packedDna);
		}

		// DK UMA
/*		DK_UMAPackedDna DK_packedDna = new DK_UMAPackedDna();
		DK_packedDna.dnaType = "Modifyers";
		DK_packedDna.tmpDNAList = DNAList2;
		//	DK_packedDna.packedDna = UMADna.SaveInstance(dna);
		umaPackRecipe.DKpackedDna = DK_packedDna; 
		//	Debug.Log ( DK_packedDna.dnaType );

		foreach (var dna in DNAList2) {
			string _value = dna.Value.ToString ("f3");
			dna.Value = float.Parse (_value);
		}
*/
		// Slots
        for (int i = 0; i < umaRecipe.slotDataList.Length; i++)
        {
            if (umaRecipe.slotDataList[i] != null)
            {

                if (//umaRecipe.slotDataList[i].listID != -1 
				    //&& 
				    ( umaPackRecipe.packedSlotDataList[i] == null || umaPackRecipe.packedSlotDataList[i].sID == null ) )
                {
                   
					packedSlotData tempPackedSlotData;

                    tempPackedSlotData = new packedSlotData();

                    tempPackedSlotData.sID = umaRecipe.slotDataList[i].slotName;
					tempPackedSlotData.oS = Mathf.FloorToInt(umaRecipe.slotDataList[i].overlayScale*100);
                    tempPackedSlotData.ODL = new DKUMAData.packedOverlayData[umaRecipe.slotDataList[i].overlayList.Count];

                    for (int oID = 0; oID < tempPackedSlotData.ODL.Length; oID++)
                    {
                        tempPackedSlotData.ODL[oID] = new packedOverlayData();
						try {
	                        tempPackedSlotData.ODL[oID].oID = umaRecipe.slotDataList[i].overlayList[oID].overlayName;
							if (umaRecipe.slotDataList[i].overlayList[oID].color != new Color(1.0f, 1.0f, 1.0f, 1.0f))
	                        {
	                            //Color32 instead of Color?
	                            tempPackedSlotData.ODL[oID].cL = new int[4];
	                            tempPackedSlotData.ODL[oID].cL[0] = Mathf.FloorToInt(umaRecipe.slotDataList[i].overlayList[oID].color.r * 255.0f);
	                            tempPackedSlotData.ODL[oID].cL[1] = Mathf.FloorToInt(umaRecipe.slotDataList[i].overlayList[oID].color.g * 255.0f);
	                            tempPackedSlotData.ODL[oID].cL[2] = Mathf.FloorToInt(umaRecipe.slotDataList[i].overlayList[oID].color.b * 255.0f);
	                            tempPackedSlotData.ODL[oID].cL[3] = Mathf.FloorToInt(umaRecipe.slotDataList[i].overlayList[oID].color.a * 255.0f);
	                        }

	                        if (umaRecipe.slotDataList[i].overlayList[oID].rect != new Rect(0, 0, 0, 0))
	                        {
	                            //Might need float in next version
	                            tempPackedSlotData.ODL[oID].rL = new int[4];
	                            tempPackedSlotData.ODL[oID].rL[0] = (int)umaRecipe.slotDataList[i].overlayList[oID].rect.x;
	                            tempPackedSlotData.ODL[oID].rL[1] = (int)umaRecipe.slotDataList[i].overlayList[oID].rect.y;
	                            tempPackedSlotData.ODL[oID].rL[2] = (int)umaRecipe.slotDataList[i].overlayList[oID].rect.width;
	                            tempPackedSlotData.ODL[oID].rL[3] = (int)umaRecipe.slotDataList[i].overlayList[oID].rect.height;
	                        }

	                        if (umaRecipe.slotDataList[i].overlayList[oID].channelMask != null)
	                        {
	                            tempPackedSlotData.ODL[oID].cML = new int[umaRecipe.slotDataList[i].overlayList[oID].channelMask.Length][];

	                            for (int channelAdjust = 0; channelAdjust < umaRecipe.slotDataList[i].overlayList[oID].channelMask.Length; channelAdjust++)
	                            {
	                                tempPackedSlotData.ODL[oID].cML[channelAdjust] = new int[4];
	                                tempPackedSlotData.ODL[oID].cML[channelAdjust][0] = umaRecipe.slotDataList[i].overlayList[oID].channelMask[channelAdjust].r;
	                                tempPackedSlotData.ODL[oID].cML[channelAdjust][1] = umaRecipe.slotDataList[i].overlayList[oID].channelMask[channelAdjust].g;
	                                tempPackedSlotData.ODL[oID].cML[channelAdjust][2] = umaRecipe.slotDataList[i].overlayList[oID].channelMask[channelAdjust].b;
	                                tempPackedSlotData.ODL[oID].cML[channelAdjust][3] = umaRecipe.slotDataList[i].overlayList[oID].channelMask[channelAdjust].a;
	                            }

	                        }
	                        if (umaRecipe.slotDataList[i].overlayList[oID].channelAdditiveMask != null)
	                        {
	                            tempPackedSlotData.ODL[oID].cAML = new int[umaRecipe.slotDataList[i].overlayList[oID].channelAdditiveMask.Length][];
	                            for (int channelAdjust = 0; channelAdjust < umaRecipe.slotDataList[i].overlayList[oID].channelAdditiveMask.Length; channelAdjust++)
	                            {
	                                tempPackedSlotData.ODL[oID].cAML[channelAdjust] = new int[4];
	                                tempPackedSlotData.ODL[oID].cAML[channelAdjust][0] = umaRecipe.slotDataList[i].overlayList[oID].channelAdditiveMask[channelAdjust].r;
	                                tempPackedSlotData.ODL[oID].cAML[channelAdjust][1] = umaRecipe.slotDataList[i].overlayList[oID].channelAdditiveMask[channelAdjust].g;
	                                tempPackedSlotData.ODL[oID].cAML[channelAdjust][2] = umaRecipe.slotDataList[i].overlayList[oID].channelAdditiveMask[channelAdjust].b;
	                                tempPackedSlotData.ODL[oID].cAML[channelAdjust][3] = umaRecipe.slotDataList[i].overlayList[oID].channelAdditiveMask[channelAdjust].a;
	                            }

	                        }
						}
						catch ( NullReferenceException ){ 
							Debug.LogError ( "DK UMA : Missing Overlay(s) or Slot(s) detected in the recipe, check the previous error messages for a solution to fixe it.") ; 
							Debug.LogError ( "DK UMA : Deleting the current avatar to avoid issues.") ; 
							// delete the avatar
							try {
							DestroyImmediate ( gameObject );
							}catch ( MissingReferenceException ){}
						}                   
					}

                    umaPackRecipe.packedSlotDataList[i] = tempPackedSlotData;

                    //Shared overlays wont generate duplicated data
                    for (int i2 = i + 1; i2 < umaRecipe.slotDataList.Length; i2++)
                    {
                        if (umaRecipe.slotDataList[i2] != null)
                        {
                            if (umaPackRecipe.packedSlotDataList[i2] == null)
                            {
                                if (umaRecipe.slotDataList[i].overlayList == umaRecipe.slotDataList[i2].overlayList)
                                {
                                    tempPackedSlotData = new packedSlotData();
                                    tempPackedSlotData.sID = umaRecipe.slotDataList[i2].slotName;
                                    tempPackedSlotData.cOI = i;
                                    //umaPackRecipe.packedSlotDataList[i2] = tempPackedSlotData;
                                }
                            }
                        }
                    }
                }
            }
        }
	//	Debug.Log ( "PackRecipe :"+umaRecipe.slotDataList.Length+" / "+umaPackRecipe.packedSlotDataList[1].sID );

	}
	
	public virtual void SaveToMemoryStream() {	
		PackRecipe();
	//	#if UNITY_5_5_OR_NEWER
		streamedUMA = JsonUtility.ToJson(umaPackRecipe);
	//	#else
	//	streamedUMA = JsonMapper.ToJson(umaPackRecipe);
	//	#endif
	}

	public virtual void UnpackRecipe() {	
	//	Debug.Log ("UnpackRecipe");
		DetectAll ();
	
		if ( umaPackRecipe == null ) {
			Debug.LogError ("PackRecipe is null");
		}
		else {
			umaRecipe.slotDataList = new DKSlotData[umaPackRecipe.packedSlotDataList.Length];
		}
		raceLibrary.Awake();
		slotLibrary.Awake();
		overlayLibrary.Awake();
//		DKumaGenerator.Awake();

		// Race
		try{
			umaRecipe.SetRace(raceLibrary.GetRace(umaPackRecipe.race));
		}catch(NullReferenceException){ Debug.LogError("Element Pack Recipe to survey : No race found"); }
	//	Debug.Log("Packed race found");

		// DNA
		umaRecipe.umaDna.Clear();
		for(int dna = 0; dna < umaPackRecipe.packedDna.Count; dna++){
			Type dnaType = DKUMADna.GetType(umaPackRecipe.packedDna[dna].dnaType);
			try{
				umaRecipe.umaDna.Add(dnaType, DKUMADna.LoadInstance(dnaType, umaPackRecipe.packedDna[dna].packedDna));
			}
			catch(ArgumentNullException){}
		}
		

		// Slot
			for (int i = 0; i < umaPackRecipe.packedSlotDataList.Length; i++)
        {
            if (umaPackRecipe.packedSlotDataList[i] != null && umaPackRecipe.packedSlotDataList[i].sID != null)
            {
				DKSlotData tempSlotData = DKSlotData.CreateInstance<DKSlotData>();
				tempSlotData = slotLibrary.InstantiateSlot(umaPackRecipe.packedSlotDataList[i].sID);
				tempSlotData.overlayScale = umaPackRecipe.packedSlotDataList[i].oS*0.01f;
				umaRecipe.slotDataList[i] = tempSlotData;

                if (umaPackRecipe.packedSlotDataList[i].cOI == -1)
                {

                    for (int overlay = 0; overlay < umaPackRecipe.packedSlotDataList[i].ODL.Length; overlay++)
                    {
                        Color tempColor;
                        Rect tempRect;

                        if (umaPackRecipe.packedSlotDataList[i].ODL[overlay].cL != null)
                        {
                            tempColor = new Color(umaPackRecipe.packedSlotDataList[i].ODL[overlay].cL[0] / 255.0f, umaPackRecipe.packedSlotDataList[i].ODL[overlay].cL[1] / 255.0f, umaPackRecipe.packedSlotDataList[i].ODL[overlay].cL[2] / 255.0f, umaPackRecipe.packedSlotDataList[i].ODL[overlay].cL[3] / 255.0f);
                        }
                        else
                        {
                            tempColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        }

                        if (umaPackRecipe.packedSlotDataList[i].ODL[overlay].rL != null)
                        {
                            tempRect = new Rect(umaPackRecipe.packedSlotDataList[i].ODL[overlay].rL[0], umaPackRecipe.packedSlotDataList[i].ODL[overlay].rL[1], umaPackRecipe.packedSlotDataList[i].ODL[overlay].rL[2], umaPackRecipe.packedSlotDataList[i].ODL[overlay].rL[3]);
                        }
                        else
                        {
                            tempRect = new Rect(0, 0, 0, 0);
                        }
						bool AlreadyIn;
						AlreadyIn = false;
						
						for(int i1 = 0; i1 <  tempSlotData.overlayList.Count; i1++){
							if ( tempSlotData.overlayList[i1].overlayName == umaPackRecipe.packedSlotDataList[i].ODL[overlay].oID ){
								AlreadyIn = true;
								iTmp = i1;
							//	tempSlotData.overlayList.Remove(tempSlotData.overlayList[i1]);
							}
						}
						tempSlotData.overlayList.Add(overlayLibrary.InstantiateOverlay(umaPackRecipe.packedSlotDataList[i].ODL[overlay].oID));
						tempSlotData.overlayList[tempSlotData.overlayList.Count-1].color = tempColor;
						tempSlotData.overlayList[tempSlotData.overlayList.Count-1].rect = tempRect;
						if ( AlreadyIn == true ){
							tempSlotData.overlayList.Remove(tempSlotData.overlayList[iTmp]);
							AlreadyIn = false;
						}
                        if (umaPackRecipe.packedSlotDataList[i].ODL[overlay].cML != null)
                        {
                            for (int channelAdjust = 0; channelAdjust < umaPackRecipe.packedSlotDataList[i].ODL[overlay].cML.Length; channelAdjust++)
                            {
                                packedOverlayData tempData = umaPackRecipe.packedSlotDataList[i].ODL[overlay];
                                tempSlotData.overlayList[tempSlotData.overlayList.Count - 1].SetColor(channelAdjust, new Color32((byte)tempData.cML[channelAdjust][0],
                                (byte)tempData.cML[channelAdjust][1],
                                (byte)tempData.cML[channelAdjust][2],
                                (byte)tempData.cML[channelAdjust][3]));
                            }
                        }

                        if (umaPackRecipe.packedSlotDataList[i].ODL[overlay].cAML != null)
                        {
                            for (int channelAdjust = 0; channelAdjust < umaPackRecipe.packedSlotDataList[i].ODL[overlay].cAML.Length; channelAdjust++)
                            {
                                packedOverlayData tempData = umaPackRecipe.packedSlotDataList[i].ODL[overlay];
                                tempSlotData.overlayList[tempSlotData.overlayList.Count - 1].SetAdditive(channelAdjust, new Color32((byte)tempData.cAML[channelAdjust][0],
                                (byte)tempData.cAML[channelAdjust][1],
                                (byte)tempData.cAML[channelAdjust][2],
                                (byte)tempData.cAML[channelAdjust][3]));
                            }
                        }

                    }
                }
                else
                {

                    tempSlotData.overlayList = umaRecipe.slotDataList[umaPackRecipe.packedSlotDataList[i].cOI].overlayList;

                }
            }
        }
	//	Debug.Log("Packed slots list :"+umaPackRecipe.packedSlotDataList.Length.ToString() );
		if ( gameObject.GetComponent<DK_RPG_UMA>() == null ){
			isShapeDirty = true;
			isTextureDirty = true;
			isMeshDirty = true;
			Dirty();
		}
	}
	
	public virtual void LoadFromMemoryStream() {
	//	Debug.Log ("LoadFromMemoryStream");
	//	#if UNITY_5_5_OR_NEWER
		umaPackRecipe = JsonUtility.FromJson<UMAPackRecipe>(streamedUMA);
	//	#else
	//	umaPackRecipe = JsonMapper.ToObject<UMAPackRecipe>(streamedUMA);
	//	#endif
		UnpackRecipe();
	}

	public virtual void LoadDNAFromMemoryStream ( string Streamed ) {
	//	#if UNITY_5_5_OR_NEWER
		umaPackRecipe = JsonUtility.FromJson<UMAPackRecipe>(Streamed);
	//	#else
	//	umaPackRecipe = JsonMapper.ToObject<UMAPackRecipe>(Streamed);
	//	#endif
		UnpackDNARecipe();
	}

	public virtual void UnpackDNARecipe() {		
		try{			
			// DNA
			umaRecipe.umaDna.Clear();
			for(int dna = 0; dna < umaPackRecipe.packedDna.Count; dna++){
				Type dnaType = DKUMADna.GetType(umaPackRecipe.packedDna[dna].dnaType);

					umaRecipe.umaDna.Add(dnaType, DKUMADna.LoadInstance(dnaType, umaPackRecipe.packedDna[dna].packedDna));
			}

			DKUMADnaHumanoid DNA = GetDna<DKUMADnaHumanoid>();
			Debug.Log ("DNA length "+DNA.Values.Length);
			for(int i = 0; i < DNA.Values.Length; i ++){
				float tmpValue = DNA.Values[i];
				if ( tmpValue < 0 ) tmpValue = 0;
				if ( i < DNAList2.Count )
					DNAList2[i].Value = tmpValue;
			}
		}
		catch(ArgumentNullException){}
	}
	
	void OnDestroy() {
		if(_hasUpdatedBefore){
			cleanTextures();
			cleanMesh();
		}
	}
	
	public void cleanTextures(){
		for(int atlasIndex = 0; atlasIndex < atlasList.atlas.Count; atlasIndex++){
			if(atlasList.atlas[atlasIndex] != null && atlasList.atlas[atlasIndex].resultingAtlasList != null){
				for(int textureIndex = 0; textureIndex < atlasList.atlas[atlasIndex].resultingAtlasList.Length; textureIndex++){
					
					if(atlasList.atlas[atlasIndex].resultingAtlasList[textureIndex] != null){
						Texture tempTexture = atlasList.atlas[atlasIndex].resultingAtlasList[textureIndex];
						if(tempTexture is RenderTexture){
							RenderTexture tempRenderTexture = tempTexture as RenderTexture;
							tempRenderTexture.Release();
							#if UNITY_EDITOR

							#endif
							if (  Application.isPlaying ) Destroy(tempRenderTexture);
							else 	
							tempRenderTexture = null;
						}else{
							if (  Application.isPlaying ) Destroy(tempTexture);
							else DestroyImmediate(tempTexture);
						}
						atlasList.atlas[atlasIndex].resultingAtlasList[textureIndex] = null;
					}				
				}
			}
		}
	}
	
	public void cleanMesh(){
	//	for(int i = 0; i < TMPmyRenderer.sharedMaterials.Length; i++){
	//		if (  Application.isPlaying ) Destroy(TMPmyRenderer.sharedMaterials[i]);
	//		else 	
	//		DestroyImmediate(TMPmyRenderer.sharedMaterials[i]);
	//	}
	//	if (  Application.isPlaying ) Destroy(TMPmyRenderer.sharedMesh);
	//	else 
	//		DestroyImmediate(TMPmyRenderer.sharedMesh);
	//	

		for(int i = 0; i < myRenderer.sharedMaterials.Length; i++){
			if (  Application.isPlaying ) Destroy(myRenderer.sharedMaterials[i]);
			else 	
				DestroyImmediate(myRenderer.sharedMaterials[i]);
		}
		if (  Application.isPlaying ) Destroy(myRenderer.sharedMesh);
		else 
			DestroyImmediate(myRenderer.sharedMesh);
	}
	
	public virtual void UpdateCollider(){
		if(capsuleCollider){
		//	UMADnaHumanoid umaDna = umaRecipe.umaDna[typeof(UMADnaHumanoid)] as UMADnaHumanoid;

			capsuleCollider.height = (DNAList2[0].Value + 0.5f)*2 + 0.0f;
			capsuleCollider.radius = (DNAList2[0].Value + 0.5f)/3.5f + 0.0f;
			capsuleCollider.center = new Vector3(0,capsuleCollider.height*0.5f - 0.05f,0);
		}
	}

    RenderTexture[] ownedRenderTextures;
    internal RenderTexture[] RetrieveRenderTextures()
    {
        return ownedRenderTextures;
    }

	internal void StoreRenderTextures(RenderTexture[] resultingRenderTextures)
    {
        ownedRenderTextures = resultingRenderTextures;
    }


	internal void EnsureBoneData(Transform[] umaBones, Dictionary<Transform, Transform> boneMap)
    {
        foreach (var bone in umaBones)
        {
			if ( bone && boneMap.ContainsKey(bone) ){
					var umaBone = boneMap[bone];
			
				try{
		            if (!boneList.ContainsKey(umaBone.name))
		            {
		                BoneData newBoneData = new BoneData();
		                newBoneData.actualBonePosition = umaBone.localPosition;
		                newBoneData.originalBonePosition = umaBone.localPosition;
		                newBoneData.actualBoneScale = umaBone.localScale;
						newBoneData.originalBoneScale = umaBone.localScale;
		                newBoneData.boneTransform = umaBone;
		                boneList.Add(umaBone.name, newBoneData);
		            }
				}catch(KeyNotFoundException)
				{
					Debug.LogWarning ("A bone is missing from one of your 3rd party content, this is not a fatal error and the bone is skipped (KeyNotFoundException: The given key was not found in the dictionary).");
					Debug.LogWarning ("Verify that the meshes and textures declared in the slot or overlay are installed to your project.");
				}
			}
        }


        if (updateBoneList.Length != umaRecipe.raceData.AnimatedBones.Length)
        {
            updateBoneList = new BoneData[umaRecipe.raceData.AnimatedBones.Length];
        }
        int i = 0;
        foreach (var updateName in umaRecipe.raceData.AnimatedBones)
        {
            updateBoneList[i++] = boneList[updateName];
        }
    }

    public T GetDna<T>()
        where T : DKUMADna
    {
        return umaRecipe.GetDna<T>();
    }


    public void Dirty(bool dnaDirty, bool textureDirty, bool meshDirty)
    {
        isShapeDirty   |= dnaDirty;
        isTextureDirty |= textureDirty;
        isMeshDirty    |= meshDirty;
		Dirty();
    }
	
	public void SetSlot(int index, DKSlotData slot){
		
		if(index >= umaRecipe.slotDataList.Length){
			DKSlotData[] tempArray = umaRecipe.slotDataList;
			umaRecipe.slotDataList = new DKSlotData[index + 1];
			for(int i = 0; i < tempArray.Length; i++){
				umaRecipe.slotDataList[i] = tempArray[i];
			}			
		}
		umaRecipe.slotDataList[index] = slot;
	}
	
	public void SetSlots(DKSlotData[] slots){
		umaRecipe.slotDataList = slots;
	}
	
	public DKSlotData GetSlot(int index){
		return umaRecipe.slotDataList[index];	
	}
}