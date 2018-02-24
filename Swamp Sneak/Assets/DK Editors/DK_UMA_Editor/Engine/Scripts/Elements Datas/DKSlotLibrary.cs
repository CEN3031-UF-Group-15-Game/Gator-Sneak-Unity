using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UMA;

#pragma warning disable 618

public class DKSlotLibrary : MonoBehaviour {
	public DKSlotData[] slotElementList = new DKSlotData[0];
	public Dictionary<string,DKSlotData> slotDictionary = new Dictionary<string,DKSlotData>();
	
	// modified by DK
	public void Awake() {
		// find lib wizard
		GameObject DK_UMA = GameObject.Find("DK_UMA");
		GameObject UMA = GameObject.Find("UMA_DCS");
		if ( UMA == null ) UMA = GameObject.Find("UMA");
	//	GameObject oldUMA = GameObject.Find("UMA");

		if ( DK_UMA != null ) {
			if ( UMA == null ){
				Debug.LogError ("DK UMA : 'UMA' object not found in the scene. " +
					"Install it using the 'Elements Manager' window.");
			}
			else if ( UMA != null ){
				// get DK UMA Game Settings
				DK_UMA_GameSettings GameSettings = DK_UMA.transform.GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings;

				if ( GameSettings._GameLibraries.UseLibWizard ){
					// get DK elements from Lib Wizard
					IList<DKSlotData> DkSlotsLibrary = GameSettings._GameLibraries.DkSlotsLibrary.AsReadOnly();
					slotElementList = new List<DKSlotData>(DkSlotsLibrary).ToArray();

					// get UMA elements from Lib Wizard
					IList<UMA.SlotDataAsset> UmaSlotsLibrary = GameSettings._GameLibraries.UmaSlotsLibrary.AsReadOnly();
				//	UMA.transform.GetComponentInChildren<SlotLibrary>().slotElementList = 
				//		new List<UMA.SlotDataAsset>(UmaSlotsLibrary).ToArray();

					#if UMADCS
					DynamicSlotLibrary UMALibrary = FindObjectOfType<DynamicSlotLibrary>();
					List<UMA.SlotDataAsset> existingList = UMALibrary.GetAllSlotAssets ().ToList();
					foreach ( UMA.SlotDataAsset element in UmaSlotsLibrary ){
							if ( existingList.Contains (element) == false )
							UMALibrary.AddSlotAsset(element);
					}
					#else
					SlotLibrary UMALibrary = FindObjectOfType<SlotLibrary>();
					List<UMA.SlotDataAsset> existingList = UMALibrary.GetAllSlotAssets ().ToList();
					foreach ( UMA.SlotDataAsset element in UmaSlotsLibrary ){
						if ( existingList.Contains (element) == false )
							UMALibrary.AddSlotAsset(element);
					}
					#endif
				}
			}
		}
		UpdateDictionary();

	}
	// end modified by DK



	public void UpdateDictionary(){
		slotDictionary.Clear();
		for(int i = 0; i < slotElementList.Length; i++){			
			if(slotElementList[i]){	
				if(!slotDictionary.ContainsKey(slotElementList[i].slotName)){
					slotElementList[i].listID = i;
					slotDictionary.Add(slotElementList[i].slotName,slotElementList[i]);	
				}
			}
		}
	}
	
	public void AddSlot(string name, DKSlotData slot)
	{
		
		var list = new DKSlotData[slotElementList.Length + 1];
		for (int i = 0; i < slotElementList.Length; i++)
		{
			if ( slotElementList[i] == null ) {
			//	Debug.LogError ("The DK Slot Library have some missing element");
			}
			else if ( slot == null ){
			//	Debug.LogError ("The DK Slot is null");
			}
		/*	else if ( list[i] == null ){
				Debug.LogError ("The tmp list have some missing element");
			}*/
			else {
				try {
				if (slotElementList[i].slotName == name)
				{
					slotElementList[i] = slot;
					return;
				}
				list[i] = slotElementList[i];
				}
				catch (System.NullReferenceException e ) {
					Debug.LogError (slot.slotName+" is different than "+name+" : "+e);
				}
			}
		}


		list[list.Length - 1] = slot;
		slotElementList = list;
		if ( !slotDictionary.ContainsKey( name ) 
		    && !slotDictionary.ContainsValue( slot ) )
			slotDictionary.Add(name, slot);
	}

	public DKSlotData InstantiateSlot(string name){
		DKSlotData source;
		if (!slotDictionary.TryGetValue(name, out source))
		{
			DKUMA_Variables _DKUMA_Variables = FindObjectOfType<DKUMA_Variables>();
			if ( _DKUMA_Variables != null && _DKUMA_Variables._DK_UMA_GameSettings != null ) _DKUMA_Variables._DK_UMA_GameSettings.EnsureAllLibraries ();
		}
		if (!slotDictionary.TryGetValue(name, out source))
		{
			Debug.LogError("Unable to find " + name+" : The slot is not present in the current DK Slots Library. If you are converting a UMA avatar to DK UMA, you need to convert the UMA slot, set it up then add it to the Library.");
			return null;
		}else{
			if ( source._UMA != null )
				VerifyUMALib ( source._UMA, name );
			
			return source.Duplicate();
		}
	}

	public DKSlotData InstantiateSlot(string name,List<DKOverlayData> overlayList){
		DKSlotData source;
		if (!slotDictionary.TryGetValue(name, out source))
		{
			GameObject DK_UMA = GameObject.Find ("DK_UMA");

			// try to get by object name
			foreach ( DKSlotData Slot in DK_UMA.transform.GetComponentInChildren<DKSlotLibrary>().slotElementList ){
				if ( Slot.name == name ) {
					source = Slot.Duplicate();
					source.overlayList = overlayList;

					if ( source._UMA != null )
						VerifyUMALib ( source._UMA, name );
				}
			}
			if ( source == null ){
				Debug.LogError("Unable to find " +name);
			}
			return source;
		}else{
			source = source.Duplicate();
			source.overlayList = overlayList;

			if ( source._UMA != null )
				VerifyUMALib ( source._UMA, name );

			return source;

		}
	}

	public void VerifyUMALib ( SlotDataAsset asset, string name ){
		SlotLibrary UslotLibrary = FindObjectOfType<SlotLibrary>();

		if ( UslotLibrary.HasSlot (name) == false ){
		//	Debug.Log ("Correcting UMASlotLib about : "+name+" ( "+asset.name+" / "+asset.slotName+" )");
			if ( asset != null )
				UslotLibrary.AddSlotAsset (asset);
			else {
				Debug.LogError ("Problem with : "+name+"");
			}
		}
	}
}