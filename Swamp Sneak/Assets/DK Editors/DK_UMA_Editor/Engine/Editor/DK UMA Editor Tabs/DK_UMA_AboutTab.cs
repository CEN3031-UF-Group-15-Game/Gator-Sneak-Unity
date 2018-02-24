using UnityEngine;
using System.Collections;
using UnityEditor;

public class DK_UMA_AboutTab : EditorWindow {
	public static Color Green = new Color (0.8f, 1f, 0.8f, 1);
	public static Color Red = new Color (0.9f, 0.5f, 0.5f);

	public static Vector2 scroll;


	public static void OnGUI () {
		#region fonts variables
		var bold = new GUIStyle ("label");
		var boldFold = new GUIStyle ("foldout");
		bold.fontStyle = FontStyle.Bold;
		bold.fontSize = 14;
		boldFold.fontStyle = FontStyle.Bold;
		var Slim = new GUIStyle ("label");
		Slim.fontStyle = FontStyle.Normal;
		Slim.fontSize = 10;	
		var style = new GUIStyle ("label");
		style.wordWrap = true;
		#endregion fonts variables

		using (new HorizontalCentered()) 
		{
			GUI.color = Color.white;
			GUILayout.Label ( "Dynamic Kit U.M.A. Editor 2", bold);
		}
		using (new HorizontalCentered()) 
		{
			GUILayout.Label ( "version 2.6.3 for UMA 2.6.");
		}

		using (new HorizontalCentered()) 
		{
			GUI.color = Green;
			GUILayout.TextField("(c) 2014,2017 DK UMA Team", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		}


		using (new Horizontal()){
			GUI.color = Color.yellow;
			if(GUILayout.Button("Website")){
				Application.OpenURL ("http://alteredreality.wix.com/dk-uma");
			}
			GUI.color = Green;
			if(GUILayout.Button("DK Forum")){
				Application.OpenURL ("http://unity3d-dk-tools.boards.net/");
			}
			GUI.color = Color.cyan;
			if(GUILayout.Button("Facebook Page")){
				Application.OpenURL ("https://www.facebook.com/DKeditorsUnity3D");
			}
			GUI.color = Color.yellow;
			if(GUILayout.Button("Web Documentation")){
				DK_UMA_Editor.OpenForumTuto ();
			}
		}
		//	GUILayout.Space(15);
		GUILayout.TextField("The Premium package contains all the tools for DK UMA.", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
		GUI.color = Color.white;
		if(GUILayout.Button("DK UMA Premium")){
			Application.OpenURL ("https://www.assetstore.unity3d.com/en/#!/content/45131");
		}

		GUILayout.Space(10);
		using (new ScrollView(ref scroll)) {
			// tools
			GUILayout.TextField("Premium Tools :", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));

			GUI.color = Color.white;
			if(GUILayout.Button("Import UMA Content Editor")){
			}
			if(GUILayout.Button("DK UMA Items Manager")){
				Application.OpenURL ("https://www.assetstore.unity3d.com/#!/content/74815");
			}
			if(GUILayout.Button("DK UMA Race Creator Editor")){
				Application.OpenURL ("https://www.assetstore.unity3d.com/#!/content/67895");
			}
			if(GUILayout.Button("DK UMA Ingame Creator")){
				Application.OpenURL ("https://www.assetstore.unity3d.com/#!/content/50237");
			}
			if(GUILayout.Button("DK UMA RPG Avatar Editor")){
				Application.OpenURL ("https://www.assetstore.unity3d.com/#!/content/37697");
			}
			GUI.color = Color.white;
			if(GUILayout.Button("UMA Natural Behaviour")){
				Application.OpenURL ("https://www.assetstore.unity3d.com/#!/content/20836");
			}

			// Integrations
			GUI.color = Color.white;

			GUILayout.TextField("Integrations :", 256, style, GUILayout.ExpandWidth (true), GUILayout.ExpandWidth (true));
			EditorGUILayout.HelpBox("They are examples of the possibilities of DK UMA used with other Unity assets." +
				" Sometime a new integration will be released, some other time an integration will be removed.", UnityEditor.MessageType.Info);
		
			using (new Horizontal()){
				GUI.color = Color.white;
				if(GUILayout.Button("Ootii Motion Controller")){
					//	Application.OpenURL ("http://unity3d-dk-tools.boards.net/thread/30/dk-uma-zeranos-rpg-kit");
				}
				GUI.color = Green;
				if(GUILayout.Button("Info",  GUILayout.ExpandWidth (false) ) ){
					Application.OpenURL ("https://www.assetstore.unity3d.com/en/#!/content/15672");
				}
				GUILayout.Label ( "Finished", GUILayout.Width (60) );
			}
			using (new Horizontal()){
				GUI.color = Color.white;
				if(GUILayout.Button("Invector 3rd Person Controller v1.3e")){
					//	Application.OpenURL ("http://unity3d-dk-tools.boards.net/thread/30/dk-uma-zeranos-rpg-kit");
				}
				GUI.color = Green;
				if(GUILayout.Button("Info",  GUILayout.ExpandWidth (false) ) ){
					Application.OpenURL ("https://www.assetstore.unity3d.com/en/#!/content/44227");
				}
				GUILayout.Label ( "Finished", GUILayout.Width (60) );
			}
			using (new Horizontal()){
				GUI.color = Color.white;
				if(GUILayout.Button("Invector 3rd Person Controller Combat v2.0")){
					//	Application.OpenURL ("http://unity3d-dk-tools.boards.net/thread/30/dk-uma-zeranos-rpg-kit");
				}
				GUI.color = Green;
				if(GUILayout.Button("Info",  GUILayout.ExpandWidth (false) ) ){
					Application.OpenURL ("https://www.assetstore.unity3d.com/en/#!/content/44227");
				}
				GUILayout.Label ( "Finished", GUILayout.Width (60) );
			}
			using (new Horizontal()){
				GUI.color = Color.white;
				if(GUILayout.Button("Invector 3rd Person Controller Shooter")){
					//	Application.OpenURL ("http://unity3d-dk-tools.boards.net/thread/30/dk-uma-zeranos-rpg-kit");
				}
				GUI.color = Color.gray;
				if(GUILayout.Button("Info",  GUILayout.ExpandWidth (false) ) ){
					
				}
				GUI.color = Color.white;
				GUILayout.Label ( "Delayed", GUILayout.Width (60) );
			}

			using (new Horizontal()){
				GUI.color = Color.white;
				if(GUILayout.Button("ORK Framework")){
					//	Application.OpenURL ("http://unity3d-dk-tools.boards.net/thread/30/dk-uma-zeranos-rpg-kit");
				}
				GUI.color = Green;
				if(GUILayout.Button("Info",  GUILayout.ExpandWidth (false) ) ){
					Application.OpenURL ("https://www.assetstore.unity3d.com/en/#!/content/14419");
				}
				GUILayout.Label ( "Beta 1.0", GUILayout.Width (60) );
			}

			/*	using (new Horizontal()){
						GUI.color = Color.white;
						if(GUILayout.Button("Opsive 3rd Person Controller v1.3")){
						//	Application.OpenURL ("http://unity3d-dk-tools.boards.net/thread/30/dk-uma-zeranos-rpg-kit");
						}
						GUI.color = Green;
						if(GUILayout.Button("Info",  GUILayout.ExpandWidth (false) ) ){
							Application.OpenURL ("https://www.assetstore.unity3d.com/en/#!/content/27438");
						}
						GUILayout.Label ( "Alpha 0.9", GUILayout.Width (60) );
					}	*/	
		
			/*	using (new Horizontal()){
				GUI.color = Color.white;
				if(GUILayout.Button("Final IK")){
					//	Application.OpenURL ("http://unity3d-dk-tools.boards.net/thread/30/dk-uma-zeranos-rpg-kit");
				}
				GUI.color = Green;
				if(GUILayout.Button("Info",  GUILayout.ExpandWidth (false) ) ){
					Application.OpenURL ("https://www.assetstore.unity3d.com/en/#!/content/14290");
				}
				GUILayout.Label ( "Beta 1.0", GUILayout.Width (60) );
			}*/
		GUI.color = Color.white;
			if(GUILayout.Button("Inventory Master Integration")){
				Application.OpenURL ("https://www.assetstore.unity3d.com/#!/content/26310");
			}
		}
	}
}
