using UnityEngine;
using System.Collections;

public class Viking : Enemy {

	HeroConf conf;
	
	public Viking():base("viking"){
		this.START_METHOD ("Viking");
		characterType = CharacterData.CharacterModel.VIKING;
		
		conf = HeroConfManager.Instance.GetHeroConfById (1);
		if (conf != null) {
			data.life = conf.hitPoint;
			data.maxLife = conf.hitPoint;
		}
		this.END_METHOD ("Viking");
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
