using UnityEngine;
using System.Collections;

public class TowerBarrack : Building {
	public GameObject barrack;
	public TowerBarrack(){
		this.START_METHOD("TowerBarrack");

		model = (GameObject)GameObject.Instantiate(Resources.Load("barrack"));
		model.name = "" + ID;
		//get the building
		Transform house = model.transform.GetChild (0);
		house.gameObject.GetComponent<Renderer>().sortingOrder = layerOrder = LAYER_BASE + 1;

		//status: idle attack
		status = model.GetComponent<CharacterStatus> ();
		buildingType = CharacterData.buildingMode.BARRACK;
		//TODO
		//name, attack number, attack range

		this.END_METHOD("TowerBarrack");
	}

	public override void Start(){
		base.Start ();
		//Init the tower from the resource
		//show the blood, which not been used if the enemy can not attack the building
		/*
		BuildingConf conf = BuildingConfManager.Instance.GetBuildingConfById (10102);
		if (conf != null) {
			data.life = conf.life;
			data.maxLife = conf.life;
		}
		*/	 
	}

	public void SetPosition(Vector3 pos){
		model.transform.localPosition = pos;
	}

	public void SetDirection(Vector3 dir){
		model.transform.localRotation = Quaternion.Euler(dir);
		}

	public override void Update(){
		base.Update ();
	}
}
