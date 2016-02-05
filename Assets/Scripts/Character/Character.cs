using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//using Pathfinding;
//using Pathfinding.RVO;
//using UnityEditorInternal
public  class Character : IMessageObject,IComparable<Character>
{
	protected static int LAYER_BASE = 3;
	protected CharacterData data;
	protected GameObject model;
	protected GameObject blood;
	protected CharacterStatus status;
	protected int layerOrder;
	public delegate void OnDieHandle ();
	public OnDieHandle OnDieEvent;
	//public bool isDirty;
	static long id;
	long mId;
	public CharacterStatus GetCharacterStatus {get{ return status;} }
	public float AttackRate {get {return data.attackRate;}}
	public long ID{ get { return mId; } }
	public float Life {get{return data.life;}}
	public float Speed { get { return data.speed; } }
	public int GetCurrentSkillId() {return data.currentUseSkillId;}
	public int GetAttackPower(){return data.attackPower;}
	public float GetAttackRange(){return data.attackRange;}
	public int GetCamp(){return data.camp;}
	public Vector3 GetLocalPos(){return model.transform.localPosition;}
	public Vector3 GetRealPos(){return model.transform.position;}
	public int GetLevel() {return data.level;}
	//public Vector3 GetPos
	private bool bNeedChange = false;
	private bool bInited = false;
	
	public Character()
	{
		this.START_METHOD ( "Character");
		//TODO:need change this to resource pool, do not use new , use it's flag isDirty, control by a data controller, Type.GetType(className, true)
		data = (CharacterData)MemoryManager.Instance.CreateNativeStruct ("CharacterData");
		//Activator.CreateInstance
		//UnityEditor.PrefabUtility.GetPrefabType
		//Vector3 pos = new Vector3 (0, 0, 0);
		//Vector3 rotation = new Vector3 (0, 0, 0);
		//status = new CharaStatus ();//gameobject create from outside
		id++;						
		mId = id;		
		bInited = false;
		this.END_METHOD( "Character");
	}
	//	public Vector3 GetRealPos(float bias)
	//	{
	//		model.transform.localPosition 
	//			= new Vector3 (model.transform.localPosition.x + bias, model.transform.localPosition.y, model.transform.localPosition.z + bias);
	//		return model.transform.position;
	//	}
	public virtual void OnBeHit(int damage)
	{
		
	}
	public virtual void OnAttackEnd()
	{
		
	}
	public virtual void CancelHitted()
	{		
		
	}
	public virtual void SetLayer(int order)
	{
		this.START_METHOD ("SetLayer");
		layerOrder = order + LAYER_BASE;
		model.transform.GetChild(0).GetComponent<Renderer>().sortingOrder = layerOrder;
		this.END_METHOD("SetLayer");
	}
	public Vector3 GetPos()
	{
		return data.pos;
	}
	public void SetPos(Vector3 pos)
	{
		this.START_METHOD ("SetPos");
		data.pos = pos;
		bNeedChange = true;
		this.END_METHOD("SetPos");
	}
	public void SetDir(Vector3 rotate)
	{
		this.START_METHOD ("SetDir");
		data.rotation = rotate;
		bNeedChange = true;
		this.END_METHOD ("SetDir");
	}
	public void SetPose(CharacterStatus.Pose pose)
	{
		this.START_METHOD ("SetPose");
		data.pose = pose;
		bNeedChange = true;
		this.END_METHOD ("SetPose");
	}
	public void SetLevel(int level)
	{
		this.START_METHOD ("SetLevel");
		data.level = level;
		this.END_METHOD ("SetLevel");
	}
	public void SetColor(Color color)
	{
		model.transform.GetChild (0).gameObject.GetComponent<Renderer>().material.SetColor ("_Color", color);
	}
	public void SetCamp(int camp)
	{
		this.START_METHOD ("SetCamp");
		data.camp = camp;
		this.END_METHOD ("SetCamp");
	}
	public void SetLife(int life){
		this.START_METHOD("SetLife");
		data.life = life;
		data.maxLife = life;
		this.END_METHOD("SetLife");
	}
	public void SetAttackPower(int power){
		this.START_METHOD("SetAttackPower");
		data.attackPower = power;
		this.END_METHOD("SetAttackPower");
	}
	public void SetAttackRange(float range){
		data.attackRange = range;
	}
	public void SetAttackRate(float rate){
		data.attackRate = rate;
	}
	public void SetSpeed(float speed){
		data.speed = speed;
	}
	public virtual void Start()
	{
		if (data.classType == (int)CharacterData.CharacterClassType.CHARACTER)
			mId += 10000;
		//Core.EventSystem.OnReachedTargetEvent += OnReachedTarget;
		//RVOController controller =	status.gameObject.GetComponent<RVOController> ();
	}
	
	public virtual void OnPathComplete()
	{
	}
	
	public virtual void OnReachedTarget()
	{
		
	}
	public virtual void Update()
	{
		if (bNeedChange) 
		{
			model.transform.localPosition = data.pos;
			model.transform.localRotation = Quaternion.Euler(data.rotation);
			status.CurPose = data.pose;
			bNeedChange = false;
			if(!bInited)
			{
				DoAI();
				//model.SetActive (true);
			}
		}
	}
	public virtual void DoAI()
	{
		
		this.START_METHOD ( "DoAI");
		this.END_METHOD ( "DoAI");
	}
	public virtual void CancelAI()
	{
		this.START_METHOD("CancelAI");
		this.END_METHOD("CancelAI");
	}
	public void Destroy()
	{
		GameObject.Destroy (model);
		GameObject.Destroy (blood);
		
		data.isDirty = true;
		//isDirty = true;
	}
	
	public int CompareTo(Character other)
	{//default sort function
		return 0;
		//		if (this.n > other.n)
		//			return 1;
		//		else if (this.n == other.n)
		//			return 0;
		//		else
		//			return -1;
	}


}

