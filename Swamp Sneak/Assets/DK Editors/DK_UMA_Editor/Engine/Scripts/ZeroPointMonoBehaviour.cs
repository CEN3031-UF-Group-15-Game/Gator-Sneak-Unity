using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ZeroPointMonoBehaviour : MonoBehaviour {

	public bool Active = false;


	public ZeroPointMonoBehaviour ()
	{
		#if UNITY_EDITOR
		EditorApplication.update += Update;
		#endif
	}


	void Update () {
	
		#if UNITY_EDITOR
		if ( this && PrefabUtility.GetPrefabParent( this.gameObject ) != null 
		    && this.transform.name == "ZeroPointDefault" ){
			GameObject OldZPoint = GameObject.Find("ZeroPoint");
			if ( OldZPoint ) DestroyImmediate(OldZPoint.gameObject);
			this.transform.name = "ZeroPoint";
			
		}
		#endif

	}
}
