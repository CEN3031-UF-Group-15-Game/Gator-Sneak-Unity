using UnityEngine;
using UnityEditor;


public class EditorSaveAvatar : MonoBehaviour {

	// Use this for initialization
/*	public static void SaveAvatar (){
	//	GameObject DKUMAGeneratorObj = GameObject.Find("DKUMAGenerator");	
		if ( EditorVariables.DKUMAGeneratorObj != null ) EditorVariables._DKUMAGenerator =  EditorVariables.DKUMAGeneratorObj.GetComponent<DKUMAGenerator>();
		EditorVariables._DKUMAGenerator.umaDirtyList.Clear();
		
		DKUMAData umaData = Selection.activeGameObject.transform.GetComponentInChildren<DKUMAData>() as DKUMAData;

		if ( umaData && umaData.transform.parent != null ){
		//	DK_Model _DK_Model = umaData.transform.parent.gameObject.GetComponent< DK_Model >();
			umaData.SaveToMemoryStream();
			umaData.Loading = true;
			DKUMASaveTool umaSaveTool = umaData.transform.GetComponent<DKUMASaveTool>();
			umaSaveTool.AutoLoad();
		}
		//	EditorUtility.SetDirty(Selection.activeGameObject.transform.parent);
		//	AssetDatabase.SaveAssets();
	}*/
}
