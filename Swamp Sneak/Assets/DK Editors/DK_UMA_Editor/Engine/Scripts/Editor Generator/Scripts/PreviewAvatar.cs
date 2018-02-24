using UMA;
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class PreviewAvatar : MonoBehaviour {
//	[HideInInspector]
	public GameObject NewPreview;
	UMAData _PreviewAvatar;
	UMAGeneratorBuiltin Generator;
	UMARecipeBase recipe;
	public bool DontDestroyAtStart = false;

	#if UNITY_EDITOR
	public void GeneratePreview (){		
		if ( EditorApplication.isPlaying == false ){
		//	Debug.Log ("Creating preview");
			Generator = FindObjectOfType<UMAGeneratorBuiltin>();

			if (Generator != null) {

				CreateAvatar ();

				Generator.Awake();

				GenerateAvatar ();

				ApplyDNA ();
			}
		}
	}

	void CreateAvatar (){
		NewPreview = new GameObject ();
		NewPreview.name = "Avatar Preview";
		NewPreview.transform.position = transform.position;
		NewPreview.transform.rotation = transform.rotation;
		NewPreview.transform.parent = transform;

		if ( _PreviewAvatar == null ) _PreviewAvatar = NewPreview.AddComponent<UMAData>();			
		_PreviewAvatar.umaGenerator = Generator as UMAGeneratorBase;
		_PreviewAvatar.umaRecipe = new UMAData.UMARecipe();
		recipe = transform.GetComponent<UMADynamicAvatar>().umaRecipe;

		NewPreview.AddComponent<EditorAvatarPreview>();
	}

	void GenerateAvatar (){
		if ( EditorApplication.isPlaying == false ){
			UMAContext _UMAContext;
			_UMAContext = UMAContext.FindInstance();
			_UMAContext.ValidateDictionaries();

			if ( _PreviewAvatar == null ) _PreviewAvatar = GetComponentInChildren<UMAData>();
			if ( _PreviewAvatar.umaRecipe == null ) _PreviewAvatar.umaRecipe = new UMAData.UMARecipe();
			if ( transform.parent.GetComponent<TransposeDK2UMA>()._LoadUMA == null ) transform.parent.GetComponent<TransposeDK2UMA>()._LoadUMA = transform.GetComponent<LoadUMA>();
			if ( transform.GetComponent<UMADynamicAvatar>().umaRecipe == null ) transform.parent.GetComponent<TransposeDK2UMA>().SendRecipe ( transform.parent.GetComponent<TransposeDK2UMA>()._StreamedUMA );
			if ( recipe == null ) recipe = transform.GetComponent<UMADynamicAvatar>().umaRecipe;

			recipe.Load(_PreviewAvatar.umaRecipe, _UMAContext);

			_PreviewAvatar.Dirty(true, true, true);
			_PreviewAvatar.firstBake = true;			
			while (!Generator.IsIdle()) Generator.OnDirtyUpdate();

			if ( Generator.textureMerge != null) DestroyImmediate(Generator.textureMerge.gameObject);
		}
	}

	public void ApplyDNA (){
		TransposeDK2UMA transpose = transform.parent.GetComponent<TransposeDK2UMA>();
		transpose.SendDNAToUMA ();
		LoadUMA Load = transform.GetComponent<LoadUMA>();
		Load.ApplyPreviewDNA ( _PreviewAvatar );
	}
	#endif
}
