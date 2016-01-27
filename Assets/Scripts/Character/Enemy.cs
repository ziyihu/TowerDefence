using UnityEngine;
using System.Collections;

public class Enemy : Character {

	public CharacterData.CharacterModel characterType;
	public Transform GetTransform (){
		return model.transform;
	}
	public Enemy(){

	}

	public Enemy(string prefebs){
		model = (GameObject)GameObject.Instantiate (Resources.Load (prefebs));

		status = model.GetComponent<CharacterStatus> ();
		status.CurPose = CharacterStatus.Pose.Run;
		status.Parent = this;

		//blood
		blood = (GameObject)GameObject.Instantiate (Resources.Load ("blood"));
		Axis2DTo3D axisTrans = model.GetComponent<Axis2DTo3D> ();
		axisTrans.Icon = blood.transform;
		//event
	}

	public void SetPosition(Vector3 position){
		Debug.Log("2");
		model.transform.localPosition = position;
	}

	public void SetDirection(Vector3 dir){
		model.transform.localRotation = Quaternion.Euler(dir);
	}

	public Vector3 GetPosition(){
		return model.transform.localPosition;
	}

	public void GetDirection(){
		//return model.transform.rotation;
	}

	public virtual void OnDie(){

	}

	public override void OnBeHit(int damage){
		base.OnBeHit (damage);
		if(model != null){
		data.life -= damage;
		if (data.life <= 0) {

			//hide the blood number panel
			Axis2DTo3D axis = model.GetComponent<Axis2DTo3D> ();
			axis.SetShow(false);

			//TODO
			//play die animation
//			QuadTextureAni ani = GetTransform().GetComponentInChildren<QuadTextureAni> ();
//			if (ani != null) {
//				ani.namePrefix = "Deth00";
//				ani.mFPS = 10;
//				ani.RebuildSpriteList();
//				ani.mirror = false;
//			}

			data.life = 0;
			status.CurPose = CharacterStatus.Pose.Die;
			OnDie();
			EnemySpawnManager._instance.enemyList.Remove(this);
			//show the destory animation
			GameManager.Instance.DeleteById(ID);
				return;
		}
		if (data.life > 0) {
				Transform bloodFull = blood.transform.GetChild (1);
				bloodFull.gameObject.transform.localScale = new Vector3 ((float)data.life / (float)data.maxLife, 
		                                                        bloodFull.gameObject.transform.localScale.y, 
		                                                        bloodFull.gameObject.transform.localScale.z);
			model.GetComponent<Axis2DTo3D>().SetShow(true);
		}
		}
	}
	


	public void Start(){
		data.classType = (int)CharacterData.CharacterClassType.CHARACTER;
		base.Start ();

	}

	public void Destory(){
		base.Destroy ();
	}


}
