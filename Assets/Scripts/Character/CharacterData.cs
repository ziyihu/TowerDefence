using UnityEngine;
using System.Collections;

public struct CharacterData{
	public enum CharacterClassType{
		CHARACTER = 1,
		BUILDING,
	}
	public enum CharacterModel{
		NONE = -1,
		SOLDIER,
		BOWMAN,
		GIANT,
		VIKING,
	}
	public enum buildingMode{
		BARRACK = 1,
		CANNON,
		TOWER1,
		TOWER2,
		TOWER3,
		TOWER4,
		TOWER5,
		TOWER6,
		TOWER7,
		TOWER8,
		TOWER9,
		TOWER10,
		MINE1,
		MINE2,
		GENERATOR1,
		GENERATOR2,
		ANTENNA,
		LAB,
		CAPACITOR,
		ALIEN
	}
	public void Reset(){
		isDirty = true;
	}

	public bool isDirty;

	//all the info for the enemy
	public long modelID;
	public int classType;
	public int modeltype;
	public int level;
	public Vector3 pos;
	public float speed;
	public int camp;
	public Vector3 rotation;
	public CharacterStatus.Pose pose;
	public float life;
	public float maxLife;
	public int attackPower;
	public float attackRange;
	public float searchInterval;
	public float attackInterval;
	public int currentUseSkillId;

}
