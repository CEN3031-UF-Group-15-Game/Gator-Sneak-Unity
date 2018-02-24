using UnityEngine;
using System;
using System.Collections.Generic;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

[Serializable]
public class DKEquipmentSetListData : ScriptableObject {
	public string Name;
	public string Description;
   
	[System.Serializable]
	public class SetsData {
		public List<DKEquipmentSetData> SetsList = new List<DKEquipmentSetData>();
	}
	public SetsData Sets = new SetsData();




  
}