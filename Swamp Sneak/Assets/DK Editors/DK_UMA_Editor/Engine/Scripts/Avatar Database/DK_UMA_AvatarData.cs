using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class DK_UMA_AvatarData : ScriptableObject {

	public string Name = "";
	public string Race;
	public string Gender;
	[HideInInspector]
	public string StreamedAvatar;

	public void AddToDB (){
		#if UNITY_EDITOR
		GameObject.Find ( "DK_UMA" ).GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings.Databases.Avatars.AddAvatar ( this );
		#endif
	}

	public void SaveData (){
		#if UNITY_EDITOR
		EditorUtility.SetDirty (this);
		AssetDatabase.SaveAssets ();
		#endif
	}
}
