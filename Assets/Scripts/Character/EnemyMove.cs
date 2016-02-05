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
				if(enemy.GetPos().x < 15.45f){
				//first path, walk down
					enemy.SetPos (new Vector3 (enemy.GetPos ().x + enemy.Speed * Time.timeScale, enemy.GetPos ().y, enemy.GetPos ().z));
				}
			else if(enemy.GetPos().x <= 19.8f && enemy.GetPos().x >=15.45f && enemy.GetPos().z >= 18.6f && enemy.GetPos().z <= 25.6f){
				//second path, walk left
					if (ani != null) {
						ani.namePrefix = "walkright";
						ani.mFPS = 20;
						ani.RebuildSpriteList();
						ani.mirror = true;
					}
				enemy.SetPos (new Vector3 (enemy.GetPos ().x , enemy.GetPos ().y, enemy.GetPos ().z - enemy.Speed * Time.timeScale));
				}
			else if(enemy.GetPos().z > 18.2f && enemy.GetPos().z < 18.6f && enemy.GetPos().x >= 15.45f && enemy.GetPos().x <= 19.8f){
				//third path, walk down
					if (ani != null) {
						ani.namePrefix = "walkdown";
						ani.mFPS = 20;
						ani.RebuildSpriteList();
						ani.mirror = false;
					}
				enemy.SetPos (new Vector3 (enemy.GetPos ().x + enemy.Speed * Time.timeScale, enemy.GetPos ().y, enemy.GetPos ().z));
				}
			else if(enemy.GetPos().x < 19.9f && enemy.GetPos().x > 19.8f && enemy.GetPos().z <= 23.5f){
				//forth path, walk right
					if (ani != null) {
						ani.namePrefix = "walkright";
						ani.mFPS = 20;
						ani.RebuildSpriteList();
						ani.mirror = false;
					}
				enemy.SetPos (new Vector3 (enemy.GetPos ().x , enemy.GetPos ().y, enemy.GetPos ().z + enemy.Speed * Time.timeScale));
				}
			else if(enemy.GetPos().z < 23.6f && enemy.GetPos().z >= 23.5f && enemy.GetPos().x > 19.8f && enemy.GetPos().x <= 22.3f){
				//fifth path, walk down
					if (ani != null) {
						ani.namePrefix = "walkdown";
						ani.mFPS = 20;
						ani.RebuildSpriteList();
						ani.mirror = false;
					}
				enemy.SetPos (new Vector3 (enemy.GetPos ().x + enemy.Speed * Time.timeScale, enemy.GetPos ().y, enemy.GetPos ().z));
				}
			else if(enemy.GetPos().x < 22.4f && enemy.GetPos().x > 22.3f && enemy.GetPos().z <= 23.6f && enemy.GetPos().z >= 19.5f){
				//sixth path,walk left
				if (ani != null) {
					ani.namePrefix = "walkright";
					ani.mFPS = 20;
					ani.RebuildSpriteList();
					ani.mirror = true;
				}
				enemy.SetPos (new Vector3 (enemy.GetPos ().x , enemy.GetPos ().y, enemy.GetPos ().z - enemy.Speed * Time.timeScale));
			}
			else if(enemy.GetPos().x >= 22.3f && enemy.GetPos().z < 19.5f){
				//seventh path,walk down
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
