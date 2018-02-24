using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using UMA;




public class DK_RPG_ReBuild25 : MonoBehaviour {
	DK_UMACrowd Crowd;

	public DKUMAData umaData;
	DK_RPG_UMA _DK_RPG_UMA;
//	DK_RPG_UMA_Generator _DK_RPG_UMA_Generator;

	public List<DKSlotData> TmpSlotDataList = new List<DKSlotData>();
	public List<DKSlotData> AssignedSlotsList = new List<DKSlotData>();
	public List<DKOverlayData> AssignedOverlayList = new List<DKOverlayData>();
	public List<DKOverlayData> TmpTorsoOverLayList = new List<DKOverlayData>();

	// Direct to UMA 2.1
	public List<SlotData> TmpUMASlotDataList =  new List<SlotData>();

	public DKSlotData FaceSlot;
	public int HeadIndex = 0;
	public DKSlotData TorsoSlot;
	public int TorsoIndex = 0;
	public DKSlotData _Slot;
	public DKOverlayData _Overlay;
	public DKOverlayData _FaceOverlay;
	public Color _Color;
	public ColorPresetData _ColorPreset;
	 
	public bool RefreshOnly = false;

	bool _save = false;

	DKUMA_Variables _DKUMA_Variables;
	DK_UMA_GameSettings _DK_UMA_GameSettings;
	public UMAContext umaContext;

	public void Launch (DKUMAData UMAData){
//		Debug.Log ("rebuilding avatar");
		Debug.Log ("DK_RPG_ReBuild25");

		umaContext = FindObjectOfType <UMAContext>();

		_DKUMA_Variables = FindObjectOfType <DKUMA_Variables>();;
		_DK_UMA_GameSettings = null;
		if ( _DKUMA_Variables != null ) _DK_UMA_GameSettings = _DKUMA_Variables._DK_UMA_GameSettings;

		_save = true;
		umaData = UMAData;
		_DK_RPG_UMA = this.gameObject.GetComponent<DK_RPG_UMA>();
		Crowd = GameObject.Find ("DKUMACrowd").GetComponent<DK_UMACrowd>();
		Crowd.overlayLibrary.Awake();
		Crowd.slotLibrary.Awake();
		Crowd.raceLibrary.Awake();

		_DK_RPG_UMA._Avatar.HeadIndex = HeadIndex;
		TorsoIndex = _DK_RPG_UMA._Avatar.TorsoIndex;
		TmpSlotDataList.Clear();
		TmpUMASlotDataList.Clear();
		AssignedSlotsList.Clear();
		AssignedOverlayList.Clear();
		TmpTorsoOverLayList.Clear();
		Crowd.Wears.HideUnderwear = false;

		if ( _DK_RPG_UMA.AnatomyRule.AnatomyToCreate == DK_RPG_UMA.AnatomyChoice.CompleteAvatar )RebuildFace ();
		RebuildBody ();
	}

	public void Launch (DKUMAData UMAData, bool save){
		_save = false;
		//	Debug.Log ("rebuilding avatar");
		umaData = UMAData;
		_DK_RPG_UMA = this.gameObject.GetComponent<DK_RPG_UMA>();
		Crowd = GameObject.Find ("DKUMACrowd").GetComponent<DK_UMACrowd>();
		Crowd.overlayLibrary.Awake();
		Crowd.slotLibrary.Awake();
		Crowd.raceLibrary.Awake();

		_DK_RPG_UMA._Avatar.HeadIndex = HeadIndex;
		TorsoIndex = _DK_RPG_UMA._Avatar.TorsoIndex;
		TmpSlotDataList.Clear();
		AssignedSlotsList.Clear();
		AssignedOverlayList.Clear();
		TmpTorsoOverLayList.Clear();
		Crowd.Wears.HideUnderwear = false;
		
		if ( _DK_RPG_UMA.AnatomyRule.AnatomyToCreate == DK_RPG_UMA.AnatomyChoice.CompleteAvatar ) RebuildFace ();
		RebuildBody ();
	}
	
	public void RebuildFace (){
//		Debug.Log ("rebuilding Face");
		// _Head
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Avatar._Face._Head.Slot;
		_Overlay = _DK_RPG_UMA._Avatar._Face._Head.Overlay;
		_Color = _DK_RPG_UMA._Avatar.SkinColor;
		if ( _Slot && _Overlay){		
			AssigningSlot (Crowd, _Slot, _Overlay, "_Head", _Color, null);	// assign the slot and its overlays				
			_FaceOverlay = _Overlay;
			HeadIndex = 0;
			_DK_RPG_UMA._Avatar.HeadIndex = HeadIndex;
		}

		// _Eyes
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Avatar._Face._Eyes.Slot;
		_Overlay = _DK_RPG_UMA._Avatar._Face._Eyes.Overlay;
		_Color = _DK_RPG_UMA._Avatar._Face._Eyes.Color;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_Eyes", _Color, null);	// assign the slot and its overlays				

		// _EyesAdjust
		_Slot = null;
		_Overlay = null;
		_Overlay = _DK_RPG_UMA._Avatar._Face._Eyes.Adjust;
		_Color = _DK_RPG_UMA._Avatar.EyeColor;
		if ( _Overlay)
			AssigningOverlay (Crowd, TmpSlotDataList.Count-1, _Overlay, "_EyesAdjust", true, _Color, null);	// assign the slot and its overlays				

		// _Ears
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Avatar._Face._Ears.Slot;
		_Overlay = _DK_RPG_UMA._Avatar._Face._Ears.Overlay;
		_Color = _DK_RPG_UMA._Avatar.SkinColor;
		if ( _Slot )
			AssigningSlot (Crowd, _Slot, _Overlay, "_Ears", _Color, null);	// assign the slot and its overlays	

		// _Head_Tatoo
		_Overlay = null;
		_Overlay = _DK_RPG_UMA._Avatar._Face._Head.Tattoo;
		_Color = _DK_RPG_UMA._Avatar._Face._Head.TattooColor;
		if ( _Overlay){
			AssigningOverlay (Crowd, HeadIndex, _Overlay, "_Head_Tatoo", true, _Color, null);	// assign the slot and its overlays				
		}
		// _Head_MakeUp
		_Overlay = null;
		_Overlay = _DK_RPG_UMA._Avatar._Face._Head.Makeup;
		_Color = _DK_RPG_UMA._Avatar._Face._Head.MakeupColor;
		if ( _Overlay){
			AssigningOverlay (Crowd, HeadIndex, _Overlay, "_Head_MakeUp", true, _Color, null);	// assign the slot and its overlays				
		}

		// _Lips
		_Overlay = null;
		_Overlay = _DK_RPG_UMA._Avatar._Face._Mouth.Lips;
		_Color = _DK_RPG_UMA._Avatar._Face._Mouth.LipsColor;
		if ( _Overlay)
			AssigningOverlay (Crowd, HeadIndex, _Overlay, "_Lips", true, _Color, null);	// assign the slot and its overlays				

		#region _FaceHair
		// _Eyebrow
		_Overlay = null;
		_Overlay = _DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrows;
		_Color = _DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrowsColor;
		if ( _Overlay)
			AssigningOverlay (Crowd, HeadIndex, _Overlay, "_Eyebrow", true, _Color, null);	// assign the slot and its overlays				

		#region  _BeardOverlayOnly
		// _Beard1
		_Overlay = null;
		_Overlay = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard1;
		_Color = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard1Color;
		if ( _Overlay)
			AssigningOverlay (Crowd, HeadIndex, _Overlay, "_Beard1", true, _Color, null);	// assign the slot and its overlays				

		// _Beard2
		_Overlay = null;
		_Overlay = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard2;
		_Color = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard2Color;
		if ( _Overlay)
			AssigningOverlay (Crowd, HeadIndex, _Overlay, "_Beard2", true, _Color, null);	// assign the slot and its overlays				

		// _Beard3
		_Overlay = null;
		_Overlay = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard3;
		_Color = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard3Color;
		if ( _Overlay)
			AssigningOverlay (Crowd, HeadIndex, _Overlay, "_Beard3", true, _Color, null);	// assign the slot and its overlays				
		#endregion _BeardOverlayOnly

		#region _BeardSlotOnly
		// _BeardSlotOnly
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Slot;
		_Overlay = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Overlay;
		_Color = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Color;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_BeardSlotOnly", _Color, null);	// assign the slot and its overlays				
		#endregion _BeardSlotOnly
		#endregion _FaceHair

		#region Hair
		#region _SlotOnly
		// _HairSlotOnly
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Avatar._Hair._SlotOnly.Slot;
		_Overlay = _DK_RPG_UMA._Avatar._Hair._SlotOnly.Overlay;
		_Color = _DK_RPG_UMA._Avatar.HairColor;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_HairSlotOnly", _Color, null);	// assign the slot and its overlays		
		#region _Hair_Module
		// _HairSlotOnlyModule
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Slot;
		_Overlay = _DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Overlay;
		_Color = _DK_RPG_UMA._Avatar.HairColor;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_HairSlotOnlyModule", _Color, null);	// assign the slot and its overlays
		#endregion _Hair_Module
		#endregion _SlotOnly

		#region _OverlayOnly
		// _HairOverlayOnly
		_Overlay = null;
		_Overlay = _DK_RPG_UMA._Avatar._Hair._OverlayOnly.Overlay;
		_Color = _DK_RPG_UMA._Avatar.HairColor;
		if ( _Overlay)
			AssigningOverlay (Crowd, HeadIndex, _Overlay, "_HairOverlayOnly", true, _Color, null);
		#endregion _OverlayOnly
		#endregion Hair

		#region _Face

		// _EyeLash
		_Slot = null;
		_Overlay = null;
		
		_Slot = _DK_RPG_UMA._Avatar._Face._EyeLash.Slot;
		_Overlay = _DK_RPG_UMA._Avatar._Face._EyeLash.Overlay;
		_Color = _DK_RPG_UMA._Avatar._Face._EyeLash.Color;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_EyeLash", _Color, null);	// assign the slot and its overlays	

		// _EyeLids
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Avatar._Face._EyeLids.Slot;
		_Overlay = _DK_RPG_UMA._Avatar._Face._EyeLids.Overlay;
		_Color = _DK_RPG_UMA._Avatar.SkinColor;
		if ( _Slot )
			AssigningSlot (Crowd, _Slot, _Overlay, "_EyeLids", _Color, null);	// assign the slot and its overlays	

		// _Nose
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Avatar._Face._Nose.Slot;
		_Overlay = _DK_RPG_UMA._Avatar._Face._Nose.Overlay;
		_Color = _DK_RPG_UMA._Avatar.SkinColor;
		if ( _Slot )
			AssigningSlot (Crowd, _Slot, _Overlay, "_Nose", _Color, null);	// assign the slot and its overlays	

		// _Mouth
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Avatar._Face._Mouth.Slot;
		_Overlay = _DK_RPG_UMA._Avatar._Face._Mouth.Overlay;
		_Overlay = _FaceOverlay;
		_Color = _DK_RPG_UMA._Avatar.SkinColor;
		if ( _Slot && _Overlay )
			AssigningSlot (Crowd, _Slot, _Overlay, "_Mouth", _Color, null);	// assign the slot and its overlays	

		// _InnerMouth
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.Slot;
		_Overlay = _DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.Overlay;
		_Color = _DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.color;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_InnerMouth", _Color, null);	// assign the slot and its overlays	
		#endregion _Face


		if ( Application.isPlaying == true ) {
//			Debug.Log ( "Launch Rebuild Body" );

		//	RebuildBody ();
		}
	}

