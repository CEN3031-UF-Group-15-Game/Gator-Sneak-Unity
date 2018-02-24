using UnityEngine;
using System.Collections;

public class DK_RPG_Element_Collider : MonoBehaviour {

	BoxCollider _Collider;
	DK_RPG_UMA _DK_RPG_UMA;

	void OnEnable (){
		// verify the collider
		if ( _Collider == null ){
			this.gameObject.GetComponent<BoxCollider>();
			if ( _Collider == null ) {
				_Collider =  this.gameObject.AddComponent<BoxCollider>();
				_Collider.isTrigger = true;
			}
		}
	}
	
	void OnTriggerEnter ( Collider other ){
		_DK_RPG_UMA = other.gameObject.GetComponent<DK_RPG_UMA>();
		// verify if it is the correct collider from the player
		if ( _DK_RPG_UMA != null 
		    && other.material.name == "DK_UMA_Collider_Material" )
		{
			if ( _DK_RPG_UMA.IsPlayer == true ){
				this.gameObject.GetComponent<DK_RPG_Element>().ElementPreparations (_DK_RPG_UMA);
			}
		}
	}

	void DestroySelf (){
		Destroy (gameObject);
	}

	public static void CreateOnGameObject ( GameObject Target ){
		Target.AddComponent<DK_RPG_Element_Collider>();
	}
}
