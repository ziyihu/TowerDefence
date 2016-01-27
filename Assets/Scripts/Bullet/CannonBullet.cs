using UnityEngine;
using System.Collections;

public class CannonBullet : MonoBehaviour, IBullet {

	public int ID { get; set; }
	public float DelayTime{ get; set; }

	public float speed = 90;

	//set a path about the movement of the bullet
	public Cannon parent{ get; set; }
	public Tower1 parent1 { get; set; }
	public Tower2 parent2 { get; set; }
	private float distanceToTarget;
	private bool move = true;
	private Character curTarget;


	
	public void Fire(Character target){
		curTarget = target;
		Transform house = transform.GetChild (0);
		house.gameObject.GetComponent<Renderer>().sortingOrder = 4;

		distanceToTarget = Vector3.Distance (this.transform.position, target.GetPos());
		StartCoroutine (Shoot (target));
	}
	IEnumerator Shoot(Character target){
		while (move) {
			if(target != null){
				//change the targetPos 
				//let the game look better
				//TODO

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

		GameObject.Destroy (gameObject);
	}
}
