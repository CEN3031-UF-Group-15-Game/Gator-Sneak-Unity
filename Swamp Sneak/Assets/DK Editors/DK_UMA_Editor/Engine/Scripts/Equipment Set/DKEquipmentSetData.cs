using UnityEngine;
using System;
using System.Collections.Generic;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.



[Serializable]
public class DKEquipmentSetData : ScriptableObject {
	public string Name;
	public string Description;
	public bool Active = true;

	[System.Serializable]
	public class Male{
		public bool Active = false;
		public DK_RPG_UMA.EquipmentData _EquipmentData = new DK_RPG_UMA.EquipmentData();
	}

	[System.Serializable]
	public class Female{
		public bool Active = false;
		public DK_RPG_UMA.EquipmentData _EquipmentData = new DK_RPG_UMA.EquipmentData();
	}

	[System.Serializable]
	public class SetData {
		public Male _Male = new Male();
		public Female _Female = new Female();
	}
	public SetData SetContent = new SetData();
	/*
	[System.Serializable]
	public class SetsData {
		public List<SetData> SetsList = new List<SetData>();
	}
	public SetsData Sets = new SetsData();
*/

//	void Awake (){
	//	if ( SetContent == null )
	//		SetContent = new SetData();
//	}
}