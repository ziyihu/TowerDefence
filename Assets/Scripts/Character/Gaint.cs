using UnityEngine;
using System.Collections;

public class Gaint : Enemy {
	
	HeroConf conf;
	
	public Gaint():base("giant"){
		this.START_METHOD ("Gaint");
		characterType = CharacterData.CharacterModel.GIANT;
		
		conf = HeroConfManager.Instance.GetHeroConfById (1);
		if (conf != null) {
			data.life = conf.hitPoint;
			data.maxLife = conf.hitPoint;
		}
		this.END_METHOD ("Gaint");
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

