using UnityEngine;
# if Editor
using UnityEditor;
# endif
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using UMA;

// Require these components when using this script
//[RequireComponent(typeof(TransposeDK2UMA))]
//[RequireComponent(typeof(DKUMAData))]


public class DK_RPG_UMA : MonoBehaviour {
	#region Variables
	public string Name = "";
	[Tooltip("Do not modify manually. It is defined during the creation of the avatar. All the slots and overlays composing the avatar are dependent of the Gender." +
		"It can only be modified by loading a saved avatar from a saved file or from a DB object.")]
	public string Gender = "";
	[Tooltip("Do not modify manually. It is defined during the creation of the avatar. All the slots and overlays composing the avatar are dependent of the Race." +
		"It can only be modified by loading a saved avatar from a saved file or from a DB object.")]
	public string Race = "";
	[Tooltip("Do not modify manually. It is defined during the creation of the avatar. All the slots and overlays composing the avatar are dependent of the RaceData." +
		"It can only be modified by loading a saved avatar from a saved file or from a DB object.")]
	public DKRaceData RaceData;

	[HideInInspector] public string Weight = "";

	public enum AnatomyChoice {CompleteAvatar, NoHead, NoHeadNoArmsNoHands};

	public enum LoadFromChoice {None, File, Data};
	[HideInInspector] public LoadFromChoice LoadFrom = new LoadFromChoice();

	[HideInInspector] public bool UsePhysics = false;

	[System.Serializable]
	public class AnatomyRuleData{
		public AnatomyChoice AnatomyToCreate = new AnatomyChoice();
	}
	[Tooltip("To use a DK UMA avatar with a first person controller. This option is used to generate a full avatar or to hide the head," +
		" the hands and to reduce the arms to the minimum possible.")]
	public AnatomyRuleData AnatomyRule = new AnatomyRuleData();

	public bool IsPlayer = false;
	// used to load a local RPG avatar from a file
	[Tooltip("Premium and Ingame Creator Only. Using the Ingame Creator you can save an avatar to a file. Is the avatar RPG Definition loaded from a saved file ? " +
		"If so, write the name of the file in the 'File Name' field.")]
	public bool LoadFromFile = false;
	[Tooltip("The name of the saved file of the avatar RPG Definition.")]
	public string FileName = "";
	// used to load a local RPG avatar from Database
	[Tooltip("Premium and Ingame Creator Only. Using the Ingame Creator you can save an avatar to a ScriptableObject called 'DK_UMA_AvatarData'. " +
		"You can assign one of the Data object here to quickly assign an avatar. It is useful for the NPCs.")]
	public DK_UMA_AvatarData AvatarFromDB;
	// used to send the avatar in network
	[Tooltip("The Streamed UMA is the packed recipe of the avatar. " +
		"It is unpacked by the 'Transpose2UMA' component and sent to the UMADynamicAvatar component of the avatar and stored in its UmaRecipe. " +
		"It is also the stream to use for a network.")]
	public string DkUmaStreaming = "";
	[HideInInspector] public string DkUmaDNAStream = "";
	[HideInInspector] public string UmaStream = "";
//	[HideInInspector] 
	public string SavedRPGStreamed = "";
	[HideInInspector] public bool AvatarLoaded;
	[Tooltip("This is the packed DK UMA DNA of the avatar. " +
		"It is the DNA stream to use for a network.")]
	public string SavedDNAStream = "";

	[System.Serializable]
	public class DKUMAPackedDNA{
		public List<DKUMAPackedDna> packedDna = new List<DKUMAPackedDna>();
	}

	[System.Serializable]
	public class DKUMAPackedDna
	{
		public string N;
		public float V;
	}
	[HideInInspector]
	public bool CreateIt;
	[HideInInspector]
	public float Distance = 0;
	[System.Serializable]
	public class LODData {
		public bool UseLOD = true;
		[HideInInspector] public bool EnabledLOD = false;
		public float Frequency = 0.1f;
		public int LOD0Resolution = 2048;
		public int LOD1Distance = 5;
		public int LOD1Resolution = 1024;
		public int LOD2Distance = 10;
		public int LOD2Resolution = 512;
		public int LOD3Distance = 15;
		public int LOD3Resolution = 256;
		public int LOD4Distance = 25;
		public int LOD4Resolution = 128;
		[Tooltip("The distance is between the LOD Viewer and the avatar. If left blank the LOD Viewer is the main Camera of the scene.")]
		public GameObject LODViewer;
		[HideInInspector] public string OldRaceName = "";
	}
	[Tooltip("DK UMA is using 5 levels of details for the textures. Set the frequency, the distances and the atlas resolution.")]
	public LODData _LOD = new LODData();

	[HideInInspector] 
	public UMA.UMAData _UMA_Avatar;

	[System.Serializable]
	public class EquipmentSetData {
		[Tooltip("Equip an Equipment Set at loading ?")]
		public bool UseDKEquipmentSet = true;
		[Tooltip("Select an Equipment Set to equip.")]
		public DKEquipmentSetData DKEquipmentSet;
		[HideInInspector] public bool SetLoaded = false;

	}
	[Tooltip("Enable this option and assign an Equipment Set for the avatar to equip it at loading. It is useful to quickly prepare some NPC.")]
	public EquipmentSetData EquipmentSet = new EquipmentSetData ();

