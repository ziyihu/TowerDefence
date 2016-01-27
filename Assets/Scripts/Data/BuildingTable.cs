using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class BuildingTable : Editor {
	//show in the menu
	[MenuItem("BuildAssetBundle/Build Csv")]
	static void BuildCsv(){
		string applicationPath = Application.dataPath;
		string saveDir = applicationPath + "/streamingAssets/";
		string savePath = saveDir + "csv.assetbundle";

		Object[] selections = Selection.GetFiltered (typeof(object), SelectionMode.DeepAssets);
		List<Object> outs = new List<Object> ();
		for (int i = 0, max = selections.Length; i < max; i++) {
			Object obj = selections[i];
			string fileAssetPath = AssetDatabase.GetAssetPath(obj);
			if(fileAssetPath.Substring(fileAssetPath.LastIndexOf('.')+1)!="csv")
				continue;
			string fileWholePath = applicationPath + "/" + fileAssetPath.Substring(fileAssetPath.IndexOf("/"));

			Csv csv = ScriptableObject.CreateInstance<Csv>();
			csv.fileName = obj.name;
			csv.content = File.ReadAllBytes(fileWholePath);
			//change csv file to asset bundle
			string assetPathTemp = "Assets/Resource_Local/Temp/" + obj.name + ".asset";
			AssetDatabase.CreateAsset(csv, assetPathTemp);

			Object outObj = AssetDatabase.LoadAssetAtPath(assetPathTemp, typeof(Csv));
			Debug.Log ("package :" + outObj.name);
			outs.Add(outObj);
		}
		Object[] outObjs = outs.ToArray ();
		//build the data to a asset bundle, save the memory
		//1 parameter: the type of the package
		//2 parameter: content
		//3 parameter: save path
		//4 parameter: package style
		if(BuildPipeline.BuildAssetBundle(null, outs.ToArray(),savePath,BuildAssetBundleOptions.CollectDependencies,BuildTarget.Android))
		   EditorUtility.DisplayDialog("ok","build"+savePath+"success, length ="+outObjs.Length,"ok");
		else
			Debug.LogWarning("build"+savePath+"failed");

		AssetDatabase.Refresh ();
	}
}
