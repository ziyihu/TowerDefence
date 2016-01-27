using UnityEngine;
using System.Collections;

public class TowerBuildManager : MonoBehaviour {

	public static bool tower01 = false;
	public static bool tower02 = false;
	public static bool tower03 = false;
	public static bool tower04 = false;
	public static bool tower05 = false;
	public static bool tower06 = false;

	CharacterManager cManager;
	GameManager gManager;

	// Use this for initialization
	void Start () {
		cManager = new CharacterManager ();
		gManager = new GameManager ();
	}
	
	// Update is called once per frame
	void Update () {

		if (tower01) {

			if(Input.GetMouseButtonDown(0)){
				RaycastHit hit = new RaycastHit();
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Physics.Raycast(ray, out hit, 100);
				if(hit.transform != null){
					if(hit.transform.tag == "Map"){
						float x = (float)((int)(hit.point.x+0.5f));
						float y = 1.0f;
						float z = (float) ((int)hit.point.z) + 0.3f;
						Vector3 obstacle3Pos = new Vector3 (x, y, z);
//						Cannon cannon = (Cannon)cManager.SpawnCharacter(CharacterData.CharacterClassType.BUILDING, (int)CharacterData.buildingMode.CANNON, 1,
//					                                                1, obstacle3Pos, new Vector3 (0, 0, 0), CharacterStatus.Pose.Idle);
//						cannon.SetPosition (obstacle3Pos);
//						gManager.can.Add(cannon);
						Tower1 tower1 = (Tower1)cManager.SpawnCharacter(CharacterData.CharacterClassType.BUILDING, (int)CharacterData.buildingMode.TOWER1, 1,
						                                                1, obstacle3Pos, new Vector3 (0, 0, 0), CharacterStatus.Pose.Idle);
						tower1.SetPosition (obstacle3Pos);
						gManager.tower1List.Add(tower1);
						//cannon.GetTransform().rotation = Quaternion.Euler(90,-90,0);
					}
				}
				tower01 = false;
			}
		} else if (tower02) {
			
			if(Input.GetMouseButtonDown(0)){
				RaycastHit hit = new RaycastHit();
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Physics.Raycast(ray, out hit, 100);
				if(hit.transform != null){
					if(hit.transform.tag == "Map"){
						float x = (float)((int)(hit.point.x+0.5f));
						float y = 1.0f;
						float z = (float) ((int)hit.point.z) + 0.3f;
						Vector3 obstacle3Pos = new Vector3 (x, y, z);
						Tower2 tower2 = (Tower2)cManager.SpawnCharacter(CharacterData.CharacterClassType.BUILDING, (int)CharacterData.buildingMode.TOWER2, 1,
						                                                1, obstacle3Pos, new Vector3 (0, 0, 0), CharacterStatus.Pose.Idle);
						tower2.SetPosition (obstacle3Pos);
						gManager.tower2List.Add(tower2);
					}
				}
				tower02 = false;
			}
		}
		if (gManager.tower1List.Count > 0) {
			foreach (Tower1 t in gManager.tower1List) {
				t.CheckEnemy ();
				t.HitEnemy ();
			}
		}
		if (gManager.tower2List.Count > 0) {
			foreach (Tower2 t in gManager.tower2List) {
				t.CheckEnemy ();
				t.HitEnemy ();
			}
		}
	}
	

	public static Vector3 WorldToUI(Vector3 point)
	{
		Vector3 pt = Camera.main.WorldToScreenPoint (point);
		Vector3 ff = UICamera.mainCamera.ScreenToWorldPoint (pt);
		ff.z = 0;
		return ff;
	}

	public void OnTower01Clicked(){
		tower01 = true;
	}

	public void OnTower02Clicked(){
		tower02 = true;
	}
}
