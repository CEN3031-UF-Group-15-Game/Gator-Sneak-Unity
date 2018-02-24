using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKUMA {
	public class SetAllToMediumDNA : MonoBehaviour {
		
		public static void SetAllDNA ( DK_RPG_UMA avatar, bool Rebuild, bool SaveToPlayerPref ){
			// get DNA
			DKUMAData data = avatar.transform.GetComponent<DKUMAData>();
			// Set DNA
			foreach ( DKRaceData.DNAConverterData dna in data.DNAList2 ){
				dna.Value = 0.5f;
			}

			if ( Rebuild ) ApplyDNA (avatar);
		}

		public static void ApplyDNA (DK_RPG_UMA avatar){
			avatar.Rebuild ();
		}
	}
}
