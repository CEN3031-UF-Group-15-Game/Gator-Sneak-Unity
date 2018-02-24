using UnityEngine;
using System.Collections;
#if UNITY_5_5_OR_NEWER
#else
//using LitJson;
#endif

public class DK_UMA_Save : MonoBehaviour {

	public static void SaveCompleteAvatar ( DK_RPG_UMA _DK_RPG_UMA, string CharacterName, DK_UMACrowd _DK_UMACrowd, bool ToFile, bool ToDB) {
		SaveCompleteAvatar ( _DK_RPG_UMA, CharacterName, ToFile, ToDB );
	}

	public static void SaveCompleteAvatar ( DK_RPG_UMA _DK_RPG_UMA, string CharacterName, DK_UMACrowd _DK_UMACrowd) {
		SaveCompleteAvatar ( _DK_RPG_UMA, CharacterName, true, false );
	}

	public static void SaveCompleteAvatar ( DK_RPG_UMA _DK_RPG_UMA, string CharacterName, DK_UMACrowd _DK_UMACrowd, bool ToFile) {
		SaveCompleteAvatar ( _DK_RPG_UMA, CharacterName, ToFile, false );
	}

	public static void SaveCompleteAvatar ( DK_RPG_UMA _DK_RPG_UMA, string CharacterName, bool ToFile, bool ToDB) {
		
	//	System.IO.Directory.CreateDirectory("Assets/StreamingAssets/");


		// rename the objects
		_DK_RPG_UMA.transform.name = CharacterName;
		_DK_RPG_UMA.transform.GetComponentInChildren<UMA.UMAData>().name = "Avatar";

		//Create data instance
		SaveData data = new SaveData(CharacterName);
		
		//Add keys with significant names and values
		data["Name"] = CharacterName;
		data["Gender"] = _DK_RPG_UMA.Gender;
		data["Race"] = _DK_RPG_UMA.Race;
		data["RaceData"] = _DK_RPG_UMA.RaceData.name;
		data["Weight"] = _DK_RPG_UMA.Weight;
		
	//	Debug.Log ("DK UMA : Saving "+data["Name"]);

		// Add Head and Body
		// Root
		data["HeadIndex"] = _DK_RPG_UMA._Avatar.HeadIndex;
		data["TorsoIndex"] = _DK_RPG_UMA._Avatar.TorsoIndex;
		data["SkinColor"] = _DK_RPG_UMA._Avatar.SkinColor;
		if ( _DK_RPG_UMA._Avatar.SkinColorPreset )
			data["SkinColorPreset"] = _DK_RPG_UMA._Avatar.SkinColorPreset.ColorPresetName;
		else data["SkinColorPreset"] = "NA";
		data["HairColor"] = _DK_RPG_UMA._Avatar.HairColor;
		if ( _DK_RPG_UMA._Avatar.HairColorPreset != null ) {
			data["HairColorPreset"] = _DK_RPG_UMA._Avatar.HairColorPreset.ColorPresetName;
		}
		else data["HairColorPreset"] = "NA";

		data["EyeColor"] = _DK_RPG_UMA._Avatar.EyeColor;
		if ( _DK_RPG_UMA._Avatar.EyeColorPreset != null ) data["EyeColorPreset"] = _DK_RPG_UMA._Avatar.EyeColorPreset.ColorPresetName;
		else data["EyeColorPreset"] = "NA";
	
		#region Head

		#region Face
		if ( _DK_RPG_UMA._Avatar._Face._Head.Slot != null )
			data["FaceHeadSlot"] = _DK_RPG_UMA._Avatar._Face._Head.Slot.name;
		else data["FaceHeadSlot"] = "NA";
		if ( _DK_RPG_UMA._Avatar._Face._Head.Overlay != null )
			data["FaceHeadOverlay"] = _DK_RPG_UMA._Avatar._Face._Head.Overlay.name;
		else data["FaceHeadOverlay"] = "NA";

		if ( _DK_RPG_UMA._Avatar._Face._Head.Tattoo != null ) {
			data["FaceHeadTatoo"] = _DK_RPG_UMA._Avatar._Face._Head.Tattoo.name;
			data["FaceHeadTatooColor"] = _DK_RPG_UMA._Avatar._Face._Head.TattooColor;
			if ( _DK_RPG_UMA._Avatar._Face._Head.TatooColorPreset ) data["FaceHeadTatooColorPreset"] = _DK_RPG_UMA._Avatar._Face._Head.TatooColorPreset.ColorPresetName;
			else data["FaceHeadTatooColorPreset"] = "NA";
		}
		else {
			data["FaceHeadTatoo"] = "NA";
			data["FaceHeadTatooColor"] = "NA";
			data["FaceHeadTatooColorPreset"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Face._Head.Makeup != null ) {
			data["FaceHeadMakeup"] = _DK_RPG_UMA._Avatar._Face._Head.Makeup.name;
			data["FaceHeadMakeupColor"] = _DK_RPG_UMA._Avatar._Face._Head.MakeupColor;
			if ( _DK_RPG_UMA._Avatar._Face._Head.MakeupColorPreset ) data["FaceHeadMakeupColorPreset"] = _DK_RPG_UMA._Avatar._Face._Head.MakeupColorPreset.ColorPresetName;
			else data["FaceHeadMakeupColorPreset"] = "NA";
		}
		else {
			data["FaceHeadMakeup"] = "NA";
			data["FaceHeadMakeupColor"] = "NA";
			data["FaceHeadMakeupColorPreset"] = "NA";
		}
		#endregion Face

		#region EyeBrows
		// Face hair
		if ( _DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrows != null && _DK_RPG_UMA._Avatar._Face._Eyebrows.Slot == null ) {
			data["EyeBrows"] = _DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrows.name;
			data["EyeBrowsColor"] = _DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrowsColor;
			data["FaceEyeBrowsSlot"] = "NA";
			data["FaceEyeBrowsOverlay"] = "NA";
		}
		else if ( _DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrows == null && _DK_RPG_UMA._Avatar._Face._Eyebrows.Slot == null ){
			data["EyeBrows"] = "NA";
			data["FaceEyeBrowsSlot"] = "NA";
			data["FaceEyeBrowsOverlay"] = "NA";
			data["EyeBrowsColor"] = "NA";
		}
		else if ( _DK_RPG_UMA._Avatar._Face._Eyebrows.Slot != null ) {
			data["EyeBrows"] = "NA";
			data["FaceEyeBrowsSlot"] = _DK_RPG_UMA._Avatar._Face._Eyebrows.Slot.name;
			data["FaceEyeBrowsOverlay"] = _DK_RPG_UMA._Avatar._Face._Eyebrows.Overlay.name;
			data["EyeBrowsColor"] = _DK_RPG_UMA._Avatar._Face._Eyebrows.Color;
		}
		else {
			if ( _DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrows == null ) data["EyeBrows"] = "NA";
			data["FaceEyeBrowsSlot"] = "NA";
			data["FaceEyeBrowsOverlay"] = "NA";
			if ( _DK_RPG_UMA._Avatar._Face._FaceHair.EyeBrows == null ) data["EyeBrowsColor"] = "NA";
		}
		#endregion EyeBrows

		#region Beard
		if ( _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard1 != null ) {
			data["Beard1OverlayOnly"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard1.name;
			data["Beard1OverlayOnlyColor"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard1Color;
		}
		else {
			data["Beard1OverlayOnly"] = "NA";
			data["Beard1OverlayOnlyColor"] = "NA";
		}
		if ( _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard2 != null ) {
			data["Beard2OverlayOnly"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard2.name;
			data["Beard2OverlayOnlyColor"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard2Color;
		}
		else {
			data["Beard2OverlayOnly"] = "NA";
			data["Beard2OverlayOnlyColor"] = "NA";
		}
		if ( _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard3 != null ) {
			data["Beard3OverlayOnly"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard3.name;
			data["Beard3OverlayOnlyColor"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardOverlayOnly.Beard3Color;
		}
		else {
			data["Beard3OverlayOnly"] = "NA";
			data["Beard3OverlayOnlyColor"] = "NA";
		}
		// Bead Slot 1
		if ( _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Slot != null ) {
			data["BeardSlotOnlySlot"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Slot.name;
			data["BeardSlotOnlyOverlay"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Overlay.name;
			data["BeardSlotOnlyColor"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly.Color;
		}
		else {
			data["BeardSlotOnlySlot"] = "NA";
			data["BeardSlotOnlyOverlay"] = "NA";
			data["BeardSlotOnlyColor"] = "NA";
		}
		// Bead Slot 2
		if ( _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly2.Slot != null ) {
			data["BeardSlotOnlySlot2"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly2.Slot.name;
			data["BeardSlotOnlyOverlay2"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly2.Overlay.name;
			data["BeardSlotOnlyColor2"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly2.Color;
		}
		else {
			data["BeardSlotOnlySlot2"] = "NA";
			data["BeardSlotOnlyOverlay2"] = "NA";
			data["BeardSlotOnlyColor2"] = "NA";
		}
		// Bead Slot 3
		if ( _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly3.Slot != null ) {
			data["BeardSlotOnlySlot3"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly3.Slot.name;
			data["BeardSlotOnlyOverlay3"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly3.Overlay.name;
			data["BeardSlotOnlyColor3"] = _DK_RPG_UMA._Avatar._Face._FaceHair._BeardSlotOnly3.Color;
		}
		else {
			data["BeardSlotOnlySlot3"] = "NA";
			data["BeardSlotOnlyOverlay3"] = "NA";
			data["BeardSlotOnlyColor3"] = "NA";
		}
		#endregion Beard

		#region Horns
		// Horns
		if ( _DK_RPG_UMA._Avatar._Face._Horns.Slot != null ) {
			data["HornsSlot"] = _DK_RPG_UMA._Avatar._Face._Horns.Slot.name;
			data["HornsOverlay"] = _DK_RPG_UMA._Avatar._Face._Horns.Overlay.name;
			data["HornsColor"] = _DK_RPG_UMA._Avatar._Face._Horns.ColorPreset.ColorPresetName;
		}
		else {
			data["HornsSlot"] =  "NA";
			data["HornsOverlay"] =  "NA";
			data["HornsColor"] =  "NA";
		}
		#endregion Horns

		#region Face Parts
		// Eyes
		if ( _DK_RPG_UMA._Avatar._Face._Eyes.Slot != null ) {
			data["FaceEyesSlot"] = _DK_RPG_UMA._Avatar._Face._Eyes.Slot.name;
			data["FaceEyesOverlay"] = _DK_RPG_UMA._Avatar._Face._Eyes.Overlay.name;
			data["FaceEyesColor"] = _DK_RPG_UMA._Avatar._Face._Eyes.Color;
			string tmpName = "NA";
			if ( _DK_RPG_UMA._Avatar._Face._Eyes.Adjust != null ) tmpName = _DK_RPG_UMA._Avatar._Face._Eyes.Adjust.name;
			data["FaceEyesAdjustOverlay"] = tmpName;
			data["FaceEyesAdjustColor"] = _DK_RPG_UMA._Avatar._Face._Eyes.AdjustColor;
		}
		else {
			data["FaceEyesSlot"] =  "NA";
			data["FaceEyesOverlay"] =  "NA";
			data["FaceEyesAdjustOverlay"] =  "NA";
		}
		// EyesIris
		if ( _DK_RPG_UMA._Avatar._Face._EyesIris.Slot != null ) {
			data["FaceEyesIrisSlot"] = _DK_RPG_UMA._Avatar._Face._EyesIris.Slot.name;
			data["FaceEyesIrisOverlay"] = _DK_RPG_UMA._Avatar._Face._EyesIris.Overlay.name;
			data["FaceEyesIrisColor"] = _DK_RPG_UMA._Avatar._Face._EyesIris.Color;
			string tmpName = "NA";
			if ( _DK_RPG_UMA._Avatar._Face._EyesIris.Adjust != null ) tmpName = _DK_RPG_UMA._Avatar._Face._EyesIris.Adjust.name;
			data["FaceEyesIrisAdjustOverlay"] = tmpName;
			data["FaceEyesIrisAdjustColor"] = _DK_RPG_UMA._Avatar._Face._EyesIris.AdjustColor;
		}
		else {
			data["FaceEyesIrisSlot"] =  "NA";
			data["FaceEyesIrisOverlay"] =  "NA";
			data["FaceEyesIrisColor"] = "NA";
			data["FaceEyesIrisAdjustOverlay"] =  "NA";
			data["FaceEyesIrisAdjustColor"] = "NA";
		}
		// eye lash
		if ( _DK_RPG_UMA._Avatar._Face._EyeLash.Slot != null ) {
			data["FaceEyeLashSlot"] = _DK_RPG_UMA._Avatar._Face._EyeLash.Slot.name;
			data["FaceEyeLashOverlay"] = _DK_RPG_UMA._Avatar._Face._EyeLash.Overlay.name;
			data["FaceEyeLashColor"] = _DK_RPG_UMA._Avatar._Face._EyeLash.Color;
		}
		else {
			data["FaceEyeLashSlot"] = "NA";
			data["FaceEyeLashOverlay"] = "NA";
			data["FaceEyeLashColor"] = "NA";
		}
		// eye lids
		if ( _DK_RPG_UMA._Avatar._Face._EyeLids.Slot ) {
			data["FaceEyeLidsSlot"] = _DK_RPG_UMA._Avatar._Face._EyeLids.Slot.name;
			data["FaceEyeLidsOverlay"] = _DK_RPG_UMA._Avatar._Face._EyeLids.Overlay.name;
			data["FaceEyeLidsColor"] = _DK_RPG_UMA._Avatar._Face._EyeLids.Color;
		}
		else {
			data["FaceEyeLidsSlot"] = "NA";
			data["FaceEyeLidsOverlay"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Face._EyeLids.Tattoo != null ) {
			data["FaceEyeLidsTatoo"] = _DK_RPG_UMA._Avatar._Face._EyeLids.Tattoo.name;
			data["FaceEyeLidsTatooColor"] = _DK_RPG_UMA._Avatar._Face._EyeLids.TattooColor;
			data["FaceEyeLidsTatooColorPreset"] = _DK_RPG_UMA._Avatar._Face._EyeLids.TatooColorPreset.ColorPresetName;
		}
		else {
			data["FaceEyeLidsTatoo"] = "NA";
			data["FaceEyeLidsTatooColor"] = "NA";
			data["FaceEyeLidsTatooColorPreset"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Face._EyeLids.Makeup != null ) {
			data["FaceEyeLidsMakeup"] = _DK_RPG_UMA._Avatar._Face._EyeLids.Makeup.name;
			data["FaceEyeLidsMakeupColor"] = _DK_RPG_UMA._Avatar._Face._EyeLids.MakeupColor;
			data["FaceEyeLidsMakeupColorPreset"] = _DK_RPG_UMA._Avatar._Face._EyeLids.MakeupColorPreset.ColorPresetName;
		}
		else {
			data["FaceEyeLidsMakeup"] = "NA";
			data["FaceEyeLidsMakeupColor"] = "NA";
			data["FaceEyeLidsMakeupColorPreset"] = "NA";
		}
		// Ears
		if ( _DK_RPG_UMA._Avatar._Face._Ears.Slot != null ) {
			data["FaceEarsSlot"] = _DK_RPG_UMA._Avatar._Face._Ears.Slot.name;
			data["FaceEarsOverlay"] = _DK_RPG_UMA._Avatar._Face._Ears.Overlay.name;
			data["FaceEarsColor"] = _DK_RPG_UMA._Avatar._Face._Ears.Color;
		}
		else {
			data["FaceEarsSlot"] = "NA";
			data["FaceEarsOverlay"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Face._Ears.Tattoo != null ) {
			data["FaceEarsTatoo"] = _DK_RPG_UMA._Avatar._Face._Ears.Tattoo.name;
			data["FaceEarsTatooColor"] = _DK_RPG_UMA._Avatar._Face._Ears.TattooColor;
			data["FaceEarsTatooColorPreset"] = _DK_RPG_UMA._Avatar._Face._Ears.TatooColorPreset.ColorPresetName;
		}
		else {
			data["FaceEarsTatoo"] = "NA";
			data["FaceEarsTatooColor"] = "NA";
			data["FaceEarsTatooColorPreset"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Face._Ears.Makeup != null ) {
			data["FaceEarsMakeup"] = _DK_RPG_UMA._Avatar._Face._Ears.Makeup.name;
			data["FaceEarsMakeupColor"] = _DK_RPG_UMA._Avatar._Face._Ears.MakeupColor;
			data["FaceEarsMakeupColorPreset"] = _DK_RPG_UMA._Avatar._Face._Ears.MakeupColorPreset.ColorPresetName;
		}
		else {
			data["FaceEarsMakeup"] = "NA";
			data["FaceEarsMakeupColor"] = "NA";
			data["FaceEarsMakeupColorPreset"] = "NA";
		}

		// Nose
		if ( _DK_RPG_UMA._Avatar._Face._Nose.Slot != null ) {
			data["FaceNoseSlot"] = _DK_RPG_UMA._Avatar._Face._Nose.Slot.name;
			data["FaceNoseOverlay"] = _DK_RPG_UMA._Avatar._Face._Nose.Overlay.name;
			data["FaceNoseColor"] = _DK_RPG_UMA._Avatar._Face._Nose.Color;
		}
		else {
			data["FaceNoseSlot"] = "NA";
			data["FaceNoseOverlay"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Face._Nose.Tattoo != null ) {
			data["FaceNoseTatoo"] = _DK_RPG_UMA._Avatar._Face._Nose.Tattoo.name;
			data["FaceNoseTatooColor"] = _DK_RPG_UMA._Avatar._Face._Nose.TattooColor;
			data["FaceNoseTatooColorPreset"] = _DK_RPG_UMA._Avatar._Face._Nose.TatooColorPreset.ColorPresetName;
		}
		else {
			data["FaceNoseTatoo"] = "NA";
			data["FaceNoseTatooColor"] = "NA";
			data["FaceNoseTatooColorPreset"] = "NA";
		}
		if ( _DK_RPG_UMA._Avatar._Face._Nose.Makeup != null ) {
			data["FaceNoseMakeup"] = _DK_RPG_UMA._Avatar._Face._Nose.Makeup.name;
			data["FaceNoseMakeupColor"] = _DK_RPG_UMA._Avatar._Face._Nose.MakeupColor;
			data["FaceNoseMakeupColorPreset"] = _DK_RPG_UMA._Avatar._Face._Nose.MakeupColorPreset.ColorPresetName;
		}
		else {
			data["FaceNoseMakeup"] = "NA";
			data["FaceNoseMakeupColor"] = "NA";
			data["FaceNoseMakeupColorPreset"] = "NA";
		}

		// Mouth
		if ( _DK_RPG_UMA._Avatar._Face._Mouth.Slot != null ) {
			data["FaceMouthSlot"] = _DK_RPG_UMA._Avatar._Face._Mouth.Slot.name;
			data["FaceMouthOverlay"] = _DK_RPG_UMA._Avatar._Face._Mouth.Overlay.name;
			data["FaceMouthColor"] = _DK_RPG_UMA._Avatar._Face._Mouth.Color;
		}
		else {
			data["FaceMouthSlot"] = "NA";
			data["FaceMouthOverlay"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Face._Mouth.Tattoo != null ) {
			data["FaceMouthTatoo"] = _DK_RPG_UMA._Avatar._Face._Mouth.Tattoo.name;
			data["FaceMouthTatooColor"] = _DK_RPG_UMA._Avatar._Face._Mouth.TattooColor;
			data["FaceMouthTatooColorPreset"] = _DK_RPG_UMA._Avatar._Face._Mouth.TatooColorPreset.ColorPresetName;
		}
		else {
			data["FaceMouthTatoo"] = "NA";
			data["FaceMouthTatooColor"] = "NA";
			data["FaceMouthTatooColorPreset"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Face._Mouth.Makeup != null ) {
			data["FaceMouthMakeup"] = _DK_RPG_UMA._Avatar._Face._Mouth.Makeup.name;
			data["FaceMouthMakeupColor"] = _DK_RPG_UMA._Avatar._Face._Mouth.MakeupColor;
			data["FaceMouthMakeupColorPreset"] = _DK_RPG_UMA._Avatar._Face._Mouth.MakeupColorPreset.ColorPresetName;
		}
		else {
			data["FaceMouthMakeup"] = "NA";
			data["FaceMouthMakeupColor"] = "NA";
			data["FaceMouthMakeupColorPreset"] = "NA";
		}

		// lips
		if ( _DK_RPG_UMA._Avatar._Face._Mouth.Lips != null ) {
			data["FaceLipsOverlay"] = _DK_RPG_UMA._Avatar._Face._Mouth.Lips.name;
			data["FaceLipsColor"] = _DK_RPG_UMA._Avatar._Face._Mouth.LipsColor;
			if ( _DK_RPG_UMA._Avatar._Face._Mouth.LipsColorPreset != null )
				data["FaceLipsColorPreset"] = _DK_RPG_UMA._Avatar._Face._Mouth.LipsColorPreset.ColorPresetName;
			else data["FaceLipsColorPreset"] = "NA";
		}
		else {
			data["FaceLipsOverlay"] = "NA";
			data["FaceLipsColor"] = "NA";
			data["FaceLipsColorPreset"] = "NA";
		}

		// InnerMouth
		if ( _DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.Slot != null ) {
			data["FaceInnerMouthSlot"] = _DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.Slot.name;
			data["FaceInnerMouthOverlay"] = _DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.Overlay.name;
			data["FaceInnerMouthColor"] = _DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.color;
			data["FaceInnerMouthColorPreset"] = _DK_RPG_UMA._Avatar._Face._Mouth._InnerMouth.ColorPreset.ColorPresetName;
		}
		else {
			data["FaceInnerMouthSlot"] = "NA";
			data["FaceInnerMouthOverlay"] = "NA";
			data["FaceInnerMouthColorPreset"] = "NA";
		}
		#endregion Face Parts

		#region Hair
		// Hair 1
		if ( _DK_RPG_UMA._Avatar._Hair._OverlayOnly.Overlay != null ) {
			data["HairOverlayOnlyOverlay"] = _DK_RPG_UMA._Avatar._Hair._OverlayOnly.Overlay.name;
			data["HairOverlayOnlyColor"] = _DK_RPG_UMA._Avatar._Hair._OverlayOnly.Color;
		}
		else {
			data["HairOverlayOnlyOverlay"] = "NA";
			data["HairOverlayOnlyColor"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Hair._SlotOnly.Slot != null ) {
			data["HairSlotOnlySlot"] = _DK_RPG_UMA._Avatar._Hair._SlotOnly.Slot.name;
			data["HairSlotOnlyOverlay"] = _DK_RPG_UMA._Avatar._Hair._SlotOnly.Overlay.name;
			data["HairSlotOnlyColor"] = _DK_RPG_UMA._Avatar._Hair._SlotOnly.Color;
		}
		else {
			data["HairSlotOnlySlot"] = "NA";
			data["HairSlotOnlyOverlay"] = "NA";
			data["HairSlotOnlyColor"] = "NA";
		}
		// Hair 2
		if ( _DK_RPG_UMA._Avatar._Hair2._OverlayOnly.Overlay != null ) {
			data["HairOverlayOnlyOverlay2"] = _DK_RPG_UMA._Avatar._Hair2._OverlayOnly.Overlay.name;
			data["HairOverlayOnlyColor2"] = _DK_RPG_UMA._Avatar._Hair2._OverlayOnly.Color;
		}
		else {
			data["HairOverlayOnlyOverlay2"] = "NA";
			data["HairOverlayOnlyColor2"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Hair2._SlotOnly.Slot != null ) {
			data["HairSlotOnlySlot2"] = _DK_RPG_UMA._Avatar._Hair2._SlotOnly.Slot.name;
			data["HairSlotOnlyOverlay2"] = _DK_RPG_UMA._Avatar._Hair2._SlotOnly.Overlay.name;
			data["HairSlotOnlyColor2"] = _DK_RPG_UMA._Avatar._Hair2._SlotOnly.Color;
		}
		else {
			data["HairSlotOnlySlot2"] = "NA";
			data["HairSlotOnlyOverlay2"] = "NA";
			data["HairSlotOnlyColor2"] = "NA";
		}
		// Hair 3
		if ( _DK_RPG_UMA._Avatar._Hair3._OverlayOnly.Overlay != null ) {
			data["HairOverlayOnlyOverlay3"] = _DK_RPG_UMA._Avatar._Hair3._OverlayOnly.Overlay.name;
			data["HairOverlayOnlyColor3"] = _DK_RPG_UMA._Avatar._Hair3._OverlayOnly.Color;
		}
		else {
			data["HairOverlayOnlyOverlay3"] = "NA";
			data["HairOverlayOnlyColor3"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Hair3._SlotOnly.Slot != null ) {
			data["HairSlotOnlySlot3"] = _DK_RPG_UMA._Avatar._Hair3._SlotOnly.Slot.name;
			data["HairSlotOnlyOverlay3"] = _DK_RPG_UMA._Avatar._Hair3._SlotOnly.Overlay.name;
			data["HairSlotOnlyColor3"] = _DK_RPG_UMA._Avatar._Hair3._SlotOnly.Color;
		}
		else {
			data["HairSlotOnlySlot3"] = "NA";
			data["HairSlotOnlyOverlay3"] = "NA";
			data["HairSlotOnlyColor3"] = "NA";
		}

		// Hair Module
		if ( _DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Slot != null ) {
			data["HairModuleSlot"] = _DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Slot.name;
			data["HairModuleOverlay"] = _DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Overlay.name;
			data["HairModuleColor"] = _DK_RPG_UMA._Avatar._Hair._SlotOnly._HairModule.Color;
		}
		else {
			data["HairModuleSlot"] = "NA";
			data["HairModuleOverlay"] = "NA";
			data["HairModuleColor"] = "NA";
		}
		#endregion Hair

		#endregion Head

		#region Body
		// Torso
		if ( _DK_RPG_UMA._Avatar._Body._Torso.Slot != null ) {
			data["BodyTorsoSlot"] = _DK_RPG_UMA._Avatar._Body._Torso.Slot.name;
			data["BodyTorsoOverlay"] = _DK_RPG_UMA._Avatar._Body._Torso.Overlay.name;
			data["BodyTorsoColor"] = _DK_RPG_UMA._Avatar._Body._Torso.Color;
		}
		else {
			data["BodyTorsoSlot"] = "NA";
			data["BodyTorsoOverlay"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Body._Torso.Tattoo != null ) {
			data["BodyTorsoTatooOverlay"] = _DK_RPG_UMA._Avatar._Body._Torso.Tattoo.name;
			data["BodyTorsoTatooColor"] = _DK_RPG_UMA._Avatar._Body._Torso.TattooColor;
			data["BodyTorsoTatooColorPreset"] = _DK_RPG_UMA._Avatar._Body._Torso.TatooColorPreset.ColorPresetName;
		}
		else {
			data["BodyTorsoTatooOverlay"] = "NA";
			data["BodyTorsoTatooColor"] = "NA";
			data["BodyTorsoTatooColorPreset"] = "NA";
			
		}
		if ( _DK_RPG_UMA._Avatar._Body._Torso.Makeup != null ) {
			data["BodyTorsoMakeupOverlay"] = _DK_RPG_UMA._Avatar._Body._Torso.Makeup.name;
			data["BodyTorsoMakeupColor"] = _DK_RPG_UMA._Avatar._Body._Torso.MakeupColor;
			data["BodyTorsoMakeupColorPreset"] = _DK_RPG_UMA._Avatar._Body._Torso.MakeupColorPreset.ColorPresetName;
		}
		else {
			data["BodyTorsoMakeupOverlay"] = "NA";
			data["BodyTorsoMakeupColor"] = "NA";
			data["BodyTorsoMakeupColorPreset"] = "NA";
		}

		// Hands
		if ( _DK_RPG_UMA._Avatar._Body._Hands.Slot != null ) {
			data["BodyHandsSlot"] = _DK_RPG_UMA._Avatar._Body._Hands.Slot.name;
			data["BodyHandsOverlay"] = _DK_RPG_UMA._Avatar._Body._Hands.Overlay.name;
			data["BodyHandsColor"] = _DK_RPG_UMA._Avatar._Body._Hands.Color;
		}
		else {
			data["BodyHandsSlot"] = "NA";
			data["BodyHandsOverlay"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Body._Hands.Tattoo != null ) {
			data["BodyHandsTatooOverlay"] = _DK_RPG_UMA._Avatar._Body._Hands.Tattoo.name;
			data["BodyHandsTatooColor"] = _DK_RPG_UMA._Avatar._Body._Hands.TattooColor;
			data["BodyHandsTatooColorPreset"] = _DK_RPG_UMA._Avatar._Body._Hands.TatooColorPreset.ColorPresetName;
		}
		else {
			data["BodyHandsTatooOverlay"] = "NA";
			data["BodyHandsTatooColor"] = "NA";
			data["BodyHandsTatooColorPreset"] = "NA";
			
		}
		if ( _DK_RPG_UMA._Avatar._Body._Hands.Makeup != null ) {
			data["BodyHandsMakeupOverlay"] = _DK_RPG_UMA._Avatar._Body._Hands.Makeup.name;
			data["BodyHandsMakeupColor"] = _DK_RPG_UMA._Avatar._Body._Hands.MakeupColor;
			data["BodyHandsMakeupColorPreset"] = _DK_RPG_UMA._Avatar._Body._Hands.MakeupColorPreset.ColorPresetName;
		}
		else {
			data["BodyHandsMakeupOverlay"] = "NA";
			data["BodyHandsMakeupColor"] = "NA";
			data["BodyHandsMakeupColorPreset"] = "NA";
		}

		// Legs
		// Legs
		if ( _DK_RPG_UMA._Avatar._Body._Legs.Slot != null ) {
			data["BodyLegsSlot"] = _DK_RPG_UMA._Avatar._Body._Legs.Slot.name;
			data["BodyLegsOverlay"] = _DK_RPG_UMA._Avatar._Body._Legs.Overlay.name;
			data["BodyLegsColor"] = _DK_RPG_UMA._Avatar._Body._Legs.Color;
		}
		else {
			data["BodyLegsSlot"] = "NA";
			data["BodyLegsOverlay"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Body._Legs.Tattoo != null ) {
			data["BodyLegsTatooOverlay"] = _DK_RPG_UMA._Avatar._Body._Legs.Tattoo.name;
			data["BodyLegsTatooColor"] = _DK_RPG_UMA._Avatar._Body._Legs.TattooColor;
			data["BodyLegsTatooColorPreset"] = _DK_RPG_UMA._Avatar._Body._Legs.TatooColorPreset.ColorPresetName;
		}
		else {
			data["BodyLegsTatooOverlay"] = "NA";
			data["BodyLegsTatooColor"] = "NA";
			data["BodyLegsTatooColorPreset"] = "NA";
			
		}
		if ( _DK_RPG_UMA._Avatar._Body._Legs.Makeup != null ) {
			data["BodyLegsMakeupOverlay"] = _DK_RPG_UMA._Avatar._Body._Legs.Makeup.name;
			data["BodyLegsMakeupColor"] = _DK_RPG_UMA._Avatar._Body._Legs.MakeupColor;
			data["BodyLegsMakeupColorPreset"] = _DK_RPG_UMA._Avatar._Body._Legs.MakeupColorPreset.ColorPresetName;
		}
		else {
			data["BodyLegsMakeupOverlay"] = "NA";
			data["BodyLegsMakeupColor"] = "NA";
			data["BodyLegsMakeupColorPreset"] = "NA";
		}
		// Feet
		// Feet
		if ( _DK_RPG_UMA._Avatar._Body._Feet.Slot != null ) {
			data["BodyFeetSlot"] = _DK_RPG_UMA._Avatar._Body._Feet.Slot.name;
			data["BodyFeetOverlay"] = _DK_RPG_UMA._Avatar._Body._Feet.Overlay.name;
			data["BodyFeetColor"] = _DK_RPG_UMA._Avatar._Body._Feet.Color;
		}
		else {
			data["BodyFeetSlot"] = "NA";
			data["BodyFeetOverlay"] = "NA";
		}

		if ( _DK_RPG_UMA._Avatar._Body._Feet.Tattoo != null ) {
			data["BodyFeetTatooOverlay"] = _DK_RPG_UMA._Avatar._Body._Feet.Tattoo.name;
			data["BodyFeetTatooColor"] = _DK_RPG_UMA._Avatar._Body._Feet.TattooColor;
			data["BodyFeetTatooColorPreset"] = _DK_RPG_UMA._Avatar._Body._Feet.TatooColorPreset.ColorPresetName;
		}
		else {
			data["BodyFeetTatooOverlay"] = "NA";
			data["BodyFeetTatooColor"] = "NA";
			data["BodyFeetTatooColorPreset"] = "NA";
			
		}
		if ( _DK_RPG_UMA._Avatar._Body._Feet.Makeup != null ) {
			data["BodyFeetMakeupOverlay"] = _DK_RPG_UMA._Avatar._Body._Feet.Makeup.name;
			data["BodyFeetMakeupColor"] = _DK_RPG_UMA._Avatar._Body._Feet.MakeupColor;
			data["BodyFeetMakeupColorPreset"] = _DK_RPG_UMA._Avatar._Body._Feet.MakeupColorPreset.ColorPresetName;
		}
		else {
			data["BodyFeetMakeupOverlay"] = "NA";
			data["BodyFeetMakeupColor"] = "NA";
			data["BodyFeetMakeupColorPreset"] = "NA";
		}
		// Wings
		// Wings
		if ( _DK_RPG_UMA._Avatar._Body._Wings.Slot != null ) {
			data["BodyWingsSlot"] = _DK_RPG_UMA._Avatar._Body._Wings.Slot.name;
			data["BodyWingsOverlay"] = _DK_RPG_UMA._Avatar._Body._Wings.Overlay.name;
			data["BodyWingsColor"] = _DK_RPG_UMA._Avatar._Body._Wings.ColorPreset.ColorPresetName;
		}
		else {
			data["BodyWingsSlot"] = "NA";
			data["BodyWingsOverlay"] = "NA";
			data["BodyWingsColor"] = "NA";
		}
		
	/*	if ( _DK_RPG_UMA._Avatar._Body._Wings.Tattoo != null ) {
			data["BodyWingsTatooOverlay"] = _DK_RPG_UMA._Avatar._Body._Wings.Tattoo.name;
			data["BodyWingsTatooColor"] = _DK_RPG_UMA._Avatar._Body._Wings.TattooColor;
			data["BodyWingsTatooColorPreset"] = _DK_RPG_UMA._Avatar._Body._Wings.TatooColorPreset.ColorPresetName;
		}
		else {
			data["BodyWingsTatooOverlay"] = "NA";
			data["BodyWingsTatooColor"] = "NA";
			data["BodyWingsTatooColorPreset"] = "NA";
			
		}
		if ( _DK_RPG_UMA._Avatar._Body._Wings.Makeup != null ) {
			data["BodyWingsMakeupOverlay"] = _DK_RPG_UMA._Avatar._Body._Wings.Makeup.name;
			data["BodyWingsMakeupColor"] = _DK_RPG_UMA._Avatar._Body._Wings.MakeupColor;
			data["BodyWingsMakeupColorPreset"] = _DK_RPG_UMA._Avatar._Body._Wings.MakeupColorPreset.ColorPresetName;
		}
		else {
			data["BodyWingsMakeupOverlay"] = "NA";
			data["BodyWingsMakeupColor"] = "NA";
			data["BodyWingsMakeupColorPreset"] = "NA";
		}*/
		// Tail
		// Tail
		if ( _DK_RPG_UMA._Avatar._Body._Tail.Slot != null ) {
			data["BodyTailSlot"] = _DK_RPG_UMA._Avatar._Body._Tail.Slot.name;
			data["BodyTailOverlay"] = _DK_RPG_UMA._Avatar._Body._Tail.Overlay.name;
			data["BodyTailColor"] = _DK_RPG_UMA._Avatar._Body._Tail.ColorPreset.ColorPresetName;
		}
		else {
			data["BodyTailSlot"] = "NA";
			data["BodyTailOverlay"] = "NA";
			data["BodyTailColor"] = "NA";
		}
	/*	if ( _DK_RPG_UMA._Avatar._Body._Tail.Tattoo != null ) {
			data["BodyTailTatooOverlay"] = _DK_RPG_UMA._Avatar._Body._Tail.Tattoo.name;
			data["BodyTailTatooColor"] = _DK_RPG_UMA._Avatar._Body._Tail.TattooColor;
			data["BodyTailTatooColorPreset"] = _DK_RPG_UMA._Avatar._Body._Tail.TatooColorPreset.ColorPresetName;
		}
		else {
			data["BodyTailTatooOverlay"] = "NA";
			data["BodyTailTatooColor"] = "NA";
			data["BodyTailTatooColorPreset"] = "NA";
			
		}
		if ( _DK_RPG_UMA._Avatar._Body._Tail.Makeup != null ) {
			data["BodyTailMakeupOverlay"] = _DK_RPG_UMA._Avatar._Body._Tail.Makeup.name;
			data["BodyTailMakeupColor"] = _DK_RPG_UMA._Avatar._Body._Tail.MakeupColor;
			data["BodyTailMakeupColorPreset"] = _DK_RPG_UMA._Avatar._Body._Tail.MakeupColorPreset.ColorPresetName;
		}
		else {
			data["BodyTailMakeupOverlay"] = "NA";
			data["BodyTailMakeupColor"] = "NA";
			data["BodyTailMakeupColorPreset"] = "NA";
		}*/
		#endregion Body

		#region Equipment

		// Underwear
		if ( _DK_RPG_UMA._Avatar._Body._Underwear.Slot ) 
			data["BodyUnderwearSlot"] = _DK_RPG_UMA._Avatar._Body._Underwear.Slot.name;
		else data["BodyUnderwearSlot"] = "NA";
		if ( _DK_RPG_UMA._Avatar._Body._Underwear.Overlay ) 
			data["BodyUnderwearOverlay"] = _DK_RPG_UMA._Avatar._Body._Underwear.Overlay.name;
		else data["BodyUnderwearOverlay"] = "NA";
		data["BodyUnderwearColor"] = _DK_RPG_UMA._Avatar._Body._Underwear.Color;
		if ( _DK_RPG_UMA._Avatar._Body._Underwear.ColorPreset ) data["BodyUnderwearColorPreset"] = _DK_RPG_UMA._Avatar._Body._Underwear.ColorPreset.ColorPresetName;
		else data["BodyUnderwearColorPreset"] = "NA";
		// Add Equipment

		// Glasses
		if ( _DK_RPG_UMA._Equipment._Glasses.Slot != null ) {
			data["EquipGlassesSlot"] = _DK_RPG_UMA._Equipment._Glasses.Slot.name;
			data["EquipGlassesOverlay"] = _DK_RPG_UMA._Equipment._Glasses.Overlay.name;
			data["EquipGlassesColor"] = _DK_RPG_UMA._Equipment._Glasses.Color;
			if ( _DK_RPG_UMA._Equipment._Glasses.ColorPreset ) 
				data["EquipGlassesColorPreset"] = _DK_RPG_UMA._Equipment._Glasses.ColorPreset.ColorPresetName;
			else data["EquipGlassesColorPreset"] = "NA";
		}
		else {
			data["EquipGlassesSlot"] = "NA";
			data["EquipGlassesOverlay"] = "NA";
			data["EquipGlassesColor"] = "NA";
			data["EquipGlassesColorPreset"] = "NA";
		}

		// Mask
		if ( _DK_RPG_UMA._Equipment._Mask.Slot != null ) {
			data["EquipMaskSlot"] = _DK_RPG_UMA._Equipment._Mask.Slot.name;
			data["EquipMaskOverlay"] = _DK_RPG_UMA._Equipment._Mask.Overlay.name;
			data["EquipMaskColor"] = _DK_RPG_UMA._Equipment._Mask.Color;
			if ( _DK_RPG_UMA._Equipment._Mask.ColorPreset ) 
				data["EquipMaskColorPreset"] = _DK_RPG_UMA._Equipment._Mask.ColorPreset.ColorPresetName;
			else data["EquipMaskColorPreset"] = "NA";
		}
		else {
			data["EquipMaskSlot"] = "NA";
			data["EquipMaskOverlay"] = "NA";
			data["EquipMaskColor"] = "NA";
			data["EquipMaskColorPreset"] = "NA";

		}

		#region Head Wear
		//Head Sub
		if ( _DK_RPG_UMA._Equipment._HeadSub.Slot != null ) {
			data["EquipHeadSubSlot"] = _DK_RPG_UMA._Equipment._HeadSub.Slot.name;
			data["EquipHeadSubColor"] = _DK_RPG_UMA._Equipment._HeadSub.Color;
			if ( _DK_RPG_UMA._Equipment._HeadSub.ColorPreset ) data["EquipHeadSubColorPreset"] = _DK_RPG_UMA._Equipment._HeadSub.ColorPreset.ColorPresetName;
			else data["EquipHeadSubColorPreset"] = "NA";
		}
		else {
			data["EquipHeadSubSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._HeadSub.Overlay != null ) {
			data["EquipHeadSubOverlay"] = _DK_RPG_UMA._Equipment._HeadSub.Overlay.name;
			data["EquipHeadSubColor"] = _DK_RPG_UMA._Equipment._HeadSub.Color;
			if ( _DK_RPG_UMA._Equipment._HeadSub.ColorPreset ) data["EquipHeadSubColorPreset"] = _DK_RPG_UMA._Equipment._HeadSub.ColorPreset.ColorPresetName;
			else data["EquipHeadSubColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._HeadSub.Opt01Color ) data["EquipHeadSubOpt01"] = _DK_RPG_UMA._Equipment._HeadSub.Opt01Color.ColorPresetName;
			else data["EquipHeadSubOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._HeadSub.Opt02Color ) data["EquipHeadSubOpt02"] = _DK_RPG_UMA._Equipment._HeadSub.Opt02Color.ColorPresetName;
			else data["EquipHeadSubOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._HeadSub.Dirt01Color ) data["EquipHeadSubDirt01"] = _DK_RPG_UMA._Equipment._HeadSub.Dirt01Color.ColorPresetName;
			else data["EquipHeadSubDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._HeadSub.Dirt02Color ) data["EquipHeadSubDirt02"] = _DK_RPG_UMA._Equipment._HeadSub.Dirt02Color.ColorPresetName;
			else data["EquipHeadSubDirt02"] = "NA";
		}
		else {
			data["EquipHeadSubOverlay"] = "NA";
			data["EquipHeadSubColor"] = "NA";
			data["EquipHeadSubColorPreset"] = "NA";
			data["EquipHeadSubOpt01"] = "NA";
			data["EquipHeadSubOpt02"] = "NA";
			data["EquipHeadSubDirt01"] = "NA";
			data["EquipHeadSubDirt02"] = "NA";
		}

		// Head
		if ( _DK_RPG_UMA._Equipment._Head.Slot != null ) {
			data["EquipHeadSlot"] = _DK_RPG_UMA._Equipment._Head.Slot.name;
			data["EquipHeadColor"] = _DK_RPG_UMA._Equipment._Head.Color;
			if ( _DK_RPG_UMA._Equipment._Head.ColorPreset ) data["EquipHeadColorPreset"] = _DK_RPG_UMA._Equipment._Head.ColorPreset.ColorPresetName;
			else data["EquipHeadColorPreset"] = "NA";
		}
		else {
			data["EquipHeadSlot"] = "NA";

		}
		if ( _DK_RPG_UMA._Equipment._Head.Overlay != null ) {
			data["EquipHeadOverlay"] = _DK_RPG_UMA._Equipment._Head.Overlay.name;
			data["EquipHeadColor"] = _DK_RPG_UMA._Equipment._Head.Color;
			if ( _DK_RPG_UMA._Equipment._Head.ColorPreset ) data["EquipHeadColorPreset"] = _DK_RPG_UMA._Equipment._Head.ColorPreset.ColorPresetName;
			else data["EquipHeadColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Head.Opt01Color ) data["EquipHeadOpt01"] = _DK_RPG_UMA._Equipment._Head.Opt01Color.ColorPresetName;
			else data["EquipHeadOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Head.Opt02Color ) data["EquipHeadOpt02"] = _DK_RPG_UMA._Equipment._Head.Opt02Color.ColorPresetName;
			else data["EquipHeadOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._Head.Dirt01Color ) data["EquipHeadDirt01"] = _DK_RPG_UMA._Equipment._Head.Dirt01Color.ColorPresetName;
			else data["EquipHeadDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Head.Dirt02Color ) data["EquipHeadDirt02"] = _DK_RPG_UMA._Equipment._Head.Dirt02Color.ColorPresetName;
			else data["EquipHeadDirt02"] = "NA";
		}
		else {
			data["EquipHeadOverlay"] = "NA";
			data["EquipHeadColor"] = "NA";
			data["EquipHeadColorPreset"] = "NA";
			data["EquipHeadOpt01"] = "NA";
			data["EquipHeadOpt02"] = "NA";
			data["EquipHeadDirt01"] = "NA";
			data["EquipHeadDirt02"] = "NA";
		}

		// Head Cover
		if ( _DK_RPG_UMA._Equipment._HeadCover.Slot != null ) {
			data["EquipHeadCoverSlot"] = _DK_RPG_UMA._Equipment._HeadCover.Slot.name;
			data["EquipHeadCoverColor"] = _DK_RPG_UMA._Equipment._HeadCover.Color;
			if ( _DK_RPG_UMA._Equipment._HeadCover.ColorPreset ) data["EquipHeadCoverColorPreset"] = _DK_RPG_UMA._Equipment._HeadCover.ColorPreset.ColorPresetName;
			else data["EquipHeadCoverColorPreset"] = "NA";
		}
		else {
			data["EquipHeadCoverSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._HeadCover.Overlay != null ) {
			data["EquipHeadCoverOverlay"] = _DK_RPG_UMA._Equipment._HeadCover.Overlay.name;
			data["EquipHeadCoverColor"] = _DK_RPG_UMA._Equipment._HeadCover.Color;
			if ( _DK_RPG_UMA._Equipment._HeadCover.ColorPreset ) data["EquipHeadCoverColorPreset"] = _DK_RPG_UMA._Equipment._HeadCover.ColorPreset.ColorPresetName;
			else data["EquipHeadCoverColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._HeadCover.Opt01Color ) data["EquipHeadCoverOpt01"] = _DK_RPG_UMA._Equipment._HeadCover.Opt01Color.ColorPresetName;
			else data["EquipHeadCoverOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._HeadCover.Opt02Color ) data["EquipHeadCoverOpt02"] = _DK_RPG_UMA._Equipment._HeadCover.Opt02Color.ColorPresetName;
			else data["EquipHeadCoverOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._HeadCover.Dirt01Color ) data["EquipHeadCoverDirt01"] = _DK_RPG_UMA._Equipment._HeadCover.Dirt01Color.ColorPresetName;
			else data["EquipHeadCoverDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._HeadCover.Dirt02Color ) data["EquipHeadCoverDirt02"] = _DK_RPG_UMA._Equipment._HeadCover.Dirt02Color.ColorPresetName;
			else data["EquipHeadCoverDirt02"] = "NA";
		}
		else {
			data["EquipHeadCoverOverlay"] = "NA";
			data["EquipHeadCoverColor"] = "NA";
			data["EquipHeadCoverColorPreset"] = "NA";
			data["EquipHeadCoverOpt01"] = "NA";
			data["EquipHeadCoverOpt02"] = "NA";
			data["EquipHeadCoverDirt01"] = "NA";
			data["EquipHeadCoverDirt02"] = "NA";
		}
		#endregion Head Wear

		#region Shoulder Wear
		//Shoulder Sub
		if ( _DK_RPG_UMA._Equipment._ShoulderSub.Slot != null ) {
			data["EquipShoulderSubSlot"] = _DK_RPG_UMA._Equipment._ShoulderSub.Slot.name;
			data["EquipShoulderSubColor"] = _DK_RPG_UMA._Equipment._ShoulderSub.Color;
			if ( _DK_RPG_UMA._Equipment._ShoulderSub.ColorPreset ) data["EquipShoulderSubColorPreset"] = _DK_RPG_UMA._Equipment._ShoulderSub.ColorPreset.ColorPresetName;
			else data["EquipShoulderSubColorPreset"] = "NA";
		}
		else {
			data["EquipShoulderSubSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._ShoulderSub.Overlay != null ) {
			data["EquipShoulderSubOverlay"] = _DK_RPG_UMA._Equipment._ShoulderSub.Overlay.name;
			data["EquipShoulderSubColor"] = _DK_RPG_UMA._Equipment._ShoulderSub.Color;
			if ( _DK_RPG_UMA._Equipment._ShoulderSub.ColorPreset ) data["EquipShoulderSubColorPreset"] = _DK_RPG_UMA._Equipment._ShoulderSub.ColorPreset.ColorPresetName;
			else data["EquipShoulderSubColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ShoulderSub.Opt01Color ) data["EquipShoulderSubOpt01"] = _DK_RPG_UMA._Equipment._ShoulderSub.Opt01Color.ColorPresetName;
			else data["EquipShoulderSubOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ShoulderSub.Opt02Color ) data["EquipShoulderSubOpt02"] = _DK_RPG_UMA._Equipment._ShoulderSub.Opt02Color.ColorPresetName;
			else data["EquipShoulderSubOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._ShoulderSub.Dirt01Color ) data["EquipShoulderSubDirt01"] = _DK_RPG_UMA._Equipment._ShoulderSub.Dirt01Color.ColorPresetName;
			else data["EquipShoulderSubDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ShoulderSub.Dirt02Color ) data["EquipShoulderSubDirt02"] = _DK_RPG_UMA._Equipment._ShoulderSub.Dirt02Color.ColorPresetName;
			else data["EquipShoulderSubDirt02"] = "NA";
		}
		else {
			data["EquipShoulderSubOverlay"] = "NA";
			data["EquipShoulderSubColor"] = "NA";
			data["EquipShoulderSubColorPreset"] = "NA";
			data["EquipShoulderSubOpt01"] = "NA";
			data["EquipShoulderSubOpt02"] = "NA";
			data["EquipShoulderSubDirt01"] = "NA";
			data["EquiShoulderSubDirt02"] = "NA";
		}

		//Shoulder
		if ( _DK_RPG_UMA._Equipment._Shoulder.Slot != null ) {
			data["EquipShoulderSlot"] = _DK_RPG_UMA._Equipment._Shoulder.Slot.name;
			data["EquipShoulderColor"] = _DK_RPG_UMA._Equipment._Shoulder.Color;
			if ( _DK_RPG_UMA._Equipment._Shoulder.ColorPreset ) data["EquipShoulderColorPreset"] = _DK_RPG_UMA._Equipment._Shoulder.ColorPreset.ColorPresetName;
			else data["EquipShoulderColorPreset"] = "NA";
		}
		else {
			data["EquipShoulderSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._Shoulder.Overlay != null ) {
			data["EquipShoulderOverlay"] = _DK_RPG_UMA._Equipment._Shoulder.Overlay.name;
			data["EquipShoulderColor"] = _DK_RPG_UMA._Equipment._Shoulder.Color;
			if ( _DK_RPG_UMA._Equipment._Shoulder.ColorPreset ) data["EquipShoulderColorPreset"] = _DK_RPG_UMA._Equipment._Shoulder.ColorPreset.ColorPresetName;
			else data["EquipShoulderColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Shoulder.Opt01Color ) data["EquipShoulderOpt01"] = _DK_RPG_UMA._Equipment._Shoulder.Opt01Color.ColorPresetName;
			else data["EquipShoulderOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Shoulder.Opt02Color ) data["EquipShoulderOpt02"] = _DK_RPG_UMA._Equipment._Shoulder.Opt02Color.ColorPresetName;
			else data["EquipShoulderOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._Shoulder.Dirt01Color ) data["EquipShoulderDirt01"] = _DK_RPG_UMA._Equipment._Shoulder.Dirt01Color.ColorPresetName;
			else data["EquipShoulderDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Shoulder.Dirt02Color ) data["EquipShoulderDirt02"] = _DK_RPG_UMA._Equipment._Shoulder.Dirt02Color.ColorPresetName;
			else data["EquipShoulderDirt02"] = "NA";
		}
		else {
			data["EquipShoulderOverlay"] = "NA";
			data["EquipShoulderColor"] = "NA";
			data["EquipShoulderColorPreset"] = "NA";
			data["EquipShoulderOpt01"] = "NA";
			data["EquipShoulderOpt02"] = "NA";
			data["EquipShoulderDirt01"] = "NA";
			data["EquipShoulderDirt02"] = "NA";
		}

		//Shoulder Cover
		if ( _DK_RPG_UMA._Equipment._ShoulderCover.Slot != null ) {
			data["EquipShoulderCoverSlot"] = _DK_RPG_UMA._Equipment._ShoulderCover.Slot.name;
			data["EquipShoulderCoverColor"] = _DK_RPG_UMA._Equipment._ShoulderCover.Color;
			if ( _DK_RPG_UMA._Equipment._ShoulderCover.ColorPreset ) data["EquipShoulderCoverColorPreset"] = _DK_RPG_UMA._Equipment._ShoulderCover.ColorPreset.ColorPresetName;
			else data["EquipShoulderCoverColorPreset"] = "NA";
		}
		else {
			data["EquipShoulderCoverSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._ShoulderCover.Overlay != null ) {
			data["EquipShoulderCoverOverlay"] = _DK_RPG_UMA._Equipment._ShoulderCover.Overlay.name;
			data["EquipShoulderCoverColor"] = _DK_RPG_UMA._Equipment._ShoulderCover.Color;
			if ( _DK_RPG_UMA._Equipment._ShoulderCover.ColorPreset ) data["EquipShoulderCoverColorPreset"] = _DK_RPG_UMA._Equipment._ShoulderCover.ColorPreset.ColorPresetName;
			else data["EquipShoulderCoverColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ShoulderCover.Opt01Color ) data["EquipShoulderCoverOpt01"] = _DK_RPG_UMA._Equipment._ShoulderCover.Opt01Color.ColorPresetName;
			else data["EquipShoulderCoverOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ShoulderCover.Opt02Color ) data["EquipShoulderCoverOpt02"] = _DK_RPG_UMA._Equipment._ShoulderCover.Opt02Color.ColorPresetName;
			else data["EquipShoulderCoverOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._ShoulderCover.Dirt01Color ) data["EquipShoulderCoverDirt01"] = _DK_RPG_UMA._Equipment._ShoulderCover.Dirt01Color.ColorPresetName;
			else data["EquipShoulderCoverDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ShoulderCover.Dirt02Color ) data["EquipShoulderCoverDirt02"] = _DK_RPG_UMA._Equipment._ShoulderCover.Dirt02Color.ColorPresetName;
			else data["EquipShoulderCoverDirt02"] = "NA";
		}
		else {
			data["EquipShoulderCoverOverlay"] = "NA";
			data["EquipShoulderCoverColor"] = "NA";
			data["EquipShoulderCoverColorPreset"] = "NA";
			data["EquipShoulderCoverOpt01"] = "NA";
			data["EquipShoulderCoverOpt02"] = "NA";
			data["EquipShoulderCoverDirt01"] = "NA";
			data["EquipShoulderCoverDirt02"] = "NA";
		}
		#endregion Shoulder Wear

		#region Torso Wear
		//Torso Sub
		if ( _DK_RPG_UMA._Equipment._TorsoSub.Slot != null ) {
			data["EquipTorsoSubSlot"] = _DK_RPG_UMA._Equipment._TorsoSub.Slot.name;
			data["EquipTorsoSubColor"] = _DK_RPG_UMA._Equipment._TorsoSub.Color;
			if ( _DK_RPG_UMA._Equipment._Torso.ColorPreset ) data["EquipTorsoSubColorPreset"] = _DK_RPG_UMA._Equipment._TorsoSub.ColorPreset.ColorPresetName;
			else data["EquipTorsoSubColorPreset"] = "NA";
		}
		else {
			data["EquipTorsoSubSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._TorsoSub.Overlay != null ) {
			data["EquipTorsoSubOverlay"] = _DK_RPG_UMA._Equipment._TorsoSub.Overlay.name;
			data["EquipTorsoSubColor"] = _DK_RPG_UMA._Equipment._TorsoSub.Color;
			if ( _DK_RPG_UMA._Equipment._TorsoSub.ColorPreset ) data["EquipTorsoSubColorPreset"] = _DK_RPG_UMA._Equipment._TorsoSub.ColorPreset.ColorPresetName;
			else data["EquipTorsoSubColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._TorsoSub.Opt01Color ) data["EquipTorsoSubOpt01"] = _DK_RPG_UMA._Equipment._TorsoSub.Opt01Color.ColorPresetName;
			else data["EquipSTorsoSubOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._TorsoSub.Opt02Color ) data["EquipTorsoSubOpt02"] = _DK_RPG_UMA._Equipment._TorsoSub.Opt02Color.ColorPresetName;
			else data["EquipTorsoSubOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._TorsoSub.Dirt01Color ) data["EquipTorsoSubDirt01"] = _DK_RPG_UMA._Equipment._TorsoSub.Dirt01Color.ColorPresetName;
			else data["EquipTorsoSubDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._TorsoSub.Dirt02Color ) data["EquipTorsoSubDirt02"] = _DK_RPG_UMA._Equipment._TorsoSub.Dirt02Color.ColorPresetName;
			else data["EquipTorsoSubDirt02"] = "NA";
		}
		else {
			data["EquipTorsoSubOverlay"] = "NA";
			data["EquipTorsoSubColor"] = "NA";
			data["EquipTorsoSubColorPreset"] = "NA";
			data["EquipTorsoSubOpt01"] = "NA";
			data["EquipTorsoSubOpt02"] = "NA";
			data["EquipTorsoSubDirt01"] = "NA";
			data["EquipTorsoSubDirt02"] = "NA";
		}

		//Torso
		if ( _DK_RPG_UMA._Equipment._Torso.Slot != null ) {
			data["EquipTorsoSlot"] = _DK_RPG_UMA._Equipment._Torso.Slot.name;
			data["EquipTorsoColor"] = _DK_RPG_UMA._Equipment._Torso.Color;
			if ( _DK_RPG_UMA._Equipment._Torso.ColorPreset ) data["EquipTorsoColorPreset"] = _DK_RPG_UMA._Equipment._Torso.ColorPreset.ColorPresetName;
			else data["EquipTorsoColorPreset"] = "NA";
		}
		else {
			data["EquipTorsoSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._Torso.Overlay != null ) {
			data["EquipTorsoOverlay"] = _DK_RPG_UMA._Equipment._Torso.Overlay.name;
			data["EquipTorsoColor"] = _DK_RPG_UMA._Equipment._Torso.Color;
			if ( _DK_RPG_UMA._Equipment._Torso.ColorPreset ) data["EquipTorsoColorPreset"] = _DK_RPG_UMA._Equipment._Torso.ColorPreset.ColorPresetName;
			else data["EquipTorsoColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Torso.Opt01Color ) data["EquipTorsoOpt01"] = _DK_RPG_UMA._Equipment._Torso.Opt01Color.ColorPresetName;
			else data["EquipSTorsoOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Torso.Opt02Color ) data["EquipTorsoOpt02"] = _DK_RPG_UMA._Equipment._Torso.Opt02Color.ColorPresetName;
			else data["EquipTorsoOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._Torso.Dirt01Color ) data["EquipTorsoDirt01"] = _DK_RPG_UMA._Equipment._Torso.Dirt01Color.ColorPresetName;
			else data["EquipTorsoDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Torso.Dirt02Color ) data["EquipTorsoDirt02"] = _DK_RPG_UMA._Equipment._Torso.Dirt02Color.ColorPresetName;
			else data["EquipTorsoDirt02"] = "NA";
		}
		else {
			data["EquipTorsoOverlay"] = "NA";
			data["EquipTorsoColor"] = "NA";
			data["EquipTorsoColorPreset"] = "NA";
			data["EquipTorsoOpt01"] = "NA";
			data["EquipTorsoOpt02"] = "NA";
			data["EquipTorsoDirt01"] = "NA";
			data["EquipTorsoDirt02"] = "NA";
		}

		//Torso Cover
		if ( _DK_RPG_UMA._Equipment._TorsoCover.Slot != null ) {
			data["EquipTorsoCoverSlot"] = _DK_RPG_UMA._Equipment._TorsoCover.Slot.name;
			data["EquipTorsoCoverColor"] = _DK_RPG_UMA._Equipment._TorsoCover.Color;
			if ( _DK_RPG_UMA._Equipment._TorsoCover.ColorPreset ) data["EquipTorsoCoverColorPreset"] = _DK_RPG_UMA._Equipment._TorsoCover.ColorPreset.ColorPresetName;
			else data["EquipTorsoCoverColorPreset"] = "NA";
		}
		else {
			data["EquipTorsoCoverSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._TorsoCover.Overlay != null ) {
			data["EquipTorsoCoverOverlay"] = _DK_RPG_UMA._Equipment._TorsoCover.Overlay.name;
			data["EquipTorsoCoverColor"] = _DK_RPG_UMA._Equipment._TorsoCover.Color;
			if ( _DK_RPG_UMA._Equipment._TorsoCover.ColorPreset ) data["EquipTorsoCoverColorPreset"] = _DK_RPG_UMA._Equipment._TorsoCover.ColorPreset.ColorPresetName;
			else data["EquipTorsoCoverColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._TorsoCover.Opt01Color ) data["EquipTorsoCoverOpt01"] = _DK_RPG_UMA._Equipment._TorsoCover.Opt01Color.ColorPresetName;
			else data["EquipSTorsoCoverOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._TorsoCover.Opt02Color ) data["EquipTorsoCoverOpt02"] = _DK_RPG_UMA._Equipment._TorsoCover.Opt02Color.ColorPresetName;
			else data["EquipTorsoCoverOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._TorsoCover.Dirt01Color ) data["EquipTorsoCoverDirt01"] = _DK_RPG_UMA._Equipment._TorsoCover.Dirt01Color.ColorPresetName;
			else data["EquipTorsoCoverDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._TorsoCover.Dirt02Color ) data["EquipTorsoCoverDirt02"] = _DK_RPG_UMA._Equipment._TorsoCover.Dirt02Color.ColorPresetName;
			else data["EquipTorsoCoverDirt02"] = "NA";
		}
		else {
			data["EquipTorsoCoverOverlay"] = "NA";
			data["EquipTorsoCoverColor"] = "NA";
			data["EquipTorsoCoverColorPreset"] = "NA";
			data["EquipTorsoCoverOpt01"] = "NA";
			data["EquipTorsoCoverOpt02"] = "NA";
			data["EquipTorsoCoverDirt01"] = "NA";
			data["EquipTorsoCoverDirt02"] = "NA";
		}
		#endregion Torso Wear

		#region Armband Wear
		//ArmBand Sub
		if ( _DK_RPG_UMA._Equipment._ArmBandSub.Slot != null ) {
			data["EquipArmBandSubSlot"] = _DK_RPG_UMA._Equipment._ArmBandSub.Slot.name;
			data["EquipArmBandSubColor"] = _DK_RPG_UMA._Equipment._ArmBandSub.Color;
			if ( _DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset ) data["EquipArmBandSubColorPreset"] = _DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset.ColorPresetName;
			else data["EquipArmBandSubColorPreset"] = "NA";
		}
		else {
			data["EquipArmBandSubSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._ArmBandSub.Overlay != null ) {
			data["EquipArmBandSubOverlay"] = _DK_RPG_UMA._Equipment._ArmBandSub.Overlay.name;
			data["EquipArmBandSubColor"] = _DK_RPG_UMA._Equipment._ArmBandSub.Color;
			if ( _DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset ) data["EquipArmBandSubColorPreset"] = _DK_RPG_UMA._Equipment._ArmBandSub.ColorPreset.ColorPresetName;
			else data["EquipArmBandSubColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ArmBandSub.Opt01Color ) data["EquipArmBandSubOpt01"] = _DK_RPG_UMA._Equipment._ArmBandSub.Opt01Color.ColorPresetName;
			else data["EquipArmBandSubOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ArmBandSub.Opt02Color ) data["EquipArmBandSubOpt02"] = _DK_RPG_UMA._Equipment._ArmBandSub.Opt02Color.ColorPresetName;
			else data["EquipArmBandSubOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._ArmBandSub.Dirt01Color ) data["EquipArmBandSubDirt01"] = _DK_RPG_UMA._Equipment._ArmBandSub.Dirt01Color.ColorPresetName;
			else data["EquipArmBandSubDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ArmBandSub.Dirt02Color ) data["EquipArmBandSubDirt02"] = _DK_RPG_UMA._Equipment._ArmBandSub.Dirt02Color.ColorPresetName;
			else data["EquipArmBandSubDirt02"] = "NA";
		}
		else {
			data["EquipArmBandSubOverlay"] = "NA";
			data["EquipArmBandSubColor"] = "NA";
			data["EquipArmBandSubColorPreset"] = "NA";
			data["EquipArmBandSubOpt01"] = "NA";
			data["EquipArmBandSubOpt02"] = "NA";
			data["EquipArmBandSubDirt01"] = "NA";
			data["EquipArmBandSubDirt02"] = "NA";
		}

		//ArmBand
		if ( _DK_RPG_UMA._Equipment._ArmBand.Slot != null ) {
			data["EquipArmBandSlot"] = _DK_RPG_UMA._Equipment._ArmBand.Slot.name;
			data["EquipArmBandColor"] = _DK_RPG_UMA._Equipment._ArmBand.Color;
			if ( _DK_RPG_UMA._Equipment._ArmBand.ColorPreset ) data["EquipArmBandColorPreset"] = _DK_RPG_UMA._Equipment._ArmBand.ColorPreset.ColorPresetName;
			else data["EquipArmBandColorPreset"] = "NA";
		}
		else {
			data["EquipArmBandSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._ArmBand.Overlay != null ) {
			data["EquipArmBandOverlay"] = _DK_RPG_UMA._Equipment._ArmBand.Overlay.name;
			data["EquipArmBandColor"] = _DK_RPG_UMA._Equipment._ArmBand.Color;
			if ( _DK_RPG_UMA._Equipment._ArmBand.ColorPreset ) data["EquipArmBandColorPreset"] = _DK_RPG_UMA._Equipment._ArmBand.ColorPreset.ColorPresetName;
			else data["EquipArmBandColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ArmBand.Opt01Color ) data["EquipArmBandOpt01"] = _DK_RPG_UMA._Equipment._ArmBand.Opt01Color.ColorPresetName;
			else data["EquipArmBandOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ArmBand.Opt02Color ) data["EquipArmBandOpt02"] = _DK_RPG_UMA._Equipment._ArmBand.Opt02Color.ColorPresetName;
			else data["EquipArmBandOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._ArmBand.Dirt01Color ) data["EquipArmBandDirt01"] = _DK_RPG_UMA._Equipment._ArmBand.Dirt01Color.ColorPresetName;
			else data["EquipArmBandDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ArmBand.Dirt02Color ) data["EquipArmBandDirt02"] = _DK_RPG_UMA._Equipment._ArmBand.Dirt02Color.ColorPresetName;
			else data["EquipArmBandDirt02"] = "NA";
		}
		else {
			data["EquipArmBandOverlay"] = "NA";
			data["EquipArmBandColor"] = "NA";
			data["EquipArmBandColorPreset"] = "NA";
			data["EquipArmBandOpt01"] = "NA";
			data["EquipArmBandOpt02"] = "NA";
			data["EquipArmBandDirt01"] = "NA";
			data["EquipArmBandDirt02"] = "NA";
		}

		//ArmBand Cover
		if ( _DK_RPG_UMA._Equipment._ArmBandCover.Slot != null ) {
			data["EquipArmBandCoverSlot"] = _DK_RPG_UMA._Equipment._ArmBandCover.Slot.name;
			data["EquipArmBandCoverColor"] = _DK_RPG_UMA._Equipment._ArmBandCover.Color;
			if ( _DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset ) data["EquipArmBandCoverColorPreset"] = _DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset.ColorPresetName;
			else data["EquipArmBandCoverColorPreset"] = "NA";
		}
		else {
			data["EquipArmBandCoverSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._ArmBandCover.Overlay != null ) {
			data["EquipArmBandCoverOverlay"] = _DK_RPG_UMA._Equipment._ArmBandCover.Overlay.name;
			data["EquipArmBandCoverColor"] = _DK_RPG_UMA._Equipment._ArmBandCover.Color;
			if ( _DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset ) data["EquipArmBandCoverColorPreset"] = _DK_RPG_UMA._Equipment._ArmBandCover.ColorPreset.ColorPresetName;
			else data["EquipArmBandCoverColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ArmBandCover.Opt01Color ) data["EquipArmBandCoverOpt01"] = _DK_RPG_UMA._Equipment._ArmBandCover.Opt01Color.ColorPresetName;
			else data["EquipArmBandCoverOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ArmBandCover.Opt02Color ) data["EquipArmBandCoverOpt02"] = _DK_RPG_UMA._Equipment._ArmBandCover.Opt02Color.ColorPresetName;
			else data["EquipArmBandCoverOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._ArmBandCover.Dirt01Color ) data["EquipArmBandCoverDirt01"] = _DK_RPG_UMA._Equipment._ArmBandCover.Dirt01Color.ColorPresetName;
			else data["EquipArmBandCoverDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._ArmBandCover.Dirt02Color ) data["EquipArmBandCoverDirt02"] = _DK_RPG_UMA._Equipment._ArmBandCover.Dirt02Color.ColorPresetName;
			else data["EquipArmBandCoverDirt02"] = "NA";
		}
		else {
			data["EquipArmBandCoverOverlay"] = "NA";
			data["EquipArmBandCoverColor"] = "NA";
			data["EquipArmBandCoverColorPreset"] = "NA";
			data["EquipArmBandCoverOpt01"] = "NA";
			data["EquipArmBandCoverOpt02"] = "NA";
			data["EquipArmBandCoverDirt01"] = "NA";
			data["EquipArmBandCoverDirt02"] = "NA";
		}
		#endregion Armband Wear

		#region Wrist Wear
		//Wrist Sub
		if ( _DK_RPG_UMA._Equipment._WristSub.Slot != null ) {
			data["EquipWristSubSlot"] = _DK_RPG_UMA._Equipment._WristSub.Slot.name;
			data["EquipWristSubColor"] = _DK_RPG_UMA._Equipment._WristSub.Color;
			if ( _DK_RPG_UMA._Equipment._WristSub.ColorPreset ) data["EquipWristSubColorPreset"] = _DK_RPG_UMA._Equipment._WristSub.ColorPreset.ColorPresetName;
			else data["EquipWristSubColorPreset"] = "NA";
		}
		else {
			data["EquipWristSubSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._WristSub.Overlay != null ) {
			data["EquipWristSubOverlay"] = _DK_RPG_UMA._Equipment._WristSub.Overlay.name;
			data["EquipWristSubColor"] = _DK_RPG_UMA._Equipment._WristSub.Color;
			if ( _DK_RPG_UMA._Equipment._WristSub.ColorPreset ) data["EquipWristSubColorPreset"] = _DK_RPG_UMA._Equipment._WristSub.ColorPreset.ColorPresetName;
			else data["EquipWristSubColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._WristSub.Opt01Color ) data["EquipWristSubOpt01"] = _DK_RPG_UMA._Equipment._WristSub.Opt01Color.ColorPresetName;
			else data["EquipWristSubOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._WristSub.Opt02Color ) data["EquipWristSubOpt02"] = _DK_RPG_UMA._Equipment._WristSub.Opt02Color.ColorPresetName;
			else data["EquipWristSubOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._WristSub.Dirt01Color ) data["EquipWristSubDirt01"] = _DK_RPG_UMA._Equipment._WristSub.Dirt01Color.ColorPresetName;
			else data["EquipWristSubDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._WristSub.Dirt02Color ) data["EquipWristSubDirt02"] = _DK_RPG_UMA._Equipment._WristSub.Dirt02Color.ColorPresetName;
			else data["EquipWristSubDirt02"] = "NA";
		}
		else {
			data["EquipWristSubOverlay"] = "NA";
			data["EquipWristSubColor"] = "NA";
			data["EquipWristSubColorPreset"] = "NA";
			data["EquipWristSubOpt01"] = "NA";
			data["EquipWristSubOpt02"] = "NA";
			data["EquipWristSubDirt01"] = "NA";
			data["EquipWristSubDirt02"] = "NA";
		}

		//Wrist
		if ( _DK_RPG_UMA._Equipment._Wrist.Slot != null ) {
			data["EquipWristSlot"] = _DK_RPG_UMA._Equipment._Wrist.Slot.name;
			data["EquipWristColor"] = _DK_RPG_UMA._Equipment._Wrist.Color;
			if ( _DK_RPG_UMA._Equipment._Wrist.ColorPreset ) data["EquipWristColorPreset"] = _DK_RPG_UMA._Equipment._Wrist.ColorPreset.ColorPresetName;
			else data["EquipWristColorPreset"] = "NA";
		}
		else {
			data["EquipWristSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._Wrist.Overlay != null ) {
			data["EquipWristOverlay"] = _DK_RPG_UMA._Equipment._Wrist.Overlay.name;
			data["EquipWristColor"] = _DK_RPG_UMA._Equipment._Wrist.Color;
			if ( _DK_RPG_UMA._Equipment._Wrist.ColorPreset ) data["EquipWristColorPreset"] = _DK_RPG_UMA._Equipment._Wrist.ColorPreset.ColorPresetName;
			else data["EquipWristColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Wrist.Opt01Color ) data["EquipWristOpt01"] = _DK_RPG_UMA._Equipment._Wrist.Opt01Color.ColorPresetName;
			else data["EquipWristOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Wrist.Opt02Color ) data["EquipWristOpt02"] = _DK_RPG_UMA._Equipment._Wrist.Opt02Color.ColorPresetName;
			else data["EquipWristOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._Wrist.Dirt01Color ) data["EquipWristDirt01"] = _DK_RPG_UMA._Equipment._Wrist.Dirt01Color.ColorPresetName;
			else data["EquipWristDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Wrist.Dirt02Color ) data["EquipWristDirt02"] = _DK_RPG_UMA._Equipment._Wrist.Dirt02Color.ColorPresetName;
			else data["EquipWristDirt02"] = "NA";
		}
		else {
			data["EquipWristOverlay"] = "NA";
			data["EquipWristColor"] = "NA";
			data["EquipWristColorPreset"] = "NA";
			data["EquipWristOpt01"] = "NA";
			data["EquipWristOpt02"] = "NA";
			data["EquipWristDirt01"] = "NA";
			data["EquipWristDirt02"] = "NA";
		}

		//Wrist Cover
		if ( _DK_RPG_UMA._Equipment._WristCover.Slot != null ) {
			data["EquipWristCoverSlot"] = _DK_RPG_UMA._Equipment._WristCover.Slot.name;
			data["EquipWristCoverColor"] = _DK_RPG_UMA._Equipment._WristCover.Color;
			if ( _DK_RPG_UMA._Equipment._WristCover.ColorPreset ) data["EquipWristCoverColorPreset"] = _DK_RPG_UMA._Equipment._WristCover.ColorPreset.ColorPresetName;
			else data["EquipWristCoverColorPreset"] = "NA";
		}
		else {
			data["EquipWristCoverSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._WristCover.Overlay != null ) {
			data["EquipWristCoverOverlay"] = _DK_RPG_UMA._Equipment._WristCover.Overlay.name;
			data["EquipWristCoverColor"] = _DK_RPG_UMA._Equipment._WristCover.Color;
			if ( _DK_RPG_UMA._Equipment._WristCover.ColorPreset ) data["EquipWristCoverColorPreset"] = _DK_RPG_UMA._Equipment._WristCover.ColorPreset.ColorPresetName;
			else data["EquipWristCoverColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._WristCover.Opt01Color ) data["EquipWristCoverOpt01"] = _DK_RPG_UMA._Equipment._WristCover.Opt01Color.ColorPresetName;
			else data["EquipWristCoverOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._WristCover.Opt02Color ) data["EquipWristCoverOpt02"] = _DK_RPG_UMA._Equipment._WristCover.Opt02Color.ColorPresetName;
			else data["EquipWristCoverOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._WristCover.Dirt01Color ) data["EquipWristCoverDirt01"] = _DK_RPG_UMA._Equipment._WristCover.Dirt01Color.ColorPresetName;
			else data["EquipWristCoverDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._WristCover.Dirt02Color ) data["EquipWristCoverDirt02"] = _DK_RPG_UMA._Equipment._WristCover.Dirt02Color.ColorPresetName;
			else data["EquipWristCoverDirt02"] = "NA";
		}
		else {
			data["EquipWristCoverOverlay"] = "NA";
			data["EquipWristCoverColor"] = "NA";
			data["EquipWristCoverColorPreset"] = "NA";
			data["EquipWristCoverOpt01"] = "NA";
			data["EquipWristCoverOpt02"] = "NA";
			data["EquipWristCoverDirt01"] = "NA";
			data["EquipWristCoverDirt02"] = "NA";
		}
		#endregion Wrist Wear

		#region Hands Wear
		//Hands Sub
		if ( _DK_RPG_UMA._Equipment._HandsSub.Slot != null ) {
			data["EquipHandsSubSlot"] = _DK_RPG_UMA._Equipment._HandsSub.Slot.name;
			data["EquipHandsSubColor"] = _DK_RPG_UMA._Equipment._HandsSub.Color;
			if ( _DK_RPG_UMA._Equipment._HandsSub.ColorPreset ) data["EquipHandsSubColorPreset"] = _DK_RPG_UMA._Equipment._HandsSub.ColorPreset.ColorPresetName;
			else data["EquipHandsSubColorPreset"] = "NA";
		}
		else {
			data["EquipHandsSubSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._HandsSub.Overlay != null ) {
			data["EquipHandsSubOverlay"] = _DK_RPG_UMA._Equipment._HandsSub.Overlay.name;
			data["EquipHandsSubColor"] = _DK_RPG_UMA._Equipment._HandsSub.Color;
			if ( _DK_RPG_UMA._Equipment._HandsSub.ColorPreset ) data["EquipHandsSubColorPreset"] = _DK_RPG_UMA._Equipment._HandsSub.ColorPreset.ColorPresetName;
			else data["EquipHandsSubColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._HandsSub.Opt01Color ) data["EquipHandsSubOpt01"] = _DK_RPG_UMA._Equipment._HandsSub.Opt01Color.ColorPresetName;
			else data["EquipHandsSubOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._HandsSub.Opt02Color ) data["EquipHandsSubOpt02"] = _DK_RPG_UMA._Equipment._HandsSub.Opt02Color.ColorPresetName;
			else data["EquipHandsSubOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._HandsSub.Dirt01Color ) data["EquipHandsSubDirt01"] = _DK_RPG_UMA._Equipment._HandsSub.Dirt01Color.ColorPresetName;
			else data["EquipHandsSubDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._HandsSub.Dirt02Color ) data["EquipHandsSubDirt02"] = _DK_RPG_UMA._Equipment._HandsSub.Dirt02Color.ColorPresetName;
			else data["EquipHandsSubDirt02"] = "NA";
		}
		else {
			data["EquipHandsSubOverlay"] = "NA";
			data["EquipHandsSubColor"] = "NA";
			data["EquipHandsSubColorPreset"] = "NA";
			data["EquipHandsSubOpt01"] = "NA";
			data["EquipHandsSubOpt02"] = "NA";
			data["EquipHandsSubDirt01"] = "NA";
			data["EquipHandsSubDirt02"] = "NA";
		}

		//Hands
		if ( _DK_RPG_UMA._Equipment._Hands.Slot != null ) {
			data["EquipHandsSlot"] = _DK_RPG_UMA._Equipment._Hands.Slot.name;
			data["EquipHandsColor"] = _DK_RPG_UMA._Equipment._Hands.Color;
			if ( _DK_RPG_UMA._Equipment._Hands.ColorPreset ) data["EquipHandsColorPreset"] = _DK_RPG_UMA._Equipment._Hands.ColorPreset.ColorPresetName;
			else data["EquipHandsColorPreset"] = "NA";
		}
		else {
			data["EquipHandsSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._Hands.Overlay != null ) {
			data["EquipHandsOverlay"] = _DK_RPG_UMA._Equipment._Hands.Overlay.name;
			data["EquipHandsColor"] = _DK_RPG_UMA._Equipment._Hands.Color;
			if ( _DK_RPG_UMA._Equipment._Hands.ColorPreset ) data["EquipHandsColorPreset"] = _DK_RPG_UMA._Equipment._Hands.ColorPreset.ColorPresetName;
			else data["EquipHandsColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Hands.Opt01Color ) data["EquipHandsOpt01"] = _DK_RPG_UMA._Equipment._Hands.Opt01Color.ColorPresetName;
			else data["EquipHandsOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Hands.Opt02Color ) data["EquipHandsOpt02"] = _DK_RPG_UMA._Equipment._Hands.Opt02Color.ColorPresetName;
			else data["EquipHandsOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._Hands.Dirt01Color ) data["EquipHandsDirt01"] = _DK_RPG_UMA._Equipment._Hands.Dirt01Color.ColorPresetName;
			else data["EquipHandsDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Hands.Dirt02Color ) data["EquipHandsDirt02"] = _DK_RPG_UMA._Equipment._Hands.Dirt02Color.ColorPresetName;
			else data["EquipHandsDirt02"] = "NA";
		}
		else {
			data["EquipHandsOverlay"] = "NA";
			data["EquipHandsColor"] = "NA";
			data["EquipHandsColorPreset"] = "NA";
			data["EquipHandsOpt01"] = "NA";
			data["EquipHandsOpt02"] = "NA";
			data["EquipHandsDirt01"] = "NA";
			data["EquipHandsDirt02"] = "NA";
		}

		//Hands Cover
		if ( _DK_RPG_UMA._Equipment._HandsCover.Slot != null ) {
			data["EquipHandsCoverSlot"] = _DK_RPG_UMA._Equipment._HandsCover.Slot.name;
			data["EquipHandsCoverColor"] = _DK_RPG_UMA._Equipment._HandsCover.Color;
			if ( _DK_RPG_UMA._Equipment._HandsCover.ColorPreset ) data["EquipHandsCoverColorPreset"] = _DK_RPG_UMA._Equipment._HandsCover.ColorPreset.ColorPresetName;
			else data["EquipHandsCoverColorPreset"] = "NA";
		}
		else {
			data["EquipHandsCoverSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._HandsCover.Overlay != null ) {
			data["EquipHandsCoverOverlay"] = _DK_RPG_UMA._Equipment._HandsCover.Overlay.name;
			data["EquipHandsCoverColor"] = _DK_RPG_UMA._Equipment._HandsCover.Color;
			if ( _DK_RPG_UMA._Equipment._HandsCover.ColorPreset ) data["EquipHandsCoverColorPreset"] = _DK_RPG_UMA._Equipment._HandsCover.ColorPreset.ColorPresetName;
			else data["EquipHandsCoverColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._HandsCover.Opt01Color ) data["EquipHandsCoverOpt01"] = _DK_RPG_UMA._Equipment._HandsCover.Opt01Color.ColorPresetName;
			else data["EquipHandsCoverOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._HandsCover.Opt02Color ) data["EquipHandsCoverOpt02"] = _DK_RPG_UMA._Equipment._HandsCover.Opt02Color.ColorPresetName;
			else data["EquipHandsCoverOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._HandsCover.Dirt01Color ) data["EquipHandsCoverDirt01"] = _DK_RPG_UMA._Equipment._HandsCover.Dirt01Color.ColorPresetName;
			else data["EquipHandsCoverDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._HandsCover.Dirt02Color ) data["EquipHandsCoverDirt02"] = _DK_RPG_UMA._Equipment._HandsCover.Dirt02Color.ColorPresetName;
			else data["EquipHandsCoverDirt02"] = "NA";
		}
		else {
			data["EquipHandsCoverOverlay"] = "NA";
			data["EquipHandsCoverColor"] = "NA";
			data["EquipHandsCoverColorPreset"] = "NA";
			data["EquipHandsCoverOpt01"] = "NA";
			data["EquipHandsCoverOpt02"] = "NA";
			data["EquipHandsCoverDirt01"] = "NA";
			data["EquipHandsCoverDirt02"] = "NA";
		}
		#endregion Hands Wear

		#region Legs Wear
		//Legs Sub
		if ( _DK_RPG_UMA._Equipment._LegsSub.Slot != null ) {
			data["EquipLegsSubSlot"] = _DK_RPG_UMA._Equipment._LegsSub.Slot.name;
			data["EquipLegsSubColor"] = _DK_RPG_UMA._Equipment._LegsSub.Color;
			if ( _DK_RPG_UMA._Equipment._LegsSub.ColorPreset ) data["EquipLegsSubColorPreset"] = _DK_RPG_UMA._Equipment._LegsSub.ColorPreset.ColorPresetName;
			else data["EquipLegsSubColorPreset"] = "NA";
		}
		else {
			data["EquipLegsSubSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._LegsSub.Overlay != null ) {
			data["EquipLegsSubOverlay"] = _DK_RPG_UMA._Equipment._LegsSub.Overlay.name;
			data["EquipLegsSubColor"] = _DK_RPG_UMA._Equipment._LegsSub.Color;
			if ( _DK_RPG_UMA._Equipment._LegsSub.ColorPreset ) data["EquipLegsSubColorPreset"] = _DK_RPG_UMA._Equipment._LegsSub.ColorPreset.ColorPresetName;
			else data["EquipLegsSubColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegsSub.Opt01Color ) data["EquipLegsSubOpt01"] = _DK_RPG_UMA._Equipment._LegsSub.Opt01Color.ColorPresetName;
			else data["EquipLegsSubOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegsSub.Opt02Color ) data["EquipLegsSubOpt02"] = _DK_RPG_UMA._Equipment._LegsSub.Opt02Color.ColorPresetName;
			else data["EquipLegsSubOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._LegsSub.Dirt01Color ) data["EquipLegsSubDirt01"] = _DK_RPG_UMA._Equipment._LegsSub.Dirt01Color.ColorPresetName;
			else data["EquipLegsSubDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegsSub.Dirt02Color ) data["EquipLegsSubDirt02"] = _DK_RPG_UMA._Equipment._LegsSub.Dirt02Color.ColorPresetName;
			else data["EquipLegsSubDirt02"] = "NA";
		}
		else {
			data["EquipLegsSubOverlay"] = "NA";
			data["EquipLegsSubColor"] = "NA";
			data["EquipLegsSubColorPreset"] = "NA";
			data["EquipLegsSubOpt01"] = "NA";
			data["EquipLegsSubOpt02"] = "NA";
			data["EquipLegsSubDirt01"] = "NA";
			data["EquipLegsSubDirt02"] = "NA";
		}

		//Legs
		if ( _DK_RPG_UMA._Equipment._Legs.Slot != null ) {
			data["EquipLegsSlot"] = _DK_RPG_UMA._Equipment._Legs.Slot.name;
			data["EquipLegsColor"] = _DK_RPG_UMA._Equipment._Legs.Color;
			if ( _DK_RPG_UMA._Equipment._Legs.ColorPreset ) data["EquipLegsColorPreset"] = _DK_RPG_UMA._Equipment._Legs.ColorPreset.ColorPresetName;
			else data["EquipLegsColorPreset"] = "NA";
		}
		else {
			data["EquipLegsSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._Legs.Overlay != null ) {
			data["EquipLegsOverlay"] = _DK_RPG_UMA._Equipment._Legs.Overlay.name;
			data["EquipLegsColor"] = _DK_RPG_UMA._Equipment._Legs.Color;
			if ( _DK_RPG_UMA._Equipment._Legs.ColorPreset ) data["EquipLegsColorPreset"] = _DK_RPG_UMA._Equipment._Legs.ColorPreset.ColorPresetName;
			else data["EquipLegsColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Legs.Opt01Color ) data["EquipLegsOpt01"] = _DK_RPG_UMA._Equipment._Legs.Opt01Color.ColorPresetName;
			else data["EquipLegsOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Legs.Opt02Color ) data["EquipLegsOpt02"] = _DK_RPG_UMA._Equipment._Legs.Opt02Color.ColorPresetName;
			else data["EquipLegsOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._Legs.Dirt01Color ) data["EquipLegsDirt01"] = _DK_RPG_UMA._Equipment._Legs.Dirt01Color.ColorPresetName;
			else data["EquipLegsDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Legs.Dirt02Color ) data["EquipLegsDirt02"] = _DK_RPG_UMA._Equipment._Legs.Dirt02Color.ColorPresetName;
			else data["EquipLegsDirt02"] = "NA";
		}
		else {
			data["EquipLegsOverlay"] = "NA";
			data["EquipLegsColor"] = "NA";
			data["EquipLegsColorPreset"] = "NA";
			data["EquipLegsOpt01"] = "NA";
			data["EquipLegsOpt02"] = "NA";
			data["EquipLegsDirt01"] = "NA";
			data["EquipLegsDirt02"] = "NA";
		}

		//Legs Cover
		if ( _DK_RPG_UMA._Equipment._LegsCover.Slot != null ) {
			data["EquipLegsCoverSlot"] = _DK_RPG_UMA._Equipment._LegsCover.Slot.name;
			data["EquipLegsCoverColor"] = _DK_RPG_UMA._Equipment._LegsCover.Color;
			if ( _DK_RPG_UMA._Equipment._LegsCover.ColorPreset ) data["EquipLegsCoverColorPreset"] = _DK_RPG_UMA._Equipment._LegsCover.ColorPreset.ColorPresetName;
			else data["EquipLegsCoverColorPreset"] = "NA";
		}
		else {
			data["EquipLegsCoverSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._LegsCover.Overlay != null ) {
			data["EquipLegsCoverOverlay"] = _DK_RPG_UMA._Equipment._LegsCover.Overlay.name;
			data["EquipLegsCoverColor"] = _DK_RPG_UMA._Equipment._LegsCover.Color;
			if ( _DK_RPG_UMA._Equipment._LegsCover.ColorPreset ) data["EquipLegsCoverColorPreset"] = _DK_RPG_UMA._Equipment._LegsCover.ColorPreset.ColorPresetName;
			else data["EquipLegsCoverColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegsCover.Opt01Color ) data["EquipLegsCoverOpt01"] = _DK_RPG_UMA._Equipment._LegsCover.Opt01Color.ColorPresetName;
			else data["EquipLegsCoverOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegsCover.Opt02Color ) data["EquipLegsCoverOpt02"] = _DK_RPG_UMA._Equipment._LegsCover.Opt02Color.ColorPresetName;
			else data["EquipLegsCoverOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._LegsCover.Dirt01Color ) data["EquipLegsCoverDirt01"] = _DK_RPG_UMA._Equipment._LegsCover.Dirt01Color.ColorPresetName;
			else data["EquipLegsCoverDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegsCover.Dirt02Color ) data["EquipLegsCoverDirt02"] = _DK_RPG_UMA._Equipment._LegsCover.Dirt02Color.ColorPresetName;
			else data["EquipLegsCoverDirt02"] = "NA";
		}
		else {
			data["EquipLegsCoverOverlay"] = "NA";
			data["EquipLegsCoverColor"] = "NA";
			data["EquipLegsCoverColorPreset"] = "NA";
			data["EquipLegsCoverOpt01"] = "NA";
			data["EquipLegsCoverOpt02"] = "NA";
			data["EquipLegsCoverDirt01"] = "NA";
			data["EquipLegsCoverDirt02"] = "NA";
		}
		#endregion Legs Wear

		#region LegBand Wear
		//LegBand Sub
		if ( _DK_RPG_UMA._Equipment._LegBandSub.Slot != null ) {
			data["EquipLegBandSubSlot"] = _DK_RPG_UMA._Equipment._LegBandSub.Slot.name;
			data["EquipLegBandSubColor"] = _DK_RPG_UMA._Equipment._LegBandSub.Color;
			if ( _DK_RPG_UMA._Equipment._LegBandSub.ColorPreset ) data["EquipLegBandSubColorPreset"] = _DK_RPG_UMA._Equipment._LegBandSub.ColorPreset.ColorPresetName;
			else data["EquipLegBandSubColorPreset"] = "NA";
		}
		else {
			data["EquipLegBandSubSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._LegBandSub.Overlay != null ) {
			data["EquipLegBandSubOverlay"] = _DK_RPG_UMA._Equipment._LegBandSub.Overlay.name;
			data["EquipLegBandSubColor"] = _DK_RPG_UMA._Equipment._LegBandSub.Color;
			if ( _DK_RPG_UMA._Equipment._LegBandSub.ColorPreset ) data["EquipLegBandSubColorPreset"] = _DK_RPG_UMA._Equipment._LegBandSub.ColorPreset.ColorPresetName;
			else data["EquipLegBandSubColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegBandSub.Opt01Color ) data["EquipLegBandSubOpt01"] = _DK_RPG_UMA._Equipment._LegBandSub.Opt01Color.ColorPresetName;
			else data["EquipLegBandSubOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegBandSub.Opt02Color ) data["EquipLegBandSubOpt02"] = _DK_RPG_UMA._Equipment._LegBandSub.Opt02Color.ColorPresetName;
			else data["EquipLegBandSubOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._LegBandSub.Dirt01Color ) data["EquipLegBandSubDirt01"] = _DK_RPG_UMA._Equipment._LegBandSub.Dirt01Color.ColorPresetName;
			else data["EquipLegBandSubDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegBandSub.Dirt02Color ) data["EquipLegBandSubDirt02"] = _DK_RPG_UMA._Equipment._LegBandSub.Dirt02Color.ColorPresetName;
			else data["EquipLegBandSubDirt02"] = "NA";
		}
		else {
			data["EquipLegBandSubOverlay"] = "NA";
			data["EquipLegBandSubColor"] = "NA";
			data["EquipLegBandSubColorPreset"] = "NA";
			data["EquipLegBandSubOpt01"] = "NA";
			data["EquipLegBandSubOpt02"] = "NA";
			data["EquipLegBandSubDirt01"] = "NA";
			data["EquipLegBandSubDirt02"] = "NA";
		}

		//LegBand
		if ( _DK_RPG_UMA._Equipment._LegBand.Slot != null ) {
			data["EquipLegBandSlot"] = _DK_RPG_UMA._Equipment._LegBand.Slot.name;
			data["EquipLegBandColor"] = _DK_RPG_UMA._Equipment._LegBand.Color;
			if ( _DK_RPG_UMA._Equipment._LegBand.ColorPreset ) data["EquipLegBandColorPreset"] = _DK_RPG_UMA._Equipment._LegBand.ColorPreset.ColorPresetName;
			else data["EquipLegBandColorPreset"] = "NA";
		}
		else {
			data["EquipLegBandSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._LegBand.Overlay != null ) {
			data["EquipLegBandOverlay"] = _DK_RPG_UMA._Equipment._LegBand.Overlay.name;
			data["EquipLegBandColor"] = _DK_RPG_UMA._Equipment._LegBand.Color;
			if ( _DK_RPG_UMA._Equipment._LegBand.ColorPreset ) data["EquipLegBandColorPreset"] = _DK_RPG_UMA._Equipment._LegBand.ColorPreset.ColorPresetName;
			else data["EquipLegBandColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegBand.Opt01Color ) data["EquipLegBandOpt01"] = _DK_RPG_UMA._Equipment._LegBand.Opt01Color.ColorPresetName;
			else data["EquipLegBandOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegBand.Opt02Color ) data["EquipLegBandOpt02"] = _DK_RPG_UMA._Equipment._LegBand.Opt02Color.ColorPresetName;
			else data["EquipLegBandOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._LegBand.Dirt01Color ) data["EquipLegBandDirt01"] = _DK_RPG_UMA._Equipment._LegBand.Dirt01Color.ColorPresetName;
			else data["EquipLegBandDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegBand.Dirt02Color ) data["EquipLegBandDirt02"] = _DK_RPG_UMA._Equipment._LegBand.Dirt02Color.ColorPresetName;
			else data["EquipLegBandDirt02"] = "NA";
		}
		else {
			data["EquipLegBandOverlay"] = "NA";
			data["EquipLegBandColor"] = "NA";
			data["EquipLegBandColorPreset"] = "NA";
			data["EquipLegBandOpt01"] = "NA";
			data["EquipLegBandOpt02"] = "NA";
			data["EquipLegBandDirt01"] = "NA";
			data["EquipLegBandDirt02"] = "NA";
		}

		//LegBand Cover
		if ( _DK_RPG_UMA._Equipment._LegBandCover.Slot != null ) {
			data["EquipLegBandCoverSlot"] = _DK_RPG_UMA._Equipment._LegBandCover.Slot.name;
			data["EquipLegBandCoverColor"] = _DK_RPG_UMA._Equipment._LegBandCover.Color;
			if ( _DK_RPG_UMA._Equipment._LegBandCover.ColorPreset ) data["EquipLegBandCoverColorPreset"] = _DK_RPG_UMA._Equipment._LegBandCover.ColorPreset.ColorPresetName;
			else data["EquipLegBandCoverColorPreset"] = "NA";
		}
		else {
			data["EquipLegBandCoverSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._LegBandCover.Overlay != null ) {
			data["EquipLegBandCoverOverlay"] = _DK_RPG_UMA._Equipment._LegBandCover.Overlay.name;
			data["EquipLegBandCoverColor"] = _DK_RPG_UMA._Equipment._LegBandCover.Color;
			if ( _DK_RPG_UMA._Equipment._LegBandCover.ColorPreset ) data["EquipLegBandCoverColorPreset"] = _DK_RPG_UMA._Equipment._LegBandCover.ColorPreset.ColorPresetName;
			else data["EquipLegBandCoverColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegBandCover.Opt01Color ) data["EquipLegBandCoverOpt01"] = _DK_RPG_UMA._Equipment._LegBandCover.Opt01Color.ColorPresetName;
			else data["EquipLegBandCoverOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegBandCover.Opt02Color ) data["EquipLegBandCoverOpt02"] = _DK_RPG_UMA._Equipment._LegBandCover.Opt02Color.ColorPresetName;
			else data["EquipLegBandCoverOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._LegBandCover.Dirt01Color ) data["EquipLegBandCoverDirt01"] = _DK_RPG_UMA._Equipment._LegBandCover.Dirt01Color.ColorPresetName;
			else data["EquipLegBandCoverDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LegBandCover.Dirt02Color ) data["EquipLegBandCoverDirt02"] = _DK_RPG_UMA._Equipment._LegBandCover.Dirt02Color.ColorPresetName;
			else data["EquipLegBandCoverDirt02"] = "NA";
		}
		else {
			data["EquipLegBandCoverOverlay"] = "NA";
			data["EquipLegBandCoverColor"] = "NA";
			data["EquipLegBandCoverColorPreset"] = "NA";
			data["EquipLegBandCoverOpt01"] = "NA";
			data["EquipLegBandCoverOpt02"] = "NA";
			data["EquipLegBandCoverDirt01"] = "NA";
			data["EquipLegBandCoverDirt02"] = "NA";
		}
		#endregion LegBand Wear

		#region Feet Wear
		//Feet Sub
		if ( _DK_RPG_UMA._Equipment._FeetSub.Slot != null ) {
			data["EquipFeetSubSlot"] = _DK_RPG_UMA._Equipment._FeetSub.Slot.name;
			data["EquipFeetSubColor"] = _DK_RPG_UMA._Equipment._FeetSub.Color;
			if ( _DK_RPG_UMA._Equipment._FeetSub.ColorPreset ) data["EquipFeetSubColorPreset"] = _DK_RPG_UMA._Equipment._FeetSub.ColorPreset.ColorPresetName;
			else data["EquipFeetSubColorPreset"] = "NA";
		}
		else {
			data["EquipFeetSubSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._FeetSub.Overlay != null ) {
			data["EquipFeetSubOverlay"] = _DK_RPG_UMA._Equipment._FeetSub.Overlay.name;
			data["EquipFeetSubColor"] = _DK_RPG_UMA._Equipment._FeetSub.Color;
			if ( _DK_RPG_UMA._Equipment._FeetSub.ColorPreset ) data["EquipFeetSubColorPreset"] = _DK_RPG_UMA._Equipment._FeetSub.ColorPreset.ColorPresetName;
			else data["EquipFeetSubColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._FeetSub.Opt01Color ) data["EquipFeetSubOpt01"] = _DK_RPG_UMA._Equipment._FeetSub.Opt01Color.ColorPresetName;
			else data["EquipFeetSubOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._FeetSub.Opt02Color ) data["EquipFeetSubOpt02"] = _DK_RPG_UMA._Equipment._FeetSub.Opt02Color.ColorPresetName;
			else data["EquipFeetSubOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._FeetSub.Dirt01Color ) data["EquipFeetSubDirt01"] = _DK_RPG_UMA._Equipment._FeetSub.Dirt01Color.ColorPresetName;
			else data["EquipFeetSubDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._FeetSub.Dirt02Color ) data["EquipFeetSubDirt02"] = _DK_RPG_UMA._Equipment._FeetSub.Dirt02Color.ColorPresetName;
			else data["EquipFeetSubDirt02"] = "NA";
		}
		else {
			data["EquipFeetSubOverlay"] = "NA";
			data["EquipFeetSubColor"] = "NA";
			data["EquipFeetSubColorPreset"] = "NA";
			data["EquipFeetSubOpt01"] = "NA";
			data["EquipFeetSubOpt02"] = "NA";
			data["EquipFeetSubDirt01"] = "NA";
			data["EquipFeetSubDirt02"] = "NA";
		}

		//Feet
		if ( _DK_RPG_UMA._Equipment._Feet.Slot != null ) {
			data["EquipFeetSlot"] = _DK_RPG_UMA._Equipment._Feet.Slot.name;
			data["EquipFeetColor"] = _DK_RPG_UMA._Equipment._Feet.Color;
			if ( _DK_RPG_UMA._Equipment._Feet.ColorPreset ) data["EquipFeetColorPreset"] = _DK_RPG_UMA._Equipment._Feet.ColorPreset.ColorPresetName;
			else data["EquipFeetColorPreset"] = "NA";
		}
		else {
			data["EquipFeetSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._Feet.Overlay != null ) {
			data["EquipFeetOverlay"] = _DK_RPG_UMA._Equipment._Feet.Overlay.name;
			data["EquipFeetColor"] = _DK_RPG_UMA._Equipment._Feet.Color;
			if ( _DK_RPG_UMA._Equipment._Feet.ColorPreset ) data["EquipFeetColorPreset"] = _DK_RPG_UMA._Equipment._Feet.ColorPreset.ColorPresetName;
			else data["EquipFeetColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Feet.Opt01Color ) data["EquipFeetOpt01"] = _DK_RPG_UMA._Equipment._Feet.Opt01Color.ColorPresetName;
			else data["EquipFeetOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Feet.Opt02Color ) data["EquipFeetOpt02"] = _DK_RPG_UMA._Equipment._Feet.Opt02Color.ColorPresetName;
			else data["EquipFeetOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._Feet.Dirt01Color ) data["EquipFeetDirt01"] = _DK_RPG_UMA._Equipment._Feet.Dirt01Color.ColorPresetName;
			else data["EquipFeetDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Feet.Dirt02Color ) data["EquipFeetDirt02"] = _DK_RPG_UMA._Equipment._Feet.Dirt02Color.ColorPresetName;
			else data["EquipFeetDirt02"] = "NA";
		}
		else {
			data["EquipFeetOverlay"] = "NA";
			data["EquipFeetColor"] = "NA";
			data["EquipFeetColorPreset"] = "NA";
			data["EquipFeetOpt01"] = "NA";
			data["EquipFeetOpt02"] = "NA";
			data["EquipFeetDirt01"] = "NA";
			data["EquipFeetDirt02"] = "NA";
		}

		//Feet Cover
		if ( _DK_RPG_UMA._Equipment._FeetCover.Slot != null ) {
			data["EquipFeetCoverSlot"] = _DK_RPG_UMA._Equipment._FeetCover.Slot.name;
			data["EquipFeetCoverColor"] = _DK_RPG_UMA._Equipment._FeetCover.Color;
			if ( _DK_RPG_UMA._Equipment._FeetCover.ColorPreset ) data["EquipFeetCoverColorPreset"] = _DK_RPG_UMA._Equipment._FeetCover.ColorPreset.ColorPresetName;
			else data["EquipFeetCoverColorPreset"] = "NA";
		}
		else {
			data["EquipFeetCoverSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._FeetCover.Overlay != null ) {
			data["EquipFeetCoverOverlay"] = _DK_RPG_UMA._Equipment._FeetCover.Overlay.name;
			data["EquipFeetCoverColor"] = _DK_RPG_UMA._Equipment._FeetCover.Color;
			if ( _DK_RPG_UMA._Equipment._FeetCover.ColorPreset ) data["EquipFeetCoverColorPreset"] = _DK_RPG_UMA._Equipment._FeetCover.ColorPreset.ColorPresetName;
			else data["EquipFeetCoverColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._FeetCover.Opt01Color ) data["EquipFeetCoverOpt01"] = _DK_RPG_UMA._Equipment._FeetCover.Opt01Color.ColorPresetName;
			else data["EquipFeetCoverOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._FeetCover.Opt02Color ) data["EquipFeetCoverOpt02"] = _DK_RPG_UMA._Equipment._FeetCover.Opt02Color.ColorPresetName;
			else data["EquipFeetCoverOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._FeetCover.Dirt01Color ) data["EquipFeetCoverDirt01"] = _DK_RPG_UMA._Equipment._FeetCover.Dirt01Color.ColorPresetName;
			else data["EquipFeetCoverDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._FeetCover.Dirt02Color ) data["EquipFeetCoverDirt02"] = _DK_RPG_UMA._Equipment._FeetCover.Dirt02Color.ColorPresetName;
			else data["EquipFeetCoverDirt02"] = "NA";
		}
		else {
			data["EquipFeetCoverOverlay"] = "NA";
			data["EquipFeetCoverColor"] = "NA";
			data["EquipFeetCoverColorPreset"] = "NA";
			data["EquipFeetCoverOpt01"] = "NA";
			data["EquipFeetCoverOpt02"] = "NA";
			data["EquipFeetCoverDirt01"] = "NA";
			data["EquipFeetCoverDirt02"] = "NA";
		}
		#endregion Feet Wear

		#region Collar Wear
		//Collar Sub
		if ( _DK_RPG_UMA._Equipment._CollarSub.Slot != null ) {
			data["EquipCollarSubSlot"] = _DK_RPG_UMA._Equipment._CollarSub.Slot.name;
			data["EquipCollarSubColor"] = _DK_RPG_UMA._Equipment._CollarSub.Color;
			if ( _DK_RPG_UMA._Equipment._CollarSub.ColorPreset ) data["EquipCollarSubColorPreset"] = _DK_RPG_UMA._Equipment._CollarSub.ColorPreset.ColorPresetName;
			else data["EquipCollarSubColorPreset"] = "NA";
		}
		else {
			data["EquipCollarSubSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._CollarSub.Overlay != null ) {
			data["EquipCollarSubOverlay"] = _DK_RPG_UMA._Equipment._CollarSub.Overlay.name;
			data["EquipCollarSubColor"] = _DK_RPG_UMA._Equipment._CollarSub.Color;
			if ( _DK_RPG_UMA._Equipment._CollarSub.ColorPreset ) data["EquipCollarSubColorPreset"] = _DK_RPG_UMA._Equipment._CollarSub.ColorPreset.ColorPresetName;
			else data["EquipCollarSubColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._CollarSub.Opt01Color ) data["EquipCollarSubOpt01"] = _DK_RPG_UMA._Equipment._CollarSub.Opt01Color.ColorPresetName;
			else data["EquipCollarSubOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._CollarSub.Opt02Color ) data["EquipCollarSubOpt02"] = _DK_RPG_UMA._Equipment._CollarSub.Opt02Color.ColorPresetName;
			else data["EquipCollarSubOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._CollarSub.Dirt01Color ) data["EquipCollarSubDirt01"] = _DK_RPG_UMA._Equipment._CollarSub.Dirt01Color.ColorPresetName;
			else data["EquipCollarSubDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._CollarSub.Dirt02Color ) data["EquipCollarSubDirt02"] = _DK_RPG_UMA._Equipment._CollarSub.Dirt02Color.ColorPresetName;
			else data["EquipCollarSubDirt02"] = "NA";
		}
		else {
			data["EquipCollarSubOverlay"] = "NA";
			data["EquipCollarSubColor"] = "NA";
			data["EquipCollarSubColorPreset"] = "NA";
			data["EquipCollarSubOpt01"] = "NA";
			data["EquipCollarSubOpt02"] = "NA";
			data["EquipCollarSubDirt01"] = "NA";
			data["EquipCollarSubDirt02"] = "NA";
		}
		//Collar
		if ( _DK_RPG_UMA._Equipment._Collar.Slot != null ) {
			data["EquipCollarSlot"] = _DK_RPG_UMA._Equipment._Collar.Slot.name;
			data["EquipCollarColor"] = _DK_RPG_UMA._Equipment._Collar.Color;
			if ( _DK_RPG_UMA._Equipment._Collar.ColorPreset ) data["EquipCollarColorPreset"] = _DK_RPG_UMA._Equipment._Collar.ColorPreset.ColorPresetName;
			else data["EquipCollarColorPreset"] = "NA";
		}
		else {
			data["EquipCollarSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._Collar.Overlay != null ) {
			data["EquipCollarOverlay"] = _DK_RPG_UMA._Equipment._Collar.Overlay.name;
			data["EquipCollarColor"] = _DK_RPG_UMA._Equipment._Collar.Color;
			if ( _DK_RPG_UMA._Equipment._Collar.ColorPreset ) data["EquipCollarColorPreset"] = _DK_RPG_UMA._Equipment._Collar.ColorPreset.ColorPresetName;
			else data["EquipCollarColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Collar.Opt01Color ) data["EquipCollarOpt01"] = _DK_RPG_UMA._Equipment._Collar.Opt01Color.ColorPresetName;
			else data["EquipCollarOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Collar.Opt02Color ) data["EquipCollarOpt02"] = _DK_RPG_UMA._Equipment._Collar.Opt02Color.ColorPresetName;
			else data["EquipCollarOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._Collar.Dirt01Color ) data["EquipCollarDirt01"] = _DK_RPG_UMA._Equipment._Collar.Dirt01Color.ColorPresetName;
			else data["EquipCollarDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Collar.Dirt02Color ) data["EquipCollarDirt02"] = _DK_RPG_UMA._Equipment._Collar.Dirt02Color.ColorPresetName;
			else data["EquipCollarDirt02"] = "NA";
		}
		else {
			data["EquipCollarOverlay"] = "NA";
			data["EquipCollarColor"] = "NA";
			data["EquipCollarColorPreset"] = "NA";
			data["EquipCollarOpt01"] = "NA";
			data["EquipCollarOpt02"] = "NA";
			data["EquipCollarDirt01"] = "NA";
			data["EquipCollarDirt02"] = "NA";
		}
		//Collar Cover
		if ( _DK_RPG_UMA._Equipment._CollarCover.Slot != null ) {
			data["EquipCollarCoverSlot"] = _DK_RPG_UMA._Equipment._CollarCover.Slot.name;
			data["EquipCollarCoverColor"] = _DK_RPG_UMA._Equipment._CollarCover.Color;
			if ( _DK_RPG_UMA._Equipment._CollarCover.ColorPreset ) data["EquipCollarCoverColorPreset"] = _DK_RPG_UMA._Equipment._CollarCover.ColorPreset.ColorPresetName;
			else data["EquipCollarCoverColorPreset"] = "NA";
		}
		else {
			data["EquipCollarCoverSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._CollarCover.Overlay != null ) {
			data["EquipCollarCoverOverlay"] = _DK_RPG_UMA._Equipment._CollarCover.Overlay.name;
			data["EquipCollarCoverColor"] = _DK_RPG_UMA._Equipment._CollarCover.Color;
			if ( _DK_RPG_UMA._Equipment._CollarCover.ColorPreset ) data["EquipCollarCoverColorPreset"] = _DK_RPG_UMA._Equipment._CollarCover.ColorPreset.ColorPresetName;
			else data["EquipCollarCoverColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._CollarCover.Opt01Color ) data["EquipCollarCoverOpt01"] = _DK_RPG_UMA._Equipment._CollarCover.Opt01Color.ColorPresetName;
			else data["EquipCollarCoverOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._CollarCover.Opt02Color ) data["EquipCollarCoverOpt02"] = _DK_RPG_UMA._Equipment._CollarCover.Opt02Color.ColorPresetName;
			else data["EquipCollarCoverOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._CollarCover.Dirt01Color ) data["EquipCollarCoverDirt01"] = _DK_RPG_UMA._Equipment._CollarCover.Dirt01Color.ColorPresetName;
			else data["EquipCollarCoverDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._CollarCover.Dirt02Color ) data["EquipCollarCoverDirt02"] = _DK_RPG_UMA._Equipment._CollarCover.Dirt02Color.ColorPresetName;
			else data["EquipCollarCoverDirt02"] = "NA";
		}
		else {
			data["EquipCollarCoverOverlay"] = "NA";
			data["EquipCollarCoverColor"] = "NA";
			data["EquipCollarCoverColorPreset"] = "NA";
			data["EquipCollarCoverOpt01"] = "NA";
			data["EquipCollarCoverOpt02"] = "NA";
			data["EquipCollarCoverDirt01"] = "NA";
			data["EquipCollarCoverDirt02"] = "NA";
		}
		#endregion Collar Wear

		#region RingLeft Wear
		//RingLeft
		if ( _DK_RPG_UMA._Equipment._RingLeft.Slot != null ) {
			data["EquipRingLeftSlot"] = _DK_RPG_UMA._Equipment._RingLeft.Slot.name;
			data["EquipRingLeftColor"] = _DK_RPG_UMA._Equipment._RingLeft.Color;
			if ( _DK_RPG_UMA._Equipment._RingLeft.ColorPreset ) data["EquipRingLeftColorPreset"] = _DK_RPG_UMA._Equipment._RingLeft.ColorPreset.ColorPresetName;
			else data["EquipRingLeftColorPreset"] = "NA";
		}
		else {
			data["EquipRingLeftSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._RingLeft.Overlay != null ) {
			data["EquipRingLeftOverlay"] = _DK_RPG_UMA._Equipment._RingLeft.Overlay.name;
			data["EquipRingLeftColor"] = _DK_RPG_UMA._Equipment._RingLeft.Color;
			if ( _DK_RPG_UMA._Equipment._RingLeft.ColorPreset ) data["EquipRingLeftColorPreset"] = _DK_RPG_UMA._Equipment._RingLeft.ColorPreset.ColorPresetName;
			else data["EquipRingLeftColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._RingLeft.Opt01Color ) data["EquipRingLeftOpt01"] = _DK_RPG_UMA._Equipment._RingLeft.Opt01Color.ColorPresetName;
			else data["EquipRingLeftOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._RingLeft.Opt02Color ) data["EquipRingLeftOpt02"] = _DK_RPG_UMA._Equipment._RingLeft.Opt02Color.ColorPresetName;
			else data["EquipRingLeftOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._RingLeft.Dirt01Color ) data["EquipRingLeftDirt01"] = _DK_RPG_UMA._Equipment._RingLeft.Dirt01Color.ColorPresetName;
			else data["EquipRingLeftDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._RingLeft.Dirt02Color ) data["EquipRingLeftDirt02"] = _DK_RPG_UMA._Equipment._RingLeft.Dirt02Color.ColorPresetName;
			else data["EquipRingLeftDirt02"] = "NA";
		}
		else {
			data["EquipRingLeftOverlay"] = "NA";
			data["EquipRingLeftColor"] = "NA";
			data["EquipRingLeftColorPreset"] = "NA";
			data["EquipRingLeftOpt01"] = "NA";
			data["EquipRingLeftOpt02"] = "NA";
			data["EquipRingLeftDirt01"] = "NA";
			data["EquipRingLeftDirt02"] = "NA";
		}
		#endregion RingLeft Wear

		#region RingRight Wear
		//RingRight
		if ( _DK_RPG_UMA._Equipment._RingRight.Slot != null ) {
			data["EquipRingRightSlot"] = _DK_RPG_UMA._Equipment._RingRight.Slot.name;
			data["EquipRingRightColor"] = _DK_RPG_UMA._Equipment._RingRight.Color;
			if ( _DK_RPG_UMA._Equipment._RingRight.ColorPreset ) data["EquipRingRightColorPreset"] = _DK_RPG_UMA._Equipment._RingRight.ColorPreset.ColorPresetName;
			else data["EquipRingRightColorPreset"] = "NA";
		}
		else {
			data["EquipRingRightSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._RingRight.Overlay != null ) {
			data["EquipRingRightOverlay"] = _DK_RPG_UMA._Equipment._RingRight.Overlay.name;
			data["EquipRingRightColor"] = _DK_RPG_UMA._Equipment._RingRight.Color;
			if ( _DK_RPG_UMA._Equipment._RingRight.ColorPreset ) data["EquipRingRightColorPreset"] = _DK_RPG_UMA._Equipment._RingRight.ColorPreset.ColorPresetName;
			else data["EquipRingRightColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._RingRight.Opt01Color ) data["EquipRingRightOpt01"] = _DK_RPG_UMA._Equipment._RingRight.Opt01Color.ColorPresetName;
			else data["EquipRingRightOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._RingRight.Opt02Color ) data["EquipRingRightOpt02"] = _DK_RPG_UMA._Equipment._RingRight.Opt02Color.ColorPresetName;
			else data["EquipRingRightOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._RingRight.Dirt01Color ) data["EquipRingRightDirt01"] = _DK_RPG_UMA._Equipment._RingRight.Dirt01Color.ColorPresetName;
			else data["EquipRingRightDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._RingRight.Dirt02Color ) data["EquipRingRightDirt02"] = _DK_RPG_UMA._Equipment._RingRight.Dirt02Color.ColorPresetName;
			else data["EquipRingRightDirt02"] = "NA";
		}
		else {
			data["EquipRingRightOverlay"] = "NA";
			data["EquipRingRightColor"] = "NA";
			data["EquipRingRightColorPreset"] = "NA";
			data["EquipRingRightOpt01"] = "NA";
			data["EquipRingRightOpt02"] = "NA";
			data["EquipRingRightDirt01"] = "NA";
			data["EquipRingRightDirt02"] = "NA";
		}
		#endregion RingRight Wear

		#region Belt Wear
		//Belt Sub
		if ( _DK_RPG_UMA._Equipment._BeltSub.Slot != null ) {
			data["EquipBeltSubSlot"] = _DK_RPG_UMA._Equipment._BeltSub.Slot.name;
			data["EquipBeltSubColor"] = _DK_RPG_UMA._Equipment._BeltSub.Color;
			if ( _DK_RPG_UMA._Equipment._BeltSub.ColorPreset ) data["EquipBeltSubColorPreset"] = _DK_RPG_UMA._Equipment._BeltSub.ColorPreset.ColorPresetName;
			else data["EquipBeltSubColorPreset"] = "NA";
		}
		else {
			data["EquipBeltSubSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._BeltSub.Overlay != null ) {
			data["EquipBeltSubOverlay"] = _DK_RPG_UMA._Equipment._BeltSub.Overlay.name;
			data["EquipBeltSubColor"] = _DK_RPG_UMA._Equipment._BeltSub.Color;
			if ( _DK_RPG_UMA._Equipment._BeltSub.ColorPreset ) data["EquipBeltSubColorPreset"] = _DK_RPG_UMA._Equipment._BeltSub.ColorPreset.ColorPresetName;
			else data["EquipBeltSubColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._BeltSub.Opt01Color ) data["EquipBeltSubOpt01"] = _DK_RPG_UMA._Equipment._BeltSub.Opt01Color.ColorPresetName;
			else data["EquipBeltSubOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._BeltSub.Opt02Color ) data["EquipBeltSubOpt02"] = _DK_RPG_UMA._Equipment._BeltSub.Opt02Color.ColorPresetName;
			else data["EquipBeltSubOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._BeltSub.Dirt01Color ) data["EquipBeltSubDirt01"] = _DK_RPG_UMA._Equipment._BeltSub.Dirt01Color.ColorPresetName;
			else data["EquipBeltSubDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._BeltSub.Dirt02Color ) data["EquipBeltSubDirt02"] = _DK_RPG_UMA._Equipment._BeltSub.Dirt02Color.ColorPresetName;
			else data["EquipBeltSubDirt02"] = "NA";
		}
		else {
			data["EquipBeltSubOverlay"] = "NA";
			data["EquipBeltSubColor"] = "NA";
			data["EquipBeltSubColorPreset"] = "NA";
			data["EquipBeltSubOpt01"] = "NA";
			data["EquipBeltSubOpt02"] = "NA";
			data["EquipBeltSubDirt01"] = "NA";
			data["EquipBeltSubDirt02"] = "NA";
		}

		//Belt
		if ( _DK_RPG_UMA._Equipment._Belt.Slot != null ) {
			data["EquipBeltSlot"] = _DK_RPG_UMA._Equipment._Belt.Slot.name;
			data["EquipBeltColor"] = _DK_RPG_UMA._Equipment._Belt.Color;
			if ( _DK_RPG_UMA._Equipment._Belt.ColorPreset ) data["EquipBeltColorPreset"] = _DK_RPG_UMA._Equipment._Belt.ColorPreset.ColorPresetName;
			else data["EquipBeltColorPreset"] = "NA";
		}
		else {
			data["EquipBeltSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._Belt.Overlay != null ) {
			data["EquipBeltOverlay"] = _DK_RPG_UMA._Equipment._Belt.Overlay.name;
			data["EquipBeltColor"] = _DK_RPG_UMA._Equipment._Belt.Color;
			if ( _DK_RPG_UMA._Equipment._Belt.ColorPreset ) data["EquipBeltColorPreset"] = _DK_RPG_UMA._Equipment._Belt.ColorPreset.ColorPresetName;
			else data["EquipBeltColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Belt.Opt01Color ) data["EquipBeltOpt01"] = _DK_RPG_UMA._Equipment._Belt.Opt01Color.ColorPresetName;
			else data["EquipBeltOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Belt.Opt02Color ) data["EquipBeltOpt02"] = _DK_RPG_UMA._Equipment._Belt.Opt02Color.ColorPresetName;
			else data["EquipBeltOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._Belt.Dirt01Color ) data["EquipBeltDirt01"] = _DK_RPG_UMA._Equipment._Belt.Dirt01Color.ColorPresetName;
			else data["EquipBeltDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Belt.Dirt02Color ) data["EquipBeltDirt02"] = _DK_RPG_UMA._Equipment._Belt.Dirt02Color.ColorPresetName;
			else data["EquipBeltDirt02"] = "NA";
		}
		else {
			data["EquipBeltOverlay"] = "NA";
			data["EquipBeltColor"] = "NA";
			data["EquipBeltColorPreset"] = "NA";
			data["EquipBeltOpt01"] = "NA";
			data["EquipBeltOpt02"] = "NA";
			data["EquipBeltDirt01"] = "NA";
			data["EquipBeltDirt02"] = "NA";
		}

		//Belt Cover
		if ( _DK_RPG_UMA._Equipment._BeltCover.Slot != null ) {
			data["EquipBeltCoverSlot"] = _DK_RPG_UMA._Equipment._BeltCover.Slot.name;
			data["EquipBeltCoverColor"] = _DK_RPG_UMA._Equipment._BeltCover.Color;
			if ( _DK_RPG_UMA._Equipment._BeltCover.ColorPreset ) data["EquipBeltCoverColorPreset"] = _DK_RPG_UMA._Equipment._BeltCover.ColorPreset.ColorPresetName;
			else data["EquipBeltCoverColorPreset"] = "NA";
		}
		else {
			data["EquipBeltCoverSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._BeltCover.Overlay != null ) {
			data["EquipBeltCoverOverlay"] = _DK_RPG_UMA._Equipment._BeltCover.Overlay.name;
			data["EquipBeltCoverColor"] = _DK_RPG_UMA._Equipment._BeltCover.Color;
			if ( _DK_RPG_UMA._Equipment._BeltCover.ColorPreset ) data["EquipBeltCoverColorPreset"] = _DK_RPG_UMA._Equipment._BeltCover.ColorPreset.ColorPresetName;
			else data["EquipBeltCoverColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._BeltCover.Opt01Color ) data["EquipBeltCoverOpt01"] = _DK_RPG_UMA._Equipment._BeltCover.Opt01Color.ColorPresetName;
			else data["EquipBeltCoverOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._BeltCover.Opt02Color ) data["EquipBeltCoverOpt02"] = _DK_RPG_UMA._Equipment._BeltCover.Opt02Color.ColorPresetName;
			else data["EquipBeltCoverOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._BeltCover.Dirt01Color ) data["EquipBeltCoverDirt01"] = _DK_RPG_UMA._Equipment._BeltCover.Dirt01Color.ColorPresetName;
			else data["EquipBeltCoverDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._BeltCover.Dirt02Color ) data["EquipBeltCoverDirt02"] = _DK_RPG_UMA._Equipment._BeltCover.Dirt02Color.ColorPresetName;
			else data["EquipBeltCoverDirt02"] = "NA";
		}
		else {
			data["EquipBeltCoverOverlay"] = "NA";
			data["EquipBeltCoverColor"] = "NA";
			data["EquipBeltCoverColorPreset"] = "NA";
			data["EquipBeltCoverOpt01"] = "NA";
			data["EquipBeltCoverOpt02"] = "NA";
			data["EquipBeltCoverDirt01"] = "NA";
			data["EquipBeltCoverDirt02"] = "NA";
		}
		#endregion Belt Wear

		#region Cloak Wear
		//Cloak
		if ( _DK_RPG_UMA._Equipment._Cloak.Slot != null ) {
			data["EquipCloakSlot"] = _DK_RPG_UMA._Equipment._Cloak.Slot.name;
			data["EquipCloakColor"] = _DK_RPG_UMA._Equipment._Cloak.Color;
			if ( _DK_RPG_UMA._Equipment._Cloak.ColorPreset ) data["EquipCloakColorPreset"] = _DK_RPG_UMA._Equipment._Cloak.ColorPreset.ColorPresetName;
			else data["EquipCloakColorPreset"] = "NA";
		}
		else {
			data["EquipCloakSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._Cloak.Overlay != null ) {
			data["EquipCloakOverlay"] = _DK_RPG_UMA._Equipment._Cloak.Overlay.name;
			data["EquipCloakColor"] = _DK_RPG_UMA._Equipment._Cloak.Color;
			if ( _DK_RPG_UMA._Equipment._Cloak.ColorPreset ) data["EquipCloakColorPreset"] = _DK_RPG_UMA._Equipment._Cloak.ColorPreset.ColorPresetName;
			else data["EquipCloakColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Cloak.Opt01Color ) data["EquipCloakOpt01"] = _DK_RPG_UMA._Equipment._Cloak.Opt01Color.ColorPresetName;
			else data["EquipCloakOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Cloak.Opt02Color ) data["EquipCloakOpt02"] = _DK_RPG_UMA._Equipment._Cloak.Opt02Color.ColorPresetName;
			else data["EquipCloakOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._Cloak.Dirt01Color ) data["EquipCloakDirt01"] = _DK_RPG_UMA._Equipment._Cloak.Dirt01Color.ColorPresetName;
			else data["EquipCloakDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Cloak.Dirt02Color ) data["EquipCloakDirt02"] = _DK_RPG_UMA._Equipment._Cloak.Dirt02Color.ColorPresetName;
			else data["EquipCloakDirt02"] = "NA";
		}
		else {
			data["EquipCloakOverlay"] = "NA";
			data["EquipCloakColor"] = "NA";
			data["EquipCloakColorPreset"] = "NA";
			data["EquipCloakOpt01"] = "NA";
			data["EquipCloakOpt02"] = "NA";
			data["EquipCloakDirt01"] = "NA";
			data["EquipCloakDirt02"] = "NA";
		}
		#endregion Cloak Wear

		#region Backpack Wear
		//Backpack Sub
		if ( _DK_RPG_UMA._Equipment._BackpackSub.Slot != null ) {
			data["EquipBackpackSubSlot"] = _DK_RPG_UMA._Equipment._BackpackSub.Slot.name;
			data["EquipBackpackSubColor"] = _DK_RPG_UMA._Equipment._BackpackSub.Color;
			if ( _DK_RPG_UMA._Equipment._BackpackSub.ColorPreset ) data["EquipBackpackSubColorPreset"] = _DK_RPG_UMA._Equipment._BackpackSub.ColorPreset.ColorPresetName;
			else data["EquipBackpackSubColorPreset"] = "NA";
		}
		else {
			data["EquipBackpackSubSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._BackpackSub.Overlay != null ) {
			data["EquipBackpackSubOverlay"] = _DK_RPG_UMA._Equipment._BackpackSub.Overlay.name;
			data["EquipBackpackSubColor"] = _DK_RPG_UMA._Equipment._BackpackSub.Color;
			if ( _DK_RPG_UMA._Equipment._BackpackSub.ColorPreset ) data["EquipBackpackSubColorPreset"] = _DK_RPG_UMA._Equipment._BackpackSub.ColorPreset.ColorPresetName;
			else data["EquipBackpackSubColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._BackpackSub.Opt01Color ) data["EquipBackpackSubOpt01"] = _DK_RPG_UMA._Equipment._BackpackSub.Opt01Color.ColorPresetName;
			else data["EquipBackpackSubOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._BackpackSub.Opt02Color ) data["EquipBackpackSubOpt02"] = _DK_RPG_UMA._Equipment._BackpackSub.Opt02Color.ColorPresetName;
			else data["EquipBackpackSubOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._BackpackSub.Dirt01Color ) data["EquipBackpackSubDirt01"] = _DK_RPG_UMA._Equipment._BackpackSub.Dirt01Color.ColorPresetName;
			else data["EquipBackpackSubDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._BackpackSub.Dirt02Color ) data["EquipBackpackSubDirt02"] = _DK_RPG_UMA._Equipment._BackpackSub.Dirt02Color.ColorPresetName;
			else data["EquipBackpackSubDirt02"] = "NA";
		}
		else {
			data["EquipBackpackSubOverlay"] = "NA";
			data["EquipBackpackSubColor"] = "NA";
			data["EquipBackpackSubColorPreset"] = "NA";
			data["EquipBackpackSubOpt01"] = "NA";
			data["EquipBackpackSubOpt02"] = "NA";
			data["EquipBackpackSubDirt01"] = "NA";
			data["EquipBackpackSubDirt02"] = "NA";
		}

		//Backpack
		if ( _DK_RPG_UMA._Equipment._Backpack.Slot != null ) {
			data["EquipBackpackSlot"] = _DK_RPG_UMA._Equipment._Backpack.Slot.name;
			data["EquipBackpackColor"] = _DK_RPG_UMA._Equipment._Backpack.Color;
			if ( _DK_RPG_UMA._Equipment._Backpack.ColorPreset ) data["EquipBackpackColorPreset"] = _DK_RPG_UMA._Equipment._Backpack.ColorPreset.ColorPresetName;
			else data["EquipBackpackColorPreset"] = "NA";
		}
		else {
			data["EquipBackpackSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._Backpack.Overlay != null ) {
			data["EquipBackpackOverlay"] = _DK_RPG_UMA._Equipment._Backpack.Overlay.name;
			data["EquipBackpackColor"] = _DK_RPG_UMA._Equipment._Backpack.Color;
			if ( _DK_RPG_UMA._Equipment._Backpack.ColorPreset ) data["EquipBackpackColorPreset"] = _DK_RPG_UMA._Equipment._Backpack.ColorPreset.ColorPresetName;
			else data["EquipBackpackColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Backpack.Opt01Color ) data["EquipBackpackOpt01"] = _DK_RPG_UMA._Equipment._Backpack.Opt01Color.ColorPresetName;
			else data["EquipBackpackOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Backpack.Opt02Color ) data["EquipBackpackOpt02"] = _DK_RPG_UMA._Equipment._Backpack.Opt02Color.ColorPresetName;
			else data["EquipBackpackOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._Backpack.Dirt01Color ) data["EquipBackpackDirt01"] = _DK_RPG_UMA._Equipment._Backpack.Dirt01Color.ColorPresetName;
			else data["EquipBackpackDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._Backpack.Dirt02Color ) data["EquipBackpackDirt02"] = _DK_RPG_UMA._Equipment._Backpack.Dirt02Color.ColorPresetName;
			else data["EquipBackpackDirt02"] = "NA";
		}
		else {
			data["EquipBackpackOverlay"] = "NA";
			data["EquipBackpackColor"] = "NA";
			data["EquipBackpackColorPreset"] = "NA";
			data["EquipBackpackOpt01"] = "NA";
			data["EquipBackpackOpt02"] = "NA";
			data["EquipBackpackDirt01"] = "NA";
			data["EquipBackpackDirt02"] = "NA";
		}

		//Backpack Cover
		if ( _DK_RPG_UMA._Equipment._BackpackCover.Slot != null ) {
			data["EquipBackpackCoverSlot"] = _DK_RPG_UMA._Equipment._BackpackCover.Slot.name;
			data["EquipBackpackCoverColor"] = _DK_RPG_UMA._Equipment._BackpackCover.Color;
			if ( _DK_RPG_UMA._Equipment._BackpackCover.ColorPreset ) data["EquipBackpackCoverColorPreset"] = _DK_RPG_UMA._Equipment._BackpackCover.ColorPreset.ColorPresetName;
			else data["EquipBackpackCoverColorPreset"] = "NA";
		}
		else {
			data["EquipBackpackCoverSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._BackpackCover.Overlay != null ) {
			data["EquipBackpackCoverOverlay"] = _DK_RPG_UMA._Equipment._BackpackCover.Overlay.name;
			data["EquipBackpackCoverColor"] = _DK_RPG_UMA._Equipment._BackpackCover.Color;
			if ( _DK_RPG_UMA._Equipment._BackpackCover.ColorPreset ) data["EquipBackpackCoverColorPreset"] = _DK_RPG_UMA._Equipment._BackpackCover.ColorPreset.ColorPresetName;
			else data["EquipBackpackCoverColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._BackpackCover.Opt01Color ) data["EquipBackpackCoverOpt01"] = _DK_RPG_UMA._Equipment._BackpackCover.Opt01Color.ColorPresetName;
			else data["EquipBackpackCoverOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._BackpackCover.Opt02Color ) data["EquipBackpackCoverOpt02"] = _DK_RPG_UMA._Equipment._BackpackCover.Opt02Color.ColorPresetName;
			else data["EquipBackpackCoverOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._BackpackCover.Dirt01Color ) data["EquipBackpackCoverDirt01"] = _DK_RPG_UMA._Equipment._BackpackCover.Dirt01Color.ColorPresetName;
			else data["EquipBackpackCoverDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._BackpackCover.Dirt02Color ) data["EquipBackpackCoverDirt02"] = _DK_RPG_UMA._Equipment._BackpackCover.Dirt02Color.ColorPresetName;
			else data["EquipBackpackCoverDirt02"] = "NA";
		}
		else {
			data["EquipBackpackCoverOverlay"] = "NA";
			data["EquipBackpackCoverColor"] = "NA";
			data["EquipBackpackCoverColorPreset"] = "NA";
			data["EquipBackpackCoverOpt01"] = "NA";
			data["EquipBackpackCoverOpt02"] = "NA";
			data["EquipBackpackCoverDirt01"] = "NA";
			data["EquipBackpackCoverDirt02"] = "NA";
		}
		#endregion Backpack Wear

		#region LeftHand Wear
		//LeftHand
		if ( _DK_RPG_UMA._Equipment._LeftHand.Slot != null ) {
			data["EquipLeftHandSlot"] = _DK_RPG_UMA._Equipment._LeftHand.Slot.name;
			data["EquipLeftHandColor"] = _DK_RPG_UMA._Equipment._LeftHand.Color;
			if ( _DK_RPG_UMA._Equipment._LeftHand.ColorPreset ) data["EquipLeftHandColorPreset"] = _DK_RPG_UMA._Equipment._LeftHand.ColorPreset.ColorPresetName;
			else data["EquipLeftHandColorPreset"] = "NA";
		}
		else {
			data["EquipLeftHandSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._LeftHand.Overlay != null ) {
			data["EquipLeftHandOverlay"] = _DK_RPG_UMA._Equipment._LeftHand.Overlay.name;
			data["EquipLeftHandColor"] = _DK_RPG_UMA._Equipment._LeftHand.Color;
			if ( _DK_RPG_UMA._Equipment._LeftHand.ColorPreset ) data["EquipLeftHandColorPreset"] = _DK_RPG_UMA._Equipment._LeftHand.ColorPreset.ColorPresetName;
			else data["EquipLeftHandColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LeftHand.Opt01Color ) data["EquipLeftHandOpt01"] = _DK_RPG_UMA._Equipment._LeftHand.Opt01Color.ColorPresetName;
			else data["EquipLeftHandOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LeftHand.Opt02Color ) data["EquipLeftHandOpt02"] = _DK_RPG_UMA._Equipment._LeftHand.Opt02Color.ColorPresetName;
			else data["EquipLeftHandOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._LeftHand.Dirt01Color ) data["EquipLeftHandDirt01"] = _DK_RPG_UMA._Equipment._LeftHand.Dirt01Color.ColorPresetName;
			else data["EquipLeftHandDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._LeftHand.Dirt02Color ) data["EquipLeftHandDirt02"] = _DK_RPG_UMA._Equipment._LeftHand.Dirt02Color.ColorPresetName;
			else data["EquipLeftHandDirt02"] = "NA";
		}
		else {
			data["EquipLeftHandOverlay"] = "NA";
			data["EquipLeftHandColor"] = "NA";
			data["EquipLeftHandColorPreset"] = "NA";
			data["EquipLeftHandOpt01"] = "NA";
			data["EquipLeftHandOpt02"] = "NA";
			data["EquipLeftHandDirt01"] = "NA";
			data["EquipLeftHandDirt02"] = "NA";
		}
		#endregion LeftHand Wear

		#region RightHand Wear
		//RightHand
		if ( _DK_RPG_UMA._Equipment._RightHand.Slot != null ) {
			data["EquipRightHandSlot"] = _DK_RPG_UMA._Equipment._RightHand.Slot.name;
			data["EquipRightHandColor"] = _DK_RPG_UMA._Equipment._RightHand.Color;
			if ( _DK_RPG_UMA._Equipment._RightHand.ColorPreset ) data["EquipRightHandColorPreset"] = _DK_RPG_UMA._Equipment._RightHand.ColorPreset.ColorPresetName;
			else data["EquipRightHandColorPreset"] = "NA";
		}
		else {
			data["EquipRightHandSlot"] = "NA";
		}
		if ( _DK_RPG_UMA._Equipment._RightHand.Overlay != null ) {
			data["EquipRightHandOverlay"] = _DK_RPG_UMA._Equipment._RightHand.Overlay.name;
			data["EquipRightHandColor"] = _DK_RPG_UMA._Equipment._RightHand.Color;
			if ( _DK_RPG_UMA._Equipment._RightHand.ColorPreset ) data["EquipRightHandColorPreset"] = _DK_RPG_UMA._Equipment._RightHand.ColorPreset.ColorPresetName;
			else data["EquipRightHandColorPreset"] = "NA";
			if ( _DK_RPG_UMA._Equipment._RightHand.Opt01Color ) data["EquipRightHandOpt01"] = _DK_RPG_UMA._Equipment._RightHand.Opt01Color.ColorPresetName;
			else data["EquipRightHandOpt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._RightHand.Opt02Color ) data["EquipRightHandOpt02"] = _DK_RPG_UMA._Equipment._RightHand.Opt02Color.ColorPresetName;
			else data["EquipRightHandOpt02"] = "NA";
			// dirt
			if ( _DK_RPG_UMA._Equipment._RightHand.Dirt01Color ) data["EquipRightHandDirt01"] = _DK_RPG_UMA._Equipment._RightHand.Dirt01Color.ColorPresetName;
			else data["EquipRightHandDirt01"] = "NA";
			if ( _DK_RPG_UMA._Equipment._RightHand.Dirt02Color ) data["EquipRightHandDirt02"] = _DK_RPG_UMA._Equipment._RightHand.Dirt02Color.ColorPresetName;
			else data["EquipRightHandDirt02"] = "NA";
		}
		else {
			data["EquipRightHandOverlay"] = "NA";
			data["EquipRightHandColor"] = "NA";
			data["EquipRightHandColorPreset"] = "NA";
			data["EquipRightHandOpt01"] = "NA";
			data["EquipRightHandOpt02"] = "NA";
			data["EquipRightHandDirt01"] = "NA";
			data["EquipRightHandDirt02"] = "NA";
		}
		#endregion RightHand Wear

		#endregion Equipment

		// Add DNA
		foreach ( DKRaceData.DNAConverterData DNA in _DK_RPG_UMA.GetComponent<DKUMAData>().DNAList2 ) {
			data[DNA.Name] = DNA.Name;
			data[DNA.Name+"Value"] = DNA.Value;
		}
		
		// finalize by Saving the data
		data.Save( _DK_RPG_UMA, ToFile, ToDB );

		SaveDNA ( _DK_RPG_UMA );
	//	_DK_RPG_UMA.saveData = data;
	//	SaveToStream ( _DK_RPG_UMA );

	//	Debug.Log ("DK UMA : Saving "+CharacterName+" at "+Application.streamingAssetsPath+"\\"+CharacterName+".uml" );
	//	Debug.Log ("SaveToStream : "+data.ToString() );
	}

	public static void SaveDNA ( DK_RPG_UMA _DK_RPG_UMA ){
		//Create data instance
		SaveData data = new SaveData("AvatarDNA");

		// Add DNA
		foreach ( DKRaceData.DNAConverterData DNA in _DK_RPG_UMA.GetComponent<DKUMAData>().DNAList2 ) {
			data[DNA.Name] = DNA.Name;
			data[DNA.Name+"Value"] = DNA.Value;
		}

		// finalize by Saving the data
		data.SaveDNAStream( _DK_RPG_UMA );
	}

	public static void SaveToStream ( DK_RPG_UMA avatar ) {	
	//	#if UNITY_5_5_OR_NEWER
		avatar.SavedRPGStreamed = JsonUtility.ToJson(avatar._Avatar._Face);
	//	#else
	//	avatar.SavedRPGStreamed = JsonMapper.ToJson(avatar._Avatar._Face);
	//	#endif
		Debug.Log ( "DK UMA RPG Avatar: SaveToStream : "+avatar.SavedRPGStreamed );
	}
}
