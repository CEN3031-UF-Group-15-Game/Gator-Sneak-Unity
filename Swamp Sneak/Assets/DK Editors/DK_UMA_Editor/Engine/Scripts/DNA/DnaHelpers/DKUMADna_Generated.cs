// UMA Auto genered code, DO NOT MODIFY!!!
// All changes to this file will be destroyed without warning or confirmation!
// Use double { to escape a single curly bracket
//
// template junk executed per UMADna derived sub class, the accumulated content is available through the {0:ID} tag
//
//#TEMPLATE GetNames UmaDna_GetNames_Fragment.cs.txt
//#TEMPLATE GetType UmaDna_GetType_Fragment.cs.txt
//#TEMPLATE GetTypes UmaDna_GetTypes_Fragment.cs.txt
//#TEMPLATE Load UmaDna_Load_Fragment.cs.txt
//#TEMPLATE Save UmaDna_Save_Fragment.cs.txt
//
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
#if UNITY_5_5_OR_NEWER
#else
//using LitJson;
#endif
using System.Collections;
using System.Collections.Generic;
#endif


public partial class DKUMADna
{
	public virtual int Count { get { return 0; } }
	public virtual float[] Values
	{ 
		get 
		{
			return new float[0];
		}
		set
		{
		}
	}
	public virtual string[] Names
	{
		get
		{
			return new string[0];
		}
	}
	public static string[] GetNames(System.Type dnaType)
	{
		if( dnaType == typeof(DKUMADnaHumanoid) )
			return DKUMADnaHumanoid.GetNames(dnaType);

		return new string[0];
	}

	public static System.Type GetType(System.String className)
	{
		if( "DKUMADnaHumanoid" == className ) 
			return typeof(DKUMADnaHumanoid);

		return null;
	}

	public static System.Type[] GetTypes()
	{
		return new System.Type[]
		{
			typeof(DKUMADnaHumanoid),
		};
	}

	public static DKUMADna LoadInstance(System.Type dnaType, System.String data)
	{
		if( dnaType == typeof(DKUMADnaHumanoid))
			return DKUMADnaHumanoid.LoadInstance(data);

		return null;
	}

	public static System.String SaveInstance(DKUMADna instance)
	{
		System.Type dnaType = instance.GetType();
		if( dnaType == typeof(DKUMADnaHumanoid))
			return DKUMADnaHumanoid.SaveInstance(instance as DKUMADnaHumanoid);

		return null;
	}

}

