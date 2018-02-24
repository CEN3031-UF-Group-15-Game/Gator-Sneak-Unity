using UnityEngine;
using System.Collections;
#pragma warning disable 0246 // variable assigned but not used.

public class TestDefine : MonoBehaviour {

	// Use this for initialization
	void Start () {
		#if DK_UMA_Define_Test
		Debug.Log ("Test");
		#endif

		#if DK_UMA_2_4_3
		Debug.Log ("Test");
		#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
