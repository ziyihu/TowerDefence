using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonBullet : MonoBehaviour, IBullet {

	public int ID { get; set; }
	public float DelayTime{ get; set; }

	public float speed = 90;
	public float maxTime = 2;
	public float Timer = 0;

	//set a path about the movement of the bullet
	public Cannon parent{ get; set; }
	public Tower1 parent1 { get; set; }
	public Tower2 parent2 { get; set; }
	public Tower7 parent7 { get; set; }
	private float distanceToTarget;
	private bool move = true;
	private Character curTarget;
	private float Distance;
	private float explosionRange = 1.0f;

	private GameObject explosionObj = null;

	public void Fire(Character target){
		curTarget = target;
		Transform house = transform.GetChild (0);
		house.gameObject.GetComponent<Renderer>().sortingOrder = 4;

		distanceToTarget = Vector3.Distance (this.transform.position, target.GetPos());
		StartCoroutine (Shoot (target));
	}

//	public void FireMulti(Character target){
//		curTarget = target;
//		Transform house = transform.GetChild (0);
//		house.gameObject.GetComponent<Renderer>().sortingOrder = 4;
//		foreach(Character chara in EnemySpawnManager._instance.enemyList){
//			if(Vector3.Distance(chara.GetPos(),curTarget.GetPos()) <= explosionRange){
//				curTargetList.Add(chara);
//			}
//		}
//		distanceToTarget = Vector3.Distance (this.transform.position, target.GetPos());
//		StartCoroutine (Shoot (target));
//	}

	IEnumerator Shoot(Character target){
		while (move) {
			if(target != null){
				Vector3 targetPos = target.GetPos();
				this.transform.LookAt(targetPos);
				float currentDist = Vector3.Distance(this.transform.position, target.GetPos());
				if(currentDist < 0.5f){
					move = false;
					OnHited();
				}
				this.transform.Translate(Vector3.forward*Mathf.Min(speed * Time.deltaTime,currentDist));
				yield return null;
			}
			else{
				move = false;
				GameObject.Destroy(gameObject);
			}
		}
	}

	public void OnExplosionEffect(){
		//create a new game object to show the explosion effect
		explosionObj = (GameObject)GameObject.Instantiate (Resources.Load ("explosion"));
		explosionObj.transform.position = curTarget.GetPos();
	}

	public void OnHited(){
		if (parent != null) {
			BulletManager.Instance.CalcuBulletDamage (curTarget, this.parent);
			if (curTarget.Life <= 0) {
				parent.curEnemy = null;
			} 
		}
		if (parent1 != null) {
			BulletManager.Instance.CalcuBulletDamage (curTarget, this.parent1);
			if (curTarget.Life <= 0) {
				parent1.curEnemy = null;
			} 
		}
		if (parent2 != null) {
			BulletManager.Instance.CalcuBulletDamage (curTarget, this.parent2);
			if (curTarget.Life <= 0) {
				parent2.curEnemy = null;
			} 
		}
		if (parent7 != null) {
			for(int i = 0 ; i< EnemySpawnManager._instance.enemyList.Count ; i++){
				Distance = Vector3.Distance(EnemySpawnManager._instance.enemyList[i].GetPos(),curTarget.GetPos());
				if(Distance <= explosionRange){
					if((EnemySpawnManager._instance.enemyList[i].Life - this.parent7.attackPower) <= 0){
						BulletManager.Instance.CalcuBulletDamage (EnemySpawnManager._instance.enemyList[i], this.parent7);
						i--;
					} else {
						BulletManager.Instance.CalcuBulletDamage (EnemySpawnManager._instance.enemyList[i], this.parent7);
					}
				}
			}
			OnExplosionEffect();
			if (curTarget.Life <= 0) {
				parent7.curEnemy = null;
				GameObject.Destroy (gameObject);
			} 
		}

		GameObject.Destroy (gameObject);
	}

	public void Update(){
		Timer += Time.deltaTime;
		if (Timer >= maxTime) {
			GameObject.Destroy(gameObject);
			Timer = 0;
		}
	}
}
