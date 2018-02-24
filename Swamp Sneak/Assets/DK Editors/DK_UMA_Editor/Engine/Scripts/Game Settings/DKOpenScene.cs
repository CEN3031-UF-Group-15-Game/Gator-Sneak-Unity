using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DKOpenScene : MonoBehaviour {
	public string NextSceneName = "";
	public bool Testing = false;

	public void OnEnable (){
		if ( Testing  ){
			if ( NextSceneName.Contains("1") )
				NextSceneName = NextSceneName.Replace ("1","2");
			else if ( NextSceneName.Contains("2") )
				NextSceneName = NextSceneName.Replace ("2","3");
			else if ( NextSceneName.Contains("3") )
				NextSceneName = NextSceneName.Replace ("3","4");
			Invoke ( "OpenScene", 10 );

		}
	}

	public void OpenScene (){
		if ( NextSceneName != "" ){
			Debug.Log ("Opening Scene "+NextSceneName);
			SceneManager.LoadScene (NextSceneName);
		}
	}
}
