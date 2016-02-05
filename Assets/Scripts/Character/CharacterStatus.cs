using UnityEngine;
using System.Collections;

public class CharacterStatus : MonoBehaviour {
	[HideInInspector]
	//direction of the enemy movement
	public enum Dir{
		LeftUp,
		Left,
		LeftDown,
		RightUp,
		Right,
		RightDown,
	}

	[HideInInspector]
	//current pose of the enemy
	public enum Pose{
		None = 1,
		Idle,
		Run,
		Attack,
		Summon,
		Die,
	}

	//bullet
	public GameObject bullet;
	[HideInInspector] 
	public bool rotateWeapon = false;

	[HideInInspector]
	public Pose CurPose;
	Dir _CurDir = Dir.RightUp;
	Dir mDir;
	Pose mPose;

	[HideInInspector]
	public Character Parent;
	private float idleRotateInterval;
	private float idleRotateSpeed;
	private float idleRotateAngle;
	private int idleInvInterval;

	// Use this for initialization
	void Start () {
		//the init status of the enmeny
		mDir = _CurDir;
		mPose = CurPose;

		if (Parent != null) {
			Parent.Start();
		}
	}

	bool needChangeStatus = false;

	// Update is called once per frame
	void Update () {
		if (Parent != null) {
			Parent.Update();
		}
		//Debug.Log ("3");
			//Parent.Update();
		//change direction
		//CheckDir ();
		//change animation
		ChangeAnim ();
		//change idle
	}

	//change the direction of the character
	public void CheckDir(){
		if (gameObject == null) {
			return;
		} 
		needChangeStatus = false;
		if (mDir != _CurDir) {
			needChangeStatus = true;
			mDir = _CurDir;
		}
		if (mPose != CurPose) {
			needChangeStatus = true;
			mPose = CurPose;
		}
		Quaternion dir = gameObject.transform.localRotation;
		float curAngle = dir.eulerAngles.y % 360.0f;
		curAngle = curAngle < 0.0f ? curAngle + 360.0f : curAngle;
		//rotate the weapon
		if (rotateWeapon && CurPose != Pose.Die) {
			QuadTextureNgui tex = transform.GetChild(0).GetComponent<QuadTextureNgui>();
			if(curAngle >= 0.0f && curAngle <= 151f){
				int angle = ((int)(curAngle/10.0f) * 10);
				tex.mSpriteName = ""+angle;
				tex.mirrorX = false;
				tex.InitFace();
			} else if (curAngle > 151.0f && curAngle <= 301.0f){
				int angle = ((int)((301.0f - curAngle)/10.0f) * 10);
				tex.mSpriteName = ""+angle;
				tex.mirrorX = true;
				tex.InitFace();
			} else if(curAngle > 301.0f && curAngle <= 321.0f){
				int angle = ((int)((661.0f - curAngle)/10.0f) * 10);
				tex.mSpriteName = ""+angle;
				tex.mirrorX = true;
				tex.InitFace();
			}
		} else {
			//will not be used
			if((curAngle >= 0.0f && curAngle < 45.0f) || (curAngle > 315.0f && curAngle <= 360.0f)){
				_CurDir = Dir.RightUp;
			} 
			else if(curAngle >= 270.0f && curAngle <= 315.0f){
				_CurDir = Dir.LeftUp;
			}
			else if(curAngle >= 45.0f && curAngle < 90.0f){
				_CurDir = Dir.Right;
			}
			else if(curAngle >= 100.0f && curAngle < 270.0f){
				_CurDir = Dir.Left;
			}
			else if(curAngle >= 127.0f && curAngle < 180.0f){
				_CurDir = Dir.LeftDown;
			} 
			else if(curAngle >= 90.0f && curAngle < 127.0f){
				_CurDir = Dir.RightDown;
			}
		}
	}

	public void DestoryAni(){
		QuadTextureNgui tex = transform.GetChild(0).GetComponent<QuadTextureNgui>();
		for(int i = 1; i <= 9; i++) {
			
			tex.mSpriteName = "Death001"+i;
			tex.mirrorX = false;
			tex.InitFace();
		} 
	}

	//change the animation
	void ChangeAnim(){
		if(needChangeStatus == false){
			return;
		}
		bool needMirror = false;
		int mfps = 12;
		string namePrefix = "";
//		bool reverse = false;
		if (mPose == Pose.Run) {
			namePrefix = "walk";
			if(mDir == Dir.RightDown){
				namePrefix += "down";
			}
			else if(mDir == Dir.Right){
				namePrefix += "right";
			}
			else if(mDir == Dir.RightUp){
				namePrefix += "up";
			}

			else if(mDir == Dir.LeftDown){
				namePrefix += "down";
				needMirror = true;
			}
			else if(mDir == Dir.Left){
				namePrefix += "left";
				needMirror = true;
			}
			else if(mDir == Dir.LeftUp){
				namePrefix += "up";
				needMirror = true;
			}
		}
		else if (mPose == Pose.Idle){
			//only the weapon have the animation idle
			//the enemy can only move
			if(rotateWeapon){

			} else {
				return;
			}
		}
		else if(mPose == Pose.Attack){
			//only the weapon can attack
			//the enemy can not attack
			if(rotateWeapon){

			} else {
				return;
			}
		}
		else if(mPose == Pose.Die) {
			namePrefix = "Death00";
		}
		QuadTextureAni ani = GetComponentInChildren<QuadTextureAni> ();
		if (ani != null && string.IsNullOrEmpty (namePrefix)) {
			ani.namePrefix = namePrefix;
			ani.mFPS = mfps;
			ani.RebuildSpriteList();
			ani.mirror = needMirror;
			if(mPose == Pose.Attack){
				//ani.OnNormalAnimFinished += OnAttackEnd;
			}
		}
		needChangeStatus = false;
	}

	void OnAttackEnd(){
		if (Parent != null) {
			Parent.OnAttackEnd();
		}
	}

}
