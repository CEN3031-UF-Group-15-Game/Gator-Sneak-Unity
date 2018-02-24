using UnityEngine;
//using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class DK_UMA_Item : ScriptableObject {
	[Tooltip("Is the Item available in game ?")]
	public bool Active = true;

	public DK_UMA_GameSettings DKGameDatabase;
	public List<string> RacesNamesList = new List<string>();

	[System.Serializable]
	public class ElementData {
		[Tooltip("The icon (Sprite) for the item used in an inventory.")]
		public Sprite Icon;
		[Tooltip("The DK Slot used by the item. It can be left blank, if blank, the assigned DK Overlay will be used.")]
		public DKSlotData Slot;
		[Tooltip("The DK Overlay used by the Slot of the item OR the Overlay used by the Item if no Slot is assigned. " +
			"If a Slot is assigned and no Overlay is assigned, the Slot requires to have a Linked Overlay in its own settings.")]
		public DKOverlayData Overlay;
		[Tooltip("The Color Preset assigned to the Overlay.")]
		public ColorPresetData ColorPreset;
		[Tooltip("")]
		public Color color;
		[Tooltip("A DK Overlay can use two stacked overlays to cover it. Those stacked overlays are managed by the Item Overlay.")]
		public ColorPresetData Stacked1Color;
		[Tooltip("A DK Overlay can use two stacked overlays to cover it. Those stacked overlays are managed by the Item Overlay.")]
		public ColorPresetData Stacked2Color;
		[Tooltip("")]
		public ColorPresetData Dirt1Color;
		public ColorPresetData DirtColor;
	}

	public enum GenderEnum { Both, Female, Male };
	public enum OtherLayersEnum { Yes, No };
	public enum LayerEnum { Sub, Main, Cover };
	public enum TypeEnum { DK_UMA, Other };
	public enum PlaceEnum { Head, Shoulder, Torso, ArmBand, Wrist, Hand, Belt, Legs, LegBand, Feet, Backpack, Cloak, Collar, RingLeft, RingRight, LeftHand, RightHand, Glasses, Mask };

	[System.Serializable]
	public class OptionsData {
		[Tooltip("")]
		public TypeEnum Type = TypeEnum.DK_UMA;
		[Tooltip("The place of the item on the avatar.")]
		public PlaceEnum Place = PlaceEnum.Head;
		[Tooltip("The gender able to use the item.")]
		public GenderEnum Gender = GenderEnum.Both;
		[Tooltip("The Layer to apply the item on the avatar.")]
		public LayerEnum Layer = LayerEnum.Main;
		[Tooltip("Does the item removes the other items of the other layers at the same Place ?")]
		public OtherLayersEnum RemoveOtherLayers = OtherLayersEnum.Yes;
	}
	public OptionsData Options = new OptionsData();

	[System.Serializable]
	public class ElementsData {
		public ElementData Male = new ElementData();
		public ElementData Female = new ElementData();
	}
	public ElementsData Elements = new ElementsData();

	DKSlotData slot;
	DKOverlayData overlay;
	Color color;
	ColorPresetData colorPreset;

	public void CreateSprite (){
		
	}

	public void EquipItemToAvatar ( DK_RPG_UMA avatar ) {
		// for a DK UMA item
		if ( Options.Type.ToString() == "DK_UMA" && avatar != null  ) ElementPreparations ( avatar );
	}

	void ElementPreparations (DK_RPG_UMA avatar){
		ColorPresetData Opt1 = null;
		ColorPresetData Opt2 = null;

		#region For Male Avatar
		// Slots : verify if the Element is available for a male avatar
		if ( avatar.Gender == "Male" ){
			// Slot Only Element
			if ( Elements.Male.Slot != null ){
				// verify if the Element is available for the race of the avatar
				if ( Elements.Male.Slot.Race.Contains(avatar.Race) ){
					// assign the tmp values
					slot = Elements.Male.Slot;
					overlay = Elements.Male.Overlay;
					Opt1 = Elements.Male.Stacked1Color;
					Opt2 = Elements.Male.Stacked2Color;
				}
				else Debug.Log ("This element ("+ Elements.Female.Slot.name+") is not defined for the race of the avatar.");
			}

			// Overlay only element
			else if ( Elements.Male.Overlay ) {
				// verify if the Element is available for the race of the avatar
				if ( Elements.Male.Overlay.Race.Contains(avatar.Race) ){

					// assign the tmp values
					slot = null;
					overlay = Elements.Male.Overlay;
					Opt1 = Elements.Male.Stacked1Color;
					Opt2 = Elements.Male.Stacked2Color;

				}
			}
			colorPreset = Elements.Male.ColorPreset;
		}
		#endregion For Male Avatar
		else
			#region For Female Avatar
			// Slots : verify if the Element is available for a Female avatar
			if ( avatar.Gender == "Female" ){

				if ( Elements.Female.Slot != null ){
					// verify if the Element is available for the race of the avatar
					if ( Elements.Female.Slot.Race.Contains(avatar.Race) ){
						slot = Elements.Female.Slot;
						overlay = Elements.Female.Overlay;
						Opt1 = Elements.Female.Stacked1Color;
						Opt2 = Elements.Female.Stacked2Color;
					}
					else Debug.Log ("This element ("+ Elements.Female.Slot.name+") is not defined for the race of the avatar.");
				}
				// Overlays : verify if the Element is available for a Female avatar
				else if ( Elements.Female.Overlay ) {
					if ( Elements.Female.Overlay.Race.Contains(avatar.Race) ){
						slot = null;
						overlay = Elements.Female.Overlay;
						Opt1 = Elements.Female.Stacked1Color;
						Opt2 = Elements.Female.Stacked2Color;
					}
				}
				colorPreset = Elements.Female.ColorPreset;					
			}
		#endregion For Female Avatar

		#region set the color
		if ( color == Color.black ) {
			// using the assigned colorpreset
			if (colorPreset != null ) {
			//	float adjust = UnityEngine.Random.Range(0.01f, 0.5f);
				color = colorPreset.PresetColor; // + new Color (adjust,adjust,adjust);
			}

			// choose a colorpreset from the overlay if possible
			else if ( overlay && overlay.ColorPresets.Count > 0 ) {
				int ran = UnityEngine.Random.Range(0, overlay.ColorPresets.Count-1);
				float adjust = UnityEngine.Random.Range(0.01f, 0.5f);
				color = overlay.ColorPresets[ran].PresetColor + new Color (adjust,adjust,adjust);
			}

			// assign the white color if the previous possibilities do not match
			else color = new Color(1,1,1,1);
		}
		#endregion set the color

		#region What to do ?
		// for a DK UMA item
		if ( Options.Type.ToString() == "DK_UMA" )
			DK_UMA_RPG_Equip.EquipSlotElement (slot, overlay, avatar, colorPreset, color, Options.Layer.ToString(), Options.RemoveOtherLayers.ToString(), Opt1, Opt2 );
		#endregion What to do ?
	}

	public void RemoveItem ( DK_RPG_UMA avatar ){

		// for a DK UMA item
		if ( Options.Type.ToString() == "DK_UMA" )
			DK_UMA_RPG_Equip.RemoveElement ( slot, overlay, avatar, Options.Layer.ToString(), Options.RemoveOtherLayers.ToString() );
	
	}

	// set the color preset of both gender by the index of the color preset in ColorPresets list of the DK UMA Overlay assigned to the DK UMA Slot
	public void SetColorPreset ( int index ){
		if ( Options.Gender == GenderEnum.Both || Options.Gender == GenderEnum.Female ){
			Elements.Female.ColorPreset = overlay.ColorPresets[index];
		}
		if ( Options.Gender == GenderEnum.Both || Options.Gender == GenderEnum.Male ){
			Elements.Male.ColorPreset = overlay.ColorPresets[index];
		}
	}

	#if UNITY_EDITOR
	public void SaveAsset (){
		EditorUtility.SetDirty (this);
		AssetDatabase.SaveAssets ();
		Debug.Log ("DK UMA Item '"+this.name+"' saved into the project.");
	}
	#endif
}
