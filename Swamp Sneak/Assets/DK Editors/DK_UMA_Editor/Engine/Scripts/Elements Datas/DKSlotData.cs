using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class DKSlotData : ScriptableObject
{
	[Tooltip("This name is used to store and retrieve the DK UMA Slot in the DK Slot Library. " +
		"This name should be exactly the same as the linked UMA Slot. Never modify it.")]
    public string slotName;
	[Tooltip("This is the link to the original UMA Slot. it is linked during the creation of the DK UMA Slot and should never be modified.")]
	public UMA.SlotDataAsset _UMA;
	[Tooltip("This is the link to the Physics UMA Slot used for clothes and ragdoll.")]
	public UMA.SlotDataAsset PhysicsUMASlot;
	public string _UMAslotName = "";
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
	public string Gender = "";
	[Tooltip("A distinction for the element, used for sorting it for the RPG Engine to be able to use it. " +
		"Use the DK UMA Editor window to assign or modify it. It can not be left blank. " +
		"Learn how to use it on the forum.")]
	public string OverlayType = "";
	[Tooltip("The weight of the element used during the random avatar(s) creation. Use the DK UMA Editor window to assign or modify it. It can not be left blank.")]
	public string WearWeight = "";
	[Tooltip("The default elements are able to be displayed or hidden in the 'Elements Manager' window.")]
	public bool Default = false;
	[Tooltip("Does this element cover an entier anatomy part ? The anatomy part can be skipped during the generation of the avatar" +
		" to avoid some visual glitches about vertexes superposition. " +
		"Use the DK UMA Editor window to set it. " +
		"Learn how to use it on the forum.")]
	public bool Replace = false;

	/*
	public bool HideMouth = false;
	public bool HideEars = false;
	public bool HideBeard = false;
	public bool HideHair = false;
	public bool HideHairModule = false;
	public bool HideShoulders = false;
	public bool HideLegs = false;
	public bool HideBelt = false;
	public bool HideArmBand = false;
	public bool HideWrist = false;
	*/

	[System.Serializable]
	public class HideData{
		public bool HideMouth = false;
		public bool HideEars = false;
		public bool HideBeard = false;
		public bool HideHair = false;
		public bool HideHairModule = false;
		public bool HideShoulders = false;
		public bool HideLegs = false;
		public bool HideBelt = false;
		public bool HideArmBand = false;
		public bool HideWrist = false;
		public bool HideCollar = false;
		public bool HideLegBand = false;
		public bool HideRingLeft = false;
		public bool HideRingRight = false;
		public bool HideCloak = false;
		public bool HideBackpack = false;
		public bool HideUnderwear = false;
	}
	[Tooltip("Does this element covering other face parts, equipment or clothes ? The face parts, equipment or clothes can be skipped during the generation of the avatar" +
		" to avoid some visual glitches about vertexes superposition. " +
		"Use the DK UMA Editor window to set it. " +
		"Learn how to use it on the forum.")]
	public HideData _HideData = new HideData();
	[Tooltip("Linked overlays are overlays assigned to a slot. This slot will only use this overlay of randomize one of them if multiple." +
		"Use the DK UMA Editor window to set it. Do not modify it manually. " +
		"Learn how to use it on the forum.")]
	public List<DKOverlayData> LinkedOverlayList = new List<DKOverlayData>();

	[System.Serializable]
	public class LODData {
		public bool IsLOD0 = true;
		public DKSlotData LOD1;
		public DKSlotData LOD2;
		public DKSlotData LOD3;
		public DKSlotData MasterLOD;

	}
	[Tooltip("DK UMA can manage up to 3 LOD slots for a single slot. If this slot is the most detailled one, it is LOD0 (by default). " +
		". Assigning some less detailled slots (of the same simplified mesh) as LOD1, LOD2 or LOD3 will assign this current slot to them as the MasterLOD." +
		"Use the DK UMA Editor window to set it. Do not modify it manually. " +
		"Learn how to use it on the forum.")]
	public LODData _LOD = new LODData();

	[System.Serializable]
	public class LegacyData{
		public bool HasLegacy = false;
		public List<DKSlotData> LegacyList = new List<DKSlotData>();
		public bool IsLegacy = false;
		public List<DKSlotData> ElderList = new List<DKSlotData>();
		public bool Replace = false;
	}
	[Tooltip("A Legacy Slot is a slot generated by another slot(Elder). " +
		"You can assign as many slots (legacy) you want to a slot (becoming an Elder), it will generate them in the list order. " +
		"An Elder can not be assigned as a Legacy. A Legacy can be assigned to multiple Elders. Every Legacy slots have a list of its Elders. If you want some Elder to be a Legacy, duplicate it, " +
		"rename the asset in your project, then assign it as a Legacy." +
		"Excepted for the duplication of an Elder, use the DK UMA Editor window to set it. Do not modify it manually. " +
		"Learn how to use it on the forum.")]
	public LegacyData _LegacyData = new LegacyData();

	[HideInInspector]
	public float overlayScale = 1.0f;

	[HideInInspector]
	public bool Elem = false;
	[System.NonSerialized]
	public int listID = -1;
	[HideInInspector]
	public SkinnedMeshRenderer meshRenderer;

	public Sprite Preview;
	[HideInInspector]
	public Material materialSample;
	[HideInInspector]
	public Transform[] umaBoneData;
//	[HideInInspector]
	public List<DKOverlayData> overlayList = new List<DKOverlayData>();

	public DKSlotData Duplicate()
    {
		DKSlotData tempSlotData = CreateInstance<DKSlotData>();

        tempSlotData.slotName = slotName;
        tempSlotData.listID = listID;
        tempSlotData.overlayScale = overlayScale;

        // All this data is passed as reference
    //    tempSlotData.meshRenderer = meshRenderer;

        tempSlotData.umaBoneData = umaBoneData;

        //Overlays are duplicated, to lose reference
        for (int i = 0; i < overlayList.Count; i++)
        {
            tempSlotData.overlayList.Add(overlayList[i].Duplicate());
        }

        return tempSlotData;
    }

    public DKSlotData()
    {

    }

    public DKSlotData(DKSlotLibrary _slotLibrary, string elementName)
    {
		DKSlotData source;
        if (!_slotLibrary.slotDictionary.TryGetValue(elementName, out source))
        {
# if Editor
			Debug.LogError("Unable to find DKSlotData " + elementName);
# endif
            this.slotName = elementName;
            return;
        }

        this.slotName = source.slotName;
        this.listID = source.listID;

        // All this data is passed as reference
    //    this.meshRenderer = source.meshRenderer;
        //this.shader = source.shader;
    //    this.materialSample = source.materialSample;
        this.overlayScale = source.overlayScale;
        this.umaBoneData = source.umaBoneData;
   //     this.slotDNA = source.slotDNA;

        //Overlays are duplicated, to lose reference
        for (int i = 0; i < source.overlayList.Count; i++)
        {
            this.overlayList.Add(source.overlayList[i].Duplicate());
        }
    }

	public DKSlotData(DKSlotLibrary _slotLibrary, string elementName, Color color)
        : this(_slotLibrary, elementName)
    {
        var source = _slotLibrary.slotDictionary[elementName];

        this.overlayList[0] = source.overlayList[0].Duplicate();
        this.overlayList[0].color = color;
    }

    internal bool RemoveOverlay(params string[] names)
    {
        bool changed = false;
        foreach (var name in names)
        {
            for (int i = 0; i < overlayList.Count; i++)
            {
                if (overlayList[i].overlayName == name)
                {
                    overlayList.RemoveAt(i);
                    changed = true;
                    break;
                }
            }
        }
        return changed;
    }

    internal bool SetOverlayColor(Color32 color, params string[] names)
    {
        bool changed = false;
        foreach (var name in names)
        {
            foreach (var overlay in overlayList)
            {
                if (overlay.overlayName == name)
                {
                    overlay.color = color;
                    changed = true;
                }
            }
        }
        return changed;
    }

    internal DKOverlayData GetOverlay(params string[] names)
    {
        foreach (var name in names)
        {
            foreach (var overlay in overlayList)
            {
                if (overlay.overlayName == name)
                {
                    return overlay;
                }
            }
        }
        return null;
    }

	#if UNITY_EDITOR
	public void SaveAsset (){
		EditorUtility.SetDirty (this);
		AssetDatabase.SaveAssets ();
	}
	#endif
}
