using UnityEngine;
using System.Collections;

public class EditorStoreDNA : MonoBehaviour {
	public static DKUMADnaHumanoid umaDna;
	
	public static void StoreDNAValues( DKUMAData _umaData, int i ){
		
		if ( _umaData.DNAList2[i].Name == EditorVariables.tmpDNAList[i].Name
		    && _umaData.DNAList2[i].Value != EditorVariables.tmpDNAList[i].Value)
		{
			_umaData.DNAList2[i].Value = EditorVariables.tmpDNAList[i].Value;
			if ( umaDna == null ) 
				umaDna = _umaData.umaRecipe.umaDna[typeof(DKUMADnaHumanoid)] as DKUMADnaHumanoid;
			
			float tmpValue = EditorVariables.tmpDNAList[i].Value;
			
			if ( i == 0 ) umaDna.N0 = tmpValue;if ( i == 1 ) umaDna.N1 = tmpValue;if ( i == 2 ) umaDna.N2 = tmpValue;
			if ( i == 3 ) umaDna.N3 = tmpValue;if ( i == 4 ) umaDna.N4 = tmpValue;if ( i == 5 ) umaDna.N5 = tmpValue;
			if ( i == 6 ) umaDna.N6 = tmpValue;if ( i == 7 ) umaDna.N7 = tmpValue;if ( i == 8 ) umaDna.N8 = tmpValue;
			if ( i == 9 ) umaDna.N9 = tmpValue;if ( i == 10 ) umaDna.N10 = tmpValue;if ( i == 11 ) umaDna.N11 = tmpValue;
			if ( i == 12 ) umaDna.N12 = tmpValue;if ( i == 13 ) umaDna.N13 = tmpValue;if ( i == 14 ) umaDna.N14 = tmpValue;
			if ( i == 15 ) umaDna.N15 = tmpValue;if ( i == 16 ) umaDna.N16 = tmpValue;if ( i == 17 ) umaDna.N17 = tmpValue;
			if ( i == 18 ) umaDna.N18 = tmpValue;if ( i == 19 ) umaDna.N19 = tmpValue;if ( i == 20 ) umaDna.N20 = tmpValue;
			if ( i == 21 ) umaDna.N21 = tmpValue;if ( i == 22 ) umaDna.N22 = tmpValue;if ( i == 23 ) umaDna.N23 = tmpValue;
			if ( i == 24 ) umaDna.N24 = tmpValue;if ( i == 25 ) umaDna.N25 = tmpValue;if ( i == 26 ) umaDna.N26 = tmpValue;
			if ( i == 27 ) umaDna.N27 = tmpValue;if ( i == 28 ) umaDna.N28 = tmpValue;if ( i == 29 ) umaDna.N29 = tmpValue;
			if ( i == 30 ) umaDna.N30 = tmpValue;if ( i == 31 ) umaDna.N31 = tmpValue;if ( i == 32 ) umaDna.N32 = tmpValue;
			if ( i == 33 ) umaDna.N33 = tmpValue;if ( i == 34 ) umaDna.N34 = tmpValue;if ( i == 35 ) umaDna.N35 = tmpValue;
			if ( i == 36 ) umaDna.N36 = tmpValue;if ( i == 37 ) umaDna.N37 = tmpValue;if ( i == 38 ) umaDna.N38 = tmpValue;
			if ( i == 39 ) umaDna.N39 = tmpValue;if ( i == 40 ) umaDna.N40 = tmpValue;if ( i == 41 ) umaDna.N41 = tmpValue;
			if ( i == 42 ) umaDna.N42 = tmpValue;if ( i == 43 ) umaDna.N43 = tmpValue;if ( i == 44 ) umaDna.N44 = tmpValue;
			if ( i == 45 ) umaDna.N45 = tmpValue;if ( i == 46 ) umaDna.N46 = tmpValue;if ( i == 47 ) umaDna.N47 = tmpValue;
			if ( i == 48 ) umaDna.N48 = tmpValue;if ( i == 49 ) umaDna.N49 = tmpValue;if ( i == 50 ) umaDna.N50 = tmpValue;
			if ( i == 51 ) umaDna.N51 = tmpValue;if ( i == 52 ) umaDna.N52 = tmpValue;
			if ( i == 53 ) umaDna.N53 = tmpValue;if ( i == 54 ) umaDna.N54 = tmpValue;if ( i == 55 ) umaDna.N55 = tmpValue;
			if ( i == 56 ) umaDna.N56 = tmpValue;if ( i == 57 ) umaDna.N57 = tmpValue;if ( i == 58 ) umaDna.N58 = tmpValue;
			if ( i == 59 ) umaDna.N59 = tmpValue;
			if ( i == 60 ) umaDna.N60 = tmpValue;if ( i == 61 ) umaDna.N61 = tmpValue;if ( i == 62 ) umaDna.N62 = tmpValue;
			if ( i == 63 ) umaDna.N63 = tmpValue;if ( i == 64 ) umaDna.N64 = tmpValue;if ( i == 65 ) umaDna.N65 = tmpValue;
			if ( i == 66 ) umaDna.N66 = tmpValue;if ( i == 67 ) umaDna.N67 = tmpValue;if ( i == 68 ) umaDna.N68 = tmpValue;
			if ( i == 69 ) umaDna.N69 = tmpValue;if ( i == 70 ) umaDna.N70 = tmpValue;
			
			_umaData.umaRecipe.umaDna.Remove(umaDna.GetType());
			_umaData.umaRecipe.umaDna.Add(umaDna.GetType(),umaDna);
			_umaData.Dirty(true,false,false);
		}	
	}
}
