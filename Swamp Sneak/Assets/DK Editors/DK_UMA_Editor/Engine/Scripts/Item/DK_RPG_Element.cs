using UnityEngine;
using System.Collections;

public class DK_RPG_Element : MonoBehaviour {
	#region Variables

	[System.Serializable]
	public class ElementOptions {
		public bool AutoPickup = false;
		public bool AutoEquip = true;
		public bool DestroyWhenPickup = true;
		public bool FemaleModel = false;

		[System.Serializable]
		public class AutoRotateData {
			public bool AutoRotate = false;
			public float _X = 0f;
			public float _Y = 0f;
			public float _Z = 0f;
		}
		public AutoRotateData _AutoRotate = new AutoRotateData();
	}
	public ElementOptions _ElementOptions = new ElementOptions();

	[System.Serializable]
	public class Engine {
		public Texture2D _Texture;
		public SkinnedMeshRenderer _SkinnedMeshRenderer;
		public BoxCollider _Collider;
		public DK_RPG_UMA _DK_RPG_UMA;
	}
	public Engine _Engine = new Engine();

	[System.Serializable]
	public class ElementData {
		public DKSlotData Male_DK_Slot;
		public DKOverlayData Male_DK_Overlay;
		public DKSlotData Female_DK_Slot;
		public DKOverlayData Female_DK_Overlay;
		public Color _color;
		public ColorPresetData _ColorPreset;
		public DKSlotData _slot;
		public DKOverlayData _overlay;
	}
	public ElementData _Element = new ElementData();

	Transform _Global;

	#endregion Variables

	void OnEnable (){
		if ( _Engine._Texture == null ) {
			ApplyTexture ();
		}
		if ( _Engine._SkinnedMeshRenderer == null ) {
			ApplyTexture ();
		}
		if ( _ElementOptions._AutoRotate.AutoRotate ) {
			AddAutoRotate ();
		}
		// verify the collider
	/*	if ( _Engine._Collider == null ){
			this.gameObject.GetComponent<BoxCollider>();
			if ( _Engine._Collider == null ) {
				_Engine._Collider =  this.gameObject.AddComponent<BoxCollider>();
				_Engine._Collider.isTrigger = true;
			}
		}*/
	}
	/*
	void OnTriggerEnter ( Collider other ){
		_Engine._DK_RPG_UMA = other.gameObject.GetComponent<DK_RPG_UMA>();
		// verify if it is the correct collider
		if ( _Engine._DK_RPG_UMA != null 
		    && other.material.name == "DK_UMA_Engine._Collider_Material" )
		{
			if ( _Engine._DK_RPG_UMA.IsPlayer ){
				ElementPreparations ();
			}
		}
	}
*/
	public void AddAutoRotate () {
		// find the global gameobject of the mesh
		Transform[] ts = transform.GetComponentsInChildren<Transform>();
		foreach ( Transform _child in ts ){
			if (_child.name == "Global")
				_Global = _child;
		}
		if ( _Global && _Global.gameObject.GetComponent<AutoRotate>() == null ){
			AutoRotate tmpAutoRotate = _Global.gameObject.AddComponent<AutoRotate>();
			tmpAutoRotate._X = _ElementOptions._AutoRotate._X;
			tmpAutoRotate._Y = _ElementOptions._AutoRotate._Y;
			tmpAutoRotate._Z = _ElementOptions._AutoRotate._Z;
			tmpAutoRotate._AutoRotate = _ElementOptions._AutoRotate.AutoRotate;
		}
	}

	public void ApplyTexture () {
		if ( _Engine._Texture == null ) {
			// for female
			if ( _ElementOptions.FemaleModel && _Element.Female_DK_Overlay ) 
				_Engine._Texture = _Element.Female_DK_Overlay.textureList[0];
			//for male
			else _Engine._Texture = _Element.Male_DK_Overlay.textureList[0];
		}
		if ( _Engine._SkinnedMeshRenderer == null ) {
			_Engine._SkinnedMeshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
		}
		if ( _Engine._Texture &&  _Engine._SkinnedMeshRenderer ) {
			if ( _Engine._SkinnedMeshRenderer.materials[0] == null ){
			//	_Engine._SkinnedMeshRenderer.materials[0] = new Material ();
				_Engine._SkinnedMeshRenderer.materials[0].mainTexture = _Engine._Texture;
				if ( _Element._ColorPreset ) _Element._color = _Element._ColorPreset.PresetColor;
				_Engine._SkinnedMeshRenderer.materials[0].color = _Element._color;
			}
		}
	}


