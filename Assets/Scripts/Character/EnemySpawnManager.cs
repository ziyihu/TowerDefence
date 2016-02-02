using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnManager : MonoBehaviour {

	public static EnemySpawnManager _instance;

	public EnemySpawn[] bowmanBornArray;
	public EnemySpawn[] vikingBornArray;
	public EnemySpawn[] gaintBornArray;

	public bool isStart = false;

	public List<Character> enemyList = new List<Character>();

	void Awake(){
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (Born ());
	}

	IEnumerator Born(){
		//the first wave enemy
		foreach (EnemySpawn s in bowmanBornArray) {
			enemyList.Add(s.Born());
			yield return new WaitForSeconds(1f);
		}
		while (enemyList.Count > 0) {
			yield return new WaitForSeconds(0.2f);
		}

		//second wave enemy
		foreach (EnemySpawn s in vikingBornArray) {
			enemyList.Add(s.Born());
			yield return new WaitForSeconds(1f);
		}
		while (enemyList.Count>0) {
			yield return new WaitForSeconds(0.2f);
		}

		//third wave enemy
		foreach (EnemySpawn s in gaintBornArray) {
			enemyList.Add(s.Born());
			yield return new WaitForSeconds(1f);
		}
		while (enemyList.Count>0) {
			yield return new WaitForSeconds(0.2f);
		}

	}

}
