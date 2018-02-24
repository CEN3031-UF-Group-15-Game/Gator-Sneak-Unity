#region UsingStatements

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

#endregion

/// <summary>
/// 	- Simple, flexible SaveData class for serializing many data types.
/// 	- Only serializes public, non-static fields and classes.
/// 	- Does not support serialization of classes derived from Component object (Transforms, Renderers, Monobehaviours, ect)
/// </summary>
public class SaveData
{
	#region PublicStaticReadonlyFields
	
	/// <summary>
	/// 	- The saved file's extension.
	/// </summary>
	public static readonly string extension = ".uml";
	
	#endregion
	
	#region PublicFields
	
	/// <summary>
	/// 	- The name of the file.
	/// </summary>
	public string fileName = "SavedData";
	
	/// <summary>
	/// 	- Array used for serialization.
	/// 	- XML requires serialized fields to be public. This will always be empty;
	/// </summary>
	public string[] serializedTypes;
	
	/// <summary>
	/// 	- Array used for serialization.
	/// 	- XML requires serialized fields to be public. This will always be empty.
	/// </summary>
	public DataContainer[] serializedData;
	
	#endregion
	
	#region PublicParameters
	
	/// <summary>
	/// Gets or sets the <see cref="SaveData"/> with the specified key.
	/// </summary>
	/// <param name='key'>
	/// 	- Key to set.
	/// </param>
	public System.Object this[string key]
	{
		get { return _data[key]; }
		set 
		{ 
			if(typeof(Component).IsAssignableFrom(value.GetType())) throw new System.InvalidOperationException("Cannot serialize classes derived from Component!");
			_data[key] = value;
		}
	}
	
	#endregion
	
	#region PrivateFields
	
	/// <summary>
	/// 	- Actual data storage.
	/// </summary>
	private Dictionary<string, System.Object> _data = new Dictionary<string, object>();
	
	#endregion
	
	#region Constructors
	
	/// <summary>
	/// 	- Initializes a new instance of the <see cref="SaveData"/> class.
	/// </summary>
	public SaveData(){}
	
	/// <summary>
	/// 	- Initializes a new instance of the <see cref="SaveData"/> class.
	/// </summary>
	/// <param name='fileName'>
	/// 	- The name of the saved file without any extension
	/// </param>
	public SaveData(string fileName)
	{
		this.fileName = fileName;
	}
	
	#endregion
	
	#region PublicStaticFunctions
	
	/// <summary>
	/// 	- Loads specified file from streaming assets folder.
	/// </summary>
	/// <param name='fileName'>
	/// 	- The file to load from Application.streamingAssetsPath.
	/// </param>
	public static SaveData LoadFromStreamingAssets(string fileName)
	{
		return Load(Application.streamingAssetsPath+"\\"+fileName);
	}
	
	/// <summary>
	/// 	- Loads a file from the specified path.
	/// </summary>
	/// <param name='path'>
	/// 	- The path to load from.
	/// </param>
	/// <exception cref='System.InvalidOperationException'>
	/// 	- Is thrown when the passed path does not exist or has the wrong extension.
	/// </exception>
	public static SaveData Load(string path)
	{	
		if(File.Exists(path) && Path.GetExtension(path) == extension)
		{
			List<System.Type> additionalTypes = new List<System.Type>();
			XmlDocument document = new XmlDocument();
			document.Load(path);
			XmlNode objectNode = document.ChildNodes[1];
			
			foreach(XmlNode node in objectNode["serializedTypes"].ChildNodes){
				additionalTypes.Add(System.Type.GetType(node.InnerXml));
			}
			
			XmlSerializer serializer = new XmlSerializer(typeof(SaveData), additionalTypes.ToArray());
			TextReader textReader = new StreamReader(path);
			SaveData instance = (SaveData)serializer.Deserialize(textReader);
			textReader.Close();	
			
			foreach(DataContainer container in instance.serializedData){
				instance[container.key] = container.value;	
			}
			
			instance.serializedData = null;
			
			return instance;
		}
		
		else throw new System.InvalidOperationException("File does not exist! ("+path+")");
	}

