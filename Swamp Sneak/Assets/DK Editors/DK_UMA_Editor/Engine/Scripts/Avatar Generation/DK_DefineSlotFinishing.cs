using UnityEngine;
using System.Collections;
using UMA.Examples;

public class DK_DefineSlotFinishing : MonoBehaviour {

	UMACrowd _UMACrowd;
	public static TransposeDK2UMA _TransposeDK2UMA;
	public static void Finish (DK_UMACrowd Crowd){

		Crowd.umaData.umaRecipe.slotDataList = Crowd.tempSlotList.ToArray();
		Crowd.umaData.atlasResolutionScale = Crowd.atlasResolutionScale;
		/*
		if ( Crowd.ToUMA == false ) {
			Crowd.umaData.Dirty(true,true,true);
			Crowd.umaData.OnUpdated += Crowd.MyOnUpdateMethod;

			Crowd.FleshOverlay = null;
			Crowd.Wears.RanActivateMesh = 0;

			Crowd.umaData.myRenderer.enabled = false;
			if ( Crowd.tempUMA.gameObject.GetComponent("DK_Model") as DK_Model == null ) 
			{
				Crowd.tempUMA.gameObject.AddComponent<DK_Model>();
			}
			Crowd._DK_Model = Crowd.tempUMA.GetComponent<DK_Model>();

			// Copy variables to DK_Model
			Crowd._DK_Model.Gender = Crowd.RaceAndGender.Gender;
			Crowd._DK_Model.IsUmaModel = true;
			Crowd.umaData.Awaking();
			//	if ( Crowd.zeroPoint == null ) Crowd.zeroPoint = GameObject.Find("ZeroPoint").transform;
			if(Crowd.zeroPoint){
				Crowd.tempUMA.transform.position = new Vector3(Crowd.zeroPoint.position.x,Crowd.zeroPoint.position.y,Crowd.zeroPoint.position.z);
			}else{
				Crowd.tempUMA.transform.position = new Vector3(0,0,0);
			}

		}*/
	

		/*
			if ( Crowd.umaDna.N0 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N0 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N0 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N0 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N1 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N1 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N1 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N1 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N2 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N2 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N2 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N2 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N3 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N3 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N3 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N3 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N4 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N4 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N4 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N4 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N5 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N5 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N5 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N5 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N6 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N6 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N6 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N6 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N7 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N7 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N7 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N7 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N8 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N8 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N8 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N8 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N9 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N9 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N9 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N9 = _TransposeDK2UMA.MaxiDNA;

			if ( Crowd.umaDna.N10 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N10 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N10 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N10 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N11 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N11 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N11 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N11 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N12 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N12 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N12 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N12 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N13 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N13 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N13 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N13 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N14 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N14 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N14 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N14 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N15 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N15 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N15 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N15 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N16 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N16 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N16 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N16 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N17 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N17 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N17 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N17 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N18 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N18 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N18 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N18 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N19 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N19 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N19 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N19 = _TransposeDK2UMA.MaxiDNA;

			if ( Crowd.umaDna.N20 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N20 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N20 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N20 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N21 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N21 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N21 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N21 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N22 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N22 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N22 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N22 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N23 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N23 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N23 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N23 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N24 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N24 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N24 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N24 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N25 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N25 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N25 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N25 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N26 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N26 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N26 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N26 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N27 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N27 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N27 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N27 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N28 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N28 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N28 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N28 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N29 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N29 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N29 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N29 = _TransposeDK2UMA.MaxiDNA;

			if ( Crowd.umaDna.N30 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N30 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N30 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N30 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N31 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N31 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N31 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N31 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N32 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N32 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N32 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N32 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N33 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N33 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N33 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N33 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N34 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N34 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N34 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N34 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N35 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N35 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N35 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N35 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N36 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N36 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N36 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N36 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N37 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N37 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N37 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N37 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N38 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N38 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N38 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N38 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N39 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N39 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N39 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N39 = _TransposeDK2UMA.MaxiDNA;

			if ( Crowd.umaDna.N40 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N40 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N40 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N40 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N41 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N41 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N41 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N41 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N42 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N42 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N42 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N42 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N43 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N43 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N43 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N43 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N44 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N44 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N44 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N44 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N45 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N45 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N45 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N45 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N46 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N46 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N46 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N46 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N47 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N47 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N47 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N47 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N48 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N48 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N48 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N48 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N49 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N49 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N49 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N49 = _TransposeDK2UMA.MaxiDNA;

			if ( Crowd.umaDna.N50 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N50 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N50 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N50 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N51 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N51 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N51 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N51 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N52 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N52 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N52 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N52 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N53 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N53 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N53 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N53 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N54 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N54 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N54 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N54 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N55 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N55 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N55 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N55 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N56 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N56 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N56 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N56 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N57 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N57 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N57 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N57 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N58 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N58 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N58 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N58 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N59 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N59 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N59 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N59 = _TransposeDK2UMA.MaxiDNA;

			if ( Crowd.umaDna.N60 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N60 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N60 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N60 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N61 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N61 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N61 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N61 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N62 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N62 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N62 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N62 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N63 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N63 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N63 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N63 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N64 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N64 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N64 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N64 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N65 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N65 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N65 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N65 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N66 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N66 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N66 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N66 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N67 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N67 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N67 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N67 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N68 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N68 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N68 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N68 = _TransposeDK2UMA.MaxiDNA;
			if ( Crowd.umaDna.N69 < _TransposeDK2UMA.MiniDNA ) Crowd.umaDna.N69 = _TransposeDK2UMA.MiniDNA;
			if ( Crowd.umaDna.N69 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N69 = _TransposeDK2UMA.MaxiDNA;

			if ( Crowd.umaDna.N70 > _TransposeDK2UMA.MaxiDNA ) Crowd.umaDna.N70 = _TransposeDK2UMA.MaxiDNA;
*/
		Crowd.umaData.umaRecipe.slotDataList = Crowd.tempSlotList.ToArray();
		
		Crowd.umaData.SaveToMemoryStream();
		string streamed =  Crowd.umaData.streamedUMA;

		if ( Crowd.ToUMA && Crowd.umaData != null ) {
			if ( Crowd.umaData.transform.gameObject.GetComponent<TransposeDK2UMA>() == null )
				_TransposeDK2UMA = Crowd.umaData.transform.gameObject.AddComponent<TransposeDK2UMA>();
			if ( _TransposeDK2UMA == null )
				Crowd.umaData.transform.gameObject.GetComponent<TransposeDK2UMA>();
			if ( _TransposeDK2UMA == null ){
				_TransposeDK2UMA  = Crowd.umaData.transform.gameObject.AddComponent<TransposeDK2UMA>();
				Debug.Log ("Launching Convertion DK RPG to UMA 2.X");
			}
			_TransposeDK2UMA.DKumaData = Crowd.umaData;
			_TransposeDK2UMA.Launch ( Crowd.umaData.transform.gameObject.GetComponent<DK_RPG_UMA>(), 
		                         Crowd, 
		                         streamed, false );
		
			// rebuild
			Rebuild ( Crowd.umaData.transform.gameObject.GetComponent<DK_RPG_UMA>() );
		}

	}

	public static void Rebuild ( DK_RPG_UMA _DK_RPG_UMA ){
		DK_RPG_ReBuild _DK_RPG_ReBuild = _DK_RPG_UMA.gameObject.GetComponent<DK_RPG_ReBuild>();
		if ( _DK_RPG_ReBuild == null ) _DK_RPG_ReBuild = _DK_RPG_UMA.gameObject.AddComponent<DK_RPG_ReBuild>();
		DKUMAData _DKUMAData = _DK_RPG_UMA.gameObject.GetComponent<DKUMAData>();
		_DK_RPG_ReBuild.RefreshOnly = true;
		_DK_RPG_ReBuild.Launch (_DKUMAData);
	}
}
