using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class DKOverlayData : ScriptableObject
{
	[Tooltip("This name is used to store and retrieve the DK UMA Overlay in the DK Overlay Library. " +
		"This name should be exactly the same as the linked UMA Overlay. Never modify it.")]
    public string overlayName;
	[Tooltip("This is the link to the original UMA Overlay. it is linked during the creation of the DK UMA Overlay and should never be modified.")]
	public UMA.OverlayDataAsset _UMA;
	public string _UMAoverlayName = "";
	[Tooltip("Active means can be used by the DK UMA Engine.")]
	public bool Active = true;
	[Tooltip("Premium and Ingame Creator Only. Is this element available during the creation of an avatar using the Ingame Creator ? " +
		"Only affect the equipment and clothes.")]
	public bool AvailableAtStart = false;
	[Tooltip("The place on the anatomy of the avatar for the element to be generated. Use the DK UMA Editor window to assign or modify it. " +
		"It can not be left blank. "+
		"Learn how to use it on the forum.")]
	public DK_SlotsAnatomyElement Place;
	public string PlaceName = "";
	[Tooltip("All the DK UMA races names able to use the element. Use the DK UMA Editor window to assign or modify it. It can not be left umpty.")]
	public List<string> Race = new List<string>();
	[Tooltip("The gender able to use the element. Use the DK UMA Editor window to assign or modify it. It can not be left blank.")]
	public string Gender;
	[Tooltip("A distinction for the element, used for sorting it for the RPG Engine to be able to use it. " +
		"Use the DK UMA Editor window to assign or modify it. It can not be left blank. " +
		"Learn how to use it on the forum.")]
	public string OverlayType;
	[Tooltip("The weight of the element used during the random avatar(s) creation. Use the DK UMA Editor window to assign or modify it. It can not be left blank.")]
	public string WearWeight;
	[Tooltip("The default elements are able to be displayed or hidden in the 'Elements Manager' window.")]
	public bool Default = false;

	[Tooltip("All the Color Presets assigned to this element used as an equipment of clothes (body colors are managed by the race). " +
		"Use the DK UMA Editor window to assign or modify it.")]
	public List<ColorPresetData> ColorPresets = new List<ColorPresetData>();
	[Tooltip("Optional overlays are also called Stacked overlays, some UMA Slots are designed to be used with more than one Overlay, " +
		"DK UMA offers the possibility to use up to 3 overlays for a single slot. " +
		"Use the DK UMA Editor window to assign or modify it.")]
	public DKOverlayData Opt01;
	[Tooltip("Optional overlays are also called Stacked overlays, some UMA Slots are designed to be used with more than one Overlay, " +
		"DK UMA offers the possibility to use up to 3 overlays for a single slot. " +
		"Use the DK UMA Editor window to assign or modify it.")]
	public DKOverlayData Opt02;
	[Tooltip("A dirt overlay to apply on the UMA Slots, " +
		"DK UMA offers the possibility to use up to 2 dirt overlays for a single slot. " +
		"Use the DK UMA Editor window to assign or modify it.")]
	public DKOverlayData Dirt01;
	[Tooltip("A dirt overlay to apply on the UMA Slots, " +
		"DK UMA offers the possibility to use up to 2 dirt overlays for a single slot. " +
		"Use the DK UMA Editor window to assign or modify it.")]
	public DKOverlayData Dirt02;
	/*
	public List<DKOverlayData> StackedOverlays = new List<DKOverlayData>();
	public List<DKOverlayData> DirtOverlays = new List<DKOverlayData>();
*/

//	[HideInInspector]
	public List<DKSlotData> LinkedToSlot = new List<DKSlotData>();
	[System.NonSerialized]
	public int listID;
	[HideInInspector]
	public Color color = new Color(1, 1, 1, 1);
//	public bool ForceRect = false;
	public enum RectFactorEnum { x1, x2, x3, x4, RectValues }
	[Tooltip("Use the 'Force Rect' option to apply the following values to the UMA element during the generation of the avatar.")]
	public RectFactorEnum RectFactor = RectFactorEnum.x1;
//	[HideInInspector]
	public Rect rect;
//	[HideInInspector]
	public Texture2D[] textureList = new Texture2D[0];
	[HideInInspector]
	public Color32[] channelMask;
	[HideInInspector]
	public Color32[] channelAdditiveMask;
	[System.NonSerialized]
	public DKUMAData umaData;
	[HideInInspector]
	public string[] tags;
	[HideInInspector]
	public bool Elem = false;
	public Sprite Preview;

	public DKOverlayData Duplicate()
    {
        DKOverlayData tempOverlay = CreateInstance<DKOverlayData>();
        tempOverlay.overlayName = overlayName;
        tempOverlay.listID = listID;
        tempOverlay.color = color;
        tempOverlay.rect = rect;
   //     tempOverlay.textureList = new Texture2D[textureList.Length];
    /*    for (int i = 0; i < textureList.Length; i++)
        {
            tempOverlay.textureList[i] = textureList[i];
        }*/

        return tempOverlay;
    }

    public DKOverlayData() {}

    public bool useAdvancedMasks { get { return channelMask != null && channelMask.Length > 0; } }
    public DKOverlayData(DKOverlayLibrary _overlayLibrary, string elementName) {

        DKOverlayData source;
        if (!_overlayLibrary.overlayDictionary.TryGetValue(elementName, out source))
        {
            Debug.LogError("Unable to find DKOverlayData " + elementName);
            this.overlayName = elementName;
            return;
        }

        this.overlayName = source.overlayName;
        this.listID = source.listID;
        this.color = new Color(source.color.r, source.color.g, source.color.b, color.a);
        this.rect = source.rect;
        this.textureList = new Texture2D[source.textureList.Length];
        for (int i = 0; i < textureList.Length; i++)
        {
            this.textureList[i] = source.textureList[i];
        }
    }


    public void SetColor(int overlay, Color32 color)
    {
        if (useAdvancedMasks)
        {
            channelMask[overlay] = color;
        }
        else if (overlay == 0)
        {
            this.color = color;
        }
        else
        {
       //     AllocateAdvancedMasks();
            channelMask[overlay] = color;
        }
        if (umaData != null)
        {
            umaData.Dirty(false, true, false);
        }
    }

    public void SetAdditive(int overlay, Color32 color)
    {
        if (!useAdvancedMasks)
        {
     //       AllocateAdvancedMasks();
        }
        channelAdditiveMask[overlay] = color;
        if (umaData != null)
        {
            umaData.Dirty(false, true, false);
        }
    }
	/*
    private void AllocateAdvancedMasks()
    {
        int channels = umaData != null ? umaData.umaGenerator.textureNameList.Length : 2;
        channelMask = new Color32[channels];
        channelAdditiveMask = new Color32[channels];
        for (int i = 0; i < channels; i++)
        {
            channelMask[i] = new Color32(255, 255, 255, 255);
            channelAdditiveMask[i] = new Color32(0, 0, 0, 0);
        }
        channelMask[0] = color;

    }
*/

	#if UNITY_EDITOR
	public void SaveAsset (){
		EditorUtility.SetDirty (this);
		AssetDatabase.SaveAssets ();
	}
	#endif
}