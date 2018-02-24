using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UMA;

#pragma warning disable 618

public class DKOverlayLibrary : MonoBehaviour {
    public DKOverlayData[] overlayElementList = new DKOverlayData[0];
	public Dictionary<string,DKOverlayData> overlayDictionary = new Dictionary<string,DKOverlayData>();
	
	public int scaleAdjust = 1;
	public bool readWrite = false;
	public bool compress = false;
	
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
					IList<DKOverlayData> DkOverlaysLibrary = GameSettings._GameLibraries.DkOverlaysLibrary.AsReadOnly();
					overlayElementList = new List<DKOverlayData>(DkOverlaysLibrary).ToArray();
					
					// get UMA elements from Lib Wizard
					IList<UMA.OverlayDataAsset> UmaOverlaysLibrary = GameSettings._GameLibraries.UmaOverlaysLibrary.AsReadOnly();
					#if UMADCS
					DynamicOverlayLibrary UMALibrary = FindObjectOfType<DynamicOverlayLibrary>();
					List<UMA.OverlayDataAsset> existingList = UMALibrary.GetAllOverlayAssets ().ToList();
					foreach ( UMA.OverlayDataAsset element in UmaOverlaysLibrary ){
						if ( existingList.Contains (element) == false )
							UMALibrary.AddOverlayAsset(element);
					}
					#else
					OverlayLibrary UMALibrary = FindObjectOfType<OverlayLibrary>();
					List<UMA.OverlayDataAsset> existingList = UMALibrary.GetAllOverlayAssets ().ToList();
					foreach ( UMA.OverlayDataAsset element in UmaOverlaysLibrary ){
						if ( existingList.Contains (element) == false )
							UMALibrary.AddOverlayAsset(element);
					}
					#endif
				}
			}
		}
		UpdateDictionary();
	}

	public void UpdateDictionary(){
		overlayDictionary.Clear();
		for(int i = 0; i < overlayElementList.Length; i++){			
			if(overlayElementList[i]){				
				if(!overlayDictionary.ContainsKey(overlayElementList[i].overlayName)){
					overlayElementList[i].listID = i;
					overlayDictionary.Add(overlayElementList[i].overlayName,overlayElementList[i]);	
				}
			}
		}
	}

    public void AddOverlay(string name, DKOverlayData overlay)
    {
        var list = new DKOverlayData[overlayElementList.Length + 1];
        for (int i = 0; i < overlayElementList.Length; i++)
        {
            if (overlayElementList[i].overlayName == name)
            {
                overlayElementList[i] = overlay;
                return;
            }
            list[i] = overlayElementList[i];
        }
        list[list.Length - 1] = overlay;
        overlayElementList = list;
        overlayDictionary.Add(name, overlay);
    }
	
	public DKOverlayData GetOverlay(string name){		
		return new DKOverlayData(this, name);
	}
	
	public DKOverlayData InstantiateOverlay(string name){
		DKOverlayData source;
        if (!overlayDictionary.TryGetValue(name, out source))
        {
			Debug.LogError("Unable to find " +name+" in '"+overlayDictionary.ToString());
			return null;
        }else{
			return source.Duplicate();
		}
	}
	
	public DKOverlayData InstantiateOverlay(string name, Color color){
		DKOverlayData source;
        if (!overlayDictionary.TryGetValue(name, out source))
        {
			GameObject DK_UMA = GameObject.Find ("DK_UMA");

			// try to get by object name
			foreach ( DKOverlayData Overlay in DK_UMA.transform.GetComponentInChildren<DKOverlayLibrary>().overlayElementList ){
				if ( Overlay.name == name ) {
					source = Overlay.Duplicate();
					source.color = color;
				}
			}
			if ( source == null ){
				Debug.LogError("DK UMA : Unable to find " +name+" in '"+this.name+". Use the Elements Manager to prepare the libraries by clicking on 'Add to Libraries'.");
			}
			return source;
        }else{
			source = source.Duplicate();
			source.color = color;
			return source;
		}
	}
}