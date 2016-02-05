using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour,IMessageObject {

	List<Character> chars = new List<Character>();
	public List<Character> building = new List<Character>();
	List<Character> allCharacter = new List<Character> ();
	List<Vector3> position = new List<Vector3>();
	
	Character chara = new Character();

	public Character SpawnCharacter(CharacterData.CharacterClassType classType, int charModeType, int camp, int level, Vector3 pos, Vector3 dir, CharacterStatus.Pose pose){
		this.START_METHOD("SpawnCharacter");
		Character tempChar = null;
		//create the enemies
		if (classType == CharacterData.CharacterClassType.CHARACTER) {
			//create the enemy
			if((CharacterData.CharacterModel)charModeType == CharacterData.CharacterModel.BOWMAN){
				Bowman chara = new Bowman();
				chara.SetPos(pos);
				chara.SetDir(dir);
				chara.SetPose(pose);
				chara.SetCamp(camp);
				chara.SetSpeed(0.01f);
				//set bowman max life
				chara.SetLife(200);
				tempChar = chara;
			} else if((CharacterData.CharacterModel)charModeType == CharacterData.CharacterModel.GIANT){
				Gaint chara = new Gaint();
				chara.SetPos(pos);
				chara.SetDir(dir);
				chara.SetPose(pose);
				chara.SetCamp(camp);
				chara.SetSpeed(0.01f);
				//set giant max life
				chara.SetLife(400);
				tempChar = chara;
			} else if((CharacterData.CharacterModel)charModeType == CharacterData.CharacterModel.VIKING){
				Viking chara = new Viking();
				chara.SetPos(pos);
				chara.SetDir(dir);
				chara.SetPose(pose);
				chara.SetCamp(camp);
				chara.SetSpeed(0.01f);
				//set viking max life
				chara.SetLife(300);
				tempChar = chara;
			} 
			if(tempChar != null){
				chars.Add(tempChar);
			} else {
				throw new UnityException("no current char type to spawn!");
			}
		}
		//create the building
		else if (classType == CharacterData.CharacterClassType.BUILDING) {
			//create the barrack to gather resources
			if((CharacterData.buildingMode)charModeType == CharacterData.buildingMode.BARRACK){
				TowerBarrack character = new TowerBarrack();
				character.SetPos(pos);
				character.SetDir(dir);
				character.SetCamp(camp);
				tempChar = character;
			} else if((CharacterData.buildingMode)charModeType == CharacterData.buildingMode.CANNON){
				Cannon character = new Cannon();
				character.SetPos(pos);
				character.SetDir(dir);
				character.SetPose (pose);
				character.SetCamp(camp);
				//set attack power
				character.SetAttackPower(50);
				tempChar = character;
			}
			//Tower01
			else if((CharacterData.buildingMode)charModeType == CharacterData.buildingMode.TOWER1){
				Tower1 character = new Tower1();
				character.SetAttackRange(3);
				character.SetLevel(1);
				character.SetPos(pos);
				character.SetDir(dir);
				character.SetPose (pose);
				character.SetCamp(camp);
				character.SetAttackRate(1f);
				//set attack power
				character.SetAttackPower(1);
				tempChar = character;
			}
			//Tower02
			else if((CharacterData.buildingMode)charModeType == CharacterData.buildingMode.TOWER2){
				Tower2 character = new Tower2();
				character.SetAttackRange(3);
				character.SetLevel(1);
				character.SetPos(pos);
				character.SetDir(dir);
				character.SetPose (pose);
				character.SetCamp(camp);
				character.SetAttackRate(1f);
				//set attack power
				character.SetAttackPower(1);
				tempChar = character;
			}
			//Tower4
			else if((CharacterData.buildingMode)charModeType == CharacterData.buildingMode.TOWER4){
				Tower4 character = new Tower4();
				character.SetAttackRange(2);
				character.SetLevel(1);
				character.SetPos(pos);
				character.SetDir(dir);
				character.SetPose(pose);
				character.SetCamp(camp);
				tempChar = character;
			}
			//Tower7
			else if((CharacterData.buildingMode)charModeType == CharacterData.buildingMode.TOWER7){
				Tower7 character = new Tower7();
				character.SetAttackRange(2);
				character.SetLevel(1);
				character.SetPos(pos);
				character.SetDir(dir);
				character.SetPose(pose);
				character.SetCamp(camp);
				character.SetAttackPower(1);
				character.SetAttackRate(1f);
				tempChar = character;
			}
			//Tower10
			else if((CharacterData.buildingMode)charModeType == CharacterData.buildingMode.TOWER10){
				Tower10 character = new Tower10();
				character.SetAttackRange(4);
				character.SetLevel(1);
				character.SetPos(pos);
				character.SetDir(dir);
				character.SetPose(pose);
				character.SetCamp(camp);
				character.SetAttackPower(1);
				character.SetAttackRate(2f);
				tempChar = character;
			}
			//created the barrack, add to the building list
			if(tempChar != null){
				building.Add(tempChar);
			} else { 
				throw new UnityException("no current building type to spawn!");
			}
		}
		//add to the all character list
		allCharacter.Add (tempChar);
		this.END_METHOD("SpawnCharacter");
		return tempChar;
	}
	

	//delete all the character
	public void DestoryAll(){
		this.START_METHOD("DestoryAll");
		allCharacter.Clear ();
		for (int i = chars.Count - 1; i >= 0; i--) {
			chars[i].Destroy();
			chars.RemoveAt(i);
		}
		for (int i = building.Count - 1; i >= 0; i--) {
			building[i].Destroy();
			building.RemoveAt(i);
		}
		Resources.UnloadUnusedAssets ();
		System.GC.Collect ();
		this.END_METHOD("DestoryAll");
	}

	//delete one character
	//using ID to find the character need to be destoryed
	public void DestoryChar(long id){
		this.START_METHOD("DestoryChar");
		for (int i = chars.Count - 1; i >= 0; i--) {
			if(chars[i].ID == id){
				chars[i].Destroy();
				chars.RemoveAt(i);
				break;
			}
		}
		for (int i = allCharacter.Count - 1; i >= 0; i--) {
			if(allCharacter[i].ID == id){
				allCharacter[i].Destroy();
				allCharacter.RemoveAt(i);
				break;
			}
		}
		this.END_METHOD("DestoryChar");
	}

	IEnumerator WaitAni(){
		yield return new WaitForSeconds (2);
	}

	//delete one building
	//using ID to find the building need to be destoryed
	public void DestoryBuilding(long id){
		this.START_METHOD("DestoryBuilding");
		for (int i = building.Count - 1; i >= 0; i--) {
			if(building[i].ID == id){
				building[i].Destroy();
				building.RemoveAt(i);
				break;
			}
		}
		for (int i = allCharacter.Count - 1; i >= 0; i--) {
			if(allCharacter[i].ID == id){
				allCharacter[i].Destroy();
				allCharacter.RemoveAt(i);
				break;
			}
		}
		this.END_METHOD("DestoryBuilding");
	}


	//get the building info
	public Building GetBuildingById(long id){
		for (int i = 0; i < building.Count; i++) {
			if(building[i].ID == id){
				return building[i] as Building;
			}
		}
		return null;
	}

	//get the closest enemy
	public Character FindEnemyByDistance(Building building){

		float shorestDis = 3f;
		//get all the ememy in the list
		for (int i = 0; i < EnemySpawnManager._instance.enemyList.Count; i++) {
			Vector3 dir = building.GetTransform().position - EnemySpawnManager._instance.enemyList[i].GetPos();
			dir.y = 0;
			float targetDist = dir.magnitude;
			//if the target is in the attack range
			if(targetDist <= building.GetAttackRange()){
				//must be the closetest target
				if(targetDist < shorestDis){
					shorestDis = targetDist;
					chara = EnemySpawnManager._instance.enemyList[i];
				}
			}
		}
		if (EnemySpawnManager._instance.enemyList.Count == 0) {
			return null;
		}
		return chara;
	}
	
}
