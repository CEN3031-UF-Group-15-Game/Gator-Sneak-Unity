using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class DK_UMA_Avatars_Databases : ScriptableObject {
	public List<DK_UMA_AvatarData> Avatars = new List<DK_UMA_AvatarData>();

	public void AddAvatar ( DK_UMA_AvatarData Avatar ){
		Avatars.Add ( Avatar );
		SaveDB ();
	}

	public void RemoveAvatar ( DK_UMA_AvatarData Avatar ){
		Avatars.Remove ( Avatar );
		SaveDB ();
	}

	void SaveDB (){
		#if UNITY_EDITOR
		EditorUtility.SetDirty (this);
		AssetDatabase.SaveAssets ();
		#endif
	}
}
