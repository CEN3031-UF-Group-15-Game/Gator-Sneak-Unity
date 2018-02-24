// UMA Auto genered code, DO NOT MODIFY!!!
// All changes to this file will be destroyed without warning or confirmation!
// Use double { to escape a single curly bracket
//
// template junk executed per dna Field , the accumulated content is available through the {0:ID} tag
//
//#TEMPLATE GetValues UmaDnaChild_GetIndex_Fragment.cs.txt
//#TEMPLATE SetValues UmaDnaChild_SetIndex_Fragment.cs.txt
//#TEMPLATE GetNames UmaDnaChild_GetNames_Fragment.cs.txt
//
// Byte Serialization Handling
// 
//#TEMPLATE Byte_Fields UmaDnaChild_Byte_Fields_Fragment.cs.txt
//#TEMPLATE Byte_ToDna UmaDnaChild_Byte_ToDna_Fragment.cs.txt
//#TEMPLATE Byte_FromDna UmaDnaChild_Byte_FromDna_Fragment.cs.txt
//


public partial class DKUMADnaHumanoid
{
	public override int Count { get { return 71; } }
	public override float[] Values
	{ 
		get 
		{
			return new float[] 
			{
				N0,
				N1,
				N2,
				N3,
				N4,
				N5,
				N6,
				N7,
				N8,
				N9,

				N10,
				N11,
				N12,
				N13,
				N14,
				N15,
				N16,
				N17,
				N18,
				N19,

				N20,
				N21,
				N22,
				N23,
				N24,
				N25,
				N26,
				N27,
				N28,
				N29,

				N30,
				N31,
				N32,
				N33,
				N34,
				N35,
				N36,
				N37,
				N38,
				N39,
				
				N40,
				N41,
				N42,
				N43,
				N44,
				N45,
				N46,
				N47,
				N48,
				N49,

				N50,
				N51,
				N52,
				N53,
				N54,
				N55,
				N56,
				N57,
				N58,
				N59,

				N60,
				N61,
				N62,
				N63,
				N64,
				N65,
				N66,
				N67,
				N68,
				N69,
				
				N70,
			};
		}
		set
		{
			N0 = value[0];
			N1 = value[1];
			N2 = value[2];
			N3 = value[3];
			N4 = value[4];
			N5 = value[5];
			N6 = value[6];
			N7 = value[7];
			N8 = value[8];
			N9 = value[9];

			N10 = value[10];
			N11 = value[11];
			N12 = value[12];
			N13 = value[13];
			N14 = value[14];
			N15 = value[15];
			N16 = value[16];
			N17 = value[17];
			N18 = value[18];
			N19 = value[19];

			N20 = value[20];
			N21 = value[21];
			N22 = value[22];
			N23 = value[23];
			N24 = value[24];
			N25 = value[25];
			N26 = value[26];
			N27 = value[27];
			N28 = value[28];
			N29 = value[29];

			N30 = value[30];
			N31 = value[31];
			N32 = value[32];
			N33 = value[33];
			N34 = value[34];
			N35 = value[35];
			N36 = value[36];
			N37 = value[37];
			N38 = value[38];
			N39 = value[39];
		
			N40 = value[40];
			N41 = value[41];
			N42 = value[42];
			N43 = value[43];
			N44 = value[44];
			N45 = value[45];
			N46 = value[46];
			N47 = value[47];
			N48 = value[48];
			N49 = value[49];
		
			N50 = value[50];
			N51 = value[51];
			N52 = value[52];
			N53 = value[53];
			N54 = value[54];
			N55 = value[55];
			N56 = value[56];
			N57 = value[57];
			N58 = value[58];
			N59 = value[59];

			N60 = value[60];
			N61 = value[61];
			N62 = value[62];
			N63 = value[63];
			N64 = value[64];
			N65 = value[65];
			N66 = value[66];
			N67 = value[67];
			N68 = value[68];
			N69 = value[69];
			
			N70 = value[70];
		}
	}
	public static string[] GetNames()
	{
		return new string[]
		{
			"N0",
			"N1",
			"N2",
			"N3",
			"N4",
			"N5",
			"N6",
			"N7",
			"N8",
			"N9",
		
			"N10",
			"N11",
			"N12",
			"N13",
			"N14",
			"N15",
			"N16",
			"N17",
			"N18",
			"N19",
		
			"N20",
			"N21",
			"N22",
			"N23",
			"N24",
			"N25",
			"N26",
			"N27",
			"N28",
			"N29",
		
			"N30",
			"N31",
			"N32",
			"N33",
			"N34",
			"N35",
			"N36",
			"N37",
			"N38",
			"N39",
		
			"N40",
			"N41",
			"N42",
			"N43",
			"N44",
			"N45",
			"N46",
			"N47",
			"N48",
			"N49",

			"N50",
			"N51",
			"N52",
			"N53",
			"N54",
			"N55",
			"N56",
			"N57",
			"N58",
			"N59",

			"N60",
			"N61",
			"N62",
			"N63",
			"N64",
			"N65",
			"N66",
			"N67",
			"N68",
			"N69",
			
			"N70",

		};
	}
	public override string[] Names
	{
		get
		{
			return GetNames();
		}
	}
	public static DKUMADnaHumanoid LoadInstance(string data)
    {
	//	#if UNITY_5_5_OR_NEWER
		return UnityEngine.JsonUtility.FromJson<DKUMADnaHumanoid_Byte>(data).ToDna();
	//	#else
	//	return LitJson.JsonMapper.ToObject<DKUMADnaHumanoid_Byte>(data).ToDna();
	//	#endif
        
    }
	public static string SaveInstance(DKUMADnaHumanoid instance)
	{
	//	#if UNITY_5_5_OR_NEWER
		return UnityEngine.JsonUtility.ToJson(DKUMADnaHumanoid_Byte.FromDna(instance));
	//	#else
	//	return LitJson.JsonMapper.ToJson(DKUMADnaHumanoid_Byte.FromDna(instance));
	//	#endif
	}
}

