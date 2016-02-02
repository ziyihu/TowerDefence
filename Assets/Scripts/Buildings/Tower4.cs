using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower4 : Building {

	//Current Enemy
	public Character curEnemy;
	public List<Character> enemyLists = new List<Character> ();
	public int attackPower = 100;
	
	float mHitDelta;
	bool endAttack = true;
	int curFps = 0;
	float mFPS = 10;
	float moveDistance = 0.5f;
	//the cannon will be bigger when attacking the enemy
	float scalefactor = 0.6f;
	int turnSpeed = 12;
	Vector3 dirPos;
	float attackIntervale = 1;
	float dirRotation;
	bool isExist;
	public Tower4(){
		model = (GameObject)GameObject.Instantiate(Resources.Load("tower4"));
		model.name = "" + ID;
		status = model.GetComponent<CharacterStatus> ();
		status.rotateWeapon = true;
		status.CurPose = CharacterStatus.Pose.None;
		//get the building
		Transform house = model.transform.GetChild (0);
		house.gameObject.GetComponent<Renderer>().sortingOrder = layerOrder = LAYER_BASE + 1;
		buildingType = CharacterData.buildingMode.TOWER4;
	}
	
	//Hit the enmey
	public void SlowEnemy(){
		if (GameManager.Instance.CurStatus != GameManager.Status.START_GAME) {
			return;
		}
		for(int i = 0 ; i < EnemySpawnManager._instance.enemyList.Count ; i++){
			if(Vector3.Distance(this.GetPos(),EnemySpawnManager._instance.enemyList[i].GetPos()) >= this.GetAttackRange()){
				EnemySpawnManager._instance.enemyList[i].SetSpeed(0.01f);
			} else {
				EnemySpawnManager._instance.enemyList[i].SetSpeed(0.008f);
			}
		}
	}

	public void SetPosition(Vector3 pos){
		if (model != null) {
			model.transform.position = pos;
		}
	}
	
	public Vector3 GetPosition(){
		return model.transform.position;
	}
}