	public static SaveData LoadStream(string streamed )
	{	
		
		List<System.Type> additionalTypes = new List<System.Type>();
		XmlDocument document = new XmlDocument();

		document.LoadXml( UnpackPackAvatar (streamed) );
		XmlNode objectNode = document.ChildNodes[1];

		foreach(XmlNode node in objectNode["serializedTypes"].ChildNodes){
			additionalTypes.Add(System.Type.GetType(node.InnerXml));
		}

		XmlSerializer serializer = new XmlSerializer(typeof(SaveData), additionalTypes.ToArray());
		TextReader textReader = new StringReader(UnpackPackAvatar (streamed));
		SaveData instance = (SaveData)serializer.Deserialize(textReader);
		textReader.Close();	

		foreach(DataContainer container in instance.serializedData){
			instance[container.key] = container.value;	
		}

		instance.serializedData = null;

		return instance;
	}
	
	#endregion	

	#region PublicFunctions
	
	/// <summary>
	/// 	- Determines whether this instance has the specified key.
	/// </summary>
	/// <returns>
	/// 	- <c>true</c> if this instance has the specified key; otherwise, <c>false</c>.
	/// </returns>
	/// <param name='key'>
	/// 	- Key to check for.
	/// </param>
	public bool HasKey(string key)
	{
		return _data.ContainsKey(key);	
	}
	
	/// <summary>
	/// 	- Gets value associated with the key.
	/// </summary>
	/// <returns>
	/// 	- The value.
	/// </returns>
	/// <param name='key'>
	/// 	- Key to get value of.
	/// </param>
	/// <typeparam name='T'>
	/// 	- The type of the returned value.
	/// </typeparam>
	public T GetValue<T>(string key)
	{
		return (T)_data[key];
	}
	
	/// <summary>
	/// 	- Tries to get the value associated with the key.
	/// </summary>
	/// <returns>
	/// 	- <c>true</c> if the instance contains the key.
	/// </returns>
	/// <param name='key'>
	/// 	- Key to get value of.
	/// </param>
	/// <param name='result'>
	/// 	- If set to <c>true</c>, the value associated with the key.
	/// </param>
	public bool TryGetValue(string key, out System.Object result)
	{
		return _data.TryGetValue(key, out result);
	}
	
	/// <summary>
	/// 	- Tries to get the value associated with the key.
	/// </summary>
	/// <returns>
	/// 	- <c>true</c> if the instance contains the key.
	/// </returns>
	/// <param name='key'>
	/// 	- Key to get value of.
	/// </param>
	/// <param name='result'>
	/// 	- If set to <c>true</c>, the value associated with the key.
	/// </param>
	/// <typeparam name='T'>
	/// 	- The type of the returned value.
	/// </typeparam>
	public bool TryGetValue<T>(string key, out T result)
	{
		System.Object resultOut;
		
		if(_data.TryGetValue(key, out resultOut) && resultOut.GetType() == typeof(T))
		{
			result = (T)resultOut;
			return true;
		}
		
		else
		{
			result = default(T);
			return false;
		}
	}
	
	/// <summary>
	/// 	- Saves this instance to the Streaming Assets path.
	/// </summary>
	public void Save() { 
		if(!Directory.Exists(Application.streamingAssetsPath)) {
		//	Debug.Log ("Path dors not exist !");
			Directory.CreateDirectory(Application.streamingAssetsPath);
		}
		Save(Application.streamingAssetsPath+"\\"+fileName+extension); 
	}

	public void Save( DK_RPG_UMA avatar, bool ToFile ) { 
		if(!Directory.Exists(Application.streamingAssetsPath)) {
		//	Debug.Log ("Path dors not exist !");
			Directory.CreateDirectory(Application.streamingAssetsPath);
		}
		SaveStream ( Application.streamingAssetsPath+"\\"+fileName+extension, avatar, ToFile ); }

