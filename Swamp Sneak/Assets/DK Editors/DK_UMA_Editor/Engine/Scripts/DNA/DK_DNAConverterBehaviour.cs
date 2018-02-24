using UnityEngine;
using System.Collections;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

public class DK_DNAConverterBehaviour : DKDnaConverterBehaviour
{
	static float clamp1 = 5;
	static float clamp2 = 5;
	static float clamp3 = 5;
	static Vector3 _Vector3 = new Vector3();
	static GameObject DNAConvObj;

	public DK_DNAConverterBehaviour()
    {
        this.ApplyDnaAction = UpdateDNABones;
        this.DNAType = typeof(DKUMADnaHumanoid);
    }

   public static void UpdateDNABones(DKUMAData umaData)
    {
     //   var umaDna = umaData.GetDna<DKUMADnaHumanoid>();
		for(int i = 0; i < umaData.umaRecipe.raceData.dnaConverterList.Length; i ++){
			if ( umaData.umaRecipe.raceData.dnaConverterList[i].GetComponent<DK_DNAConverterBehaviour>() != null ){
				DNAConvObj = umaData.umaRecipe.raceData.dnaConverterList[i].gameObject;
			}
		}

		DKRaceData _RaceData = umaData.umaRecipe.raceData;

		if ( _RaceData.BoneDataList.Count > 0 ){
	
			if ( umaData.boneList.Count == 0 ) umaData.UpdateBoneData();
			else{
				if ( _RaceData ) 
				for (int i = 0; i < _RaceData.BoneDataList.Count ; i++)
				{
					DKRaceData.BoneData _BoneData = _RaceData.BoneDataList[i];
					if ( _BoneData.Active ) {
						if ( _BoneData.UseClamps ) {
							if ( _BoneData.Clamp1.Index == 1 ) {
								clamp1 = Mathf.Clamp(_BoneData.Clamp1.XYZ.x, _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 2 ) {
								clamp1 = Mathf.Clamp(_BoneData.Clamp1.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 3 ) {
								clamp1 = Mathf.Clamp(_BoneData.Clamp1.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv2.Index].Value ) - _BoneData.Clamp1.WXYZ1.y) 
								                     * _BoneData.Clamp1.WXYZ1.z
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 4 ) {
								clamp1 = Mathf.Clamp(_BoneData.Clamp1.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv2.Index].Value ) - _BoneData.Clamp1.WXYZ1.y) 
								                     * _BoneData.Clamp1.WXYZ1.z
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv3.Index].Value ) - _BoneData.Clamp1.WXYZ2.w) 
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 5 ) {
								clamp1 = Mathf.Clamp(_BoneData.Clamp1.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv2.Index].Value ) - _BoneData.Clamp1.WXYZ1.y) 
								                     * _BoneData.Clamp1.WXYZ1.z
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv3.Index].Value ) - _BoneData.Clamp1.WXYZ2.w) 
								                     * _BoneData.Clamp1.WXYZ2.x
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 6 ) {
								clamp1 = Mathf.Clamp(_BoneData.Clamp1.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv2.Index].Value ) - _BoneData.Clamp1.WXYZ1.y) 
								                     * _BoneData.Clamp1.WXYZ1.z
								                     - ( (umaData.DNAList2[_BoneData.Clamp1.Conv3.Index].Value ) - _BoneData.Clamp1.WXYZ2.w) 
								                     * _BoneData.Clamp1.WXYZ2.x
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 7 ) {
								clamp1 = Mathf.Clamp( ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 8 ) {
								clamp1 = Mathf.Clamp( -( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 9 ) {
								clamp1 = Mathf.Clamp( ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv2.Index].Value ) - _BoneData.Clamp1.WXYZ1.y) 
								                     * _BoneData.Clamp1.WXYZ1.z
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv3.Index].Value ) - _BoneData.Clamp1.WXYZ2.w) 
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 10 ) {
								clamp1 = Mathf.Clamp(_BoneData.Clamp1.XYZ.x 
								                     - ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp2.Index == 1 ) {
								clamp2 = Mathf.Clamp(_BoneData.Clamp2.XYZ.x, _BoneData.Clamp2.XYZ.y, _BoneData.Clamp2.XYZ.z);
							}
							if ( _BoneData.Clamp2.Index == 2 ) {
								clamp2 = Mathf.Clamp(_BoneData.Clamp2.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp2.Conv1.Index].Value ) - _BoneData.Clamp2.WXYZ1.w) 
								                     * _BoneData.Clamp2.WXYZ1.x
								                     , _BoneData.Clamp2.XYZ.y, _BoneData.Clamp2.XYZ.z);
							}
							if ( _BoneData.Clamp2.Index == 3 ) {
								clamp2 = Mathf.Clamp(_BoneData.Clamp2.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp2.Conv1.Index].Value ) - _BoneData.Clamp2.WXYZ1.w) 
								                     * _BoneData.Clamp2.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp2.Conv2.Index].Value ) - _BoneData.Clamp2.WXYZ1.y) 
								                     * _BoneData.Clamp2.WXYZ1.z
								                     , _BoneData.Clamp2.XYZ.y, _BoneData.Clamp2.XYZ.z);
							}
							if ( _BoneData.Clamp2.Index == 4 ) {
								clamp2 = Mathf.Clamp(_BoneData.Clamp2.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp2.Conv1.Index].Value ) - _BoneData.Clamp2.WXYZ1.w) 
								                     * _BoneData.Clamp2.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp2.Conv2.Index].Value ) - _BoneData.Clamp2.WXYZ1.y) 
								                     * _BoneData.Clamp2.WXYZ1.z
								                     + ( (umaData.DNAList2[_BoneData.Clamp2.Conv3.Index].Value ) - _BoneData.Clamp2.WXYZ2.w) 
								                     , _BoneData.Clamp2.XYZ.y, _BoneData.Clamp2.XYZ.z);
							}
							if ( _BoneData.Clamp2.Index == 5 ) {
								clamp2 = Mathf.Clamp(_BoneData.Clamp2.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp2.Conv1.Index].Value ) - _BoneData.Clamp2.WXYZ1.w) 
								                     * _BoneData.Clamp2.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp2.Conv2.Index].Value ) - _BoneData.Clamp2.WXYZ1.y) 
								                     * _BoneData.Clamp2.WXYZ1.z
								                     + ( (umaData.DNAList2[_BoneData.Clamp2.Conv3.Index].Value ) - _BoneData.Clamp2.WXYZ2.w) 
								                     * _BoneData.Clamp2.WXYZ2.x
								                     , _BoneData.Clamp2.XYZ.y, _BoneData.Clamp2.XYZ.z);
							}
							if ( _BoneData.Clamp2.Index == 6 ) {
								clamp2 = Mathf.Clamp(_BoneData.Clamp2.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp2.Conv1.Index].Value ) - _BoneData.Clamp2.WXYZ1.w) 
								                     * _BoneData.Clamp2.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp2.Conv2.Index].Value ) - _BoneData.Clamp2.WXYZ1.y) 
								                     * _BoneData.Clamp2.WXYZ1.z
								                     - ( (umaData.DNAList2[_BoneData.Clamp2.Conv3.Index].Value ) - _BoneData.Clamp2.WXYZ2.w) 
								                     * _BoneData.Clamp2.WXYZ2.x
								                     , _BoneData.Clamp2.XYZ.y, _BoneData.Clamp2.XYZ.z);
							}
							if ( _BoneData.Clamp2.Index == 7 ) {
								clamp2 = Mathf.Clamp( ( (umaData.DNAList2[_BoneData.Clamp2.Conv1.Index].Value ) - _BoneData.Clamp2.WXYZ1.w) 
								                     * _BoneData.Clamp2.WXYZ1.x
								                     , _BoneData.Clamp2.XYZ.y, _BoneData.Clamp2.XYZ.z);
							}
							if ( _BoneData.Clamp2.Index == 8 ) {
								clamp2 = Mathf.Clamp( -( (umaData.DNAList2[_BoneData.Clamp2.Conv1.Index].Value ) - _BoneData.Clamp2.WXYZ1.w) 
								                     * _BoneData.Clamp2.WXYZ1.x
								                     , _BoneData.Clamp2.XYZ.y, _BoneData.Clamp2.XYZ.z);
							}
							if ( _BoneData.Clamp2.Index == 9 ) {
								clamp2 = Mathf.Clamp( ( (umaData.DNAList2[_BoneData.Clamp2.Conv1.Index].Value ) - _BoneData.Clamp2.WXYZ1.w) 
								                     * _BoneData.Clamp2.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp2.Conv2.Index].Value ) - _BoneData.Clamp2.WXYZ1.y) 
								                     * _BoneData.Clamp2.WXYZ1.z
								                     + ( (umaData.DNAList2[_BoneData.Clamp2.Conv3.Index].Value ) - _BoneData.Clamp2.WXYZ2.w) 
								                     , _BoneData.Clamp2.XYZ.y, _BoneData.Clamp2.XYZ.z);
							}
							if ( _BoneData.Clamp2.Index == 10 ) {
								clamp2 = Mathf.Clamp(_BoneData.Clamp2.XYZ.x 
								                     - ( (umaData.DNAList2[_BoneData.Clamp2.Conv1.Index].Value ) - _BoneData.Clamp2.WXYZ1.w) 
								                     * _BoneData.Clamp2.WXYZ1.x
								                     , _BoneData.Clamp2.XYZ.y, _BoneData.Clamp2.XYZ.z);
							}
							if ( _BoneData.Clamp3.Index == 1 ) {
								clamp3 = Mathf.Clamp(_BoneData.Clamp3.XYZ.x, _BoneData.Clamp3.XYZ.y, _BoneData.Clamp3.XYZ.z);
							}
							if ( _BoneData.Clamp3.Index == 2 ) {
								clamp3 = Mathf.Clamp(_BoneData.Clamp3.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp3.Conv1.Index].Value ) - _BoneData.Clamp3.WXYZ1.w) 
								                     * _BoneData.Clamp3.WXYZ1.x
								                     , _BoneData.Clamp3.XYZ.y, _BoneData.Clamp3.XYZ.z);
							}
							if ( _BoneData.Clamp3.Index == 3 ) {
								clamp3 = Mathf.Clamp(_BoneData.Clamp3.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp3.Conv1.Index].Value ) - _BoneData.Clamp3.WXYZ1.w) 
								                     * _BoneData.Clamp3.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp3.Conv2.Index].Value ) - _BoneData.Clamp3.WXYZ1.y) 
								                     * _BoneData.Clamp3.WXYZ1.z
								                     , _BoneData.Clamp3.XYZ.y, _BoneData.Clamp3.XYZ.z);
							}
							if ( _BoneData.Clamp3.Index == 4 ) {
								clamp3 = Mathf.Clamp(_BoneData.Clamp3.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp3.Conv1.Index].Value ) - _BoneData.Clamp3.WXYZ1.w) 
								                     * _BoneData.Clamp3.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp3.Conv2.Index].Value ) - _BoneData.Clamp3.WXYZ1.y) 
								                     * _BoneData.Clamp3.WXYZ1.z
								                     + ( (umaData.DNAList2[_BoneData.Clamp3.Conv3.Index].Value ) - _BoneData.Clamp3.WXYZ2.w) 
								                     , _BoneData.Clamp3.XYZ.y, _BoneData.Clamp3.XYZ.z);
							}
						//0	Mathf.Clamp(1 + (umaDna.lowerWeight - 0.5f) * 0.15f + (umaDna.lowerMuscle - 0.5f) * 0.95f - (umaDna.legsSize - 0.5f), 0.65f, 1.45f),

							if ( _BoneData.Clamp3.Index == 5 ) {
								clamp3 = Mathf.Clamp(_BoneData.Clamp3.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp3.Conv1.Index].Value ) - _BoneData.Clamp3.WXYZ1.w) 
								                     * _BoneData.Clamp3.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp3.Conv2.Index].Value ) - _BoneData.Clamp3.WXYZ1.y) 
								                     * _BoneData.Clamp3.WXYZ1.z
								                     + ( (umaData.DNAList2[_BoneData.Clamp3.Conv3.Index].Value ) - _BoneData.Clamp3.WXYZ2.w) 
								                     * _BoneData.Clamp3.WXYZ2.x
								                     , _BoneData.Clamp3.XYZ.y, _BoneData.Clamp3.XYZ.z);
							}
							if ( _BoneData.Clamp3.Index == 6 ) {
								clamp3 = Mathf.Clamp(_BoneData.Clamp3.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp3.Conv1.Index].Value ) - _BoneData.Clamp3.WXYZ1.w) 
								                     * _BoneData.Clamp3.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp3.Conv2.Index].Value ) - _BoneData.Clamp3.WXYZ1.y) 
								                     * _BoneData.Clamp3.WXYZ1.z
								                     - ( (umaData.DNAList2[_BoneData.Clamp3.Conv3.Index].Value ) - _BoneData.Clamp3.WXYZ2.w) 
								                     * _BoneData.Clamp3.WXYZ2.x
								                     , _BoneData.Clamp3.XYZ.y, _BoneData.Clamp3.XYZ.z);
							}
							if ( _BoneData.Clamp3.Index == 7 ) {
								clamp3 = Mathf.Clamp( ( (umaData.DNAList2[_BoneData.Clamp3.Conv1.Index].Value ) - _BoneData.Clamp3.WXYZ1.w) 
								                     * _BoneData.Clamp3.WXYZ1.x
								                     , _BoneData.Clamp3.XYZ.y, _BoneData.Clamp3.XYZ.z);
							}
							if ( _BoneData.Clamp3.Index == 8 ) {
								clamp3 = Mathf.Clamp( -( (umaData.DNAList2[_BoneData.Clamp3.Conv1.Index].Value ) - _BoneData.Clamp3.WXYZ1.w) 
								                     * _BoneData.Clamp3.WXYZ1.x
								                     , _BoneData.Clamp3.XYZ.y, _BoneData.Clamp3.XYZ.z);
							}
							if ( _BoneData.Clamp3.Index == 9 ) {
								clamp3 = Mathf.Clamp( ( (umaData.DNAList2[_BoneData.Clamp3.Conv1.Index].Value ) - _BoneData.Clamp3.WXYZ1.w) 
								                     * _BoneData.Clamp3.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp3.Conv2.Index].Value ) - _BoneData.Clamp3.WXYZ1.y) 
								                     * _BoneData.Clamp3.WXYZ1.z
								                     + ( (umaData.DNAList2[_BoneData.Clamp3.Conv3.Index].Value ) - _BoneData.Clamp3.WXYZ2.w) 
								                     , _BoneData.Clamp3.XYZ.y, _BoneData.Clamp3.XYZ.z);
							}
							if ( _BoneData.Clamp3.Index == 10 ) {
								clamp3 = Mathf.Clamp(_BoneData.Clamp3.XYZ.x 
								                     - ( (umaData.DNAList2[_BoneData.Clamp3.Conv1.Index].Value ) - _BoneData.Clamp3.WXYZ1.w) 
								                     * _BoneData.Clamp3.WXYZ1.x
								                     , _BoneData.Clamp3.XYZ.y, _BoneData.Clamp3.XYZ.z);
							}
							// Apply
							if ( _BoneData.Clamp1.Type == 1 ) {
								umaData.boneList[_RaceData.BoneDataList[i].Name].boneTransform.localScale = new Vector3( clamp1, clamp2, clamp3 );
							}
							if ( _BoneData.Clamp1.Type == 2 ) {
								umaData.boneList[_RaceData.BoneDataList[i].Name].boneTransform.localEulerAngles = new Vector3( clamp1, clamp2, clamp3 );
							}
							if ( _BoneData.Clamp1.Type == 3 ) {
								umaData.boneList[_RaceData.BoneDataList[i].Name].boneTransform.localPosition = umaData.boneList[_RaceData.BoneDataList[i].Name].actualBonePosition + new Vector3( clamp1, clamp2, clamp3 );
							}
							if ( _BoneData.Clamp1.Type == 4 ) {
								umaData.ChangeBoneScale(_RaceData.BoneDataList[i].Name, new Vector3( clamp1, clamp2, clamp3 ));

							}
							if ( _BoneData.Clamp1.Type == 5 ) {
								umaData.ChangeBoneMoveRelative(_RaceData.BoneDataList[i].Name, new Vector3( clamp1, clamp2, clamp3 ));
							}
							if ( _BoneData.Clamp1.Type == 6 ) {
								umaData.boneList[_RaceData.BoneDataList[i].Name].boneTransform.localPosition = umaData.boneList[_RaceData.BoneDataList[i].LinkedTo].actualBonePosition + new Vector3( clamp1, clamp2, clamp3 );
							}
						}
						else {

							if ( _BoneData.Clamp1.Index == 1 ) {
								_Vector3 = new Vector3(_BoneData.Clamp1.XYZ.x, _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 2 ) {
								_Vector3 = new Vector3(_BoneData.Clamp1.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 3 ) {
								_Vector3 = new Vector3(_BoneData.Clamp1.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv2.Index].Value ) - _BoneData.Clamp1.WXYZ1.y) 
								                     * _BoneData.Clamp1.WXYZ1.z
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 4 ) {
								_Vector3 = new Vector3(_BoneData.Clamp1.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv2.Index].Value ) - _BoneData.Clamp1.WXYZ1.y) 
								                     * _BoneData.Clamp1.WXYZ1.z
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv3.Index].Value ) - _BoneData.Clamp1.WXYZ2.w) 
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 5 ) {
								_Vector3 = new Vector3(_BoneData.Clamp1.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv2.Index].Value ) - _BoneData.Clamp1.WXYZ1.y) 
								                     * _BoneData.Clamp1.WXYZ1.z
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv3.Index].Value ) - _BoneData.Clamp1.WXYZ2.w) 
								                     * _BoneData.Clamp1.WXYZ2.x
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 6 ) {
								_Vector3 = new Vector3(_BoneData.Clamp1.XYZ.x 
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv2.Index].Value ) - _BoneData.Clamp1.WXYZ1.y) 
								                     * _BoneData.Clamp1.WXYZ1.z
								                     - ( (umaData.DNAList2[_BoneData.Clamp1.Conv3.Index].Value ) - _BoneData.Clamp1.WXYZ2.w) 
								                     * _BoneData.Clamp1.WXYZ2.x
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 7 ) {
								_Vector3 = new Vector3( ( umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value - _BoneData.Clamp1.WXYZ1.w ) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);

							}
							if ( _BoneData.Clamp1.Index == 8 ) {
								_Vector3 = new Vector3( -( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 9 ) {
								_Vector3 = new Vector3( ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv2.Index].Value ) - _BoneData.Clamp1.WXYZ1.y) 
								                     * _BoneData.Clamp1.WXYZ1.z
								                     + ( (umaData.DNAList2[_BoneData.Clamp1.Conv3.Index].Value ) - _BoneData.Clamp1.WXYZ2.w) 
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							if ( _BoneData.Clamp1.Index == 10 ) {
								_Vector3 = new Vector3(_BoneData.Clamp1.XYZ.x 
								                     - ( (umaData.DNAList2[_BoneData.Clamp1.Conv1.Index].Value ) - _BoneData.Clamp1.WXYZ1.w) 
								                     * _BoneData.Clamp1.WXYZ1.x
								                     , _BoneData.Clamp1.XYZ.y, _BoneData.Clamp1.XYZ.z);
							}
							// Apply
							if ( _BoneData.Clamp1.Type == 1 ) {
								umaData.boneList[_RaceData.BoneDataList[i].Name].boneTransform.localScale = _Vector3;
							}
							if ( _BoneData.Clamp1.Type == 2 ) {
								umaData.boneList[_RaceData.BoneDataList[i].Name].boneTransform.localEulerAngles = _Vector3 /* new Vector3( clamp1, clamp2, clamp3 ) */;

							}
							if ( _BoneData.Clamp1.Type == 3 ) {
								umaData.boneList[_RaceData.BoneDataList[i].Name].boneTransform.localPosition = umaData.boneList[_RaceData.BoneDataList[i].Name].actualBonePosition + _Vector3;
							}
							if ( _BoneData.Clamp1.Type == 4 ) {
								umaData.ChangeBoneScale(_RaceData.BoneDataList[i].Name, _Vector3);
							}
							if ( _BoneData.Clamp1.Type == 5 ) {
								umaData.ChangeBoneMoveRelative(_RaceData.BoneDataList[i].Name, new Vector3( clamp1, clamp2, clamp3 ));
							}
							if ( _BoneData.Clamp1.Type == 6 ) {
								umaData.boneList[_RaceData.BoneDataList[i].Name].boneTransform.localPosition = umaData.boneList[_RaceData.BoneDataList[i].LinkedTo].actualBonePosition + _Vector3;
							}
						}
					}
				}
			}
		}
    }
}