	public void ElementPreparations (DK_RPG_UMA dkRPGUMA){
		_Engine._DK_RPG_UMA = dkRPGUMA;
		#region For Male Avatar
		// Slots : verify if the Element is available for a male avatar
		if ( _Engine._DK_RPG_UMA.Gender == "Male" ){
			// Slot Only Element
			if ( _Element.Male_DK_Slot != null ){
				// verify if the Element is available for the race of the avatar
				if ( _Element.Male_DK_Slot.Race.Contains(_Engine._DK_RPG_UMA.Race) ){
					
					// assign the tmp values
					_Element._slot = _Element.Male_DK_Slot;
					_Element._overlay = _Element.Male_DK_Overlay;
					
				}
			}
			
			// Overlay only element
			else if ( _Element.Male_DK_Overlay ) {
				// verify if the Element is available for the race of the avatar
				if ( _Element.Male_DK_Overlay.Race.Contains(_Engine._DK_RPG_UMA.Race) ){
					
					// assign the tmp values
					_Element._slot = null;
					_Element._overlay = _Element.Male_DK_Overlay;
					
				}
			}
		}
		#endregion For Male Avatar
		else
			#region For Female Avatar
			// Slots : verify if the Element is available for a Female avatar
		if ( _Engine._DK_RPG_UMA.Gender == "Female" ){
			
			if ( _Element.Female_DK_Slot != null ){
				// verify if the Element is available for the race of the avatar
				if ( _Element.Female_DK_Slot.Race.Contains(_Engine._DK_RPG_UMA.Race) ){
					_Element._slot = _Element.Female_DK_Slot;
					_Element._overlay = _Element.Female_DK_Overlay;
				}
			}
			// Overlays : verify if the Element is available for a Female avatar
			else if ( _Element.Female_DK_Overlay ) {
				if ( _Element.Female_DK_Overlay.Race.Contains(_Engine._DK_RPG_UMA.Race) ){
					_Element._slot = null;
					_Element._overlay = _Element.Female_DK_Overlay;
				}
			}
		}
		#endregion For Female Avatar
		
		#region set the color
		if ( _Element._color == Color.black ) {
			// using the assigned colorpreset
			if (_Element._ColorPreset ) {
				float adjust = Random.Range(0.01f, 0.5f);
				_Element._color = _Element._ColorPreset.PresetColor + new Color (adjust,adjust,adjust);
			}
			
			// choose a colorpreset from the overlay if possible
			else if ( _Element._overlay && _Element._overlay.ColorPresets.Count > 0 ) {
				int ran = Random.Range(0, _Element._overlay.ColorPresets.Count-1);
				float adjust = Random.Range(0.01f, 0.5f);
				_Element._color = _Element._overlay.ColorPresets[ran].PresetColor + new Color (adjust,adjust,adjust);
			}

			// assign the white color if the previous possibilities do not match
			else _Element._color = new Color(1,1,1,1);
		}
		#endregion set the color


		#region What to do ?
		// Auto Equip the element and launch the rebuilder for the avatar
		if ( _ElementOptions.AutoEquip ) {
			DK_UMA_RPG_Equip.EquipSlotElement (_Element._slot, _Element._overlay, _Engine._DK_RPG_UMA, _Element._ColorPreset, _Element._color, "Main", "Yes" );
		}
		
		// destroy this gameobject if necessary
		if ( _ElementOptions.DestroyWhenPickup ) DestroySelf ();
		#endregion What to do ?
	}

	void DestroySelf (){
		Destroy (gameObject);
	}


	public static void CreateOnGameObject ( GameObject Target ){
		Target.AddComponent<DK_RPG_Element>();
	}
}