[System.Serializable]
public class DKUMADnaHumanoid_Byte
{
		public System.Byte N0;
		public System.Byte N1;
		public System.Byte N2;
		public System.Byte N3;
		public System.Byte N4;
		public System.Byte N5;
		public System.Byte N6;
		public System.Byte N7;
		public System.Byte N8;
		public System.Byte N9;
		public System.Byte N10;
		public System.Byte N11;
		public System.Byte N12;
		public System.Byte N13;
		public System.Byte N14;
		public System.Byte N15;
		public System.Byte N16;
		public System.Byte N17;
		public System.Byte N18;
		public System.Byte N19;
		public System.Byte N20;
		public System.Byte N21;
		public System.Byte N22;
		public System.Byte N23;
		public System.Byte N24;
		public System.Byte N25;
		public System.Byte N26;
		public System.Byte N27;
		public System.Byte N28;
		public System.Byte N29;
		public System.Byte N30;
		public System.Byte N31;
		public System.Byte N32;
		public System.Byte N33;
		public System.Byte N34;
		public System.Byte N35;
		public System.Byte N36;
		public System.Byte N37;
		public System.Byte N38;
		public System.Byte N39;
		public System.Byte N40;
		public System.Byte N41;
		public System.Byte N42;
		public System.Byte N43;
		public System.Byte N44;
		public System.Byte N45;
		public System.Byte N46;
		public System.Byte N47;
		public System.Byte N48;
		public System.Byte N49;

		public System.Byte N50;
		public System.Byte N51;
		public System.Byte N52;
		public System.Byte N53;
		public System.Byte N54;
		public System.Byte N55;
		public System.Byte N56;
		public System.Byte N57;
		public System.Byte N58;
		public System.Byte N59;

		public System.Byte N60;
		public System.Byte N61;
		public System.Byte N62;
		public System.Byte N63;
		public System.Byte N64;
		public System.Byte N65;
		public System.Byte N66;
		public System.Byte N67;
		public System.Byte N68;
		public System.Byte N69;
		
		public System.Byte N70;

