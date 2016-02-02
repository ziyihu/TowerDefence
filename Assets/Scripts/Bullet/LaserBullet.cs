using UnityEngine;
using System.Collections;

public class LaserBullet : MonoBehaviour, IBullet {

		
		public int ID { get; set; }
		public float DelayTime{ get; set; }
		
		public float speed = 90;
		public float maxTime = 2;
		public float Timer = 0;
		
		//set a path about the movement of the bullet
		public Tower10 parent10 { get; set; }
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
			if (parent10 != null) {
				BulletManager.Instance.CalcuBulletDamage (curTarget, this.parent10);
				if (curTarget.Life <= 0) {
					parent10.curEnemy = null;
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
