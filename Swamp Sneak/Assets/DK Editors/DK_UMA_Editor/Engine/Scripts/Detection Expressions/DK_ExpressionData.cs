using UnityEngine;
using System.Collections;

[System.Serializable]
public class DK_ExpressionData : ScriptableObject {
	public string Name = "";
	public GameObject Place;
	public string OverlayType = "";
	public bool Elem = false;
	public bool Replace = false;
	public bool HideMouth = false;
	public bool HideEars = false;
	public bool HideBeard = false;
	public bool HideHair = false;
	public bool HideHairModule = false;
	public string WearWeight = "";
	public bool Selected = false;
	public DKDnaConverterBehaviour[] dnaConverterList;
	public bool ElemAlreadyIn = false;

}
