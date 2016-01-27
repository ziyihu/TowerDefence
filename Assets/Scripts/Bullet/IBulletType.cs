using UnityEngine;
using System.Collections;

public interface IBulletType {

	int ID{ get; set; }
	string Name{get;set;}
	float DelayTime{ get; set; }
	void OnDie();
}
