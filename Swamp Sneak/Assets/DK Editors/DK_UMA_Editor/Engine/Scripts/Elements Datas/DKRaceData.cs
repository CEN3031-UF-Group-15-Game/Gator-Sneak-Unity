using UnityEngine;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

[Serializable]
public class DKRaceData : ScriptableObject {
	[Tooltip("This name is used to store and retrieve the race in the DK Race Library. Do not modify it without using the Race Editor (Premium only).")]
	public string raceName = "";
	[Tooltip("This is UMA Race linked to this DK UMA Race. If left blank, the default Human race of the same gender will be used to generate the avatars. " +
		"Do not modify it without using the Race Editor (Premium only).")]
	public UMA.RaceData UMA;
	public string UMAraceName = "";
	[Tooltip("This is the name declared in every DK UMA Slots and DK UMA Overlays able to be used by this race. Do not modify.")]
	public string Race = "";
	[Tooltip("The gender of this race. Each race must have 2 DKRaceData objects, one for males and one for Females. " +
		"Do not modify it without using the Race Editor (Premium only).")]
	public string Gender = "";
	[Tooltip("Active means can be used by the DK UMA Engine.")]
	public bool Active = true;
	[System.Serializable]
	public class Male{
		public DK_RPG_UMA_Generator.AvatarData _AvatarData;
		public DK_RPG_UMA_Generator.EquipmentData _EquipmentData;
	}
	[Tooltip("Contains all the DK UMA Slots and DK UMA Overlays usable by this race and the corresponding Gender. " +
		"It is prepared using 'Add to Libraries' option of the 'Elements Manager' window. " +
		"It is also prepare at the loading of the scene to prevent any mistake about it from the user. " +
		"Never modify it.")]
	public Male _Male = new Male();

	[System.Serializable]
	public class Female{
		public DK_RPG_UMA_Generator.AvatarData _AvatarData;
		public DK_RPG_UMA_Generator.EquipmentData _EquipmentData;
	}
	[Tooltip("Contains all the DK UMA Slots and DK UMA Overlays usable by this race and the corresponding Gender. " +
		"It is prepared using 'Add to Libraries' option of the 'Elements Manager' window. " +
		"It is also prepare at the loading of the scene to prevent any mistake about it from the user. " +
		"Never modify it.")]
	public Female _Female = new Female();

	[System.Serializable]
	public class DNAConverterData{
		public string Name;
		public float Value;
		public string Part;
		public string Part2;
		public string Type;
	}
	[System.Serializable]
	public class AgeDNAData{
		public string Name;
		public float Age;
		public float Modif;
		public string Part;
		public string Part2;
		public string Type;
	}
	[System.Serializable]
	public class ConverterData{
		public string Name;
		public int LocalizedIndex = -1;
		public float ValueMini;
		public float ValueMaxi;
		public string Part;
		public string Part2;
		public string Type;
		public string LinkedTo;
	}
	[System.Serializable]
	public class Conv{
		public string Name;
		public int Index;

	}
	[System.Serializable]
	public class ClampData{
		public int Type;
		public int Index;
		//	public float Value;
		public Conv Conv1;
		public Conv Conv2;
		public Conv Conv3;
		public Vector3 XYZ;
		public Quaternion WXYZ1;
		public Quaternion WXYZ2;

	}

	[System.Serializable]
	public class BoneData{
		public string Name;
		public bool Active;
		public bool UseClamps;
		public ClampData Clamp1;
		public ClampData Clamp2;
		public ClampData Clamp3;
		public bool IsClone;
		public int LinkedIndex;
		public string LinkedTo;
	}



	[Tooltip("All the DNA modifiers used by this race. It contains the mini and maxi values, also contains some information about it. " +
		"Do not modify it without using the Race Editor (Premium only).")]
	public List<ConverterData> ConverterDataList = new List<ConverterData>();

	[Tooltip("All the Color Presets assigned to this race, for the skin, eyes, hair etc. " +
		"Do not modify it without using the Race Editor (Premium only).")]
	public List<ColorPresetData> ColorPresetDataList = new List<ColorPresetData>();
	public Dictionary<Type, System.Action<DKUMAData>> raceDictionary = new Dictionary<Type, System.Action<DKUMAData>>();

	[HideInInspector] public List<DNAConverterData> DNAConverterDataList = new List<DNAConverterData>();
	[HideInInspector] public List<BoneData> BoneDataList = new List<BoneData>();
	[HideInInspector] public DKDnaConverterBehaviour[] dnaConverterList /*= new DKDnaConverterBehaviour[0]*/;
	[HideInInspector]  public String[] AnimatedBones/* = new string[0]*/;
	[HideInInspector] public GameObject racePrefab;
	[HideInInspector] public UMA.UmaTPose TPose;


    void Awake()
    {
        UpdateDictionary();
    }

    public void UpdateDictionary()
    {
        raceDictionary.Clear();
        for (int i = 0; i < dnaConverterList.Length; i++)
        {
            if (dnaConverterList[i])
            {
                if (!raceDictionary.ContainsKey(dnaConverterList[i].DNAType))
                {
                    raceDictionary.Add(dnaConverterList[i].DNAType, dnaConverterList[i].ApplyDnaAction);
                }
            }
        }
    }

	public void ResetRaceAllLists (){
		_Male = new Male();
		_Female = new Female();
	}

    internal void UpdateAnimatedBones()
    {
        throw new NotImplementedException();
    }

	#if UNITY_EDITOR
	public void SaveAsset (){
		EditorUtility.SetDirty(this);
		AssetDatabase.SaveAssets();
	}
	#endif
}