	public DKUMADnaHumanoid ToDna()
	{
		var res = new DKUMADnaHumanoid();
		res.N0 = N0 * (1f / 255f);
		res.N1 = N1 * (1f / 255f);
		res.N2 = N2 * (1f / 255f);
		res.N3 = N3 * (1f / 255f);
		res.N4 = N4 * (1f / 255f);
		res.N5 = N5 * (1f / 255f);
		res.N6 = N6 * (1f / 255f);
		res.N7 = N7 * (1f / 255f);
		res.N8 = N8 * (1f / 255f);
		res.N9 = N9 * (1f / 255f);
		res.N10 = N10 * (1f / 255f);
		res.N11 = N11 * (1f / 255f);
		res.N12 = N12 * (1f / 255f);
		res.N13 = N13 * (1f / 255f);
		res.N14 = N14 * (1f / 255f);
		res.N15 = N15 * (1f / 255f);
		res.N16 = N16 * (1f / 255f);
		res.N17 = N17 * (1f / 255f);
		res.N18 = N18 * (1f / 255f);
		res.N19 = N19 * (1f / 255f);
		res.N20 = N20 * (1f / 255f);
		res.N21 = N21 * (1f / 255f);
		res.N22 = N22 * (1f / 255f);
		res.N23 = N23 * (1f / 255f);
		res.N24 = N24 * (1f / 255f);
		res.N25 = N25 * (1f / 255f);
		res.N26 = N26 * (1f / 255f);
		res.N27 = N27 * (1f / 255f);
		res.N28 = N28 * (1f / 255f);
		res.N29 = N29 * (1f / 255f);
		res.N30 = N30 * (1f / 255f);
		res.N31 = N31 * (1f / 255f);
		res.N32 = N32 * (1f / 255f);
		res.N33 = N33 * (1f / 255f);
		res.N34 = N34 * (1f / 255f);
		res.N35 = N35 * (1f / 255f);
		res.N36 = N36 * (1f / 255f);
		res.N37 = N37 * (1f / 255f);
		res.N38 = N38 * (1f / 255f);
		res.N39 = N39 * (1f / 255f);
		res.N40 = N40 * (1f / 255f);
		res.N41 = N41 * (1f / 255f);
		res.N42 = N42 * (1f / 255f);
		res.N43 = N43 * (1f / 255f);
		res.N44 = N44 * (1f / 255f);
		res.N45 = N45 * (1f / 255f);
		res.N46 = N46 * (1f / 255f);
		res.N47 = N47 * (1f / 255f);
		res.N48 = N48 * (1f / 255f);
		res.N49 = N49 * (1f / 255f);
		res.N50 = N50 * (1f / 255f);
		res.N51 = N51 * (1f / 255f);
		res.N52 = N52 * (1f / 255f);
		res.N53 = N53 * (1f / 255f);
		res.N54 = N54 * (1f / 255f);
		res.N55 = N55 * (1f / 255f);
		res.N56 = N56 * (1f / 255f);
		res.N57 = N57 * (1f / 255f);
		res.N58 = N58 * (1f / 255f);
		res.N59 = N59 * (1f / 255f);
		res.N60 = N60 * (1f / 255f);
		res.N61 = N61 * (1f / 255f);
		res.N62 = N62 * (1f / 255f);
		res.N63 = N63 * (1f / 255f);
		res.N64 = N64 * (1f / 255f);
		res.N65 = N65 * (1f / 255f);
		res.N66 = N66 * (1f / 255f);
		res.N67 = N67 * (1f / 255f);
		res.N68 = N68 * (1f / 255f);
		res.N69 = N69 * (1f / 255f);
		res.N70 = N70 * (1f / 255f);
		return res;
	}
	public static DKUMADnaHumanoid_Byte FromDna(DKUMADnaHumanoid dna)
	{
		var res = new DKUMADnaHumanoid_Byte();
		res.N0 = (System.Byte)(dna.N0 * 255f+0.5f);
		res.N1 = (System.Byte)(dna.N1 * 255f+0.5f);
		res.N2 = (System.Byte)(dna.N2 * 255f+0.5f);
		res.N3 = (System.Byte)(dna.N3 * 255f+0.5f);
		res.N4 = (System.Byte)(dna.N4 * 255f+0.5f);
		res.N5 = (System.Byte)(dna.N5 * 255f+0.5f);
		res.N6 = (System.Byte)(dna.N6 * 255f+0.5f);
		res.N7 = (System.Byte)(dna.N7 * 255f+0.5f);
		res.N8 = (System.Byte)(dna.N8 * 255f+0.5f);
		res.N9 = (System.Byte)(dna.N9 * 255f+0.5f);
		res.N10 = (System.Byte)(dna.N10 * 255f+0.5f);
		res.N11 = (System.Byte)(dna.N11 * 255f+0.5f);
		res.N12 = (System.Byte)(dna.N12 * 255f+0.5f);
		res.N13 = (System.Byte)(dna.N13 * 255f+0.5f);
		res.N14 = (System.Byte)(dna.N14 * 255f+0.5f);
		res.N15 = (System.Byte)(dna.N15 * 255f+0.5f);
		res.N16 = (System.Byte)(dna.N16 * 255f+0.5f);
		res.N17 = (System.Byte)(dna.N17 * 255f+0.5f);
		res.N18 = (System.Byte)(dna.N18 * 255f+0.5f);
		res.N19 = (System.Byte)(dna.N19 * 255f+0.5f);
		res.N20 = (System.Byte)(dna.N20 * 255f+0.5f);
		res.N21 = (System.Byte)(dna.N21 * 255f+0.5f);
		res.N22 = (System.Byte)(dna.N22 * 255f+0.5f);
		res.N23 = (System.Byte)(dna.N23 * 255f+0.5f);
		res.N24 = (System.Byte)(dna.N24 * 255f+0.5f);
		res.N25 = (System.Byte)(dna.N25 * 255f+0.5f);
		res.N26 = (System.Byte)(dna.N26 * 255f+0.5f);
		res.N27 = (System.Byte)(dna.N27 * 255f+0.5f);
		res.N28 = (System.Byte)(dna.N28 * 255f+0.5f);
		res.N29 = (System.Byte)(dna.N29 * 255f+0.5f);
		res.N30 = (System.Byte)(dna.N30 * 255f+0.5f);
		res.N31 = (System.Byte)(dna.N31 * 255f+0.5f);
		res.N32 = (System.Byte)(dna.N32 * 255f+0.5f);
		res.N33 = (System.Byte)(dna.N33 * 255f+0.5f);
		res.N34 = (System.Byte)(dna.N34 * 255f+0.5f);
		res.N35 = (System.Byte)(dna.N35 * 255f+0.5f);
		res.N36 = (System.Byte)(dna.N36 * 255f+0.5f);
		res.N37 = (System.Byte)(dna.N37 * 255f+0.5f);
		res.N38 = (System.Byte)(dna.N38 * 255f+0.5f);
		res.N39 = (System.Byte)(dna.N39 * 255f+0.5f);
		res.N40 = (System.Byte)(dna.N40 * 255f+0.5f);
		res.N41 = (System.Byte)(dna.N41 * 255f+0.5f);
		res.N42 = (System.Byte)(dna.N42 * 255f+0.5f);
		res.N43 = (System.Byte)(dna.N43 * 255f+0.5f);
		res.N44 = (System.Byte)(dna.N44 * 255f+0.5f);
		res.N45 = (System.Byte)(dna.N45 * 255f+0.5f);
		res.N46 = (System.Byte)(dna.N46 * 255f+0.5f);
		res.N47 = (System.Byte)(dna.N47 * 255f+0.5f);
		res.N48 = (System.Byte)(dna.N48 * 255f+0.5f);
		res.N49 = (System.Byte)(dna.N49 * 255f+0.5f);
		res.N50 = (System.Byte)(dna.N50 * 255f+0.5f);
		res.N51 = (System.Byte)(dna.N51 * 255f+0.5f);
		res.N52 = (System.Byte)(dna.N52 * 255f+0.5f);
		res.N53 = (System.Byte)(dna.N53 * 255f+0.5f);
		res.N54 = (System.Byte)(dna.N54 * 255f+0.5f);
		res.N55 = (System.Byte)(dna.N55 * 255f+0.5f);
		res.N56 = (System.Byte)(dna.N56 * 255f+0.5f);
		res.N57 = (System.Byte)(dna.N57 * 255f+0.5f);
		res.N58 = (System.Byte)(dna.N58 * 255f+0.5f);
		res.N59 = (System.Byte)(dna.N59 * 255f+0.5f);
		res.N60 = (System.Byte)(dna.N60 * 255f+0.5f);
		res.N61 = (System.Byte)(dna.N61 * 255f+0.5f);
		res.N62 = (System.Byte)(dna.N62 * 255f+0.5f);
		res.N63 = (System.Byte)(dna.N63 * 255f+0.5f);
		res.N64 = (System.Byte)(dna.N64 * 255f+0.5f);
		res.N65 = (System.Byte)(dna.N65 * 255f+0.5f);
		res.N66 = (System.Byte)(dna.N66 * 255f+0.5f);
		res.N67 = (System.Byte)(dna.N67 * 255f+0.5f);
		res.N68 = (System.Byte)(dna.N68 * 255f+0.5f);
		res.N69 = (System.Byte)(dna.N69 * 255f+0.5f);
		res.N70 = (System.Byte)(dna.N70 * 255f+0.5f);

		return res;
	}
}