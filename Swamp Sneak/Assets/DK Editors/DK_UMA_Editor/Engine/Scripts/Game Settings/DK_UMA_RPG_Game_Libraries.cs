using UnityEngine;
using System.Collections;
using System.Collections.Generic;





[System.Serializable]
public class DK_UMA_RPG_Game_Libraries : ScriptableObject {

	#region Lists
	#region avatar Meshs and Textures
	[System.Serializable]
	public class AvatarData {
		#region Face
		[System.Serializable]
		public class FaceData {
			#region Head
			[System.Serializable]
			public class HeadData {
				public List<DKSlotData> SlotList = new List<DKSlotData>();
				public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
				public List<DKOverlayData> TattooList = new List<DKOverlayData>();
				public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
				public List<DKOverlayData> LipsList = new List<DKOverlayData>();
				public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
				
				public void ClearAll(){
					SlotList.Clear();
					OverlayList.Clear();
					TattooList.Clear();
					MakeupList.Clear();
					OptionalList.Clear();
					
					Debug.Log ("Head Lists Cleared");
				}
				
				public void Select(){
					Debug.Log ("Test Select");
				}
				
				public void Assign(){
					Debug.Log ("Test Assign");
				}
			}
			public HeadData _Head = new HeadData();
			#endregion Head
			
			#region FaceHair
			[System.Serializable]
			public class FaceHairData {
				public List<DKOverlayData> EyeBrowsList = new List<DKOverlayData>();
				
				[System.Serializable]
				public class BeardOvOnlyData {
					public List<DKOverlayData> BeardList = new List<DKOverlayData>();
				}
				public BeardOvOnlyData _BeardOverlayOnly = new BeardOvOnlyData();
				
				[System.Serializable]
				public class BeardSlotOnlyData {
					public List<DKSlotData> SlotList = new List<DKSlotData>();
					public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
				}
				public BeardSlotOnlyData _BeardSlotOnly = new BeardSlotOnlyData();
				
			}
			public FaceHairData _FaceHair = new FaceHairData();
			#endregion FaceHair
			
			#region Eyes
			[System.Serializable]
			public class EyesData {
				public List<DKSlotData> SlotList = new List<DKSlotData>();
				public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
				public List<DKOverlayData> AdjustList = new List<DKOverlayData>();
				public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			}
			public EyesData _Eyes = new EyesData();
			#endregion Eyes
			
			#region EyeLash
			[System.Serializable]
			public class EyeLashData {
				public List<DKSlotData> SlotList = new List<DKSlotData>();
				public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			}
			public EyeLashData _EyeLash = new EyeLashData();
			#endregion EyeLash
			
			#region Lids
			[System.Serializable]
			public class EyeLidsData {
				public List<DKSlotData> SlotList = new List<DKSlotData>();
				public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
				public List<DKOverlayData> TattooList = new List<DKOverlayData>();
				public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
				public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			}
			public EyeLidsData _EyeLids = new EyeLidsData();
			#endregion Lids
			
			#region Ears
			[System.Serializable]
			public class EarsData {
				public List<DKSlotData> SlotList = new List<DKSlotData>();
				public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
				public List<DKOverlayData> TattooList = new List<DKOverlayData>();
				public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
				public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
				
			}
			public EarsData _Ears = new EarsData();
			#endregion Ears
			
			#region Nose
			[System.Serializable]
			public class NoseData {
				public List<DKSlotData> SlotList = new List<DKSlotData>();
				public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
				public List<DKOverlayData> TattooList = new List<DKOverlayData>();
				public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
				public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
				
			}
			public NoseData _Nose = new NoseData();
			#endregion Nose
			
			#region Mouth
			[System.Serializable]
			public class MouthData {
				public List<DKSlotData> SlotList = new List<DKSlotData>();
				public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
				public List<DKOverlayData> TattooList = new List<DKOverlayData>();
				public List<DKOverlayData> LipsList = new List<DKOverlayData>();
				public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
				public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
				
				
				[System.Serializable]
				public class InnerMouthData {
					public List<DKSlotData> SlotList = new List<DKSlotData>();
					public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
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
				public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			}
			public OvOnlyData _OverlayOnly = new OvOnlyData();
			
			[System.Serializable]
			public class SlotOnlyData {
				public List<DKSlotData> SlotList = new List<DKSlotData>();
				public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
				
				[System.Serializable]
				public class HairModuleData {
					public List<DKSlotData> SlotList = new List<DKSlotData>();
					public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
				}
				public HairModuleData _HairModule = new HairModuleData();
			}
			public SlotOnlyData _SlotOnly = new SlotOnlyData();
		} 
		public HairData _Hair = new HairData();
		#endregion Hair
		
		#region Body
		[System.Serializable]
		public class BodyData {
			[System.Serializable]
			public class TorsoData {
				public List<DKSlotData> SlotList = new List<DKSlotData>();
				public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
				public List<DKOverlayData> TattooList = new List<DKOverlayData>();
				public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
				public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			} 
			public TorsoData _Torso = new TorsoData();
			
			[System.Serializable]
			public class HandsData {
				public List<DKSlotData> SlotList = new List<DKSlotData>();
				public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
				public List<DKOverlayData> TattooList = new List<DKOverlayData>();
				public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
				public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			} 
			public HandsData _Hands = new HandsData();
			
