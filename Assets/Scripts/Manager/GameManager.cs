using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : UnityAllSceneSingleton<GameManager>,IMessageObject {
	//the different game status
	public enum Status{
		NONE = 1,
		LOAD_RESOURCE,
		LOAD_SCENE,
		PREPARE_SCAN,
		START_GAME,
		END_GAME,
	}
	public List<Cannon> can = new List<Cannon> ();
	public List<Tower1> tower1List = new List<Tower1> ();
	public List<Tower2> tower2List = new List<Tower2> ();
	public CharacterManager cManager;
	public Status CurStatus = Status.NONE;

	public override void Awake(){
		base.Awake ();
		cManager = new CharacterManager ();
	}

	void Start(){

	}

	public void ReloadScene(int scene){
		this.START_METHOD ("ReloadScene");
		//create the towers in the scene
		//tower1
		Vector3 obstaclePos = new Vector3 (17f, 1.0f, 19.3f);
		TowerBarrack barrack = (TowerBarrack)cManager.SpawnCharacter (CharacterData.CharacterClassType.BUILDING, (int)CharacterData.buildingMode.BARRACK, 1,
		                                                              1, obstaclePos, new Vector3 (0, 0, 0), CharacterStatus.Pose.Idle);
		barrack.SetPosition (obstaclePos);

		//tower2
		Vector3 obstacle1Pos = new Vector3 (18f, 1.0f, 19.3f);
		 barrack = (TowerBarrack)cManager.SpawnCharacter (CharacterData.CharacterClassType.BUILDING, (int)CharacterData.buildingMode.BARRACK, 1,
		                                                 1, obstacle1Pos, new Vector3 (0, 0, 0), CharacterStatus.Pose.Idle);
		barrack.SetPosition (obstacle1Pos);

		//tower3
		Vector3 obstacle2Pos = new Vector3 (15f, 1.0f, 17.3f);
		barrack = (TowerBarrack)cManager.SpawnCharacter(CharacterData.CharacterClassType.BUILDING, (int)CharacterData.buildingMode.BARRACK, 1,
		                                                1, obstacle2Pos, new Vector3 (0, 0, 0), CharacterStatus.Pose.Idle);
		barrack.SetPosition (obstacle2Pos);


		//enemy
		this.END_METHOD ("ReloadScene");
	}
	//get the closest enemy
	public Character FindEnemyByDistance(Building building){
		return cManager.FindEnemyByDistance (building);
	}

	//Create an enemy perfab
	public Character SpawnCharacter(CharacterData.CharacterClassType type, CharacterData.CharacterModel model, int camp, int level, Vector3 startPos, Vector3 startDir, CharacterStatus.Pose pose){
		return cManager.SpawnCharacter (type, (int)model, camp, level, startPos, startDir, pose);
	}

	//destory an enemy
	public void DeleteById(long id){
		cManager.DestoryChar (id);
	}

	void Update(){

//		if (Input.GetKeyDown (KeyCode.A)) {
//			Vector3 obstacle3Pos = new Vector3 (20f, 1.0f, 19.3f);
//			Cannon cannon = (Cannon)cManager.SpawnCharacter(CharacterData.CharacterClassType.BUILDING, (int)CharacterData.buildingMode.CANNON, 1,
//			                                                1, obstacle3Pos, new Vector3 (0, 0, 0), CharacterStatus.Pose.Idle);
//			cannon.SetPosition (obstacle3Pos);
//			
//			//cannon.GetTransform().rotation = Quaternion.Euler(90,-90,0);
//			can.Add(cannon);
//		}
		if (can.Count > 0) {
						foreach (Cannon c in can) {
								c.CheckEnemy ();
								c.HitEnemy ();
						}
				}
		switch (CurStatus) {
		case Status.LOAD_RESOURCE:
			break;
		case Status.LOAD_SCENE:
			ReloadScene(1);
			CurStatus = Status.PREPARE_SCAN;
			break;
		case Status.PREPARE_SCAN:
			//A* scan algorithm
			CurStatus = Status.START_GAME;
			break;
		case Status.START_GAME:
			break;
		case Status.END_GAME:
			break;
		}
	}
}