	#region avatar Meshs and Textures
	[System.Serializable]
	public class AvatarData {
		[HideInInspector] public int HeadIndex = 0;
		[HideInInspector] public int TorsoIndex = 0;
	//	[HideInInspector] 
		public Color SkinColor;
	//	[HideInInspector] 
		public ColorPresetData SkinColorPreset;
	//	[HideInInspector] 
		public Color HairColor;
	//	[HideInInspector] 
		public ColorPresetData HairColorPreset;
	//	[HideInInspector] 
		public Color EyeColor;
	//	[HideInInspector] 
		public ColorPresetData EyeColorPreset;

		#region Face
		[System.Serializable]
		public class FaceData {
			#region Head
			[System.Serializable]
			public class HeadData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public DKOverlayData Tattoo;
				public Color TattooColor;
				public DKOverlayData Makeup;
				public Color MakeupColor;
				public ColorPresetData TatooColorPreset;
				public ColorPresetData MakeupColorPreset;
				public DKOverlayData Optional;
				public Color OptionalColor;
			}
			public HeadData _Head = new HeadData();
			#endregion Head

			#region Horns
			[System.Serializable]
			public class HornsData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public ColorPresetData ColorPreset;
			}
			public HornsData _Horns = new HornsData();
			#endregion Horns

			#region FaceHair
			[System.Serializable]
			public class FaceHairData {
				public DKOverlayData EyeBrows;
				public Color EyeBrowsColor;

				[System.Serializable]
				public class BeardOvOnlyData {
					public DKOverlayData Beard1;
					public Color Beard1Color;
					public DKOverlayData Beard2;
					public Color Beard2Color;
					public DKOverlayData Beard3;
					public Color Beard3Color;
				}
				public BeardOvOnlyData _BeardOverlayOnly = new BeardOvOnlyData();

				[System.Serializable]
				public class BeardSlotOnlyData {
					public DKSlotData Slot;
					public DKOverlayData Overlay;
					public Color Color;
				}
				public BeardSlotOnlyData _BeardSlotOnly = new BeardSlotOnlyData();
				public BeardSlotOnlyData _BeardSlotOnly2 = new BeardSlotOnlyData();
				public BeardSlotOnlyData _BeardSlotOnly3 = new BeardSlotOnlyData();
			}
			public FaceHairData _FaceHair = new FaceHairData();
			#endregion FaceHair

