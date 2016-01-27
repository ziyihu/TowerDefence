using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : UnityAllSceneSingleton<BulletManager> {

	Dictionary<int,IBullet> bullet = new Dictionary<int, IBullet>();

	public void CalcuBulletDamage(Character enemy, Building building){
		enemy.OnBeHit (building.GetAttackPower ());
	}
}
