using UnityEngine;
using System.Collections;
//this class in used to evaluate the performance of the game, to monitor the current frames 
public class GlobalManager : UnityAllSceneSingleton<GlobalManager> {
	//update frequence
	public float f_UpdateFre = 0.3f;
	//last frame update frequence
	private float f_LastUpdateFre;
	//total frames
	private int i_Frames = 0;
	//average frame, frame per second
	private float f_FPS;
	//vertexs 
	public static int vertexs;
	//triangles
	public static int tris;

	public virtual void Awake(){
		//set up the basic frame
		Application.targetFrameRate = 45;
	}

	void Start(){
		//load all managers
		GameManager.Instance.CurStatus = GameManager.Status.LOAD_RESOURCE;
		DataManager dataIns = DataManager.Instance;
		InputListener inputIns = InputListener.Instance;
		DataManager.Init ();
		//total time from the game begin
		f_LastUpdateFre = Time.realtimeSinceStartup;
		i_Frames = 0;
	}

	//get the vertexs and faces number of all game objects
	void GetObjectStates(){
		vertexs = 0;
		tris = 0;
		GameObject[] go = FindObjectsOfType (typeof(GameObject)) as GameObject[];
		foreach (GameObject obj in go) {
			GetObjectStates(obj);
		}
	}

	//get the vertexs and faces of one game object
	void GetObjectStates(GameObject go){
		Component[] filters;
		//Get the MeshFilter component 
		filters = go.GetComponentsInChildren<MeshFilter> ();
		foreach (MeshFilter f in filters) {
			vertexs += f.sharedMesh.vertexCount;
			tris += f.sharedMesh.triangles.Length / 3;
		}
	}

	//show the result
	void OnGUI(){
		GUI.skin.label.normal.textColor = new Color (255,255,255,1.0f);
		GUI.Label (new Rect (0, 10, 200, 200), "FPS:" + (f_FPS).ToString("f2"));
		string vertexsShow = vertexs.ToString("#,##0 vertexs");
		GUILayout.Label (vertexsShow);
		string trisShow = tris.ToString("#,##0 tris");
		GUILayout.Label (trisShow);
	}

	void Update(){
		++i_Frames;
		if (Time.realtimeSinceStartup > f_LastUpdateFre + f_UpdateFre) {
			f_FPS = i_Frames / (Time.realtimeSinceStartup - f_LastUpdateFre);
			i_Frames = 0;
			f_LastUpdateFre = Time.realtimeSinceStartup;
			GetObjectStates();
		}
	}
}