	public void Save( DK_RPG_UMA avatar, bool ToFile,  bool ToDB ) { 
		if(!Directory.Exists(Application.streamingAssetsPath)) {
		//	Debug.Log ("Path dors not exist !");
			Directory.CreateDirectory(Application.streamingAssetsPath);
		}
		SaveStream ( Application.streamingAssetsPath+"\\"+fileName+extension, avatar, ToFile, ToDB ); }


	/// <summary>
	/// 	- Saves this instance to the specified path.
	/// </summary>
	/// <param name='path'>
	/// 	- Path to save to.
	/// </param>
	public void Save(string path)
	{
		List<System.Type> additionalTypes = new List<System.Type>();
		List<string> typeNameList = new List<string>();
		List<DataContainer> dataList = new List<DataContainer>();
		
		System.Object result;
		System.Type resultType;
		
		foreach(string key in _data.Keys){
			result = _data[key];
			resultType = result.GetType();
			
			if(!resultType.IsPrimitive && !additionalTypes.Contains(resultType))
			{
				additionalTypes.Add(resultType);
				typeNameList.Add(resultType.AssemblyQualifiedName);
			}
			
			dataList.Add(new DataContainer(key, result));
		}
		
		serializedData = dataList.ToArray();
		serializedTypes = typeNameList.ToArray();
		
		XmlSerializer serializer = new XmlSerializer(typeof(SaveData), additionalTypes.ToArray());
		TextWriter textWriter = new StreamWriter(path);
		serializer.Serialize(textWriter, this);

		serializedData = null;
		serializedTypes = null;
	}

	public void SaveStream (string path, DK_RPG_UMA avatar, bool ToFile ){
		SaveStream (path, avatar, ToFile, false );
	}

