using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Character {

	private float timer = 0;
	private float showDieTime = 2;
	private Vector3 diePos;
	private GameObject dieObj = null;

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
			data.life = 0;
			status.CurPose = CharacterStatus.Pose.Die;
			OnDie();
			//die position
			diePos = model.transform.position;
			EnemySpawnManager._instance.enemyList.Remove(this);
			
			//destory the game object
			GameManager.Instance.DeleteById(ID);
			//create a new game object to show the animation
			dieObj = (GameObject)GameObject.Instantiate (Resources.Load ("die"));
			dieObj.transform.position = diePos;
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