	public void RebuildBody (){
//		Debug.Log ( "Rebuild Body" );
		TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Avatar._Body._Torso.Overlay.overlayName, _DK_RPG_UMA._Avatar.SkinColor));
		TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Avatar._Body._Torso.Overlay._UMA;
		if ( _DK_RPG_UMA._Avatar._Body._Torso.Tattoo ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Avatar._Body._Torso.Tattoo.overlayName, _DK_RPG_UMA._Avatar._Body._Torso.TattooColor));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Avatar._Body._Torso.Tattoo._UMA;
		}
		if ( _DK_RPG_UMA._Avatar._Body._Torso.Makeup ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Avatar._Body._Torso.Makeup.overlayName, _DK_RPG_UMA._Avatar._Body._Torso.MakeupColor));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Avatar._Body._Torso.Makeup._UMA;
		}
		if ( _DK_RPG_UMA._Avatar._Body._Underwear.Overlay ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Avatar._Body._Underwear.Overlay.overlayName, _DK_RPG_UMA._Avatar._Body._Underwear.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Avatar._Body._Underwear.Overlay._UMA;
		}

		// Equipment overlay only
		if ( _DK_RPG_UMA.AnatomyRule.AnatomyToCreate == DK_RPG_UMA.AnatomyChoice.CompleteAvatar ){
			if ( _DK_RPG_UMA._Equipment._HeadSub.Slot == null && _DK_RPG_UMA._Equipment._HeadSub.Overlay != null ) {
				TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._HeadSub.Overlay.overlayName, _DK_RPG_UMA._Equipment._HeadSub.Color));
				TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._HeadSub.Overlay._UMA;
			}
			if ( _DK_RPG_UMA._Equipment._Head.Slot == null && _DK_RPG_UMA._Equipment._Head.Overlay != null ) {
				TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._Head.Overlay.overlayName, _DK_RPG_UMA._Equipment._Head.Color));
				TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._Head.Overlay._UMA;
			}
			if ( _DK_RPG_UMA._Equipment._HeadCover.Slot == null && _DK_RPG_UMA._Equipment._HeadCover.Overlay != null ) {
				TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._HeadCover.Overlay.overlayName, _DK_RPG_UMA._Equipment._HeadCover.Color));
				TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._HeadCover.Overlay._UMA;
			}
		}

		if ( _DK_RPG_UMA._Equipment._CollarSub.Slot == null && _DK_RPG_UMA._Equipment._CollarSub.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._CollarSub.Overlay.overlayName, _DK_RPG_UMA._Equipment._CollarSub.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._CollarSub.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._Collar.Slot == null && _DK_RPG_UMA._Equipment._Collar.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._Collar.Overlay.overlayName, _DK_RPG_UMA._Equipment._Collar.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._Collar.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._CollarCover.Slot == null && _DK_RPG_UMA._Equipment._CollarCover.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._CollarCover.Overlay.overlayName, _DK_RPG_UMA._Equipment._CollarCover.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._CollarCover.Overlay._UMA;
		}

		if ( _DK_RPG_UMA._Equipment._ShoulderSub.Slot == null && _DK_RPG_UMA._Equipment._ShoulderSub.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._ShoulderSub.Overlay.overlayName, _DK_RPG_UMA._Equipment._ShoulderSub.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._ShoulderSub.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._Shoulder.Slot == null && _DK_RPG_UMA._Equipment._Shoulder.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._Shoulder.Overlay.overlayName, _DK_RPG_UMA._Equipment._Shoulder.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._Shoulder.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._ShoulderCover.Slot == null && _DK_RPG_UMA._Equipment._ShoulderCover.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._ShoulderCover.Overlay.overlayName, _DK_RPG_UMA._Equipment._ShoulderCover.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._ShoulderCover.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._TorsoSub.Slot == null && _DK_RPG_UMA._Equipment._TorsoSub.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._TorsoSub.Overlay.overlayName, _DK_RPG_UMA._Equipment._TorsoSub.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._TorsoSub.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._Torso.Slot == null && _DK_RPG_UMA._Equipment._Torso.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._Torso.Overlay.overlayName, _DK_RPG_UMA._Equipment._Torso.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._Torso.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._TorsoCover.Slot == null && _DK_RPG_UMA._Equipment._TorsoCover.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._TorsoCover.Overlay.overlayName, _DK_RPG_UMA._Equipment._TorsoCover.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._TorsoCover.Overlay._UMA;
		}
		if ( _DK_RPG_UMA.AnatomyRule.AnatomyToCreate == DK_RPG_UMA.AnatomyChoice.CompleteAvatar 
			|| _DK_RPG_UMA.AnatomyRule.AnatomyToCreate == DK_RPG_UMA.AnatomyChoice.NoHead ){
			if ( _DK_RPG_UMA._Equipment._ArmBandSub.Slot == null && _DK_RPG_UMA._Equipment._ArmBandSub.Overlay != null ) {
				TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._ArmBandSub.Overlay.overlayName, _DK_RPG_UMA._Equipment._ArmBandSub.Color));
				TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._ArmBandSub.Overlay._UMA;
			}
			if ( _DK_RPG_UMA._Equipment._ArmBand.Slot == null && _DK_RPG_UMA._Equipment._ArmBand.Overlay != null ) {
				TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._ArmBand.Overlay.overlayName, _DK_RPG_UMA._Equipment._ArmBand.Color));
				TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._ArmBand.Overlay._UMA;
			}
			if ( _DK_RPG_UMA._Equipment._ArmBandCover.Slot == null && _DK_RPG_UMA._Equipment._ArmBandCover.Overlay != null ) {
				TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._ArmBandCover.Overlay.overlayName, _DK_RPG_UMA._Equipment._ArmBandCover.Color));
				TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._ArmBandCover.Overlay._UMA;
			}
			if ( _DK_RPG_UMA._Equipment._WristSub.Slot == null && _DK_RPG_UMA._Equipment._WristSub.Overlay != null ) {
				TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._WristSub.Overlay.overlayName, _DK_RPG_UMA._Equipment._WristSub.Color));
				TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._WristSub.Overlay._UMA;
			}
			if ( _DK_RPG_UMA._Equipment._Wrist.Slot == null && _DK_RPG_UMA._Equipment._Wrist.Overlay != null ) {
				TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._Wrist.Overlay.overlayName, _DK_RPG_UMA._Equipment._Wrist.Color));
				TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._Wrist.Overlay._UMA;
			}
			if ( _DK_RPG_UMA._Equipment._WristCover.Slot == null && _DK_RPG_UMA._Equipment._WristCover.Overlay != null ) {
				TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._WristCover.Overlay.overlayName, _DK_RPG_UMA._Equipment._WristCover.Color));
				TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._WristCover.Overlay._UMA;
			}
			if ( _DK_RPG_UMA._Equipment._HandsSub.Slot == null && _DK_RPG_UMA._Equipment._HandsSub.Overlay != null ) {
				TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._HandsSub.Overlay.overlayName, _DK_RPG_UMA._Equipment._HandsSub.Color));
				TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._HandsSub.Overlay._UMA;
			}
			if ( _DK_RPG_UMA._Equipment._Hands.Slot == null && _DK_RPG_UMA._Equipment._Hands.Overlay != null ) {
				TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._Hands.Overlay.overlayName, _DK_RPG_UMA._Equipment._Hands.Color));
				TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._Hands.Overlay._UMA;
			}
			if ( _DK_RPG_UMA._Equipment._HandsCover.Slot == null && _DK_RPG_UMA._Equipment._HandsCover.Overlay != null ) {
				TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._HandsCover.Overlay.overlayName, _DK_RPG_UMA._Equipment._HandsCover.Color));
				TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._HandsCover.Overlay._UMA;
			}
		}

		if ( _DK_RPG_UMA._Equipment._BeltSub.Slot == null && _DK_RPG_UMA._Equipment._BeltSub.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._BeltSub.Overlay.overlayName, _DK_RPG_UMA._Equipment._BeltSub.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._BeltSub.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._Belt.Slot == null && _DK_RPG_UMA._Equipment._Belt.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._Belt.Overlay.overlayName, _DK_RPG_UMA._Equipment._Belt.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._Belt.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._BeltCover.Slot == null && _DK_RPG_UMA._Equipment._BeltCover.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._BeltCover.Overlay.overlayName, _DK_RPG_UMA._Equipment._BeltCover.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._BeltCover.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._LegsSub.Slot == null && _DK_RPG_UMA._Equipment._LegsSub.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._LegsSub.Overlay.overlayName, _DK_RPG_UMA._Equipment._BeltSub.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._LegsSub.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._Legs.Slot == null && _DK_RPG_UMA._Equipment._Legs.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._Legs.Overlay.overlayName, _DK_RPG_UMA._Equipment._Legs.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._Legs.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._LegsCover.Slot == null && _DK_RPG_UMA._Equipment._LegsCover.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._LegsCover.Overlay.overlayName, _DK_RPG_UMA._Equipment._LegsCover.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._LegsCover.Overlay._UMA;
		}

		if ( _DK_RPG_UMA._Equipment._LegBandSub.Slot == null && _DK_RPG_UMA._Equipment._LegBandSub.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._LegBandSub.Overlay.overlayName, _DK_RPG_UMA._Equipment._LegBandSub.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._LegBandSub.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._LegBand.Slot == null && _DK_RPG_UMA._Equipment._LegBand.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._LegBand.Overlay.overlayName, _DK_RPG_UMA._Equipment._LegBand.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._LegBand.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._LegBandCover.Slot == null && _DK_RPG_UMA._Equipment._LegBandCover.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._LegBandCover.Overlay.overlayName, _DK_RPG_UMA._Equipment._LegBandCover.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._LegBandCover.Overlay._UMA;
		}

		if ( _DK_RPG_UMA._Equipment._FeetSub.Slot == null && _DK_RPG_UMA._Equipment._FeetSub.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._FeetSub.Overlay.overlayName, _DK_RPG_UMA._Equipment._FeetSub.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._FeetSub.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._Feet.Slot == null && _DK_RPG_UMA._Equipment._Feet.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._Feet.Overlay.overlayName, _DK_RPG_UMA._Equipment._Feet.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._Feet.Overlay._UMA;
		}
		if ( _DK_RPG_UMA._Equipment._FeetCover.Slot == null && _DK_RPG_UMA._Equipment._FeetCover.Overlay != null ) {
			TmpTorsoOverLayList.Add(Crowd.overlayLibrary.InstantiateOverlay(_DK_RPG_UMA._Equipment._FeetCover.Overlay.overlayName, _DK_RPG_UMA._Equipment._FeetCover.Color));
			TmpTorsoOverLayList[TmpTorsoOverLayList.Count-1]._UMA = _DK_RPG_UMA._Equipment._FeetCover.Overlay._UMA;
		}

		_DK_RPG_UMA.TmpTorsoOverLayList = TmpTorsoOverLayList;

		if ( _DK_RPG_UMA.AnatomyRule.AnatomyToCreate == DK_RPG_UMA.AnatomyChoice.CompleteAvatar 
			|| _DK_RPG_UMA.AnatomyRule.AnatomyToCreate == DK_RPG_UMA.AnatomyChoice.NoHead ){
			// _Hands
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Avatar._Body._Hands.Slot;
			_Overlay = _DK_RPG_UMA._Avatar._Body._Hands.Overlay;
			_Color = _DK_RPG_UMA._Avatar.SkinColor;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_Hands", _Color, null);	// assign the slot and its overlays	
		}

		// _Feet
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Avatar._Body._Feet.Slot;
		_Overlay = _DK_RPG_UMA._Avatar._Body._Feet.Overlay;
		_Color = _DK_RPG_UMA._Avatar.SkinColor;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_Feet", _Color, null);	// assign the slot and its overlays	

		// _Torso
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Avatar._Body._Torso.Slot;
		_Overlay = _DK_RPG_UMA._Avatar._Body._Torso.Overlay;
		_Color = _DK_RPG_UMA._Avatar.SkinColor;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_Torso", _Color, null);	// assign the slot and its overlays	
		
		// _Legs
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Avatar._Body._Legs.Slot;
		_Overlay = _DK_RPG_UMA._Avatar._Body._Legs.Overlay;
		_Color = _DK_RPG_UMA._Avatar.SkinColor;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_Legs", _Color, null);	// assign the slot and its overlays	


		RebuildEquipment ();
	}

	public void RebuildEquipment (){
		if ( _DK_RPG_UMA.AnatomyRule.AnatomyToCreate == DK_RPG_UMA.AnatomyChoice.CompleteAvatar ){
		
			#region Head Gear
			// _HeadSubWear
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._HeadSub.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._HeadSub.Overlay;
			_Color = _DK_RPG_UMA._Equipment._HeadSub.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._HeadSub.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_HeadSubWear", _Color, _ColorPreset);	// assign the slot and its overlays	

			// _HeadWear
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._Head.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._Head.Overlay;
			_Color = _DK_RPG_UMA._Equipment._Head.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._Head.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_HeadWear", _Color, _ColorPreset);	// assign the slot and its overlays	

			// _HeadCoverWear
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._HeadCover.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._HeadCover.Overlay;
			_Color = _DK_RPG_UMA._Equipment._HeadCover.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._HeadCover.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_HeadCoverWear", _Color, _ColorPreset);	// assign the slot and its overlays	
			#endregion Head Gear
		}
	
		#region Torso Gear
		// _SubTorsoWear
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._TorsoSub.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._TorsoSub.Overlay;
		_Color = _DK_RPG_UMA._Equipment._TorsoSub.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._TorsoSub.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_TorsoSubWear", _Color, _ColorPreset);	// assign the slot and its overlays	
		
		// _TorsoWear
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._Torso.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._Torso.Overlay;
		_Color = _DK_RPG_UMA._Equipment._Torso.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._Torso.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_TorsoWear", _Color, _ColorPreset);	// assign the slot and its overlays	

		// _CoverTorsoWear
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._TorsoCover.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._TorsoCover.Overlay;
		_Color = _DK_RPG_UMA._Equipment._TorsoCover.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._TorsoCover.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_TorsoCoverWear", _Color, _ColorPreset);	// assign the slot and its overlays	
		#endregion Torso Gear
	
		if ( _DK_RPG_UMA.AnatomyRule.AnatomyToCreate == DK_RPG_UMA.AnatomyChoice.CompleteAvatar 
			|| _DK_RPG_UMA.AnatomyRule.AnatomyToCreate == DK_RPG_UMA.AnatomyChoice.NoHead ){
	
			#region Handled Gear
			// _LeftHand
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._LeftHand.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._LeftHand.Overlay;
			_Color = _DK_RPG_UMA._Equipment._LeftHand.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._LeftHand.ColorPreset;
			if ( _Slot && _Overlay){
				AssigningSlot (Crowd, _Slot, _Overlay, "_LeftHand", _Color, _ColorPreset);	// assign the slot and its overlays	
			}
			// _RightHand
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._RightHand.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._RightHand.Overlay;
			_Color = _DK_RPG_UMA._Equipment._RightHand.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._RightHand.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_RightHand", _Color, _ColorPreset);	// assign the slot and its overlays	
			#endregion Handled Gear
	
			#region Armband Gear
			// _ArmBandSub
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._ArmBandSub.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._ArmBandSub.Overlay;
			_Color = _DK_RPG_UMA._Equipment._ArmBandSub.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_ArmBandSub", _Color, _ColorPreset);	// assign the slot and its overlays	

			// _ArmBand
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._ArmBand.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._ArmBand.Overlay;
			_Color = _DK_RPG_UMA._Equipment._ArmBand.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._ArmBand.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_ArmBand", _Color, _ColorPreset);	// assign the slot and its overlays	

			// _ArmCoverBand
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._ArmBandCover.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._ArmBandCover.Overlay;
			_Color = _DK_RPG_UMA._Equipment._ArmBandCover.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_ArmBandCover", _Color, _ColorPreset);	// assign the slot and its overlays	
			#endregion Armband Gear

			#region Wrist Gear
			// _WristSub
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._WristSub.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._WristSub.Overlay;
			_Color = _DK_RPG_UMA._Equipment._WristSub.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._WristSub.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_WristSub", _Color, _ColorPreset);	// assign the slot and its overlays	

			// _Wrist
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._Wrist.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._Wrist.Overlay;
			_Color = _DK_RPG_UMA._Equipment._Wrist.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._Wrist.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_Wrist", _Color, _ColorPreset);	// assign the slot and its overlays	

			// _WristCover
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._WristCover.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._WristCover.Overlay;
			_Color = _DK_RPG_UMA._Equipment._WristCover.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._WristCover.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_WristCover", _Color, _ColorPreset);	// assign the slot and its overlays	
			#endregion Wrist Gear

			#region Hand Gear
			// _HandsSubWear
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._HandsSub.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._HandsSub.Overlay;
			_Color = _DK_RPG_UMA._Equipment._HandsSub.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._HandsSub.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_HandsSubWear", _Color, _ColorPreset);	// assign the slot and its overlays	

			// _HandsWear
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._Hands.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._Hands.Overlay;
			_Color = _DK_RPG_UMA._Equipment._Hands.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._Hands.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_HandsWear", _Color, _ColorPreset);	// assign the slot and its overlays	

			// _HandsCoverWear
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._HandsCover.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._HandsCover.Overlay;
			_Color = _DK_RPG_UMA._Equipment._HandsCover.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._HandsCover.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_HandsCoverWear", _Color, _ColorPreset);	// assign the slot and its overlays	
			#endregion Hand Gear

			#region Shoulder Gear
			// _ShoulderSubWear
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._ShoulderSub.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._ShoulderSub.Overlay;
			_Color = _DK_RPG_UMA._Equipment._ShoulderSub.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._ShoulderSub.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_ShoulderSubWear", _Color, _ColorPreset);	// assign the slot and its overlays	

			// _ShoulderWear
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._Shoulder.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._Shoulder.Overlay;
			_Color = _DK_RPG_UMA._Equipment._Shoulder.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._Shoulder.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_ShoulderWear", _Color, _ColorPreset);	// assign the slot and its overlays	

			// _ShoulderCoverWear
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._ShoulderCover.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._ShoulderCover.Overlay;
			_Color = _DK_RPG_UMA._Equipment._ShoulderCover.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._ShoulderCover.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_ShoulderCoverWear", _Color, _ColorPreset);	// assign the slot and its overlays	
			#endregion Shoulder Gear

			#region rings Gear
			// _RingLeft
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._RingLeft.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._RingLeft.Overlay;
			_Color = _DK_RPG_UMA._Equipment._RingLeft.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._RingLeft.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_RingLeft", _Color, _ColorPreset);	// assign the slot and its overlays

			// _RingRight
			_Slot = null;
			_Overlay = null;
			_Slot = _DK_RPG_UMA._Equipment._RingRight.Slot;
			_Overlay = _DK_RPG_UMA._Equipment._RingRight.Overlay;
			_Color = _DK_RPG_UMA._Equipment._RingRight.Color;
			_ColorPreset = _DK_RPG_UMA._Equipment._RingRight.ColorPreset;
			if ( _Slot && _Overlay)
				AssigningSlot (Crowd, _Slot, _Overlay, "_RingRight", _Color, _ColorPreset);	// assign the slot and its overlays
			#endregion rings Gear
		}

		#region Legs Gear
		// _LegsSubWear
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._LegsSub.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._LegsSub.Overlay;
		_Color = _DK_RPG_UMA._Equipment._LegsSub.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._LegsSub.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_LegsSubWear", _Color, _ColorPreset);	// assign the slot and its overlays	

		// _LegsWear
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._Legs.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._Legs.Overlay;
		_Color = _DK_RPG_UMA._Equipment._Legs.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._Legs.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_LegsWear", _Color, _ColorPreset);	// assign the slot and its overlays	

		// _LegsCoverWear
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._LegsCover.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._LegsCover.Overlay;
		_Color = _DK_RPG_UMA._Equipment._LegsCover.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._LegsCover.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_LegsCoverWear", _Color, _ColorPreset);	// assign the slot and its overlays	
		#endregion Legs Gear

		#region Feet Gear
		// _FeetSubWear
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._FeetSub.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._FeetSub.Overlay;
		_Color = _DK_RPG_UMA._Equipment._FeetSub.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._FeetSub.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_FeetSubWear", _Color, _ColorPreset);	// assign the slot and its overlays	

		// _FeetWear
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._Feet.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._Feet.Overlay;
		_Color = _DK_RPG_UMA._Equipment._Feet.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._Feet.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_FeetWear", _Color, _ColorPreset);	// assign the slot and its overlays	

		// _FeetCoverWear
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._FeetCover.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._FeetCover.Overlay;
		_Color = _DK_RPG_UMA._Equipment._FeetCover.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._FeetCover.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_FeetCoverWear", _Color, _ColorPreset);	// assign the slot and its overlays	
		#endregion Feet Gear

		#region Belt Gear
		// _BeltSubWear
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._BeltSub.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._BeltSub.Overlay;
		_Color = _DK_RPG_UMA._Equipment._BeltSub.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._BeltSub.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_BeltSubWear", _Color, _ColorPreset);	// assign the slot and its overlays	

		// _BeltWear
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._Belt.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._Belt.Overlay;
		_Color = _DK_RPG_UMA._Equipment._Belt.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._Belt.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_BeltWear", _Color, _ColorPreset);	// assign the slot and its overlays	

		// _BeltCoverWear
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._BeltCover.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._BeltCover.Overlay;
		_Color = _DK_RPG_UMA._Equipment._BeltCover.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._BeltCover.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_BeltCoverWear", _Color, _ColorPreset);	// assign the slot and its overlays	
		#endregion Belt Gear

		#region LegBand Gear
		// _LegBandSub
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._LegBandSub.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._LegBandSub.Overlay;
		_Color = _DK_RPG_UMA._Equipment._LegBandSub.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._LegBandSub.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_LegBandSub", _Color, _ColorPreset);	// assign the slot and its overlays	

		// _LegBand
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._LegBand.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._LegBand.Overlay;
		_Color = _DK_RPG_UMA._Equipment._LegBand.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._LegBand.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_LegBand", _Color, _ColorPreset);	// assign the slot and its overlays	

		// _LegBandCover
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._LegBandCover.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._LegBandCover.Overlay;
		_Color = _DK_RPG_UMA._Equipment._LegBandCover.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._LegBandCover.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_LegBandCover", _Color, _ColorPreset);	// assign the slot and its overlays	
		#endregion LegBand Gear

		#region Collar Gear
		// _CollarSub
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._CollarSub.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._CollarSub.Overlay;
		_Color = _DK_RPG_UMA._Equipment._CollarSub.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._CollarSub.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_CollarSub", _Color, _ColorPreset);	// assign the slot and its overlays	

		// _Collar
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._Collar.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._Collar.Overlay;
		_Color = _DK_RPG_UMA._Equipment._Collar.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._Collar.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_Collar", _Color, _ColorPreset);	// assign the slot and its overlays	

		// _CollarCover
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._CollarCover.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._CollarCover.Overlay;
		_Color = _DK_RPG_UMA._Equipment._CollarCover.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._CollarCover.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_CollarCover", _Color, _ColorPreset);	// assign the slot and its overlays	
		#endregion Collar Gear

		#region Backpack Gear
		// _BackpackSub
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._BackpackSub.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._BackpackSub.Overlay;
		_Color = _DK_RPG_UMA._Equipment._BackpackSub.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._BackpackSub.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_BackpackSub", _Color, _ColorPreset);	// assign the slot and its overlays

		// _Backpack
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._Backpack.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._Backpack.Overlay;
		_Color = _DK_RPG_UMA._Equipment._Backpack.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._Backpack.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_Backpack", _Color, _ColorPreset);	// assign the slot and its overlays

		// _BackpackCover
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._BackpackCover.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._BackpackCover.Overlay;
		_Color = _DK_RPG_UMA._Equipment._BackpackCover.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._BackpackCover.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_BackpackCover", _Color, _ColorPreset);	// assign the slot and its overlays
		#endregion Backpack Gear

		#region Cloak Gear
		// _Cloak
		_Slot = null;
		_Overlay = null;
		_Slot = _DK_RPG_UMA._Equipment._Cloak.Slot;
		_Overlay = _DK_RPG_UMA._Equipment._Cloak.Overlay;
		_Color = _DK_RPG_UMA._Equipment._Cloak.Color;
		_ColorPreset = _DK_RPG_UMA._Equipment._Cloak.ColorPreset;
		if ( _Slot && _Overlay)
			AssigningSlot (Crowd, _Slot, _Overlay, "_Cloak", _Color, _ColorPreset);	// assign the slot and its overlays	
		#endregion Cloak Gear
	
		Cleaning ();
	}

	public void AssigningSlot (DK_UMACrowd Crowd, DKSlotData slot, DKOverlayData Overlay, string type, Color color, ColorPresetData ColorPreset){
		// force color
		Color black1 = new Color(0,0,0,0);
		Color black2 = new Color(0,0,0,1);
		if ( ColorPreset != null ){
			// if color is black
			if ( color == black1 || color == black2 ){
				color = ColorPreset.PresetColor;
			}
		}

		if ( type == "_EyeLids" || type == "_Nose" || type == "_Mouth" || type == "_Ears" ){
			if ( slot.LinkedOverlayList.Count == 0 
			    && Crowd && Crowd.slotLibrary
			    && slot && slot.slotName != null 
				&& TmpSlotDataList[HeadIndex] )	{	
				// adding slot to tmp lists
				TmpSlotDataList.Add(Crowd.slotLibrary.InstantiateSlot(slot.slotName,TmpSlotDataList[HeadIndex].overlayList));
				if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 ){
					// adding directly to UMA Recipe
					TmpUMASlotDataList.Add (GetSlotLibrary().InstantiateSlot(slot._UMA.slotName, TmpUMASlotDataList[HeadIndex].GetOverlayList() ));
				}					
			}
			else {
				int _index = TmpSlotDataList.Count;
				TmpSlotDataList.Add(Crowd.slotLibrary.InstantiateSlot( slot.slotName ));
				if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
					// adding directly to UMA Recipe
					TmpUMASlotDataList.Add(GetSlotLibrary().InstantiateSlot(slot._UMA.slotName));
			//	Debug.Log ( "slot.LinkedOverlayList.Count "+slot.LinkedOverlayList.Count.ToString());

				AssigningOverlay (Crowd, _index, Overlay, type, false, color, ColorPreset);
			}
		}
		else if ( ( type == "_Torso" || ( type == "_Hands" || type == "_Legs" || type == "_Feet" )/* && TmpTorsoOverLayList.Contains(Overlay) == true*/ ) && slot.LinkedOverlayList.Count == 0 ){
			TmpSlotDataList.Add(Crowd.slotLibrary.InstantiateSlot(slot.slotName, TmpTorsoOverLayList));
			if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 ){
				// adding directly to UMA Recipe
				TmpUMASlotDataList.Add(GetSlotLibrary().InstantiateSlot(slot._UMA.slotName));
			//	Debug.Log ("TmpTorsoOverLayList count "+TmpTorsoOverLayList.Count );
				foreach ( DKOverlayData overlay in TmpTorsoOverLayList ){
				//	Debug.Log ("TorsoOverLay "+overlay.overlayName+" / "+overlay._UMA );
					if ( overlay != null )
						TmpUMASlotDataList[TmpUMASlotDataList.Count - 1].AddOverlay(GetOverlayLibrary().InstantiateOverlay(overlay._UMA.overlayName, 
							new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1)));
				}
			}	

		//	Debug.Log ("adding TmpTorsoOverLayList ("+TmpTorsoOverLayList.Count+") to slot "+slot.slotName);
		}
		else {
			int _index = TmpSlotDataList.Count;
			TmpSlotDataList.Add(Crowd.slotLibrary.InstantiateSlot( slot.slotName ));
		//	Debug.Log ("instantiating slot "+slot.slotName+" / slot in list "+TmpSlotDataList[TmpSlotDataList.Count-1].slotName);
			if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
				// directly to UMA Recipe
				TmpUMASlotDataList.Add(GetSlotLibrary().InstantiateSlot(slot._UMA.slotName));

			AssigningOverlay (Crowd, _index, Overlay, type, false, color, ColorPreset);

			#region if Head
			// add Stacked Overlays
			if ( type == "_Head" ){
			//	Debug.Log ("Verify Stacked Overlay for the head");
				if ( _Overlay.Opt01 != null ){
				//	Debug.Log ("adding Stacked Overlay to the head "+_Overlay.Opt01.overlayName);
					TmpSlotDataList[0].overlayList.Add( Crowd.overlayLibrary.InstantiateOverlay(_Overlay.Opt01.overlayName, Color.white ));
					if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
						// directly to UMA Recipe
						TmpUMASlotDataList[HeadIndex].AddOverlay(GetOverlayLibrary().InstantiateOverlay(_Overlay.Opt01._UMA.overlayName, Color.white));
				}
				if ( _Overlay.Opt02 != null ){
					TmpSlotDataList[0].overlayList.Add( Crowd.overlayLibrary.InstantiateOverlay(_Overlay.Opt02.overlayName, Color.white ));
					if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
						// directly to UMA Recipe
						TmpUMASlotDataList[HeadIndex].AddOverlay(GetOverlayLibrary().InstantiateOverlay(_Overlay.Opt02._UMA.overlayName, Color.white));
				}
			}
			#endregion if Head
		}

		// Copy the values
		CopyValues (Crowd, slot, TmpSlotDataList.Count-1);

		// Remove the face elements for a head wear
		if ( type == "_HeadWear" || type == "_HeadSubWear" || type == "_HeadCoverWear" )
		for (int i1 = 0; i1 < TmpSlotDataList.Count; i1 ++){

			#region if Hide Hair
			if ( slot._HideData.HideHair == true ) {
				if ( TmpSlotDataList[i1].Place && TmpSlotDataList[i1].Place.name == "Hair" ) {
					//	Debug.Log (slot.slotName+" removes "+TmpSlotDataList[i1].slotName);
					ToRemoveList.Add(TmpSlotDataList[i1]);
				}
			}
			#endregion if Hide Hair 

			#region if Hide Hair Module
			if ( slot._HideData.HideHairModule == true ) {
				if ( TmpSlotDataList[i1].Place && TmpSlotDataList[i1].Place.name == "Hair_Module" ) {
				
					ToRemoveList.Add(TmpSlotDataList[i1]);
				}
			}
			#endregion if Hide Hair Module 
			
			#region if Hide Mouth
			if ( slot._HideData.HideMouth == true ) {
				if ( TmpSlotDataList[i1].Place && TmpSlotDataList[i1].Place.name == "Mouth" ) {
				
					ToRemoveList.Add(TmpSlotDataList[i1]);
				}
			}
			#endregion if Hide Mouth 
			
			#region if Hide Beard
			if ( slot._HideData.HideBeard == true ) {

				if ( TmpSlotDataList[i1].OverlayType == "Beard" ) {
				
					ToRemoveList.Add(TmpSlotDataList[i1]);
				}
			}
			#endregion if Hide Mouth 

			#region if Hide Ears
			if ( slot._HideData.HideEars == true ) {
				if ( TmpSlotDataList[i1].Place && TmpSlotDataList[i1].Place.name == "Ears" ) {
					ToRemoveList.Add(TmpSlotDataList[i1]);
				}
			}
			#endregion if Hide Ears
		}

		// verify materials
		if ( slot != null  && slot._UMA != null )
			VerifyMaterials ( slot );
	//	else Debug.Log ("DK UMA : slot or overlay not available : "+slot.slotName+"/"+Overlay.name);
	}

	public DKOverlayData overlay;
	public void AssigningOverlay (DK_UMACrowd Crowd, int index, DKOverlayData Overlay, string type, bool OverlayOnly, Color color, ColorPresetData ColorPreset){
		// force color
		Color black1 = new Color(0,0,0,0);
		Color black2 = new Color(0,0,0,1);
		if ( ColorPreset != null ){
			// if color is black
			if ( color == black1 || color == black2 ){
				color = ColorPreset.PresetColor;
			}
		}

		Color ColorToApply = new Color(color.r, color.g, color.b, 1) ;
	
		overlay = Overlay;

		// set the alpha to 1 for cutout material
		if ( Overlay != null && Overlay._UMA != null && Overlay._UMA.material != null && Overlay._UMA.material.name.Contains("Cutout") ){
			ColorToApply.a = 1;
		//	Debug.Log ("color alpha set to "+ColorToApply.a);
		}

		// Assign the Overlay
		try {
			if ( type.Contains("Wear") && type.Contains("SubWear") == false ) {
				TmpSlotDataList[index].overlayList.Clear();
				if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
					// directly to UMA Recipe
					TmpUMASlotDataList[index].SetOverlayList( new List<OverlayData>() );
			}
		}catch (System.NullReferenceException e) { Debug.Log ( e );}



		if ( overlay == null ) Debug.LogError ( "Overlay is missing, skipping it." );
		if ( TmpSlotDataList[index] == null ) {Debug.LogError ( this.transform.parent.name+" have a missing "+type+" Slot, skipping it. (TmpSlotDataList "+index+"/"+TmpSlotDataList.Count+")" );}
		else{
		//	if ( overlay.OverlayType == "Tatoo" ) Debug.Log ( "Assigning TATOO Overlay "+Overlay+" / "+type );
		//	if ( overlay.OverlayType == "Makeup" ) Debug.Log ( "Assigning MakeUp Overlay "+Overlay+" / "+type );
			bool alreadyIn = false;

			//test 
			if ( type == "_Torso" || type == "_Hands" || type == "_Legs" || type == "_Feet" ) {
				TmpSlotDataList[index].overlayList.Clear();
				if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
					// directly to UMA Recipe
					TmpUMASlotDataList[index].SetOverlayList( new List<OverlayData>() );
			}
		
			if ( alreadyIn == false ){		
				TmpSlotDataList[index].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(overlay.overlayName,ColorToApply));
				if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
					// directly to UMA Recipe
					TmpUMASlotDataList[index].AddOverlay(GetOverlayLibrary().InstantiateOverlay(overlay._UMA.overlayName,ColorToApply));

				TmpSlotDataList[index].overlayList[TmpSlotDataList[index].overlayList.Count-1].OverlayType = overlay.OverlayType;
				if ( overlay.ColorPresets.Count > 0 )
					TmpSlotDataList[index].overlayList[TmpSlotDataList[index].overlayList.Count-1].ColorPresets = overlay.ColorPresets;
				TmpSlotDataList[index].overlayList[TmpSlotDataList[index].overlayList.Count-1]._UMA = overlay._UMA;
			}
		//	VerifyUMAMaterial ( TmpSlotDataList[index]._UMA, overlay._UMA );
		}
		if ( type == "_Head" ) _FaceOverlay = overlay;
		else if ( type == "_Ears" ) {}

		// assign stacked overlays
		if ( overlay.Opt01 != null ) AssignStackedOverlays ( overlay, "Opt01", type, index );
		else ClearStackedOverlays ( "Opt01", type );
		if ( overlay.Opt02 != null ) AssignStackedOverlays ( overlay, "Opt02", type, index );
		else ClearStackedOverlays ( "Opt02", type );
		if ( overlay.Dirt01 != null ) AssignStackedOverlays ( overlay, "Dirt01", type, index );
		else ClearStackedOverlays ( "Dirt01", type );
		if ( overlay.Dirt02 != null ) AssignStackedOverlays ( overlay, "Dirt02", type, index );
		else ClearStackedOverlays ( "Dirt02", type );
	//	if ( overlay.DirtOverlays.Count > 0 ) AssignDirtOverlays ( overlay, index );
	}

	void AssignStackedOverlays ( DKOverlayData Overlay, string Opt, string type, int index ){
		if ( Overlay._UMA.material != null ){
			if ( type.Contains("HeadSub") == true || type.Contains("HeadWear") == true || type.Contains("HeadCover") == true )
				DK_AssignHeadWearStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("ShoulderSub") == true || type.Contains("ShoulderWear") == true || type.Contains("ShoulderCover") == true )
				DK_AssignShoulderWearStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("TorsoSub") == true || type.Contains("TorsoWear") == true || type.Contains("TorsoCover") == true )
				DK_AssignTorsoWearStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("ArmBandSub") == true || type.Contains("ArmBandWear") == true || type.Contains("ArmBandCover") == true )
				DK_AssignArmBandWearStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("WristSub") == true || type.Contains("WristWear") == true || type.Contains("WristCover") == true )
				DK_AssignWristWearStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("HandsSub") == true || type.Contains("HandsWear") == true || type.Contains("HandsCover") == true )
				DK_AssignHandsWearStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("BeltSub") == true || type.Contains("BeltWear") == true || type.Contains("BeltCover") == true )
				DK_AssignBeltWearStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("LegsSub") == true || type.Contains("LegsWear") == true || type.Contains("LegsCover") == true )
				DK_AssignLegsWearStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("LegBandSub") == true || type.Contains("LegBandWear") == true || type.Contains("LegBandCover") == true )
				DK_AssignLegBandWearStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("FeetSub") == true || type.Contains("FeetWear") == true || type.Contains("FeetCover") == true )
				DK_AssignFeetWearStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("LeftHand") == true )
				DK_AssignLeftHandStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("RightHand") == true )
				DK_AssignRightHandStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("RingRight") == true )
				DK_AssignRightRingStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("RingLeft") == true )
				DK_AssignLeftRingStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("Cloak") == true )
				DK_AssignCloakStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("Collar") == true )
				DK_AssignCollarStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			if ( type.Contains("Backpack") == true )
				DK_AssignBackpackStackedOverlays.AssignStackedOverlays ( Crowd, TmpSlotDataList, TmpUMASlotDataList, _DK_RPG_UMA, overlay, Opt, type, index );
			
		}
		else Debug.LogError ("DK UMA : The UMA overlay "+Overlay._UMA.name+" does not have a UMAMaterial assigned. Select the UMA overlay using the 'Elements Manager' and assign a valid UMAMaterial. For a PBR UMA overlay, assign 'UMA_Diffuse_Normal_Metallic' or 'UMA_Cutout_Diffuse_Normal_Metallic'");
	}

	void ClearStackedOverlays ( string opt, string type ){
		if ( opt == "Opt01" ) {
			if ( type.Contains("HeadSub") == true ) _DK_RPG_UMA._Equipment._HeadSub.Opt01Color = null;
			else if ( type.Contains("HeadWear") == true ) _DK_RPG_UMA._Equipment._Head.Opt01Color = null;
			else if ( type.Contains("HeadCover") == true ) _DK_RPG_UMA._Equipment._HeadCover.Opt01Color = null;
			else if ( type.Contains("ShoulderSub") == true ) _DK_RPG_UMA._Equipment._ShoulderSub.Opt01Color = null;
			else if ( type.Contains("ShoulderWear") == true ) _DK_RPG_UMA._Equipment._Shoulder.Opt01Color = null;
			else if ( type.Contains("ShoulderCover") == true ) _DK_RPG_UMA._Equipment._ShoulderCover.Opt01Color = null;
			else if ( type.Contains("ArmBandSub") == true ) _DK_RPG_UMA._Equipment._ArmBandSub.Opt01Color = null;
			else if ( type.Contains("ArmBandWear") == true ) _DK_RPG_UMA._Equipment._ArmBand.Opt01Color = null;
			else if ( type.Contains("ArmBandCover") == true ) _DK_RPG_UMA._Equipment._ArmBandCover.Opt01Color = null;
			else if ( type.Contains("WristSub") == true ) _DK_RPG_UMA._Equipment._WristSub.Opt01Color = null;
			else if ( type.Contains("WristWear") == true ) _DK_RPG_UMA._Equipment._Wrist.Opt01Color = null;
			else if ( type.Contains("WristCover") == true ) _DK_RPG_UMA._Equipment._WristCover.Opt01Color = null;
			else if ( type.Contains("HandsSub") == true ) _DK_RPG_UMA._Equipment._HandsSub.Opt01Color = null;
			else if ( type.Contains("HandsWear") == true ) _DK_RPG_UMA._Equipment._Hands.Opt01Color = null;
			else if ( type.Contains("HandsCover") == true ) _DK_RPG_UMA._Equipment._HandsCover.Opt01Color = null;
			else if ( type.Contains("BeltSub") == true ) _DK_RPG_UMA._Equipment._BeltSub.Opt01Color = null;
			else if ( type.Contains("BeltWear") == true ) _DK_RPG_UMA._Equipment._Belt.Opt01Color = null;
			else if ( type.Contains("BeltCover") == true ) _DK_RPG_UMA._Equipment._BeltCover.Opt01Color = null;
			else if ( type.Contains("LegsSub") == true ) _DK_RPG_UMA._Equipment._LegsSub.Opt01Color = null;
			else if ( type.Contains("LegsWear") == true ) _DK_RPG_UMA._Equipment._Legs.Opt01Color = null;
			else if ( type.Contains("LegsCover") == true ) _DK_RPG_UMA._Equipment._LegsCover.Opt01Color = null;
			else if ( type.Contains("LegBandSub") == true ) _DK_RPG_UMA._Equipment._LegBandSub.Opt01Color = null;
			else if ( type.Contains("LegBandWear") == true ) _DK_RPG_UMA._Equipment._LegBand.Opt01Color = null;
			else if ( type.Contains("LegBandCover") == true ) _DK_RPG_UMA._Equipment._LegBandCover.Opt01Color = null;
			else if ( type.Contains("FeetSub") == true ) _DK_RPG_UMA._Equipment._FeetSub.Opt01Color = null;
			else if ( type.Contains("FeetWear") == true ) _DK_RPG_UMA._Equipment._Feet.Opt01Color = null;
			else if ( type.Contains("FeetCover") == true ) _DK_RPG_UMA._Equipment._FeetCover.Opt01Color = null;
		} 
		else if ( opt == "Opt02" ) {
			if ( type.Contains("HeadSub") == true ) _DK_RPG_UMA._Equipment._HeadSub.Opt02Color = null;
			else if ( type.Contains("HeadWear") == true ) _DK_RPG_UMA._Equipment._Head.Opt02Color = null;
			else if ( type.Contains("HeadCover") == true ) _DK_RPG_UMA._Equipment._HeadCover.Opt02Color = null;
			else if ( type.Contains("ShoulderSub") == true ) _DK_RPG_UMA._Equipment._ShoulderSub.Opt02Color = null;
			else if ( type.Contains("ShoulderWear") == true ) _DK_RPG_UMA._Equipment._Shoulder.Opt02Color = null;
			else if ( type.Contains("ShoulderCover") == true ) _DK_RPG_UMA._Equipment._ShoulderCover.Opt02Color = null;
			else if ( type.Contains("ArmBandSub") == true ) _DK_RPG_UMA._Equipment._ArmBandSub.Opt02Color = null;
			else if ( type.Contains("ArmBandWear") == true ) _DK_RPG_UMA._Equipment._ArmBand.Opt02Color = null;
			else if ( type.Contains("ArmBandCover") == true ) _DK_RPG_UMA._Equipment._ArmBandCover.Opt02Color = null;
			else if ( type.Contains("WristSub") == true ) _DK_RPG_UMA._Equipment._WristSub.Opt02Color = null;
			else if ( type.Contains("WristWear") == true ) _DK_RPG_UMA._Equipment._Wrist.Opt02Color = null;
			else if ( type.Contains("WristCover") == true ) _DK_RPG_UMA._Equipment._WristCover.Opt02Color = null;
			else if ( type.Contains("HandsSub") == true ) _DK_RPG_UMA._Equipment._HandsSub.Opt02Color = null;
			else if ( type.Contains("HandsWear") == true ) _DK_RPG_UMA._Equipment._Hands.Opt02Color = null;
			else if ( type.Contains("HandsCover") == true ) _DK_RPG_UMA._Equipment._HandsCover.Opt02Color = null;
			else if ( type.Contains("BeltSub") == true ) _DK_RPG_UMA._Equipment._BeltSub.Opt02Color = null;
			else if ( type.Contains("BeltWear") == true ) _DK_RPG_UMA._Equipment._Belt.Opt02Color = null;
			else if ( type.Contains("BeltCover") == true ) _DK_RPG_UMA._Equipment._BeltCover.Opt02Color = null;
			else if ( type.Contains("LegsSub") == true ) _DK_RPG_UMA._Equipment._LegsSub.Opt02Color = null;
			else if ( type.Contains("LegsWear") == true ) _DK_RPG_UMA._Equipment._Legs.Opt02Color = null;
			else if ( type.Contains("LegsCover") == true ) _DK_RPG_UMA._Equipment._LegsCover.Opt02Color = null;
			else if ( type.Contains("LegBandSub") == true ) _DK_RPG_UMA._Equipment._LegBandSub.Opt02Color = null;
			else if ( type.Contains("LegBandWear") == true ) _DK_RPG_UMA._Equipment._LegBand.Opt02Color = null;
			else if ( type.Contains("LegBandCover") == true ) _DK_RPG_UMA._Equipment._LegBandCover.Opt02Color = null;
			else if ( type.Contains("FeetSub") == true ) _DK_RPG_UMA._Equipment._FeetSub.Opt02Color = null;
			else if ( type.Contains("FeetWear") == true ) _DK_RPG_UMA._Equipment._Feet.Opt02Color = null;
			else if ( type.Contains("FeetCover") == true ) _DK_RPG_UMA._Equipment._FeetCover.Opt02Color = null;
		}
		// dirt
		if ( opt == "Opt01" ) {
			if ( type.Contains("HeadSub") == true ) _DK_RPG_UMA._Equipment._HeadSub.Dirt01Color = null;
			else if ( type.Contains("HeadWear") == true ) _DK_RPG_UMA._Equipment._Head.Dirt01Color = null;
			else if ( type.Contains("HeadCover") == true ) _DK_RPG_UMA._Equipment._HeadCover.Dirt01Color = null;
			else if ( type.Contains("ShoulderSub") == true ) _DK_RPG_UMA._Equipment._ShoulderSub.Dirt01Color = null;
			else if ( type.Contains("ShoulderWear") == true ) _DK_RPG_UMA._Equipment._Shoulder.Dirt01Color = null;
			else if ( type.Contains("ShoulderCover") == true ) _DK_RPG_UMA._Equipment._ShoulderCover.Dirt01Color = null;
			else if ( type.Contains("ArmBandSub") == true ) _DK_RPG_UMA._Equipment._ArmBandSub.Dirt01Color = null;
			else if ( type.Contains("ArmBandWear") == true ) _DK_RPG_UMA._Equipment._ArmBand.Dirt01Color = null;
			else if ( type.Contains("ArmBandCover") == true ) _DK_RPG_UMA._Equipment._ArmBandCover.Dirt01Color = null;
			else if ( type.Contains("WristSub") == true ) _DK_RPG_UMA._Equipment._WristSub.Dirt01Color = null;
			else if ( type.Contains("WristWear") == true ) _DK_RPG_UMA._Equipment._Wrist.Dirt01Color = null;
			else if ( type.Contains("WristCover") == true ) _DK_RPG_UMA._Equipment._WristCover.Dirt01Color = null;
			else if ( type.Contains("HandsSub") == true ) _DK_RPG_UMA._Equipment._HandsSub.Dirt01Color = null;
			else if ( type.Contains("HandsWear") == true ) _DK_RPG_UMA._Equipment._Hands.Dirt01Color = null;
			else if ( type.Contains("HandsCover") == true ) _DK_RPG_UMA._Equipment._HandsCover.Dirt01Color = null;
			else if ( type.Contains("BeltSub") == true ) _DK_RPG_UMA._Equipment._BeltSub.Dirt01Color = null;
			else if ( type.Contains("BeltWear") == true ) _DK_RPG_UMA._Equipment._Belt.Dirt01Color = null;
			else if ( type.Contains("BeltCover") == true ) _DK_RPG_UMA._Equipment._BeltCover.Dirt01Color = null;
			else if ( type.Contains("LegsSub") == true ) _DK_RPG_UMA._Equipment._LegsSub.Dirt01Color = null;
			else if ( type.Contains("LegsWear") == true ) _DK_RPG_UMA._Equipment._Legs.Dirt01Color = null;
			else if ( type.Contains("LegsCover") == true ) _DK_RPG_UMA._Equipment._LegsCover.Dirt01Color = null;
			else if ( type.Contains("LegBandSub") == true ) _DK_RPG_UMA._Equipment._LegBandSub.Dirt01Color = null;
			else if ( type.Contains("LegBandWear") == true ) _DK_RPG_UMA._Equipment._LegBand.Dirt01Color = null;
			else if ( type.Contains("LegBandCover") == true ) _DK_RPG_UMA._Equipment._LegBandCover.Dirt01Color = null;
			else if ( type.Contains("FeetSub") == true ) _DK_RPG_UMA._Equipment._FeetSub.Dirt01Color = null;
			else if ( type.Contains("FeetWear") == true ) _DK_RPG_UMA._Equipment._Feet.Dirt01Color = null;
			else if ( type.Contains("FeetCover") == true ) _DK_RPG_UMA._Equipment._FeetCover.Dirt01Color = null;
		} 
		else if ( opt == "Opt02" ) {
			if ( type.Contains("HeadSub") == true ) _DK_RPG_UMA._Equipment._HeadSub.Dirt02Color = null;
			else if ( type.Contains("HeadWear") == true ) _DK_RPG_UMA._Equipment._Head.Dirt02Color = null;
			else if ( type.Contains("HeadCover") == true ) _DK_RPG_UMA._Equipment._HeadCover.Dirt02Color = null;
			else if ( type.Contains("ShoulderSub") == true ) _DK_RPG_UMA._Equipment._ShoulderSub.Dirt02Color = null;
			else if ( type.Contains("ShoulderWear") == true ) _DK_RPG_UMA._Equipment._Shoulder.Dirt02Color = null;
			else if ( type.Contains("ShoulderCover") == true ) _DK_RPG_UMA._Equipment._ShoulderCover.Dirt02Color = null;
			else if ( type.Contains("ArmBandSub") == true ) _DK_RPG_UMA._Equipment._ArmBandSub.Dirt02Color = null;
			else if ( type.Contains("ArmBandWear") == true ) _DK_RPG_UMA._Equipment._ArmBand.Dirt02Color = null;
			else if ( type.Contains("ArmBandCover") == true ) _DK_RPG_UMA._Equipment._ArmBandCover.Dirt02Color = null;
			else if ( type.Contains("WristSub") == true ) _DK_RPG_UMA._Equipment._WristSub.Dirt02Color = null;
			else if ( type.Contains("WristWear") == true ) _DK_RPG_UMA._Equipment._Wrist.Dirt02Color = null;
			else if ( type.Contains("WristCover") == true ) _DK_RPG_UMA._Equipment._WristCover.Dirt02Color = null;
			else if ( type.Contains("HandsSub") == true ) _DK_RPG_UMA._Equipment._HandsSub.Dirt02Color = null;
			else if ( type.Contains("HandsWear") == true ) _DK_RPG_UMA._Equipment._Hands.Dirt02Color = null;
			else if ( type.Contains("HandsCover") == true ) _DK_RPG_UMA._Equipment._HandsCover.Dirt02Color = null;
			else if ( type.Contains("BeltSub") == true ) _DK_RPG_UMA._Equipment._BeltSub.Dirt02Color = null;
			else if ( type.Contains("BeltWear") == true ) _DK_RPG_UMA._Equipment._Belt.Dirt02Color = null;
			else if ( type.Contains("BeltCover") == true ) _DK_RPG_UMA._Equipment._BeltCover.Dirt02Color = null;
			else if ( type.Contains("LegsSub") == true ) _DK_RPG_UMA._Equipment._LegsSub.Dirt02Color = null;
			else if ( type.Contains("LegsWear") == true ) _DK_RPG_UMA._Equipment._Legs.Dirt02Color = null;
			else if ( type.Contains("LegsCover") == true ) _DK_RPG_UMA._Equipment._LegsCover.Dirt02Color = null;
			else if ( type.Contains("LegBandSub") == true ) _DK_RPG_UMA._Equipment._LegBandSub.Dirt02Color = null;
			else if ( type.Contains("LegBandWear") == true ) _DK_RPG_UMA._Equipment._LegBand.Dirt02Color = null;
			else if ( type.Contains("LegBandCover") == true ) _DK_RPG_UMA._Equipment._LegBandCover.Dirt02Color = null;
			else if ( type.Contains("FeetSub") == true ) _DK_RPG_UMA._Equipment._FeetSub.Dirt02Color = null;
			else if ( type.Contains("FeetWear") == true ) _DK_RPG_UMA._Equipment._Feet.Dirt02Color = null;
			else if ( type.Contains("FeetCover") == true ) _DK_RPG_UMA._Equipment._FeetCover.Dirt02Color = null;
		}
	}

	/*
	void AssignDirtOverlays ( DKOverlayData Overlay, int index ){
		Color ColorToApply = new Color ();
		foreach ( DKOverlayData stackedOv in Overlay.DirtOverlays ) {
			TmpSlotDataList[index].overlayList.Add( Crowd.overlayLibrary.InstantiateOverlay(stackedOv.overlayName, ColorToApply ));
			TmpSlotDataList[index].overlayList[TmpSlotDataList[index].overlayList.Count-1].OverlayType = stackedOv.OverlayType;
			TmpSlotDataList[index].overlayList[TmpSlotDataList[index].overlayList.Count-1].ColorPresets = stackedOv.ColorPresets;
		}
	}
*/
	void VerifyMaterials ( DKSlotData slot ) {
		// verify material
		if ( slot._UMA.material == null ) {
			GameObject DK_UMA = GameObject.Find("DK_UMA");
			DKUMA_Variables _DKUMA_Variables;

			_DKUMA_Variables = DK_UMA.GetComponent<DKUMA_Variables>();

			// verify PBR Material
			if ( slot._UMA != null && slot._UMA.material == null ) {
				if ( slot.LinkedOverlayList.Count > 0 ) {
					// verify the first linked overlay UMA Mat
					if ( slot.LinkedOverlayList[0]._UMA.material != null ){
						// if ok, assign the UMA Mat to the slot
						slot._UMA.material = slot.LinkedOverlayList[0]._UMA.material;
						Debug.Log ("DK UMA : "+slot._UMA.name+" does not have a UMA Material Assigned, assigning  the one from its Overlay "+slot.LinkedOverlayList[0]._UMA.name+"." );
					}
					// assign default UMA Mat PBR
					else {
						slot._UMA.material = _DKUMA_Variables.DefaultUmaMaterial;
						slot.LinkedOverlayList[0]._UMA.material = _DKUMA_Variables.DefaultUmaMaterial;
						Debug.Log ("DK UMA : "+slot._UMA.name+" and "+slot.LinkedOverlayList[0]._UMA.name+" do not have a UMA Material Assigned, assigning default ("+_DKUMA_Variables.DefaultUmaMaterial.name+")." );
					}
				}
				// if no Linked Overlay
				else {
					slot._UMA.material = _DKUMA_Variables.DefaultUmaMaterial;
					Debug.Log ("DK UMA : "+slot._UMA.name+" does not have a UMA Material Assigned and no Linked Overlay, assignind default ("+_DKUMA_Variables.DefaultUmaMaterial.name+")." );
				}
			}
			//	slot._UMA.material = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(Legacymaterials[0])
			//	                                                    , typeof(UMA.UMAMaterial)) as UMA.UMAMaterial;
		}
	}

	List<DKSlotData> ToRemoveList = new List<DKSlotData>();
	void Cleaning (){
//		Debug.Log ( "Cleaning Avatar" );
		#region Cleaning Avatar
		for(int i = 0; i < TmpSlotDataList.Count; i ++){
			#region if Replace activated
			if ( TmpSlotDataList[i].Replace == true ) {
				for(int i1 = 0; i1 < TmpSlotDataList.Count; i1 ++){
					if ( TmpSlotDataList[i].Place.dk_SlotsAnatomyElement.Place == TmpSlotDataList[i1].Place ) {
						ToRemoveList.Add ( TmpSlotDataList[i1] );
					}
				}
			}
			#endregion if Replace activated
			
			if ( TmpSlotDataList[i].Place != null ) {
				#region hide shoulders
				// detect 'hide shoulders'
				if ( TmpSlotDataList[i]._HideData.HideShoulders ){
					Crowd.Wears.HideShoulders = true;
				}
				// detect the shoulders
				if ( TmpSlotDataList[i].Place.name == "ShoulderWear" ){
					Crowd.Wears.Shoulders = TmpSlotDataList[i];
				}
				#endregion hide shoulders
				
				#region hide LegWear
				// detect 'hide Legs'
				if ( TmpSlotDataList[i]._HideData.HideLegs ){
					Crowd.Wears.HideLegs = true;
				}
				// detect the Legs
				if ( TmpSlotDataList[i].Place.name == "LegsWear" ){
					Crowd.Wears.Legs = TmpSlotDataList[i];
				}
				#endregion hide LegWear

				#region hide BeltWear
				// detect 'hide Belt'
				if ( TmpSlotDataList[i]._HideData.HideBelt ){
					Crowd.Wears.HideBelt = true;
				}
				// detect the belt
				if ( TmpSlotDataList[i].Place.name == "BeltWear" ){
					Crowd.Wears.Belt = TmpSlotDataList[i];
				}
				#endregion hide BeltWear

				#region hide armband
				// detect 'ArmbandWear'
				if ( TmpSlotDataList[i]._HideData.HideArmBand ){
					Crowd.Wears.HideArmBand = true;
				}
				// detect the ArmbandWear
				if ( TmpSlotDataList[i].Place.name == "ArmbandWear" ){
					Crowd.Wears.ArmBand = TmpSlotDataList[i];
				}
				#endregion hide armband

				#region hide wrist
				// detect 'hide wrist'
				if ( TmpSlotDataList[i]._HideData.HideWrist ){
					Crowd.Wears.HideWrist = true;
				}
				// detect the WristWear
				if ( TmpSlotDataList[i].Place.name == "WristWear" ){
					Crowd.Wears.Wrist = TmpSlotDataList[i];
				}
				#endregion hide armband

				#region hide leg band
				// detect 'hide leg band'
				if ( TmpSlotDataList[i]._HideData.HideLegBand ){
					Crowd.Wears.HideLegBand = true;
				}
				// detect the LegBandWear
				if ( TmpSlotDataList[i].Place.name == "LegBandWear" ){
					Crowd.Wears.LegBand = TmpSlotDataList[i];
				}
				#endregion hide leg band

				#region hide collar
				// detect 'hide collar'
				if ( TmpSlotDataList[i]._HideData.HideCollar ){
					Crowd.Wears.HideCollar = true;
				}
				// detect the collar
				if ( TmpSlotDataList[i].Place.name == "Collar" ){
					Crowd.Wears.Collar = TmpSlotDataList[i];
				}
				#endregion hide collar

				#region hide rings
				// detect 'hide rings'
				if ( TmpSlotDataList[i]._HideData.HideRingLeft ){
					Crowd.Wears.HideRingLeft = true;
				}
				if ( TmpSlotDataList[i]._HideData.HideRingRight ){
					Crowd.Wears.HideRingRight = true;
				}
				// detect the rings
				if ( TmpSlotDataList[i].Place.name == "RingLeft" ){
					Crowd.Wears.RingLeft = TmpSlotDataList[i];
				}

				if ( TmpSlotDataList[i].Place.name == "RingRight" ){
					Crowd.Wears.RingRight = TmpSlotDataList[i];
				}
				#endregion hide rings

				#region hide cloak
				// detect 'hide cloak'
				if ( TmpSlotDataList[i]._HideData.HideCloak ){
					Crowd.Wears.HideCloak = true;
				}
				// detect the cloak
				if ( TmpSlotDataList[i].Place.name == "CloakWear" ){
					Crowd.Wears.Cloak = TmpSlotDataList[i];
				}
				#endregion hide cloak

				#region hide Backpack
				// detect 'hide Backpack'
				if ( TmpSlotDataList[i]._HideData.HideBackpack ){
					Crowd.Wears.HideBackpack = true;
				}
				// detect the cloak
				if ( TmpSlotDataList[i].Place.name == "Backpack" ){
					Crowd.Wears.Backpack = TmpSlotDataList[i];
				}
				#endregion hide Backpack

				#region hide underwear
				// detect 'hide underwear'
				if ( TmpSlotDataList[i]._HideData.HideUnderwear ){
					Crowd.Wears.HideUnderwear = true;
				}
				if ( TmpSlotDataList[i].Place.name == "Torso" && TmpSlotDataList[i].OverlayType == "Flesh" )
					TorsoIndex = i;
				#endregion hide underwear
			}

			// detect the underwear
			if ( Crowd.Wears.HideUnderwear){
				for(int i1 = 0; i1 < TmpSlotDataList[TorsoIndex].overlayList.Count; i1 ++){
					if ( _DK_RPG_UMA._Avatar._Body._Underwear.Overlay 
						&& TmpSlotDataList[TorsoIndex].overlayList[i1].overlayName == _DK_RPG_UMA._Avatar._Body._Underwear.Overlay.overlayName ){
						TmpSlotDataList[TorsoIndex].overlayList.Remove ( TmpSlotDataList[TorsoIndex].overlayList[i1] );
						if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
							// directly to UMA Recipe
							TmpUMASlotDataList[TorsoIndex].RemoveOverlay(TmpSlotDataList[TorsoIndex].overlayList[i1]._UMA.overlayName);
					}
				}
			}

			// detect Legacy
			DKSlotData Slot = TmpSlotDataList[i];
			if ( Slot._LegacyData.HasLegacy == true && ToRemoveList.Contains (Slot) == false ) {
				if ( Slot._LegacyData.LegacyList.Count > 0 ){
					foreach ( DKSlotData LegacySlot in Slot._LegacyData.LegacyList ){
						// select the overlay
						try {

							#region Choose Color to apply
							if ( _DK_RPG_UMA == null ){
								if ( LegacySlot.OverlayType == "Hair" ) Crowd.Colors.ColorToApply = Crowd.Colors.HairColor;
								else if ( LegacySlot.OverlayType == "Beard" ) Crowd.Colors.ColorToApply =Crowd.Colors.HairColor;
								else if ( LegacySlot.OverlayType == "TorsoWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.TorsoWearColor;
								else if ( LegacySlot.OverlayType == "LegsWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.LegsWearColor;
								else if ( LegacySlot.OverlayType == "HandWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.HandWearColor;
								else if ( LegacySlot.OverlayType == "FeetWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.FeetWearColor;
								else if ( LegacySlot.OverlayType == "ShoulderWear" ) Crowd.Colors.ColorToApply =Crowd.Colors.TorsoWearColor;
								else if ( LegacySlot.OverlayType == "Eyes" ) Crowd.Colors.ColorToApply =Crowd.Colors.EyesColor;
								else if ( LegacySlot.OverlayType == "Face" ) Crowd.Colors.ColorToApply =Crowd.Colors.skinColor;
								else if ( LegacySlot.OverlayType == "Flesh" ) Crowd.Colors.ColorToApply =Crowd.Colors.skinColor;
								else if ( LegacySlot.Place && LegacySlot.Place.name == "InnerMouth" ) Crowd.Colors.ColorToApply =Crowd.Colors.InnerMouthColor;
								else {
									// assign Linked overlay color first 
									if ( LegacySlot.LinkedOverlayList.Count > 0 && LegacySlot.LinkedOverlayList[0].ColorPresets.Count > 0 ) 
										Crowd.Colors.ColorToApply = LegacySlot.LinkedOverlayList[0].ColorPresets[0].PresetColor;
									// or assign white color
									else Crowd.Colors.ColorToApply = Color.white;								}
							}
							// for the DK UMA RPG Avatars
							else {
								if ( LegacySlot.OverlayType == "Hair" ) Crowd.Colors.ColorToApply =_DK_RPG_UMA._Avatar.HairColor;
								else if ( LegacySlot.OverlayType == "Beard" ) Crowd.Colors.ColorToApply =_DK_RPG_UMA._Avatar.HairColor;
								else if ( LegacySlot.OverlayType == "TorsoWear" )Crowd.Colors.ColorToApply =_DK_RPG_UMA._Equipment._Torso.Color;
								else if ( LegacySlot.OverlayType == "LegsWear" ) Crowd.Colors.ColorToApply =_DK_RPG_UMA._Equipment._Legs.Color;
								else if ( LegacySlot.OverlayType == "HandWear" ) Crowd.Colors.ColorToApply =_DK_RPG_UMA._Equipment._Hands.Color;
								else if ( LegacySlot.OverlayType == "FeetWear" ) Crowd.Colors.ColorToApply =_DK_RPG_UMA._Equipment._Feet.Color;
								else if ( LegacySlot.OverlayType == "ShoulderWear" ) Crowd.Colors.ColorToApply =_DK_RPG_UMA._Equipment._Shoulder.Color;
								else if ( LegacySlot.OverlayType == "Eyes" ) Crowd.Colors.ColorToApply =Crowd.Colors.EyesColor;
								else if ( LegacySlot.OverlayType == "Face" ) Crowd.Colors.ColorToApply =_DK_RPG_UMA._Avatar.SkinColor;
								else if ( LegacySlot.OverlayType == "Flesh" ) Crowd.Colors.ColorToApply =_DK_RPG_UMA._Avatar.SkinColor;
								else if ( LegacySlot.Place && LegacySlot.Place.name == "InnerMouth" ) Crowd.Colors.ColorToApply =_DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.color;
								else {
									Crowd.Colors.ColorToApply = new Color (UnityEngine.Random.Range(0.01f,0.9f),UnityEngine.Random.Range(0.01f,0.9f),UnityEngine.Random.Range(0.01f,0.9f));
								}
							}
							#endregion Choose Color to apply
														
							DKSlotData placeHolder = ScriptableObject.CreateInstance("DKSlotData") as DKSlotData;
							foreach ( DKSlotData slot in TmpSlotDataList ){
								if ( slot.Place == LegacySlot.Place ) {
									placeHolder = slot;
								}
							}

							// add
							// For flesh
							if ( LegacySlot.OverlayType != null && LegacySlot.OverlayType == "Flesh" && LegacySlot.LinkedOverlayList.Count == 0 ) {

								TmpSlotDataList.Add(Crowd.slotLibrary.InstantiateSlot(LegacySlot.slotName,TmpTorsoOverLayList));
								if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
									// adding directly to UMA Recipe
									TmpUMASlotDataList.Add(GetSlotLibrary().InstantiateSlot(LegacySlot._UMA.slotName, TmpUMASlotDataList[TorsoIndex].GetOverlayList() ));

								DKSlotData slot = TmpSlotDataList[TmpSlotDataList.Count-1];
							
								slot._LegacyData.IsLegacy = true;
								slot._LegacyData.ElderList.Add(TmpSlotDataList[i]);

								// del placeHolder if necessary
								if ( LegacySlot._LegacyData.Replace ) {
									ToRemoveList.Add (placeHolder);
								}
							}
							else if ( LegacySlot.OverlayType != null && LegacySlot.OverlayType == "Flesh" && LegacySlot.LinkedOverlayList.Count > 0 ) {

								DKOverlayData overlay = LegacySlot.LinkedOverlayList[0];
																	
								TmpSlotDataList.Add(Crowd.slotLibrary.InstantiateSlot(LegacySlot.slotName ));
								if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
									// adding directly to UMA Recipe
									TmpUMASlotDataList.Add(GetSlotLibrary().InstantiateSlot(LegacySlot._UMA.slotName ));
								
								AssignedOverlayList.Add (overlay);
								TmpSlotDataList[TmpSlotDataList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(overlay.overlayName,_DK_RPG_UMA._Avatar.SkinColor));
								if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
									// adding directly to UMA Recipe
									TmpUMASlotDataList[TmpUMASlotDataList.Count-1].AddOverlay(GetOverlayLibrary().InstantiateOverlay(overlay._UMA.overlayName,_DK_RPG_UMA._Avatar.SkinColor));
								
								DKSlotData slot = TmpSlotDataList[TmpSlotDataList.Count-1];
								
								slot._LegacyData.IsLegacy = true;
								slot._LegacyData.ElderList.Add(TmpSlotDataList[i]);
								
								// del placeHolder if necessary
								if ( LegacySlot._LegacyData.Replace ) {
									ToRemoveList.Add (placeHolder);
								}
							}

							// for Wear and hair
							else {
									// assign Linked overlay color first 
									if ( LegacySlot.LinkedOverlayList.Count > 0 && LegacySlot.LinkedOverlayList[0].ColorPresets.Count > 0 ) 
										Crowd.Colors.ColorToApply = LegacySlot.LinkedOverlayList[0].ColorPresets[0].PresetColor;
									// or assign white color
									else Crowd.Colors.ColorToApply = Color.white;

								DKSlotData slot = Crowd.slotLibrary.InstantiateSlot(LegacySlot.slotName);
								TmpSlotDataList.Add(slot);
								slot._LegacyData.IsLegacy = true;
								slot._LegacyData.ElderList.Add(TmpSlotDataList[TmpSlotDataList.Count-1]);
						
								
								// define legacy slot's overlay
									if ( LegacySlot.LinkedOverlayList.Count > 0 ) {

									// define color preset
									if (LegacySlot.LinkedOverlayList[0].ColorPresets.Count > 0) {

										// get elder color for metal
										if ( Slot.overlayList.Count > 0 && Slot.overlayList[0].ColorPresets.Count > 0
										    && Slot.overlayList[0].ColorPresets[0].OverlayType == "Metal" ){
											Crowd.Colors.ColorToApply = Slot.overlayList[0].color;
										}
											// ran own color presets
											else Crowd.Colors.ColorToApply = Color.white;
									}
									AssignedOverlayList.Add (LegacySlot.LinkedOverlayList[0]);
									TmpSlotDataList[TmpSlotDataList.Count-1].overlayList.Add(Crowd.overlayLibrary.InstantiateOverlay(LegacySlot.LinkedOverlayList[0].overlayName,Crowd.Colors.ColorToApply));
									if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
										// adding directly to UMA Recipe
										TmpUMASlotDataList[TmpUMASlotDataList.Count-1].AddOverlay(GetOverlayLibrary().InstantiateOverlay(LegacySlot.LinkedOverlayList[0]._UMA.overlayName,Crowd.Colors.ColorToApply));
									
								}
							}
						}
						catch (System.NullReferenceException) {
							if ( LegacySlot == null ){
								Debug.LogError ( "slot '"+Slot.slotName+"' Legacy can't be generated. The legacy slot is missing. Verify the setting of '"+Slot.slotName+"' about the legacy. Skipping the legacy for "+Slot.slotName);
								#if UNITY_EDITOR
								#endif
							}
						}
					}
				}
			}
		}
	
		// clear the list of place holders
		foreach (DKSlotData placeHolder in ToRemoveList ){
			int phIndex = TmpSlotDataList.IndexOf (placeHolder);
			TmpSlotDataList.Remove (placeHolder);
			if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
				//  directly to UMA Recipe
				TmpUMASlotDataList.RemoveAt ( phIndex );			
		}

		#region Legacy
		#endregion Legacy
		
		#region hide shoulders
		if ( Crowd.Wears.Shoulders && Crowd.Wears.HideShoulders == true ) {
			int phIndex = TmpSlotDataList.IndexOf (Crowd.Wears.Shoulders);
			TmpSlotDataList.Remove(Crowd.Wears.Shoulders);
			if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
				//  directly to UMA Recipe
				TmpUMASlotDataList.RemoveAt ( phIndex );
		}
		Crowd.Wears.Shoulders = null;
		Crowd.Wears.HideShoulders = false; 
		#endregion hide shoulders
		
		#region hide Legs
		if ( Crowd.Wears.Legs && Crowd.Wears.HideLegs == true ) {
			int phIndex = TmpSlotDataList.IndexOf (Crowd.Wears.Legs);
			TmpSlotDataList.Remove(Crowd.Wears.Legs);
			if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
				//  directly to UMA Recipe
				TmpUMASlotDataList.RemoveAt ( phIndex );
		}
		Crowd.Wears.Legs = null;
		Crowd.Wears.HideLegs = false; 
		#endregion hide legs

		#region hide BeltWear
		if ( Crowd.Wears.Belt && Crowd.Wears.HideBelt == true ) {
			int phIndex = TmpSlotDataList.IndexOf (Crowd.Wears.Belt);
			TmpSlotDataList.Remove(Crowd.Wears.Belt);
			if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
				//  directly to UMA Recipe
				TmpUMASlotDataList.RemoveAt ( phIndex );
		}
		Crowd.Wears.Belt = null;
		Crowd.Wears.HideBelt = false; 
		#endregion hide BeltWear
		
		#region hide armband
		if ( Crowd.Wears.ArmBand && Crowd.Wears.HideArmBand == true ) {
			int phIndex = TmpSlotDataList.IndexOf (Crowd.Wears.ArmBand);
			TmpSlotDataList.Remove(Crowd.Wears.ArmBand);
			if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
				//  directly to UMA Recipe
				TmpUMASlotDataList.RemoveAt ( phIndex );
		}
		Crowd.Wears.ArmBand = null;
		Crowd.Wears.HideArmBand = false; 
		#endregion hide armband

		#region hide wrist
		if ( Crowd.Wears.Wrist && Crowd.Wears.HideWrist == true ) {
			int phIndex = TmpSlotDataList.IndexOf (Crowd.Wears.Wrist);
			TmpSlotDataList.Remove(Crowd.Wears.Wrist);
			if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
				//  directly to UMA Recipe
				TmpUMASlotDataList.RemoveAt ( phIndex );
		}
		Crowd.Wears.Wrist = null;
		Crowd.Wears.HideWrist = false; 
		#endregion hide wrist

		#region hide Leg band
		if ( Crowd.Wears.LegBand && Crowd.Wears.HideLegBand == true ) {
			int phIndex = TmpSlotDataList.IndexOf (Crowd.Wears.LegBand);
			TmpSlotDataList.Remove(Crowd.Wears.LegBand);
			if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
				//  directly to UMA Recipe
				TmpUMASlotDataList.RemoveAt ( phIndex );
		}
		Crowd.Wears.LegBand = null;
		Crowd.Wears.HideLegBand = false; 
		#endregion hide Leg band

		#region hide Collar
		if ( Crowd.Wears.Collar && Crowd.Wears.HideCollar == true ) {
			int phIndex = TmpSlotDataList.IndexOf (Crowd.Wears.Collar);
			TmpSlotDataList.Remove(Crowd.Wears.Collar);
			if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
				//  directly to UMA Recipe
				TmpUMASlotDataList.RemoveAt ( phIndex );
		}
		Crowd.Wears.Collar = null;
		Crowd.Wears.HideCollar = false; 
		#endregion hide Collar

		#region hide RingLeft
		if ( Crowd.Wears.RingLeft && Crowd.Wears.HideRingLeft == true ) {
			int phIndex = TmpSlotDataList.IndexOf (Crowd.Wears.RingLeft);
			TmpSlotDataList.Remove(Crowd.Wears.RingLeft);
			if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
				//  directly to UMA Recipe
				TmpUMASlotDataList.RemoveAt ( phIndex );
		}
		Crowd.Wears.RingLeft = null;
		Crowd.Wears.HideRingLeft = false; 
		#endregion hide RingLeft

		#region hide RingRight
		if ( Crowd.Wears.RingRight && Crowd.Wears.HideRingRight == true ) {
			int phIndex = TmpSlotDataList.IndexOf (Crowd.Wears.RingRight);
			TmpSlotDataList.Remove(Crowd.Wears.RingRight);
			if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
				//  directly to UMA Recipe
				TmpUMASlotDataList.RemoveAt ( phIndex );
		}
		Crowd.Wears.RingRight = null;
		Crowd.Wears.HideRingRight = false; 
		#endregion hide RingRight

		#region hide Cloak
		if ( Crowd.Wears.Cloak && Crowd.Wears.HideCloak == true ) {
			int phIndex = TmpSlotDataList.IndexOf (Crowd.Wears.Cloak);
			TmpSlotDataList.Remove(Crowd.Wears.Cloak);
			if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
				//  directly to UMA Recipe
				TmpUMASlotDataList.RemoveAt ( phIndex );
		}
		Crowd.Wears.Cloak = null;
		Crowd.Wears.HideCloak = false; 
		#endregion hide Cloak

		#region hide Backpack
		if ( Crowd.Wears.Backpack && Crowd.Wears.HideBackpack == true ) {
			int phIndex = TmpSlotDataList.IndexOf (Crowd.Wears.Cloak);
			TmpSlotDataList.Remove(Crowd.Wears.Backpack);
			if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 )
				//  directly to UMA Recipe
				TmpUMASlotDataList.RemoveAt ( phIndex );
		}
		Crowd.Wears.Backpack = null;
		Crowd.Wears.HideBackpack = false; 
		#endregion hide Backpack

		 
		#endregion Cleaning Avatar
		
		Finishing();
	}

	public void VerifyUMAMaterial ( UMA.SlotDataAsset slot, UMA.OverlayDataAsset overlay ){
		if ( slot != null && slot.material == null ) Debug.LogError ( "UMA Slot '"+slot.name+"' doesn't have a UMAMaterial assigned. Please fixe that issue by selecting the slot using the Element Manager and assign a UMAMaterial. " +
			"For a PBR element, the default material is 'UMA_Diffuse_Normal_Metallic'.");
		else if ( overlay != null && overlay.material == null ){
			overlay.material = slot.material;
			Debug.Log ( "UMA overlay '"+overlay.name+"' doesn't have a UMAMaterial assigned. Auto assigning the UMAMaterial from '"+slot.name+"' slot ("+slot.material.name+")." );
		}
	}

	public void AddToRPG ( DK_UMACrowd Crowd, DKSlotData slot, DKOverlayData overlay, string type, bool OverlayOnly, Color color){
		// Add the slot to the RPG values of the Avatar

	//	VerifyUMAMaterial ( slot._UMA, overlay._UMA );

		#region Head
		if ( type == "_Head" ){
			_DK_RPG_UMA._Avatar._Face._Head.Slot = slot;
			_DK_RPG_UMA._Avatar._Face._Head.Overlay = overlay;
			_DK_RPG_UMA._Avatar._Face._Head.Color = color;
		}
		else if ( type == "_Head_Tatoo" ){
			_DK_RPG_UMA._Avatar._Face._Head.Tattoo = overlay;
			_DK_RPG_UMA._Avatar._Face._Head.TattooColor = color;
		}
		else if ( type == "_Head_MakeUp" ){
			_DK_RPG_UMA._Avatar._Face._Head.Makeup = overlay;
			_DK_RPG_UMA._Avatar._Face._Head.MakeupColor = color;
		}
		
		#region FaceHair
		else if ( type == "_EyeBrows" ){
			_DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrows = overlay;
			_DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrowsColor = color;
		}
		if ( type == "_BeardSlotOnly" ){
			_DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Slot = slot;
			_DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Overlay = overlay;
			_DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Color = color;
		}
		// Overlay only
		else if ( type == "_Beard1" ){
			_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard1 = overlay;
			_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard1Color = color;
		}
		else if ( type == "Beard2" ){
			_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard2 = overlay;
			_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard2Color = color;
			
		}
		else if ( type == "Beard3" ){
			_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard3 = overlay;
			_DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard3Color = color;
		}
		#endregion FaceHair
		
		else if ( type == "_EyeLash" ){
			_DK_RPG_UMA._Avatar._Face._EyeLash.Slot = slot;
			_DK_RPG_UMA._Avatar._Face._EyeLash.Overlay = overlay;
			_DK_RPG_UMA._Avatar._Face._EyeLash.Color = color;
		}
		else if ( type == "_EyeLids" ){
			_DK_RPG_UMA._Avatar._Face._EyeLids.Slot = slot;
			_DK_RPG_UMA._Avatar._Face._EyeLids.Overlay = overlay;
			_DK_RPG_UMA._Avatar._Face._EyeLids.Color = color;
		}
		else if ( type == "_Eyes" ){
			_DK_RPG_UMA._Avatar._Face._Eyes.Slot = slot;
			_DK_RPG_UMA._Avatar._Face._Eyes.Overlay = overlay;
			_DK_RPG_UMA._Avatar._Face._Eyes.Color = color;
		}
		else if ( type == "_Ears" ){
			_DK_RPG_UMA._Avatar._Face._Ears.Slot = slot;
			_DK_RPG_UMA._Avatar._Face._Ears.Overlay = overlay;
			_DK_RPG_UMA._Avatar._Face._Ears.Color = color;
		}
		else if ( type == "_Nose" ){
			_DK_RPG_UMA._Avatar._Face._Nose.Slot = slot;
			_DK_RPG_UMA._Avatar._Face._Nose.Overlay = overlay;
		}
		else if ( type == "_Mouth" ){
			_DK_RPG_UMA._Avatar._Face._Mouth.Slot = slot;
		}
		else if ( type == "_Lips" ){
			_DK_RPG_UMA._Avatar._Face._Mouth.Lips = overlay;
			_DK_RPG_UMA._Avatar._Face._Mouth.LipsColor = color;
		}
		else if ( type == "_InnerMouth" ){
			_DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.Slot = slot;
			_DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.Overlay = overlay;
			_DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.color = color;
		}
		#endregion Head
		
		#region Hair
		else if ( type == "_HairSlotOnly" ){
			_DK_RPG_UMA._Avatar._Hair._SlotOnly.Slot = slot;
			_DK_RPG_UMA._Avatar._Hair._SlotOnly.Overlay = overlay;
			_DK_RPG_UMA._Avatar._Hair._SlotOnly.Color = color;
		}
		// Overlay only
		else if ( type == "_HairOverlayOnly" ){
			_DK_RPG_UMA._Avatar._Hair._OverlayOnly.Overlay = overlay;
			_DK_RPG_UMA._Avatar._Hair._OverlayOnly.Color = color;
		}
		#endregion Hair
		
		#region Body
		#region _Torso
		if ( type == "_Torso" ){
			_DK_RPG_UMA._Avatar._Body._Torso.Slot = slot;
			_DK_RPG_UMA._Avatar._Body._Torso.Overlay = overlay;
			_DK_RPG_UMA._Avatar._Body._Torso.Color = color;
		}
		else if ( type == "_Torso_Tatoo" ){
			_DK_RPG_UMA._Avatar._Body._Torso.Tattoo = overlay;
			_DK_RPG_UMA._Avatar._Body._Torso.TattooColor = color;
		}
		else if ( type == "_Torso_MakeUp" ){
			_DK_RPG_UMA._Avatar._Body._Torso.Makeup = overlay;
			_DK_RPG_UMA._Avatar._Body._Torso.MakeupColor = color;
		}
		#endregion _Torso
		#region _Hands
		if ( type == "_Hands" ){
			_DK_RPG_UMA._Avatar._Body._Hands.Slot = slot;
			_DK_RPG_UMA._Avatar._Body._Hands.Overlay = overlay;
			_DK_RPG_UMA._Avatar._Body._Hands.Color = color;
		}

		else if ( type == "_Hands_Tatoo" ){
			_DK_RPG_UMA._Avatar._Body._Hands.Tattoo = overlay;
			_DK_RPG_UMA._Avatar._Body._Hands.TattooColor = color;
		}
		else if ( type == "_Hands_MakeUp" ){
			_DK_RPG_UMA._Avatar._Body._Hands.Makeup = overlay;
			_DK_RPG_UMA._Avatar._Body._Hands.MakeupColor = color;
		}
		#endregion _Hands
		#region _Legs
		if ( type == "_Legs" ){
			_DK_RPG_UMA._Avatar._Body._Legs.Slot = slot;
			_DK_RPG_UMA._Avatar._Body._Legs.Overlay = overlay;
			_DK_RPG_UMA._Avatar._Body._Legs.Color = color;
		}
		else if ( type == "_Legs_Tatoo" ){
			_DK_RPG_UMA._Avatar._Body._Legs.Tattoo = overlay;
			_DK_RPG_UMA._Avatar._Body._Legs.TattooColor = color;
		}
		else if ( type == "_Legs_MakeUp" ){
			_DK_RPG_UMA._Avatar._Body._Legs.Makeup = overlay;
			_DK_RPG_UMA._Avatar._Body._Legs.MakeupColor = color;
		}
		#endregion _Legs
		#region _Feet
		if ( type == "_Feet" ){
			_DK_RPG_UMA._Avatar._Body._Feet.Slot = slot;
			_DK_RPG_UMA._Avatar._Body._Feet.Overlay = overlay;
			_DK_RPG_UMA._Avatar._Body._Feet.Color = color;
		}
		else if ( type == "_Feet_Tatoo" ){
			_DK_RPG_UMA._Avatar._Body._Feet.Tattoo = overlay;
			_DK_RPG_UMA._Avatar._Body._Feet.TattooColor = color;
		}
		else if ( type == "_Feet_MakeUp" ){
			_DK_RPG_UMA._Avatar._Body._Feet.Makeup = overlay;
			_DK_RPG_UMA._Avatar._Body._Feet.MakeupColor = color;
		}
		#endregion _Feet
		#endregion Body
	}
	
	public void CopyValues (DK_UMACrowd Crowd, DKSlotData slot, int index){
		if ( !overlay && slot.OverlayType != "Flesh" ) Debug.LogError ( "Overlay is missing for '"+slot.name+"', skipping it." );
		if ( !TmpSlotDataList[index] ){/* Debug.LogError ( this.transform.parent.name+" Slot is missing, skipping it." );*/}
		else{
			TmpSlotDataList[index].OverlayType = slot.OverlayType;
			TmpSlotDataList[index].Place = slot.Place;
			TmpSlotDataList[index]._UMA = slot._UMA;
			TmpSlotDataList[index].Replace = slot.Replace;
			TmpSlotDataList[index]._HideData.HideHair = slot._HideData.HideHair;
			TmpSlotDataList[index]._HideData.HideHairModule = slot._HideData.HideHairModule;
			TmpSlotDataList[index]._HideData.HideLegs = slot._HideData.HideLegs;

			TmpSlotDataList[index]._HideData.HideBelt = slot._HideData.HideBelt;
			TmpSlotDataList[index]._HideData.HideArmBand = slot._HideData.HideArmBand;
			TmpSlotDataList[index]._HideData.HideLegBand = slot._HideData.HideLegBand;
			TmpSlotDataList[index]._HideData.HideWrist = slot._HideData.HideWrist;
			TmpSlotDataList[index]._HideData.HideCollar = slot._HideData.HideCollar;
			TmpSlotDataList[index]._HideData.HideRingLeft = slot._HideData.HideRingLeft;
			TmpSlotDataList[index]._HideData.HideRingRight = slot._HideData.HideRingRight;
			TmpSlotDataList[index]._HideData.HideCloak = slot._HideData.HideCloak;
			TmpSlotDataList[index]._HideData.HideBackpack = slot._HideData.HideBackpack;
			TmpSlotDataList[index]._HideData.HideUnderwear = slot._HideData.HideUnderwear;

			TmpSlotDataList[index]._HideData.HideMouth = slot._HideData.HideMouth;
			TmpSlotDataList[index]._HideData.HideShoulders = slot._HideData.HideShoulders;
			TmpSlotDataList[index]._HideData.HideBeard = slot._HideData.HideBeard;
			TmpSlotDataList[index]._HideData.HideEars = slot._HideData.HideEars;
			TmpSlotDataList[index]._LegacyData.HasLegacy = slot._LegacyData.HasLegacy;
			TmpSlotDataList[index]._LegacyData.LegacyList = slot._LegacyData.LegacyList;
			TmpSlotDataList[index]._LegacyData.IsLegacy = slot._LegacyData.IsLegacy;
			TmpSlotDataList[index]._LegacyData.ElderList = slot._LegacyData.ElderList;

			TmpSlotDataList[index]._LOD.IsLOD0 = slot._LOD.IsLOD0;
			TmpSlotDataList[index]._LOD.LOD1 = slot._LOD.LOD1;
			TmpSlotDataList[index]._LOD.LOD2 = slot._LOD.LOD2;
			TmpSlotDataList[index]._LOD.MasterLOD = slot._LOD.MasterLOD;
		}
	}

	void Finishing (){
	//	Debug.Log ("DK UMA : Finishing rebuild for "+umaData.gameObject.GetComponent<DK_RPG_UMA>().FileName);

		// assign the recipe
		if ( umaData == null ) umaData = transform.GetComponentInChildren<DKUMAData>();
		umaData.umaRecipe.slotDataList = TmpSlotDataList.ToArray();

		if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 ){
			// create a UMA 2.5 recipe
			NewUmaData = new UMAData ();
			NewUmaData.umaRecipe = new UMA.UMAData.UMARecipe ();
		
			Debug.Log ("TmpSlotDataList.Count "+TmpSlotDataList.Count);
			Debug.Log ("TmpUMASlotDataList.Count "+TmpUMASlotDataList.Count);
			// set race
			if ( umaData.umaRecipe.raceData.UMA != null )
				NewUmaData.umaRecipe.SetRace (umaData.umaRecipe.raceData.UMA);
			else {
				if ( _DK_RPG_UMA.Gender == "Male" ){
					NewUmaData.umaRecipe.SetRace(GetRaceLibrary().GetRace("HumanMale"));
				}
				else NewUmaData.umaRecipe.SetRace(GetRaceLibrary().GetRace("HumanFemale"));
			}

			NewUmaData.SetSlots(TmpUMASlotDataList.ToArray());		
		//	NewUmaData.SetSlots(TmpUMASlotDataList.ToArray());		
		}
		if ( _save ) 
			SaveAvatar (umaData);
		else TransposeOnly (umaData);
	}
	UMAData NewUmaData = new UMAData ();
	public static DKDnaConverterBehaviour _DnaConverterBehaviour;
	public void TransposeOnly ( DKUMAData umaData ){
		PrepareDNA ();
		string streamed =  "";
		if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 ){
			Debug.Log ("TransposeOnly version25 ");
			// create a UMA 2.5 recipe
			var asset = ScriptableObject.CreateInstance<UMATextRecipe>();
			//check if Avatar is DCS
			asset.Save(NewUmaData.umaRecipe, umaContext);	
			streamed = asset.recipeString;
			umaData.streamedUMA = streamed;
		}
		else {
			umaData.SaveToMemoryStream();
			streamed =  umaData.streamedUMA;
		}
		TransposeDK2UMA _TransposeDK2UMA = umaData.gameObject.GetComponent<TransposeDK2UMA>();
		if ( _TransposeDK2UMA == null ){
			_TransposeDK2UMA  = umaData.gameObject.AddComponent<TransposeDK2UMA>();
		}
		_TransposeDK2UMA.Launch ( umaData.gameObject.GetComponent<DK_RPG_UMA>(), 
		                         Crowd, 
		                         streamed, RefreshOnly );
	
		DeleteScripts ();
	}

	public void SaveAvatar ( DKUMAData umaData ){
		PrepareDNA ();

		string streamed =  "";
		if ( _DK_UMA_GameSettings.UMAVersion == DK_UMA_GameSettings.UMAVersionEnum.version25 ){
			Debug.Log ("SaveAvatar version25 ");
			// create a UMA 2.5 recipe
			var asset = ScriptableObject.CreateInstance<UMATextRecipe>();
			//check if Avatar is DCS
			asset.Save(NewUmaData.umaRecipe, umaContext);	
			streamed = asset.recipeString;
			umaData.streamedUMA = streamed;
		}
		else {
			umaData.SaveToMemoryStream();
			streamed =  umaData.streamedUMA;
		}
		TransposeDK2UMA _TransposeDK2UMA = umaData.gameObject.GetComponent<TransposeDK2UMA>();
		if ( _TransposeDK2UMA == null ){
			_TransposeDK2UMA  = umaData.gameObject.AddComponent<TransposeDK2UMA>();
		}
	//	_TransposeDK2UMA.UpdateOnly = true;
		_TransposeDK2UMA.Launch ( umaData.gameObject.GetComponent<DK_RPG_UMA>(), 
		                         Crowd, 
	                         streamed, RefreshOnly );

		// save

	//	Debug.Log ("DK UMA : Preparing to Save "+umaData.gameObject.GetComponent<DK_RPG_UMA>().FileName);

		DK_UMACrowd _DK_UMACrowd = GameObject.Find("DK_UMA").GetComponentInChildren<DK_UMACrowd>();
		DKUMA_Variables	_DKUMA_Variables = GameObject.Find("DK_UMA").GetComponent<DKUMA_Variables>();


		if ( umaData.gameObject.GetComponent<DK_RPG_UMA>().LoadFromFile
		    && umaData.gameObject.GetComponent<DK_RPG_UMA>().FileName != "" )
			DK_UMA_Save.SaveCompleteAvatar( umaData.gameObject.GetComponent<DK_RPG_UMA>(), umaData.gameObject.GetComponent<DK_RPG_UMA>().FileName, _DK_UMACrowd, _DKUMA_Variables._DK_UMA_GameSettings.SaveToFile );

		DeleteScripts ();
	}

	public DKUMADnaHumanoid umaDna;
	void PrepareDNA (){
		umaDna = new DKUMADnaHumanoid();

		for(int i = 0; i < umaData.DNAList2.Count; i ++){
			// add to DK_UMAdnaHumanoid
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
			if ( i == 48 ) umaDna.N48 = tmpValue;if ( i == 49 ) umaDna.N49 = tmpValue;
			if ( i == 50 ) umaDna.N50 = tmpValue;if ( i == 51 ) umaDna.N51 = tmpValue;if ( i == 52 ) umaDna.N52 = tmpValue;
			if ( i == 53 ) umaDna.N53 = tmpValue;if ( i == 54 ) umaDna.N54 = tmpValue;if ( i == 55 ) umaDna.N55 = tmpValue;
			if ( i == 56 ) umaDna.N56 = tmpValue;if ( i == 57 ) umaDna.N57 = tmpValue;if ( i == 58 ) umaDna.N58 = tmpValue;
			if ( i == 59 ) umaDna.N59 = tmpValue;
			if ( i == 60 ) umaDna.N60 = tmpValue;if ( i == 61 ) umaDna.N61 = tmpValue;if ( i == 62 ) umaDna.N62 = tmpValue;
			if ( i == 63 ) umaDna.N63 = tmpValue;if ( i == 64 ) umaDna.N64 = tmpValue;if ( i == 65 ) umaDna.N65 = tmpValue;
			if ( i == 66 ) umaDna.N66 = tmpValue;if ( i == 67 ) umaDna.N67 = tmpValue;if ( i == 68 ) umaDna.N68 = tmpValue;
			if ( i == 69 ) umaDna.N69 = tmpValue;
			if ( i == 70 ) umaDna.N70 = tmpValue;
		}
		umaData.umaRecipe.umaDna.Clear ();
		umaData.umaRecipe.umaDna.Add(umaDna.GetType(),umaDna);
	}

	void DeleteScripts (){
		if (  Application.isPlaying ) Destroy(this);
		else DestroyImmediate(this);
	}

	private RaceLibraryBase GetRaceLibrary()
	{
		return umaContext.raceLibrary;
	}

	private SlotLibraryBase GetSlotLibrary()
	{
		return umaContext.slotLibrary;
	}

	private OverlayLibraryBase GetOverlayLibrary()
	{
		return umaContext.overlayLibrary;
	}
}