			[System.Serializable]
			public class LegsData {
				public List<DKSlotData> SlotList = new List<DKSlotData>();
				public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
				public List<DKOverlayData> TattooList = new List<DKOverlayData>();
				public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
				public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			} 
			public LegsData _Legs = new LegsData();
			
			[System.Serializable]
			public class FeetData {
				public List<DKSlotData> SlotList = new List<DKSlotData>();
				public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
				public List<DKOverlayData> TattooList = new List<DKOverlayData>();
				public List<DKOverlayData> MakeupList = new List<DKOverlayData>();
				public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			} 
			public FeetData _Feet = new FeetData();
			
			[System.Serializable]
			public class UnderwearData {
				public List<DKSlotData> SlotList = new List<DKSlotData>();
				public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
				public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			} 
			public UnderwearData _Underwear = new UnderwearData();
		} 
		public BodyData _Body = new BodyData();
		#endregion Body
		
	} 
	public AvatarData _MaleAvatar = new AvatarData();
	public AvatarData _FemaleAvatar = new AvatarData();
	
	#endregion avatar Meshs and Textures
	
	#region Equipment
	[System.Serializable]
	public class EquipmentData {
		
		[System.Serializable]
		public class HeadData {
			public List<DKSlotData> SlotList = new List<DKSlotData>();
			public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			public List<DKOverlayData> OverlayOnlyList = new List<DKOverlayData>();
			
			
		} 
		public HeadData _Head = new HeadData();
		
		[System.Serializable]
		public class ShoulderData {
			public List<DKSlotData> SlotList = new List<DKSlotData>();
			public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			public List<DKOverlayData> OverlayOnlyList = new List<DKOverlayData>();
			
		} 
		public ShoulderData _Shoulder = new ShoulderData();
		
		[System.Serializable]
		public class TorsoData {
			public List<DKSlotData> SlotList = new List<DKSlotData>();
			public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			public List<DKOverlayData> OverlayOnlyList = new List<DKOverlayData>();
			
		} 
		public TorsoData _Torso = new TorsoData();
		
		[System.Serializable]
		public class ArmbandData {
			public List<DKSlotData> SlotList = new List<DKSlotData>();
			public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			public List<DKOverlayData> OverlayOnlyList = new List<DKOverlayData>();
			
		} 
		public ArmbandData _Armband = new ArmbandData();
		
		[System.Serializable]
		public class LegBandData {
			public List<DKSlotData> SlotList = new List<DKSlotData>();
			public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			public List<DKOverlayData> OverlayOnlyList = new List<DKOverlayData>();
			
		} 
		public LegBandData _LegBand = new LegBandData();
		
		
		[System.Serializable]
		public class WristData {
			public List<DKSlotData> SlotList = new List<DKSlotData>();
			public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			public List<DKOverlayData> OverlayOnlyList = new List<DKOverlayData>();
			
		} 
		public WristData _Wrist = new WristData();
		
		[System.Serializable]
		public class HandsData {
			public List<DKSlotData> SlotList = new List<DKSlotData>();
			public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			public List<DKOverlayData> OverlayOnlyList = new List<DKOverlayData>();
			
		} 
		public HandsData _Hands = new HandsData();
		
		[System.Serializable]
		public class LegsData {
			public List<DKSlotData> SlotList = new List<DKSlotData>();
			public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			public List<DKOverlayData> OverlayOnlyList = new List<DKOverlayData>();
			
		} 
		public LegsData _Legs = new LegsData();
		
		[System.Serializable]
		public class FeetData {
			public List<DKSlotData> SlotList = new List<DKSlotData>();
			public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			public List<DKOverlayData> OverlayOnlyList = new List<DKOverlayData>();
		} 
		public FeetData _Feet = new FeetData();
		
		[System.Serializable]
		public class BeltData {
			public List<DKSlotData> SlotList = new List<DKSlotData>();
			public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
			public List<DKOverlayData> OverlayOnlyList = new List<DKOverlayData>();
			
		} 
		public BeltData _Belt = new BeltData();
		
		[System.Serializable]
		public class CloakData {
			public List<DKSlotData> SlotList = new List<DKSlotData>();
			public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
		} 
		public CloakData _Cloak = new CloakData();
		
		[System.Serializable]
		public class BackpackData {
			public List<DKSlotData> SlotList = new List<DKSlotData>();
			public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
		} 
		public BackpackData _Backpack = new BackpackData();
		
		[System.Serializable]
		public class LeftHandData {
			public List<DKSlotData> SlotList = new List<DKSlotData>();
			public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
		} 
		public LeftHandData _LeftHand = new LeftHandData();
		
		[System.Serializable]
		public class RightHandData {
			public List<DKSlotData> SlotList = new List<DKSlotData>();
			public List<DKOverlayData> OverlayList = new List<DKOverlayData>();
			public List<DKOverlayData> OptionalList = new List<DKOverlayData>();
		} 
		public RightHandData _RightHand = new RightHandData();
	} 
	public EquipmentData _MaleEquipment = new EquipmentData();
	public EquipmentData _FemaleEquipment = new EquipmentData();
	
	#endregion Equipment
	#endregion Lists

}
