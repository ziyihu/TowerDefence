using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
			for (int i = 0; i < EnemySpawnManager._instance.enemyList.Count; i++) {
				Enemy enemy = (Enemy)EnemySpawnManager._instance.enemyList [i];
				QuadTextureAni ani = enemy.GetTransform().GetComponentInChildren<QuadTextureAni> ();
				//enemy.SetPos (new Vector3 (enemy.GetPos ().x + 0.01f * Time.timeScale, enemy.GetPos ().y, enemy.GetPos ().z));
				if(enemy.GetPos().x < 18.0f){
				//first path, walk down
					enemy.SetPos (new Vector3 (enemy.GetPos ().x + enemy.Speed * Time.timeScale, enemy.GetPos ().y, enemy.GetPos ().z));
				}
				if(enemy.GetPos().x >=18.0f && enemy.GetPos().x < 21.5f && enemy.GetPos().z >= 19.0f && enemy.GetPos().z <= 23.5f){
				//second path, walk right
					if (ani != null) {
						ani.namePrefix = "walkright";
						ani.mFPS = 20;
						ani.RebuildSpriteList();
						ani.mirror = false;
					}
				enemy.SetPos (new Vector3 (enemy.GetPos ().x , enemy.GetPos ().y, enemy.GetPos ().z+ enemy.Speed * Time.timeScale));
				}
				if(enemy.GetPos().z > 23.5f && enemy.GetPos().x >= 18.0f && enemy.GetPos().x <= 21.5f){
				//third path, walk down
					if (ani != null) {
						ani.namePrefix = "walkdown";
						ani.mFPS = 20;
						ani.RebuildSpriteList();
						ani.mirror = false;
					}
				enemy.SetPos (new Vector3 (enemy.GetPos ().x + enemy.Speed * Time.timeScale, enemy.GetPos ().y, enemy.GetPos ().z));
				}
				if(enemy.GetPos().x > 21.5f && enemy.GetPos().z >= 20.3f){
				//forth path, walk left
					if (ani != null) {
						ani.namePrefix = "walkright";
						ani.mFPS = 20;
						ani.RebuildSpriteList();
						ani.mirror = true;
					}
				enemy.SetPos (new Vector3 (enemy.GetPos ().x , enemy.GetPos ().y, enemy.GetPos ().z - enemy.Speed * Time.timeScale));
				}
				if(enemy.GetPos().z < 20.3f && enemy.GetPos().x > 21.5f && enemy.GetPos().x <= 25.0f){
				//fifth path, walk down
					if (ani != null) {
						ani.namePrefix = "walkdown";
						ani.mFPS = 20;
						ani.RebuildSpriteList();
						ani.mirror = false;
					}
				enemy.SetPos (new Vector3 (enemy.GetPos ().x + enemy.Speed * Time.timeScale, enemy.GetPos ().y, enemy.GetPos ().z));
				}
				if(enemy.GetPos().x > 25.0f){
						EnemySpawnManager._instance.enemyList.Remove(enemy);
						enemy.Destroy();
					}
				}

	}
}