	public void SaveStream (string path, DK_RPG_UMA avatar, bool ToFile, bool ToDB )
	{
		List<System.Type> additionalTypes = new List<System.Type>();
		List<string> typeNameList = new List<string>();
		List<DataContainer> dataList = new List<DataContainer>();

		System.Object result;
		System.Type resultType;

		foreach(string key in _data.Keys){
			result = _data[key];
			resultType = result.GetType();

			if(!resultType.IsPrimitive && !additionalTypes.Contains(resultType))
			{
				additionalTypes.Add(resultType);
				typeNameList.Add(resultType.AssemblyQualifiedName);
			}
			dataList.Add(new DataContainer(key, result));
		}

		serializedData = dataList.ToArray();
		serializedTypes = typeNameList.ToArray();

		XmlSerializer serializer = new XmlSerializer(typeof(SaveData), additionalTypes.ToArray());

		// Save to stream
		MemoryStream memStream = new MemoryStream();
		serializer.Serialize(memStream, this);
		memStream.Seek(0, System.IO.SeekOrigin.Begin); // this is the line I was forgetting before
		XmlDocument doc = new XmlDocument();
		doc.Load(memStream);
		memStream.Close();

		// return string
		avatar.SavedRPGStreamed = GetXMLAsString(doc);

		// save to file
		if ( ToFile ){
			TextWriter textWriter = new StreamWriter(path);
			serializer.Serialize(textWriter, this);
			textWriter.Close();	
		}

		// save to Database
		if ( ToDB ){
			#if UNITY_EDITOR

			System.IO.Directory.CreateDirectory("Assets/DK Editors/DK_UMA_Content/Avatars/");
			string _path = ("Assets/DK Editors/DK_UMA_Content/Avatars/"+fileName+".asset");

			DK_UMA_AvatarData avatarData = (DK_UMA_AvatarData)AssetDatabase.LoadAssetAtPath( _path, typeof(DK_UMA_AvatarData));
			if ( avatarData != null ) {
				avatarData.name = fileName;
				avatarData.Name = fileName;
				avatarData.StreamedAvatar = avatar.SavedRPGStreamed;
				avatarData.SaveData ();
			}
			else {
				// create a new avatar object
				avatarData = ScriptableObject.CreateInstance<DK_UMA_AvatarData> ();
				avatarData.name = fileName;
				avatarData.Name = fileName;
				avatarData.Race = avatar.Race;
				avatarData.Gender =  avatar.Gender;
				avatarData.StreamedAvatar = avatar.SavedRPGStreamed;

				// Create the prefab
				AssetDatabase.CreateAsset( avatarData, _path );
				avatarData.StreamedAvatar =  avatar.SavedRPGStreamed;
				avatarData.SaveData ();
			}
			AssetDatabase.Refresh ();

			// add the object to the db
			 if ( GameObject.Find ( "DK_UMA" ).GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings.Databases.Avatars == null ) {
				Debug.LogError ("Ingame Creator : No Avatars Database assigned to the 'DK_UMA_GameSettings' database. Trying to correct this by detecting the Avatars Database in your project ...");
			#if UNITY_EDITOR
				FindAssignAvatarsDB ();
			#endif
			}
			if ( GameObject.Find ( "DK_UMA" ).GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings.Databases.Avatars != null
				&& GameObject.Find ( "DK_UMA" ).GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings.Databases.Avatars.Avatars.Contains(avatarData) == false )
				GameObject.Find ( "DK_UMA" ).GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings.Databases.Avatars.AddAvatar ( avatarData );
			#endif
		}
		serializedData = null;
		serializedTypes = null;
	}	
	#if UNITY_EDITOR
	void FindAssignAvatarsDB (){
		// Find all element of type placed in 'Assets' folder
		string[] lookFor = new string[] {"Assets"};
		string[] guids2 = AssetDatabase.FindAssets ("t:DK_UMA_Avatars_Databases", lookFor);
		if ( guids2.Length > 0 ) {			
			string path =  AssetDatabase.GUIDToAssetPath(guids2[0]).Replace(@"\", "/").Replace(Application.dataPath, "Assets");
				GameObject.Find ( "DK_UMA" ).GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings.Databases.Avatars
				= (DK_UMA_Avatars_Databases)AssetDatabase.LoadAssetAtPath(path, typeof(DK_UMA_Avatars_Databases));
			Debug.LogError ("Ingame Creator : Avatars Database found in your project and assigned. ("
				+GameObject.Find ( "DK_UMA" ).GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings.Databases.Avatars.name+")");
			// Save Settings DB
			GameObject.Find ( "DK_UMA" ).GetComponent<DKUMA_Variables>()._DK_UMA_GameSettings.SaveSettings ();
		}
		else Debug.LogError ("Ingame Creator : No Avatars Database in your project. Anyway the avatar have been created and its object saved into the project.");
	}
	#endif
	#endregion

	public void SaveDNAStream ( DK_RPG_UMA avatar ){
		List<System.Type> additionalTypes = new List<System.Type>();
		List<string> typeNameList = new List<string>();
		List<DataContainer> dataList = new List<DataContainer>();

		System.Object result;
		System.Type resultType;

		foreach(string key in _data.Keys){
			result = _data[key];
			resultType = result.GetType();

			if(!resultType.IsPrimitive && !additionalTypes.Contains(resultType))
			{
				additionalTypes.Add(resultType);
				typeNameList.Add(resultType.AssemblyQualifiedName);
			}
			dataList.Add(new DataContainer(key, result));
		}

		serializedData = dataList.ToArray();
		serializedTypes = typeNameList.ToArray();

		XmlSerializer serializer = new XmlSerializer(typeof(SaveData), additionalTypes.ToArray());

		// Save to stream
		MemoryStream memStream = new MemoryStream();
		serializer.Serialize(memStream, this);
		memStream.Seek(0, System.IO.SeekOrigin.Begin); // this is the line I was forgetting before
		XmlDocument doc = new XmlDocument();
		doc.Load(memStream);
		memStream.Close();

		// return string
		avatar.SavedDNAStream = GetXMLAsString(doc);
	//	Debug.Log ("DNA : "+avatar.SavedDNAStream);
	//	System.IO.File.WriteAllText("d:/PackedAvatar.txt", avatar.SavedDNAStream);
	}

	public string GetXMLAsString(XmlDocument myxml)
	{
		using (var stringWriter = new StringWriter())
		{
			using (var xmlTextWriter = XmlWriter.Create(stringWriter))
			{
				myxml.WriteTo(xmlTextWriter);

				// pack XML
				string _String = "";

				_String = stringWriter.ToString();
			
				_String = PackAvatar ( _String );

			
				return _String;
			}
		}    
	}

	public static string PackAvatar ( string _String ) {
		// capsules
	//	Debug.Log (_String);

		_String = _String.Replace( "<string>", "<st>" );
		_String = _String.Replace( "</string>", "</st>" );

		_String = _String.Replace( "<serializedData>", "<sD>" );
		_String = _String.Replace( "</serializedData>", "</sD>" );

		_String = _String.Replace( "<DataContainer>", "<DC>" );
		_String = _String.Replace( "</DataContainer>", "</DC>" );

		_String = _String.Replace( "<value xsi:type=\"string\">", "</VS>" );
		_String = _String.Replace( "<value xsi:type=\"string\" />", "<VS />" );

		_String = _String.Replace( "<key>", "<k>" );
		_String = _String.Replace( "</key>", "</k>" );

		_String = _String.Replace( "<value xsi:type=\"xsd:int\">", "<i>" );
		_String = _String.Replace( "<value xsi:type=\"Color\">", "<c>" );

		_String = _String.Replace( "</value>", "</v>" );

		_String = _String.Replace( "</v></DC><DC><k>", "<DC2>" );

		// DNA
		_String = _String.Replace( "<value xsi:type=\"xsd:float\">", "<Fl>" );
		_String = _String.Replace( "Value<", "<v<" );

		// fields
		_String = _String.Replace( "OverlayOnly", "<OO>" );
		_String = _String.Replace( "SlotOnly", "<SO>" );
		_String = _String.Replace( "OverlayOnly", "<OO>" );
		_String = _String.Replace( "SlotOnly", "<SO>" );
		_String = _String.Replace( "Color", "<Co>" );

		_String = _String.Replace( "Preset", "<P>" );
		_String = _String.Replace( "Torso", "<t>" );
		_String = _String.Replace( "Head", "<H>" );
		_String = _String.Replace( "head", "<h>" );
		_String = _String.Replace( "Face", "<f>" );
		_String = _String.Replace( "Mouth", "<mh>" );
		_String = _String.Replace( "Nose", "<n>" );
		_String = _String.Replace( "Hair", "<R>" );
		_String = _String.Replace( "ArmBand", "<Ab>" );
		_String = _String.Replace( "LegBand", "<Lb>" );
		_String = _String.Replace( "Shoulder", "<Sh>" );
		_String = _String.Replace( "Backpack", "<Bp>" );
		_String = _String.Replace( "LeftHand", "<Lh>" );
		_String = _String.Replace( "RightHand", "<Rh>" );
		_String = _String.Replace( "Human", "<u>" );

		_String = _String.Replace( "Tatoo", "<T>" );
		_String = _String.Replace( "MakeUp", "<M>" );
		_String = _String.Replace( "Makeup", "<m>" );
		_String = _String.Replace( "Overlay", "<O>" );
		_String = _String.Replace( "Slot", "<S>" );
		_String = _String.Replace( "Equip", "<E>" );
	//	_String = _String.Replace( "Body", "<b>" );
	//	_String = _String.Replace( "Cover", "<c>" );
	//	_String = _String.Replace( "Sub", "<s>" );
	//	_String = _String.Replace( "Size", "<z>" );
	//	_String = _String.Replace( "Position", "<p>" );
	//	_String = _String.Replace( "Rotation", "<r>" );

	//	_String = _String.Replace( "</k></VS>NA<DC2>", "<DC3>" );

		return _String;
	}

	public static string UnpackPackAvatar ( string _String ) {
		// capsules
	//	_String = _String.Replace(  "<DC3>", "</k></VS>NA<DC2>" );
		_String = _String.Replace( "<DC2>", "</v></DC><DC><k>" );

		_String = _String.Replace( "<st>", "<string>" );
		_String = _String.Replace( "</st>", "</string>" );

		_String = _String.Replace( "<sD>", "<serializedData>" );
		_String = _String.Replace( "</sD>", "</serializedData>" );

		_String = _String.Replace( "<DC>", "<DataContainer>" );
		_String = _String.Replace( "</DC>", "</DataContainer>" );

		_String = _String.Replace( "</VS>", "<value xsi:type=\"string\">" );
		_String = _String.Replace( "<VS />", "<value xsi:type=\"string\" />" );

		_String = _String.Replace( "<k>", "<key>" );
		_String = _String.Replace( "</k>", "</key>" );

		_String = _String.Replace( "<i>", "<value xsi:type=\"xsd:int\">" );
		_String = _String.Replace( "<c>", "<value xsi:type=\"Color\">" );

		_String = _String.Replace( "</v>", "</value>" );

		// DNA
		_String = _String.Replace( "<Fl>", "<value xsi:type=\"xsd:float\">" );
		_String = _String.Replace( "<v<", "Value<" );

		// fields
		_String = _String.Replace( "<OO>", "OverlayOnly" );
		_String = _String.Replace( "<SO>", "SlotOnly" );
		_String = _String.Replace( "<OO>", "OverlayOnly" );
		_String = _String.Replace( "<SO>", "SlotOnly" );
		_String = _String.Replace( "<Co>", "Color" );

		_String = _String.Replace( "<P>", "Preset" );
		_String = _String.Replace( "<t>", "Torso" );
		_String = _String.Replace( "<H>", "Head" );
		_String = _String.Replace( "<h>", "head" );
		_String = _String.Replace( "<f>", "Face" );
		_String = _String.Replace( "<mh>", "Mouth" );
		_String = _String.Replace( "<n>", "Nose" );
		_String = _String.Replace( "<R>", "Hair" );
		_String = _String.Replace( "<Ab>", "ArmBand" );
		_String = _String.Replace( "<Lb>", "LegBand" );
		_String = _String.Replace( "<Sh>", "Shoulder" );
		_String = _String.Replace( "<Bp>", "Backpack" );
		_String = _String.Replace( "<Lh>", "LeftHand" );
		_String = _String.Replace( "<Rh>", "RightHand" );
		_String = _String.Replace( "<u>", "Human" );

		_String = _String.Replace( "<T>", "Tatoo" );
		_String = _String.Replace( "<M>", "MakeUp" );
		_String = _String.Replace( "<m>", "Makeup" );
		_String = _String.Replace( "<O>", "Overlay" );
		_String = _String.Replace( "<S>", "Slot" );
		_String = _String.Replace( "<E>", "Equip" );
	//	_String = _String.Replace( "<b>", "Body" );
	//	_String = _String.Replace( "<c>", "Cover" );
	//	_String = _String.Replace( "<s>", "Sub" );
	//	_String = _String.Replace( "<z>", "Size" );
	//	_String = _String.Replace( "<p>", "Position" );
	//	_String = _String.Replace( "<r>", "Rotation" );

	//	Debug.Log (_String);

		return _String;
	}
	#region Utility


	/// <summary>
	/// 	- Serializable data container, used for saving and loading.
	/// </summary>
	public class DataContainer
	{
		public string key;
		public System.Object value;
		
		public DataContainer(){}
		public DataContainer(string key, System.Object value)
		{
			this.key = key;
			this.value = value;
		}
	}
	
	#endregion
}