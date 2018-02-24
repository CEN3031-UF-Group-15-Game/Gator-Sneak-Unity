using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DKUMAEngineLibrariesRules : MonoBehaviour {

	public bool DontDestroyOnStart = false;
	GameObject UMAObject;

	[System.Serializable]
	public class DontUseInScenesData{
		public List<string> ScenesNamesList = new List<string>();
	}
	public DontUseInScenesData DontUseInScenes = new DontUseInScenesData();

	void Start (){
		
		// get current scene name
		string sceneName = SceneManager.GetActiveScene().name;

		if ( DontDestroyOnStart == true
			&& DontUseInScenes.ScenesNamesList.Contains (sceneName) == false ){
			UMAObject = GameObject.Find ("UMA");
			KeepObjects ();
		}
		else if ( DontDestroyOnStart == true ){
			NoMoreKeepObjects ();	
		}
	}

	void OneLevelWasLoaded (){
		Debug.Log ("Starting DKUMAEngineLibrariesRules");
		Start ();
	}

	public void KeepObjects (){
		DontDestroyOnLoad( gameObject );
		if ( UMAObject != null ) DontDestroyOnLoad( UMAObject );
	}

	public void NoMoreKeepObjects (){
		if ( UMAObject != null ) Destroy ( UMAObject );
		Destroy ( gameObject );

	}
}
