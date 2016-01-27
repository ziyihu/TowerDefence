using UnityEngine;
using System.Collections;

public class Tower2 : Building {
	//Current Enemy
	public Character curEnemy;
	
	public int level = 1;
	
	float mHitDelta;
	bool endAttack = true;
	int curFps = 0;
	float mFPS = 10;
	float moveDistance = 0.5f;
	//the cannon will be bigger when attacking the enemy
	float scalefactor = 0.6f;
	int turnSpeed = 12;
	Vector3 dirPos;
	
	float dirRotation;
	
	public Tower2(){
		this.START_METHOD("Tower2");
		
		model = (GameObject)GameObject.Instantiate(Resources.Load("tower2"));
		model.name = "" + ID;
		
		status = model.GetComponent<CharacterStatus> ();
		status.rotateWeapon = true;
		status.CurPose = CharacterStatus.Pose.None;
		
		//set the weapon attack power
		
		
		//get the building
		Transform house = model.transform.GetChild (0);
		house.gameObject.GetComponent<Renderer>().sortingOrder = layerOrder = LAYER_BASE + 1;
		buildingType = CharacterData.buildingMode.TOWER1;
		//TODO
		//name, attack number, attack range
		
		this.END_METHOD("Tower2");
	}
	
	
	public override void Start(){
		
	}
	
	public virtual void Update(){
		base.Update ();
		//check enemy
		CheckEnemy ();
		//hit enemy
		HitEnemy ();
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
				curEnemy = EnemySpawnManager._instance.enemyList[0];
				if(curEnemy != null){
					status.CurPose = CharacterStatus.Pose.Attack;
					dirPos = this.GetPosition() - curEnemy.GetPos();
					
					//dir.y = 0;
					RotateTowards();
					ChangeDirection();
				}
				lastTime = Time.realtimeSinceStartup;
			}
		}
	}
	
	//Hit the enmey
	public void HitEnemy(){
		if (GameManager.Instance.CurStatus != GameManager.Status.START_GAME) {
			return;
		}
		Debug.Log ("1");
		if (EnemySpawnManager._instance.enemyList.Count != 0) {
			curEnemy = EnemySpawnManager._instance.enemyList [0];
		}
		if (curEnemy == null) {
			if(GetTransform()!=null){
				GetTransform().GetChild(0).localPosition = new Vector3(0.0f, 0.3f, 0.0f);
				status.CurPose = CharacterStatus.Pose.None;
			}
			return;
			
		} 
		//hit enemy
		HitAnimation ();
		if (Time.realtimeSinceStartup > lastTime + data.attackInterval) {
			endAttack = false;
			lastAttackTime = Time.realtimeSinceStartup;
		}
	}
	
	void HitAnimation()
	{
		Debug.Log ("33");
		if (endAttack||canAttack == false) 
			return;
		mHitDelta += RealTime.deltaTime;
		float rate = 0.5f;
		
		if (  rate < mHitDelta) {
			
			mHitDelta = (rate > 0f) ? mHitDelta - rate : 0f;
			if(curFps >0)
			{
				curFps = 0;				
				GetTransform().GetChild(0).localPosition = new Vector3(0.0f,0.3f,0.0f);	
				QuadTextureNgui gui = GetTransform().GetChild(0).GetComponent<QuadTextureNgui>();
				gui.ScaleFactor = 0.5f;
				
				endAttack = true;
				canAttack = false;
				
				
			}
			else
			{	
				GameObject bulletgo = (GameObject)GameObject.Instantiate(Resources.Load("cannonbullet"));
				CannonBullet bullet = bulletgo.GetComponent<CannonBullet>();
				Transform gun =	GetTransform().GetChild(0);
				
				bulletgo.transform.position = GetTransform().position+GetTransform().forward * 0.6f;
				//bulletgo.transform.position = gun.position ;
				
				bullet.parent2 = this;
				
				if(EnemySpawnManager._instance.enemyList.Count > 0){
					curEnemy = EnemySpawnManager._instance.enemyList[0];
					bullet.Fire(curEnemy);
					Vector3 dir = Vector3.back* moveDistance;
					GetTransform().GetChild(0).localPosition =new Vector3(dir.x , dir.y + 0.3f, dir.z) ;
					QuadTextureNgui gui = GetTransform().GetChild(0).GetComponent<QuadTextureNgui>();
					gui.ScaleFactor = scalefactor; 
					curFps++;
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
			int angle = 19 - (int)(dirRotation /10.0f) ;
			tex.mSpriteName = level + " " + "(" + angle + ")";
			if(dirPos.z >= 0){
				tex.mirrorX = true;
			}else {
				tex.mirrorX = false;
			}
			tex.InitFace();
		}
	} 
	
	
	
}
