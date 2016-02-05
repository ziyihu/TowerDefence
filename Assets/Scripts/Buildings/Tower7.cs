using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower7 : Building {

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
	public Tower7(){
		model = (GameObject)GameObject.Instantiate(Resources.Load("tower7"));
		model.name = "" + ID;
		status = model.GetComponent<CharacterStatus> ();
		status.rotateWeapon = true;
		status.CurPose = CharacterStatus.Pose.None;
		//get the building
		Transform house = model.transform.GetChild (0);
		house.gameObject.GetComponent<Renderer>().sortingOrder = layerOrder = LAYER_BASE + 1;
		buildingType = CharacterData.buildingMode.TOWER7;
	}
	
	//attack one time
	float lastTime = Time.realtimeSinceStartup;
	float lastAttackTime = Time.realtimeSinceStartup;
	bool canAttack = false;
	//change the cannon direction
	Vector3 lastDir = Vector3.zero;
	
	void RotateTowards(){
		canAttack = true;
	}
	
	//Find the nearest enemy
	public void CheckEnemy(){
		if (GameManager.Instance.CurStatus != GameManager.Status.START_GAME) {
			return;
		}
		if (Time.realtimeSinceStartup > lastTime + data.attackInterval) {
			//get the enemy list
			if(EnemySpawnManager._instance.enemyList.Count > 0){
				//attack the enemy
				foreach(Character chara in EnemySpawnManager._instance.enemyList){
					if(Vector3.Distance(this.GetPos(),chara.GetPos()) < this.GetAttackRange() && chara.Life >= 0){
						isExist = false;
						for(int i = 0 ; i < enemyLists.Count; i++) {
							if(enemyLists[i].ID == chara.ID){
								isExist = true;
							}
						}
						if(isExist == false){
							enemyLists.Add(chara);
						}
						for(int i = 0;i < enemyLists.Count ; i ++){
							if(enemyLists[i].Life <= 0){
								enemyLists.Remove(enemyLists[i]);
							}
						}
					}
				}
				if(enemyLists.Count > 0){
					curEnemy = enemyLists[0];
				} else if(enemyLists.Count == 0){
					curEnemy = null;
					return;
				}
				if(curEnemy != null){
					if (Vector3.Distance (this.GetPos (), curEnemy.GetPos ()) >= this.GetAttackRange ()) {
						enemyLists.Remove (curEnemy);
						curEnemy = null;
					}
					if(curEnemy!= null){
						status.CurPose = CharacterStatus.Pose.Attack;
						dirPos = this.GetPosition() - curEnemy.GetPos();
						//dir.y = 0;
						RotateTowards();
						ChangeDirection();
						
					}
				}
				lastTime = Time.realtimeSinceStartup;
			} else if(EnemySpawnManager._instance.enemyList.Count == 0){
				enemyLists.Clear();
				curEnemy = null;
			}
		}
	}
	
	//Hit the enmey
	public void HitEnemy(){
		if (GameManager.Instance.CurStatus != GameManager.Status.START_GAME) {
			return;
		}
		if(enemyLists.Count > 0){
			curEnemy = enemyLists[0];
		} else if(enemyLists.Count == 0){
			curEnemy = null;
			return;
		}
		if (curEnemy == null) {
			if(GetTransform()!=null){
				GetTransform().GetChild(0).localPosition = new Vector3(0.0f, 0.3f, 0.0f);
				status.CurPose = CharacterStatus.Pose.None;
			}
			return;
		} 
		if(Vector3.Distance(this.GetPos(),curEnemy.GetPos()) >= this.GetAttackRange() || curEnemy.Life <= 0){
			enemyLists.Remove(curEnemy);
			curEnemy = null;
		}
		//hit enemy
		HitAnimation ();
		if (Time.realtimeSinceStartup > lastTime + data.attackInterval) {
			endAttack = false;
			lastAttackTime = Time.realtimeSinceStartup;
		}
	}
	
	void HitAnimation() {
		if (endAttack||canAttack == false) 
			return;
		mHitDelta += RealTime.deltaTime;
		float rate = 1f;
		if (data.attackRate < mHitDelta) {
			mHitDelta = (data.attackRate > 0f) ? mHitDelta - data.attackRate : 0f;
			if(curFps >0) {
				curFps = 0;				
				GetTransform().GetChild(0).localPosition = new Vector3(0.0f,0.3f,0.0f);	
				QuadTextureNgui gui = GetTransform().GetChild(0).GetComponent<QuadTextureNgui>();
				gui.ScaleFactor = 0.5f;
				endAttack = true;
				canAttack = false;
			} else {	
				if(curEnemy!= null) {
					GameObject bulletgo = (GameObject)GameObject.Instantiate(Resources.Load("misslebullet"));
					CannonBullet bullet = bulletgo.GetComponent<CannonBullet>();
					Transform gun =	GetTransform().GetChild(0);
					bulletgo.transform.position = GetTransform().position+GetTransform().forward * 0.6f;
					//bulletgo.transform.position = gun.position ;
					bullet.parent7 = this;
					if(enemyLists.Count > 0){
						curEnemy = enemyLists[0];
						if(curEnemy!=null){
							bullet.Fire(curEnemy);
						}
						Vector3 dir = Vector3.back* moveDistance;
						GetTransform().GetChild(0).localPosition =new Vector3(dir.x , dir.y + 0.3f, dir.z) ;
						QuadTextureNgui gui = GetTransform().GetChild(0).GetComponent<QuadTextureNgui>();
						gui.ScaleFactor = scalefactor; 
						curFps++;
					} else if(enemyLists.Count == 0){
						curEnemy = null;
						return;
					}
				}
			}
		}
	}
	
	
	public void SetPosition(Vector3 pos){
		if (model != null) {
			model.transform.position = pos;
		}
	}
	
	public void SetDirection(Vector3 dir){
		if (model != null) {
			model.transform.localRotation = Quaternion.Euler (dir);
		}
	}
	
	public Vector3 GetPosition(){
		return model.transform.position;
	}
	
	public void ChangeDirection(){
		//	dirRotation = Vector3.Angle(this.GetPosition(), curEnemy.GetPos ());
		dirRotation = Vector3.Angle (dirPos, Vector3.right);
		QuadTextureNgui tex = GetTransform().GetChild(0).GetComponent<QuadTextureNgui>();
		if(dirRotation >= 0.0f && dirRotation <= 180f){
			int angle = ((int)(dirRotation /10.0f) * 10);
			if(angle == 0){
				tex.mSpriteName = data.level + "0000";
			}
			else if(10 <= angle && angle < 100){
				tex.mSpriteName = data.level + "00" + angle;
			} else {
				tex.mSpriteName = data.level + "0" + angle;
			}
			if(dirPos.z >= 0){
				tex.mirrorX = true;
			}else {
				tex.mirrorX = false;
			}
			tex.InitFace();
		}
	} 
}