			#region Eyes
			[System.Serializable]
			public class EyesData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public DKOverlayData Adjust;
				public Color AdjustColor;
				public DKOverlayData Optional;
				public Color OptionalColor;
			}
			public EyesData _Eyes = new EyesData();
			#endregion Eyes

			#region Eyes Iris
			[System.Serializable]
			public class EyesIrisData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public DKOverlayData Adjust;
				public Color AdjustColor;
			}
			public EyesIrisData _EyesIris = new EyesIrisData();
			#endregion Eyes Iris

			#region Eyebrows
			[System.Serializable]
			public class EyebrowsData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
			}
			public EyebrowsData _Eyebrows = new EyebrowsData();
			#endregion Eyebrows

			#region EyeLash
			[System.Serializable]
			public class EyeLashData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
			}
			public EyeLashData _EyeLash = new EyeLashData();
			#endregion EyeLash

			#region Lids
			[System.Serializable]
			public class EyeLidsData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public DKOverlayData Tattoo;
				public Color TattooColor;
				public DKOverlayData Makeup;
				public Color MakeupColor;
				public ColorPresetData TatooColorPreset;
				public ColorPresetData MakeupColorPreset;
				public DKOverlayData Optional;
				public Color OptionalColor;
			}
			public EyeLidsData _EyeLids = new EyeLidsData();
			#endregion Lids

			#region Ears
			[System.Serializable]
			public class EarsData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public DKOverlayData Tattoo;
				public Color TattooColor;
				public DKOverlayData Makeup;
				public Color MakeupColor;
				public ColorPresetData TatooColorPreset;
				public ColorPresetData MakeupColorPreset;
				public DKOverlayData Optional;
				public Color OptionalColor;

			}
			public EarsData _Ears = new EarsData();
			#endregion Ears

			#region Nose
			[System.Serializable]
			public class NoseData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public DKOverlayData Tattoo;
				public Color TattooColor;
				public DKOverlayData Makeup;
				public Color MakeupColor;
				public ColorPresetData TatooColorPreset;
				public ColorPresetData MakeupColorPreset;
				public DKOverlayData Optional;
				public Color OptionalColor;

			}
			public NoseData _Nose = new NoseData();
			#endregion Nose

			#region Mouth
			[System.Serializable]
			public class MouthData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public DKOverlayData Lips;
				public Color LipsColor;
				public DKOverlayData Tattoo;
				public Color TattooColor;
				public DKOverlayData Makeup;
				public Color MakeupColor;
				public ColorPresetData LipsColorPreset;
				public ColorPresetData TatooColorPreset;
				public ColorPresetData MakeupColorPreset;
				public DKOverlayData Optional;
				public Color OptionalColor;

				[System.Serializable]
				public class InnerMouthData {
					public DKSlotData Slot;
					public DKOverlayData Overlay;
					public Color color;
					public ColorPresetData ColorPreset;
				}
				public InnerMouthData _InnerMouth = new InnerMouthData();

			}
			public MouthData _Mouth = new MouthData();
			#endregion Mouth
		} 
		public FaceData _Face = new FaceData();
		#endregion Face

		#region Hair
		[System.Serializable]
		public class HairData {

			[System.Serializable]
			public class OvOnlyData {
				public DKOverlayData Overlay;
				public Color Color;

			}
			public OvOnlyData _OverlayOnly = new OvOnlyData();

			[System.Serializable]
			public class SlotOnlyData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;

				[System.Serializable]
				public class HairModuleData {
					public DKSlotData Slot;
					public DKOverlayData Overlay;
					public Color Color;

				}
				public HairModuleData _HairModule = new HairModuleData();
			}
			public SlotOnlyData _SlotOnly = new SlotOnlyData();
		} 
		public HairData _Hair = new HairData();
		public HairData _Hair2 = new HairData();
		public HairData _Hair3 = new HairData();
		#endregion Hair

		#region Body
		[System.Serializable]
		public class BodyData {
			[System.Serializable]
			public class TorsoData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public DKOverlayData Tattoo;
				public Color TattooColor;
				public DKOverlayData Makeup;
				public Color MakeupColor;
				public ColorPresetData TatooColorPreset;
				public ColorPresetData MakeupColorPreset;
				public DKOverlayData Optional;
				public Color OptionalColor;
			} 
			public TorsoData _Torso = new TorsoData();


			[System.Serializable]
			public class HandsData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public DKOverlayData Tattoo;
				public Color TattooColor;
				public DKOverlayData Makeup;
				public Color MakeupColor;
				public ColorPresetData TatooColorPreset;
				public ColorPresetData MakeupColorPreset;
				public DKOverlayData Optional;
				public Color OptionalColor;
			} 
			public HandsData _Hands = new HandsData();

			[System.Serializable]
			public class LegsData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public DKOverlayData Tattoo;
				public Color TattooColor;
				public DKOverlayData Makeup;
				public Color MakeupColor;
				public ColorPresetData TatooColorPreset;
				public ColorPresetData MakeupColorPreset;
				public DKOverlayData Optional;
				public Color OptionalColor;
			} 
			public LegsData _Legs = new LegsData();

			[System.Serializable]
			public class FeetData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public DKOverlayData Tattoo;
				public Color TattooColor;
				public DKOverlayData Makeup;
				public Color MakeupColor;
				public ColorPresetData TatooColorPreset;
				public ColorPresetData MakeupColorPreset;
				public DKOverlayData Optional;
				public Color OptionalColor;
			} 
			public FeetData _Feet = new FeetData();

			[System.Serializable]
			public class WingsData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public ColorPresetData ColorPreset;
			} 
			public WingsData _Wings = new WingsData();

			[System.Serializable]
			public class TailData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public ColorPresetData ColorPreset;
			} 
			public TailData _Tail = new TailData();

			[System.Serializable]
			public class UnderwearData {
				public DKSlotData Slot;
				public DKOverlayData Overlay;
				public Color Color;
				public ColorPresetData ColorPreset;
				public DKOverlayData Optional;
			} 
			public UnderwearData _Underwear = new UnderwearData();
		} 
		public BodyData _Body = new BodyData();
		#endregion Body
	} 
	public AvatarData _Avatar = new AvatarData();
	#endregion avatar Meshs and Textures

	#region Additional Equipment (Gear)
	[System.Serializable]
	public class GearLayerData {
		public DKSlotData Slot;
		public DKOverlayData Overlay;
		public Color Color;
		public ColorPresetData ColorPreset;
		public ColorPresetData Opt01Color;
		public ColorPresetData Opt02Color;
		public ColorPresetData Opt03Color;
		public ColorPresetData Opt04Color;
		public ColorPresetData Dirt01Color;
		public ColorPresetData Dirt02Color;
	} 

	[System.Serializable]
	public class GearData {
		public GearLayerData Sub = new GearLayerData();
		public GearLayerData Main = new GearLayerData();
		public GearLayerData Cover = new GearLayerData();
	} 
	#endregion Additional Equipment (Gear)


	#region Equipment
	[System.Serializable]
	public class EquipmentData {

		[System.Serializable]
		public class GlassesData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public GlassesData _Glasses = new GlassesData();

		[System.Serializable]
		public class MaskData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public MaskData _Mask = new MaskData();

		[System.Serializable]
		public class HeadSubData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public HeadSubData _HeadSub = new HeadSubData();

		[System.Serializable]
		public class HeadData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public HeadData _Head = new HeadData();

		
		[System.Serializable]
		public class HeadCoverData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public HeadCoverData _HeadCover = new HeadCoverData();

		#region Additional Equipment


		#endregion Additional Equipment


		[System.Serializable]
		public class ShoulderSubData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public ShoulderSubData _ShoulderSub = new ShoulderSubData();

		[System.Serializable]
		public class ShoulderData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public ShoulderData _Shoulder = new ShoulderData();

		[System.Serializable]
		public class ShoulderCoverData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public ShoulderCoverData _ShoulderCover = new ShoulderCoverData();

		[System.Serializable]
		public class TorsoSubData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public TorsoSubData _TorsoSub = new TorsoSubData();

		[System.Serializable]
		public class TorsoData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public TorsoData _Torso = new TorsoData();

		[System.Serializable]
		public class TorsoCoverData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public TorsoCoverData _TorsoCover = new TorsoCoverData();

		[System.Serializable]
		public class HandsSubData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public HandsSubData _HandsSub = new HandsSubData();

		[System.Serializable]
		public class HandsData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public HandsData _Hands = new HandsData();

		[System.Serializable]
		public class HandsCoverData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public HandsCoverData _HandsCover = new HandsCoverData();

		[System.Serializable]
		public class LegsSubData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public LegsSubData _LegsSub = new LegsSubData();

		[System.Serializable]
		public class LegsData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public LegsData _Legs = new LegsData();

		[System.Serializable]
		public class LegsCoverData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public LegsCoverData _LegsCover = new LegsCoverData();

		[System.Serializable]
		public class FeetSubData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public FeetSubData _FeetSub = new FeetSubData();

		[System.Serializable]
		public class FeetData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public FeetData _Feet = new FeetData();

		[System.Serializable]
		public class FeetCoverData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public FeetCoverData _FeetCover = new FeetCoverData();

		[System.Serializable]
		public class ArmBandSubData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public ArmBandSubData _ArmBandSub = new ArmBandSubData();

		[System.Serializable]
		public class ArmBandData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public ArmBandData _ArmBand = new ArmBandData();

		[System.Serializable]
		public class ArmBandCoverData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public ArmBandCoverData _ArmBandCover = new ArmBandCoverData();

		[System.Serializable]
		public class WristSubData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public WristSubData _WristSub = new WristSubData();

		[System.Serializable]
		public class WristData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public WristData _Wrist = new WristData();

		[System.Serializable]
		public class WristCoverData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public WristCoverData _WristCover = new WristCoverData();

		[System.Serializable]
		public class LegBandSubData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public LegBandSubData _LegBandSub = new LegBandSubData();

		[System.Serializable]
		public class LegBandData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public LegBandData _LegBand = new LegBandData();

		[System.Serializable]
		public class LegBandCoverData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public LegBandCoverData _LegBandCover = new LegBandCoverData();

		[System.Serializable]
		public class CollarSubData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public CollarSubData _CollarSub = new CollarSubData();

		[System.Serializable]
		public class CollarData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public CollarData _Collar = new CollarData();

		[System.Serializable]
		public class CollarCoverData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public CollarCoverData _CollarCover = new CollarCoverData();
		[System.Serializable]
		public class RingData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public RingData _RingLeft = new RingData();
		public RingData _RingRight = new RingData();

		[System.Serializable]
		public class BeltSubData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public BeltSubData _BeltSub = new BeltSubData();

		[System.Serializable]
		public class BeltData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public BeltData _Belt = new BeltData();

		[System.Serializable]
		public class BeltCoverData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public BeltCoverData _BeltCover = new BeltCoverData();

		[System.Serializable]
		public class CloakData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public CloakData _Cloak = new CloakData();

		[System.Serializable]
		public class BackpackSubData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public BackpackSubData _BackpackSub = new BackpackSubData();

		[System.Serializable]
		public class BackpackData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public BackpackData _Backpack = new BackpackData();

		[System.Serializable]
		public class BackpackCoverData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public BackpackCoverData _BackpackCover = new BackpackCoverData();

		[System.Serializable]
		public class LeftHandData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public LeftHandData _LeftHand = new LeftHandData();

		[System.Serializable]
		public class RightHandData {
			public DKSlotData Slot;
			public DKOverlayData Overlay;
			public Color Color;
			public ColorPresetData ColorPreset;
			public DKOverlayData Optional;
			public Color OptionalColor;
			public ColorPresetData Opt01Color;
			public ColorPresetData Opt02Color;
			public ColorPresetData Dirt01Color;
			public ColorPresetData Dirt02Color;
		} 
		public RightHandData _RightHand = new RightHandData();
	} 
	public EquipmentData _Equipment = new EquipmentData();
	#endregion Equipment

	[HideInInspector] public List<DKOverlayData> TmpTorsoOverLayList = new List<DKOverlayData>();

	[HideInInspector] public float LODType = 0;
	[HideInInspector]public DKUMAData umaData;
	[HideInInspector]public UMA.UMAData uma;
	[HideInInspector]public bool CreateUMADataAvatar;
	
	DK_UMACrowd crowd;
	List<DKSlotData> slotsList = new List<DKSlotData>();
	DKUMA_Variables _DKUMA_Variables;
	DK_UMA_GameSettings GameSettings;
	#endregion Variables

	void OnEnable (){
		// get ID
	//	if ( AvatarID == "" ) AvatarID = gameObject.GetInstanceID().ToString();
		if ( GameObject.Find ("DK_UMA" ) == null ){
			InstallDKUMA ();
			InstallUMA ();
		}
		_DKUMA_Variables = GameObject.Find ("DK_UMA").GetComponent<DKUMA_Variables>();

		// Detect and assign DK UMA Collider Material
		CapsuleCollider _CapsuleCollider = gameObject.GetComponent<CapsuleCollider>();
		if ( _CapsuleCollider ){
			_CapsuleCollider.material = _DKUMA_Variables.DK_UMA_Collider_Material;
			_CapsuleCollider.material.name = "DK_UMA_Collider_Material";
		}
		GameSettings = _DKUMA_Variables._DK_UMA_GameSettings;

		// get LOD values
		GameSettings._LOD.UseMeshLOD = false;
		_LOD.UseLOD = GameSettings._LOD.UseLOD;
		_LOD.Frequency = GameSettings._LOD.Frequency;
		_LOD.LOD0Resolution = GameSettings._LOD.LOD0Resolution;
		_LOD.LOD1Distance = GameSettings._LOD.LOD1Distance;
		_LOD.LOD1Resolution = GameSettings._LOD.LOD1Resolution;
		_LOD.LOD2Distance = GameSettings._LOD.LOD2Distance;
		_LOD.LOD2Resolution = GameSettings._LOD.LOD2Resolution;
		_LOD.LOD3Distance = GameSettings._LOD.LOD3Distance;
		_LOD.LOD3Resolution = GameSettings._LOD.LOD3Resolution;
		_LOD.LOD4Distance = GameSettings._LOD.LOD4Distance;
		_LOD.LOD4Resolution = GameSettings._LOD.LOD4Resolution;

		if ( IsPlayer && GameSettings.Player == null  )
			GameSettings.Player = transform;

		// Desable this for the camera to be the LOD Viewer
		_LOD.LODViewer = GameObject.Find ("LOD Viewer Example");

		// Launch LOD managment if used
		if ( _LOD.UseLOD /*&& IsPlayer == false*/ )
			InvokeRepeating ("CheckIfVisible", _LOD.Frequency, _LOD.Frequency );

		// load from file
		if ( LoadFromFile ) {
			// get name
			if ( IsPlayer 
			    && PlayerPrefs.GetString( "FileToLoad" ) != ""
				&& FileName == "" ) {
			//	Debug.Log ("DK UMA : Player Prefs file name" );
				FileName = PlayerPrefs.GetString( "FileToLoad" );
			}
			if ( FileName != "" ) {
				Invoke ("LoadOnStart", 0.05f );
			//	Debug.Log ("DK UMA :Load From File "+FileName );
			}
		}
		else if ( AvatarFromDB != null ) {			
			SavedRPGStreamed = AvatarFromDB.StreamedAvatar;
			Invoke ("LoadOnStart", 0.05f );
		//	Debug.Log ("DK UMA : Load From Database "+AvatarFromDB.name );

		}
		else if ( SavedRPGStreamed != "" ) {			
			Invoke ("LoadOnStart", 0.05f );
		//	Debug.Log ("DK UMA : Load From Streaming" );

		}
		else if ( IsPlayer && AvatarFromDB == null ) {
			// try loading RPG streamed from PlayerPref
			if ( /*SavedRPGStreamed == "" &&*/ PlayerPrefs.GetString ("DKRPGStreamed" ) != "" ) {
				// get streamed from PlayerPref
				if ( /*SavedRPGStreamed == "" &&*/ PlayerPrefs.GetString ("DKRPGStreamed" ) != "" ){
					SavedRPGStreamed = PlayerPrefs.GetString ("DKRPGStreamed" );
					Invoke ("LoadOnStart", 0.05f );
				}
			}
		}
	}

	void InstallDKUMA (){
		DKUMACorrectors.InstallDKUMA ();
	}

	void InstallUMA (){
		DKUMACorrectors.InstallUMA ();
	}

	void LoadOnStart (){
	//	Debug.Log ("DK UMA : 2 Load On start "+FileName );

		// Load from file
		if ( FileName != "" && LoadFromFile ){
			if ( FileName.Contains(".uml") )
				LoadFile ( FileName );
			else
				LoadFile ( FileName+".uml" );
		}
		// Load from RPG Streamed
		else if ( SavedRPGStreamed != "" ){
			LoadRPGStreamed ();
		}
	}

	public void LoadDkUmaStreaming (){

	}

	public void LoadRPGStreamed (){
		// load
		DK_UMA_Load.LoadAvatar ( this, PlayerPrefs.GetString ( "FileToLoad" ), 
			GameObject.Find("DKUMACrowd").GetComponent<DK_UMACrowd>()
			, SaveData.LoadStream( SavedRPGStreamed ) );
	}

	public void LoadFile ( string fileName ){
		// load
		DK_UMA_Load.LoadAvatar ( this, fileName, 
		                        GameObject.Find("DKUMACrowd").GetComponent<DK_UMACrowd>()
		                        , Application.streamingAssetsPath );
	}

	void CheckIfVisible () {
		// is the avatar visible by the camera ?
		if ( transform.GetComponentInChildren<SkinnedMeshRenderer>()
			&& transform.GetComponentInChildren<SkinnedMeshRenderer>().isVisible ){
			if ( _LOD.UseLOD ) CheckDistanceToMainCam ();

		}
	}

	void CheckDistanceToMainCam () {
		// get the LODViewer
		if ( _LOD.LODViewer == null )
			_LOD.LODViewer = Camera.main.gameObject;

		 // Calculate distance
		if ( umaData == null ) umaData = GetComponent<DKUMAData>();
		if ( uma == null ) uma = GetComponentInChildren<UMA.UMAData>();
		Distance = Vector3.Distance ( uma.transform.position, _LOD.LODViewer.transform.position );
		UMAGenerator generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();

		if ( IsPlayer && transform.GetComponentInChildren<UMA.UMAData>() != null && GameSettings.PlayerUMA == null ) 
			GameSettings.PlayerUMA = transform.GetComponentInChildren<UMA.UMAData>();
			
		if ( Distance <= _LOD.LOD1Distance/2 ) {
				// Change to LOD0
			if ( LODType != 0 ) {

					crowd = GameObject.Find ("DKUMACrowd").GetComponent<DK_UMACrowd>();
					umaData = transform.GetComponent<DKUMAData>();
					
					// Change to resolution of the texture
					generator.atlasResolution = _LOD.LOD0Resolution;
				uma.atlasResolutionScale = 1f;
					
					_LOD.EnabledLOD = false;
					LODType = 0f;

					// get slots list
					if ( GameSettings._LOD.UseMeshLOD ) slotsList = umaData.umaRecipe.slotDataList.ToList();
					
					// launch the generation
				//	Generate ();
					if ( GameSettings._LOD.UseMeshLOD ) CheckLOD ();
					else CheckTextureLOD ();
				}
			}
		if ( !IsPlayer && GameSettings.PlayerUMA != null /*&& generator.umaDirtyList.Contains ( GameSettings.PlayerUMA ) == false*/ ){
		if ( !IsPlayer && Distance > _LOD.LOD1Distance/2 
			    && Distance <= _LOD.LOD1Distance ) {
				if ( LODType != 0.5f ) {
				//	Debug.Log ("Change to LOD0.5");
					_LOD.EnabledLOD = false;
					LODType = 0.5f;
				if ( GameSettings._LOD.UseMeshLOD ) CheckLOD ();
				else CheckTextureLOD ();

				}
			}

		if ( !IsPlayer && Distance > _LOD.LOD1Distance 
			    &&  Distance <= _LOD.LOD2Distance ) {
				// Change to LOD1
				if ( LODType != 1 ) {
				//	Debug.Log ("Change to LOD1");
					_LOD.EnabledLOD = false;
					LODType = 1;
				if ( GameSettings._LOD.UseMeshLOD ) CheckLOD ();
				else CheckTextureLOD ();
				}
			}
		else if ( !IsPlayer && Distance > _LOD.LOD2Distance
			    &&  Distance <= _LOD.LOD3Distance   ) {
				// Change to LOD2
				if ( LODType != 2 ) {
				//	Debug.Log ("Change to LOD2");
					_LOD.EnabledLOD = false;
					LODType = 2;
				if ( GameSettings._LOD.UseMeshLOD ) CheckLOD ();
				else CheckTextureLOD ();
				}
			}
		else if ( !IsPlayer && Distance > _LOD.LOD3Distance
			         &&  Distance <= _LOD.LOD4Distance   ) {
				// Change to LOD3
				if ( LODType != 3 ) {
				//	Debug.Log ("Change to LOD3");
					_LOD.EnabledLOD = false;
					LODType = 3;
				if ( GameSettings._LOD.UseMeshLOD ) CheckLOD ();
				else CheckTextureLOD ();
				}
			}
		else if ( !IsPlayer && Distance > _LOD.LOD4Distance ) {
				// Change to LOD4
				if ( LODType != 4 ) {
					_LOD.EnabledLOD = false;
					LODType = 4;
				if ( GameSettings._LOD.UseMeshLOD ) CheckLOD ();
				else CheckTextureLOD ();
				}
			}
		}

	/*	else // Change to LOD0
		if ( umaData.atlasResolutionScale != 1 ) {

		//	Debug.Log ("Assigning the Player avatar LOD.");

			crowd = GameObject.Find ("DKUMACrowd").GetComponent<DK_UMACrowd>();
			umaData = transform.GetComponent<DKUMAData>();
			UMAGenerator generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();
			
			// Change to resolution of the texture
			generator.atlasResolution = _LOD.LOD0Resolution;
			umaData.atlasResolutionScale = 1f;
			
			_LOD.EnabledLOD = false;

			// get slots list
			slotsList = umaData.umaRecipe.slotDataList.ToList();
			
			// launch the generation
			Generate ();
		}*/
	}

	void CheckTextureLOD (){
		// for LOD 0
		if (LODType == 0 ){
			Debug.Log ("LODType == 0");
			// change the texture resolution
			UMAGenerator _generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();
			_generator.atlasResolution = _LOD.LOD0Resolution;
			uma.atlasResolutionScale = 1f;
		}
		else if (LODType == 0.5f ){

			// change the texture resolution
			UMAGenerator _generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();
			_generator.atlasResolution = _LOD.LOD0Resolution/2;
			uma.atlasResolutionScale = 1f;
		}
		else if (LODType == 1 ){
			// change the texture resolution
			UMAGenerator generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();
			generator.atlasResolution = _LOD.LOD1Resolution;
			uma.atlasResolutionScale = 1f;
		}
		else if (LODType == 2 ){
			// change the texture resolution
			UMAGenerator generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();
			generator.atlasResolution = _LOD.LOD2Resolution;
			uma.atlasResolutionScale = 1f;
		}
		// for LOD 3
		else if (LODType == 3 ){
			// change the texture resolution
			UMAGenerator generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();
			generator.atlasResolution = _LOD.LOD3Resolution;
			uma.atlasResolutionScale = 1f;
		}
		// for LOD 4
		else if (LODType == 4 ){
			// change the texture resolution
			UMAGenerator generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();
			generator.atlasResolution = _LOD.LOD4Resolution;
			uma.atlasResolutionScale = 1f;
		}
		ApplyTextureLOD ();
	}

	void ApplyTextureLOD (){
		uma.isTextureDirty = true;
		uma.isMeshDirty = false;
		uma.isShapeDirty = false;
		UMAGenerator generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();
		if ( uma.atlasResolutionScale <= 0 ) uma.atlasResolutionScale = 0.025f;
		if ( generator.atlasResolution <= 0 ) generator.atlasResolution = 128;
	//	generator.umaDirtyList.Add(uma);
		generator.addDirtyUMA(uma);
	}

	void CheckLOD (){
		// LOD
		if ( GameSettings._LOD.UseMeshLOD ) {
			// Create list
			umaData = transform.GetComponent<DKUMAData>();
			if ( uma == null ) uma = GetComponentInChildren<UMA.UMAData>();

			slotsList = umaData.umaRecipe.slotDataList.ToList();
			crowd = GameObject.Find ("DKUMACrowd").GetComponent<DK_UMACrowd>();

			for (int i = 0; i < slotsList.Count; i ++) {
				// create slot
				DKSlotData slot = slotsList[i];
				// check if LOD content is present on the avatar

				// for LOD 0
				if (LODType == 0 ){

					Debug.Log ("LODType == 0");
					// change the texture resolution
					UMAGenerator _generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();
					_generator.atlasResolution = _LOD.LOD0Resolution;
					umaData.atlasResolutionScale = 1f;
					
					if ( GameSettings._LOD.UseMeshLOD && (( slot._LOD.IsLOD0 == true && slot._LOD.LOD1 != null )
					    || slot._LOD.MasterLOD != null )){
						// High details rebuild
						
						// get the index and change to the correct LOD slot
						if ( slot._LOD.MasterLOD != null ) {
							
							// LOD solution
							
							// instantiate the new slot
							DKSlotData newSlot = new DKSlotData ();
							slotsList.Add(crowd.slotLibrary.InstantiateSlot( slot._LOD.MasterLOD.slotName ));
							
							newSlot = slotsList[slotsList.Count-1];
							
							// instantiate the new overlays
							foreach ( DKOverlayData overlay in slot.overlayList ){
								newSlot.overlayList.Add(crowd.overlayLibrary.InstantiateOverlay(overlay.overlayName, overlay.color));
							}
							
							// copy values
							CopyValues ( slotsList.Count-1, slot, slot._LOD.MasterLOD );
							
							// remove old slot
							slotsList.Remove ( slot );
							
							_LOD.EnabledLOD = true;
						}
					}
				}

				if (LODType == 0.5f ){

					// change the texture resolution
					UMAGenerator _generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();
					_generator.atlasResolution = _LOD.LOD0Resolution/2;
					umaData.atlasResolutionScale = 0.4f;

					if ( GameSettings._LOD.UseMeshLOD && slot )
						if ( ( slot._LOD.IsLOD0 == true && slot._LOD.LOD1 != null )
					    || slot._LOD.MasterLOD != null ){
						// High details rebuild

						// get the index and change to the correct LOD slot
						if ( slot._LOD.MasterLOD != null ) {

							// LOD solution
							Debug.Log ("slot._LOD.MasterLOD != null");

							// instantiate the new slot
							DKSlotData newSlot = new DKSlotData ();
							slotsList.Add(crowd.slotLibrary.InstantiateSlot( slot._LOD.MasterLOD.slotName ));

							newSlot = slotsList[slotsList.Count-1];

							// instantiate the new overlays
							foreach ( DKOverlayData overlay in slot.overlayList ){
								newSlot.overlayList.Add(crowd.overlayLibrary.InstantiateOverlay(overlay.overlayName, overlay.color));
							}

							// copy values
							CopyValues ( slotsList.Count-1, slot, slot._LOD.MasterLOD );

							// remove old slot
							slotsList.Remove ( slot );

							_LOD.EnabledLOD = true;
						}
					}
				}
					
				// for LOD 1
				if (LODType == 1 ){
					// get the index and change to the correct LOD slot
					if ( GameSettings._LOD.UseMeshLOD && ( (slot._LOD.MasterLOD != null && slot._LOD.MasterLOD._LOD.LOD1 != null)
					    || ( slot._LOD.IsLOD0 == true && slot._LOD.LOD1 != null ) )) {

						DKSlotData SlotToinst = new DKSlotData ();
						if ( slot._LOD.MasterLOD != null && slot._LOD.MasterLOD._LOD.LOD1 != null )
							SlotToinst = slot._LOD.MasterLOD._LOD.LOD1;
						else if ( slot._LOD.IsLOD0 == true && slot._LOD.LOD1 != null )
							SlotToinst = slot._LOD.LOD1;

						InstantiateSlot ( SlotToinst, slot );
													
						_LOD.EnabledLOD = true;
					}
					
					// change the texture resolution
					UMAGenerator generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();
					generator.atlasResolution = _LOD.LOD1Resolution;
					umaData.atlasResolutionScale = 0.2f;
				}

				// for LOD 2
				if (LODType == 2 ){
					// get the index and change to the correct LOD slot
					if ( GameSettings._LOD.UseMeshLOD && ((slot._LOD.MasterLOD != null && slot._LOD.MasterLOD._LOD.LOD2 != null)
					    || ( slot._LOD.IsLOD0 == true && slot._LOD.LOD2 != null ) )) {
						
						DKSlotData SlotToinst = new DKSlotData ();
						if ( slot._LOD.MasterLOD != null && slot._LOD.MasterLOD._LOD.LOD2 != null )
							SlotToinst = slot._LOD.MasterLOD._LOD.LOD2;
						else if ( slot._LOD.IsLOD0 == true && slot._LOD.LOD2 != null )
							SlotToinst = slot._LOD.LOD2;
						
						InstantiateSlot ( SlotToinst, slot );

						_LOD.EnabledLOD = true;
					}
					// change the texture resolution
					UMAGenerator generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();
					generator.atlasResolution = _LOD.LOD2Resolution;
					umaData.atlasResolutionScale = 0.1f;
				}
				// for LOD 3
				if (LODType == 3 ){
					// change the texture resolution
					UMAGenerator generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();
					generator.atlasResolution = _LOD.LOD3Resolution;
					umaData.atlasResolutionScale = 0.05f;
				}
				// for LOD 4
				if (LODType == 4 ){
					// change the texture resolution
					UMAGenerator generator = GameObject.Find ("UMAGenerator").GetComponent<UMAGenerator>();
					generator.atlasResolution = _LOD.LOD4Resolution;
					umaData.atlasResolutionScale = 0.025f;
				}
			}
			Generate ();
		}
	}

	public void Rebuild (){	

		DK_RPG_ReBuild _DK_RPG_ReBuild = GetComponent<DK_RPG_ReBuild>();
		if ( _DK_RPG_ReBuild == null ) _DK_RPG_ReBuild = gameObject.AddComponent<DK_RPG_ReBuild>();
		DKUMAData _DKUMAData = GetComponent<DKUMAData>();
		_DK_RPG_ReBuild.RefreshOnly = true;
		_DK_RPG_ReBuild.Launch (_DKUMAData);
	}

	void Generate (){
		_UMA_Avatar = GetComponentInChildren<UMA.UMAData>();

		// launch the generation
		if ( GameSettings._LOD.UseMeshLOD ) umaData.umaRecipe.slotDataList = slotsList.ToArray();

		umaData.SaveToMemoryStream();
		string streamed =  umaData.streamedUMA;

		TransposeDK2UMA _TransposeDK2UMA;

		_TransposeDK2UMA = gameObject.GetComponent<TransposeDK2UMA>();
	//	_TransposeDK2UMA.UpdateOnly = true;

		if ( crowd.ToUMA ) {
			if ( _TransposeDK2UMA == null )
				umaData.transform.gameObject.GetComponent<TransposeDK2UMA>();
			if ( _TransposeDK2UMA == null ){
				_TransposeDK2UMA  = umaData.transform.gameObject.AddComponent<TransposeDK2UMA>();
			}
			_TransposeDK2UMA.DKumaData = umaData;
			_TransposeDK2UMA.Launch ( this, crowd, streamed, true/*, false*/ );
		}
	}

	void InstantiateSlot ( DKSlotData SlotToinst, DKSlotData slot ) {
		// instantiate the new slot
		DKSlotData newSlot = new DKSlotData ();
		slotsList.Add(crowd.slotLibrary.InstantiateSlot( SlotToinst.slotName ));
		
		newSlot = slotsList[slotsList.Count-1];
		// instantiate the new overlays
		foreach ( DKOverlayData overlay in slot.overlayList ){
			newSlot.overlayList.Add(crowd.overlayLibrary.InstantiateOverlay(overlay.overlayName, overlay.color));
		}
		
		// copy values
		CopyValues ( slotsList.Count-1, slot, SlotToinst );
		
		// remove old slot
		slotsList.Remove ( slot );
	}

	void CopyValues ( int index, DKSlotData slot, DKSlotData SlotToinst ) {
		slotsList[index].OverlayType = slot.OverlayType;
		slotsList[index].Place = slot.Place;
		slotsList[index]._UMA = slot._UMA;
		slotsList[index].Replace = slot.Replace;
		slotsList[index]._HideData.HideHair = slot._HideData.HideHair;
		slotsList[index]._HideData.HideHairModule = slot._HideData.HideHairModule;
		slotsList[index]._HideData.HideLegs = slot._HideData.HideLegs;
		
		slotsList[index]._HideData.HideBelt = slot._HideData.HideBelt;
		slotsList[index]._HideData.HideArmBand = slot._HideData.HideArmBand;
		slotsList[index]._HideData.HideLegBand = slot._HideData.HideLegBand;
		slotsList[index]._HideData.HideWrist = slot._HideData.HideWrist;
		slotsList[index]._HideData.HideCollar = slot._HideData.HideCollar;
		slotsList[index]._HideData.HideRingLeft = slot._HideData.HideRingLeft;
		slotsList[index]._HideData.HideRingRight = slot._HideData.HideRingRight;
		slotsList[index]._HideData.HideCloak = slot._HideData.HideCloak;
		slotsList[index]._HideData.HideBackpack = slot._HideData.HideBackpack;
		slotsList[index]._HideData.HideUnderwear = slot._HideData.HideUnderwear;
		
		slotsList[index]._HideData.HideMouth = slot._HideData.HideMouth;
		slotsList[index]._HideData.HideShoulders = slot._HideData.HideShoulders;
		slotsList[index]._HideData.HideBeard = slot._HideData.HideBeard;
		slotsList[index]._HideData.HideEars = slot._HideData.HideEars;
		slotsList[index]._LegacyData.HasLegacy = slot._LegacyData.HasLegacy;
		slotsList[index]._LegacyData.LegacyList = slot._LegacyData.LegacyList;
		slotsList[index]._LegacyData.IsLegacy = slot._LegacyData.IsLegacy;
		slotsList[index]._LegacyData.ElderList = slot._LegacyData.ElderList;
		
		slotsList[index]._LOD.IsLOD0 = SlotToinst._LOD.IsLOD0;
		slotsList[index]._LOD.LOD1 = SlotToinst._LOD.LOD1;
		slotsList[index]._LOD.LOD2 = SlotToinst._LOD.LOD2;
		slotsList[index]._LOD.MasterLOD = SlotToinst._LOD.MasterLOD;
	}
}
