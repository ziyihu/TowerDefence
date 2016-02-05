using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBuildManager : MonoBehaviour {
	//can put the basic tower or not
	public static bool tower01 = false;
	//can put the shotgun tower or not
	public static bool tower02 = false;
	//can put the laser tower or not
	public static bool tower10 = false;
	//can put the slow tower or not
	public static bool tower04 = false;
	//can put the missile tower or not
	public static bool tower07 = false;

	public static bool tower03 = false;
	public static bool tower05 = false;
	public static bool tower06 = false;

	public static float tower1AttackRate = 1f;
	public static float tower2AttackRate = 2f;
	public static float tower4AttackRate = 2f;
	public static float tower7AttackRate = 2f;
	public static float tower10AttackRate = 2f;

	public static float time = 0;

	//circle to show the attack range
	public GameObject circleObj;

	//tower info panel
	public TweenPosition towerInfoTween;
	private int level;
	private int attackNum;
	public GameObject towerInfo;
	Building building;
	CharacterManager cManager;
	GameManager gManager;

	public UIButton upgrade;

	// Use this for initialization
	void Start () {
		cManager = new CharacterManager ();
		gManager = new GameManager ();
	}
	
	// Update is called once per frame
	void Update () {
//		RaycastHit hit = new RaycastHit();
//		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//		Physics.Raycast(ray, out hit, 100);
//		if (hit.collider.tag == "Map") {
//			Debug.Log("x:"+hit.point.x + "\t\ty:"+hit.point.y+"\t\tz:"+hit.point.z);
//		}
		time = time + Time.deltaTime;
		if (tower01) {
			if(Input.GetMouseButtonDown(0)){
				SetTower("tower1");
				tower01 = false;
			}
		} else if (tower02) {
			if(Input.GetMouseButtonDown(0)){
				SetTower("tower2");
				tower02 = false;
			}
		} else if(tower04){
			if(Input.GetMouseButtonDown(0)){
				SetTower("tower4");
				tower04 = false;
			}
		} else if(tower07){
			if(Input.GetMouseButtonDown(0)){
				SetTower("tower7");
				tower07 = false;
			}
		} else if(tower10){
			if(Input.GetMouseButtonDown(0)){
				SetTower("tower10");
				tower10 = false;
			}
		}
		//if(time > tower1AttackRate){
			if (gManager.tower1List.Count > 0 && Time.timeScale == 1) {
				foreach (Tower1 t in gManager.tower1List) {
					t.CheckEnemy ();
					t.HitEnemy ();
				}
			}
		//	time = 0;
		//}
		if (gManager.tower2List.Count > 0 && Time.timeScale == 1) {
			foreach (Tower2 t in gManager.tower2List) {
				t.CheckEnemy ();
				t.HitEnemy();
			}
		}
		if (gManager.tower4List.Count > 0 && Time.timeScale == 1) {
			foreach (Tower4 t in gManager.tower4List) {
				t.SlowEnemy();
			}
		}
		if (gManager.tower7List.Count > 0 && Time.timeScale == 1) {
			foreach (Tower7 t in gManager.tower7List) {
				t.CheckEnemy();
				t.HitEnemy();
			}
		}
		if (gManager.tower10List.Count > 0 && Time.timeScale == 1) {
			foreach (Tower10 t in gManager.tower10List){
				t.CheckEnemy();
				t.HitEnemy();
			}
		}
		//click on the screen, if click on the tower, show the tower info
		if (Input.GetMouseButtonDown (0)) {
			GetTower();
		}
	}

	string name = null;
	string description = null;
	string attackNumber = null;
	string levelNumber = null;

	//show the tower info panel, update the info in the panel
	private void GetTower(){
		RaycastHit hit = new RaycastHit();
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(ray, out hit, 100);
		if (hit.transform != null) {
			if (hit.transform.tag == "Tower1") {
				//show the current tower attack, upgrade button, destory button
				name = "Basic Tower";
				description = "this tower has middle attack range, medium rate of fire";
				building = cManager.GetBuildingById(int.Parse(hit.collider.transform.name));
				attackNumber = building.GetAttackPower()+"";
				levelNumber = building.GetLevel()+"";
				//show the attack range
				SetAttackRangeShow(building);
				SetPanel(name,description,attackNumber,levelNumber);
			} else if(hit.transform.tag == "Tower2"){
				name = "Shotgun Tower";
				description = "this tower has low range, medium rate of fire, can shoot two enemies";
				building = cManager.GetBuildingById(int.Parse(hit.collider.transform.name));
				attackNumber = building.GetAttackPower()+"";
				levelNumber = building.GetLevel()+"";
				SetAttackRangeShow(building);
				SetPanel(name,description,attackNumber,levelNumber);
			} else if(hit.transform.tag == "Tower4"){
				name = "Stasis Tower";
				description = "this tower slows the near enemies";
				building = cManager.GetBuildingById(int.Parse(hit.collider.transform.name));
				attackNumber = 0 + "";
				levelNumber = building.GetLevel()+"";
				SetAttackRangeShow(building);
				SetPanel(name,description,attackNumber,levelNumber);
			} else if(hit.transform.tag == "Tower7"){
				name = "Missile Tower";
				description = "this tower attack the area enemies";
				building = cManager.GetBuildingById(int.Parse(hit.collider.transform.name));
				attackNumber = building.GetAttackPower()+"";
				levelNumber = building.GetLevel()+"";
				SetAttackRangeShow(building);
				SetPanel(name,description,attackNumber,levelNumber);
			} else if(hit.transform.tag == "Tower10"){
				name = "Laser Tower";
				description = "this tower is the most powerful tower";
				building = cManager.GetBuildingById(int.Parse(hit.collider.transform.name));
				attackNumber = building.GetAttackPower()+"";
				levelNumber = building.GetLevel()+"";
				SetAttackRangeShow(building);
				SetPanel(name,description,attackNumber,levelNumber);
			} else {
				SetAttackRangeHide();
			}
		}
	}

	private void SetAttackRangeShow(Building building){
		circleObj.gameObject.transform.GetComponent<DrawCircle>().m_Radius = building.GetAttackRange();
		circleObj.SetActive(true);
		circleObj.transform.position = building.GetPos();
	}

	private void SetAttackRangeHide(){
		circleObj.SetActive (false);
	}
	
	private void SetPanel(string nameText, string desText, string attackNumText, string levelNumText){
		if (building.GetLevel() <= 2) {
			upgrade.normalSprite = "btn_red1";
			upgrade.enabled = true;
		}else if (building.GetLevel() == 3) {
			upgrade.normalSprite = "btn_red4";
			upgrade.enabled = false;
		}
		if(building.buildingType == CharacterData.buildingMode.TOWER4){
			upgrade.normalSprite = "btn_red4";
			upgrade.enabled = false;
		}
		towerInfoTween.PlayForward ();
		UILabel name = towerInfo.transform.Find ("Panel/NameLabel").GetComponent<UILabel> ();
		name.text = nameText;
		UILabel description = towerInfo.transform.Find ("Panel/DesLabel").GetComponent<UILabel> ();
		description.text = desText;
		UILabel attackNum = towerInfo.transform.Find ("Panel/AttackPanel/AttackNumber").GetComponent<UILabel> ();
		attackNum.text = attackNumText;
		UILabel levelNum = towerInfo.transform.Find("Panel/LevelPanel/LevelNumber").GetComponent<UILabel>();
		levelNum.text = levelNumText;
	}

	public void UpGradeBtnClick(){
		if (building.GetLevel() <= 1) {
			upgrade.normalSprite = "btn_red1";
			upgrade.enabled = true;
		}else if (building.GetLevel() > 1) {
			upgrade.normalSprite = "btn_red4";
			upgrade.enabled = false;
		}
		level = building.GetLevel ();
		//increase the attack power
		attackNum = building.GetAttackPower ();
		building.SetAttackPower (attackNum + 50);
		//increase the level
		level = level + 1;
		building.SetLevel (level);
		UpdatePanel ();
		if(building.buildingType == CharacterData.buildingMode.TOWER1 || building.buildingType == CharacterData.buildingMode.TOWER10
		   || building.buildingType == CharacterData.buildingMode.TOWER7){
			UpdateTexture ();
		} else if(building.buildingType == CharacterData.buildingMode.TOWER2){
			UpdateTexture2();
		}
	}

	public void DestoryBtnClick(){
		cManager.DestoryBuilding (building.ID);
		DestoryBuildingInList (building.ID);
		towerInfoTween.PlayReverse ();
	}

	private void UpdateTexture(){
		QuadTextureNgui tex = building.GetTransform().GetChild(0).GetComponent<QuadTextureNgui>();
		tex.mSpriteName = building.GetLevel() + "0000";
		tex.InitFace();
	}

	private void UpdateTexture2(){
		QuadTextureNgui tex = building.GetTransform().GetChild(0).GetComponent<QuadTextureNgui>();
		tex.mSpriteName = building.GetLevel() + " " + "(" + 19 + ")";;
		tex.InitFace();
	}

	public void DestoryBuildingInList(long id){
		for (int i = gManager.tower1List.Count - 1; i >= 0; i--) {
			if(gManager.tower1List[i].ID == id){
				gManager.tower1List[i].Destroy();
				gManager.tower1List.RemoveAt(i);
				break;
			}
		}
		for (int i = gManager.tower2List.Count - 1; i >= 0; i--) {
			if(gManager.tower2List[i].ID == id){
				gManager.tower2List[i].Destroy();
				gManager.tower2List.RemoveAt(i);
				break;
			}
		}
		for (int i = gManager.tower10List.Count - 1; i >= 0; i--) {
			if(gManager.tower10List[i].ID == id){
				gManager.tower10List[i].Destroy();
				gManager.tower10List.RemoveAt(i);
				break;
			}
		}
		for (int i = gManager.tower4List.Count - 1; i >= 0; i--) {
			if(gManager.tower4List[i].ID == id){
				gManager.tower4List[i].Destroy();
				gManager.tower4List.RemoveAt(i);
				break;
			}
		}
		for (int i = gManager.tower7List.Count - 1; i >= 0; i--) {
			if(gManager.tower7List[i].ID == id){
				gManager.tower7List[i].Destroy();
				gManager.tower7List.RemoveAt(i);
				break;
			}
		}
	}

	private void UpdatePanel(){
		UILabel attackNum = towerInfo.transform.Find ("Panel/AttackPanel/AttackNumber").GetComponent<UILabel> ();
		attackNum.text = building.GetAttackPower()+"";
		UILabel levelNum = towerInfo.transform.Find ("Panel/LevelPanel/LevelNumber").GetComponent<UILabel> ();
		levelNum.text = building.GetLevel () + "";
	}

	public void OnCloseBtnClick(){
		towerInfoTween.PlayReverse ();
	}

	private void SetTower(string tower){
		RaycastHit hit = new RaycastHit();
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(ray, out hit, 100);
		if(hit.transform != null){
			if(hit.transform.tag == "Map"){
				//set the tower position
				float x = 0;
				if(hit.point.x - (int)hit.point.x <= 0.8f && hit.point.x - (int)hit.point.x > 0.3f){
					x = (int)hit.point.x + 0.65f;
				} else if(hit.point.x - (int)hit.point.x <= 0.3f && hit.point.x - (int)hit.point.x >= 0){
					x = (int)hit.point.x + 0.15f;
				} else {
					x = (int)hit.point.x + 1.15f;
				}
				float y = 1.0f;
				float z = 0;
				if(hit.point.z - (int)hit.point.z <= 0.05f && hit.point.z - (int)hit.point.z >= 0){
					z = (int)hit.point.z - 0.2f;
				} else if(hit.point.z - (int)hit.point.z <= 0.55f && hit.point.z - (int)hit.point.z > 0.05f){
					z = (int)hit.point.z + 0.3f;
				} else {
					z = (int)hit.point.z + 0.8f;
				}
				//in the enemy position, can not set the tower
				//restriction
				if(x <= 14.65f || x >= 23.65f || z <= 16.55f || z >= 25.3f){
					return;
				}
				//first path
				if(x == 15.65f && z >= 18.3f && z <= 24.8f){
					return;
				}
				//second path
				if(x >= 16.15f && x <= 20.15f && (z == 18.3f || z == 18.8f)){
					return;
				}
				//third path
				if((x == 19.65f || x == 20.15f) && z >= 18.8f && z <= 23.8f){
					return;
				}
				//forth path
				if((z == 23.3f || z == 23.8f) && x >= 20.65f && x <= 22.65f){
					return;
				}
				//fifth path
				if((x == 22.15f || x == 22.65f) && z >= 19.3f && z <= 22.8f){
					return;
				}
				if(x == 23.15f && (z == 19.8f || z == 19.3f)){
					return;
				}
				Vector3 obstacle3Pos = new Vector3 (x, y, z);
				foreach(Building building in cManager.building){
					if(obstacle3Pos == building.GetPos()){
						return;
					}
				}
				if(tower == "tower1"){
					Tower1 tower1 = (Tower1)cManager.SpawnCharacter(CharacterData.CharacterClassType.BUILDING, (int)CharacterData.buildingMode.TOWER1, 1,
				                                                1, obstacle3Pos, new Vector3 (0, 0, 0), CharacterStatus.Pose.Idle);
					tower1.SetPosition (obstacle3Pos);
					gManager.tower1List.Add(tower1);
				} else if(tower == "tower2"){
					Tower2 tower2 = (Tower2)cManager.SpawnCharacter(CharacterData.CharacterClassType.BUILDING, (int)CharacterData.buildingMode.TOWER2, 1,
					                                                1, obstacle3Pos, new Vector3 (0, 0, 0), CharacterStatus.Pose.Idle);
					tower2.SetPosition (obstacle3Pos);
					gManager.tower2List.Add(tower2);
				}else if(tower == "tower10"){
					Tower10 tower10 = (Tower10)cManager.SpawnCharacter(CharacterData.CharacterClassType.BUILDING, (int)CharacterData.buildingMode.TOWER10, 1,
					                                                1, obstacle3Pos, new Vector3 (0, 0, 0), CharacterStatus.Pose.Idle);
					tower10.SetPosition (obstacle3Pos);
					gManager.tower10List.Add(tower10);
				} else if(tower == "tower4"){
					Tower4 tower4 = (Tower4)cManager.SpawnCharacter(CharacterData.CharacterClassType.BUILDING, (int)CharacterData.buildingMode.TOWER4, 1,
					                                                   1, obstacle3Pos, new Vector3 (0, 0, 0), CharacterStatus.Pose.Idle);
					tower4.SetPosition (obstacle3Pos);
					gManager.tower4List.Add(tower4);   
				} else if(tower == "tower7"){
					Tower7 tower7 = (Tower7)cManager.SpawnCharacter(CharacterData.CharacterClassType.BUILDING, (int)CharacterData.buildingMode.TOWER7, 1,
					                                                1, obstacle3Pos, new Vector3 (0, 0, 0), CharacterStatus.Pose.Idle);
					tower7.SetPosition (obstacle3Pos);
					gManager.tower7List.Add(tower7);   
				}
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

	public void OnTower10Clicked(){
		tower10 = true;
	}

	public void OnTower04Clicked(){
		tower04 = true;
	}

	public void OnTower07Clicked(){
		tower07 = true;
	}
}
