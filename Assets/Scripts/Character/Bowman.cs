using UnityEngine;
using System.Collections;

public class Bowman : Enemy {

	HeroConf conf;

	public Bowman():base("bowmanwalk"){
		this.START_METHOD ("Bowman");
		characterType = CharacterData.CharacterModel.BOWMAN;

		conf = HeroConfManager.Instance.GetHeroConfById (1);
		if (conf != null) {
			data.life = conf.hitPoint;
			data.maxLife = conf.hitPoint;
		}
		this.END_METHOD ("Bowman");
	}

	public void Destory(){
		base.Destory ();
	}

	// Use this for initialization
	public void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	public void Update () {
		base.Update ();
	}
}
