using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TechNode : MonoBehaviour {
	//the node has been unlocked or not
	private static bool isTower1 = false;
	private static bool isTower2 = false;
	private static bool isTower3 = false;
	private static bool isTower4 = false;
	private static bool isTower5 = false;
	private static bool isTower6 = false;
	private static bool isTower7 = false;
	private static bool isTower8 = false;
	private static bool isTower9 = false;
	private static bool isTower10 = false;

	public bool GetTower1{
		get { return isTower1; }
	}

	public bool GetTower2{
		get { return isTower2; }
	}

	public bool GetTower3{
		get { return isTower3; }
	}

	public bool GetTower4{
		get { return isTower4; }
	}

	public bool GetTower5{
		get { return isTower5; }
	}

	public bool GetTower6{
		get { return isTower6; }
	}

	public bool GetTower7{
		get { return isTower7; }
	}

	public bool GetTower8{
		get { return isTower8; }
	}

	public bool GetTower9{
		get { return isTower9; }
	}

	public bool GetTower10{
		get { return isTower10; }
	}

	public UIAtlas Atlas;
	public List<TechNode> techNodeList = new List<TechNode>();
	public string ableSprite;
	public string unableSprite;
	//active sprite, if the tech is loaded, set this active
	public UISprite active;
	private bool isActive = false;
	public UIButton nodeButton;

	void Start(){
		foreach (TechNode node in techNodeList) {
			node.nodeButton.isEnabled = false;
			if(unableSprite != null){
				node.nodeButton.normalSprite = node.unableSprite;
			}
			node.nodeButton.SetState(UIButtonColor.State.Disabled,true);
		}
	}

	public void ActiveNextNode(){
		if(Atlas != null){
			foreach (TechNode node in techNodeList) {
				node.nodeButton.isEnabled = true;
				if(ableSprite != null){
					node.nodeButton.normalSprite = node.ableSprite;
				}
				node.nodeButton.SetState(UIButtonColor.State.Normal,true);
//				node.nodeButton.hoverSprite = "btn_2";
//				node.nodeButton.pressedSprite = "btn_3";
//				node.nodeButton.disabledSprite = "btn_4";
			}
		}
	}

	public void OnBtnClicked(){
		isActive = true;
		nodeButton.SetState(UIButtonColor.State.Pressed,true);
		nodeButton.isEnabled = false;
		if(active != null)
			active.gameObject.SetActive (true);
		if (ableSprite == "bloodraven_1") {
			isTower8 = true;
		}
		if (ableSprite == "marine_1") {
			isTower1 = true;
		}
		if (ableSprite == "firebat_1") {
			isTower2 = true;
		}
		if (ableSprite == "shining_1") {
			isTower5 = true;
		}
		if (ableSprite == "rapidfire_1") {
			isTower3 = true;
		}
		if (ableSprite == "tank_1") {
			isTower4 = true;
		}
		if (ableSprite == "thor_1") {
			isTower6 = true;
		}
		if (ableSprite == "skyrunnerG_1") {
			isTower9 = true;
		}
		if (ableSprite == "toxic_1") {
			isTower7 = true;
		}
		if (ableSprite == "bismarck_1") {
			isTower10 = true;
		}
	}

	void Update(){
		if (isActive) {
			ActiveNextNode();
			isActive = false;
		}
	}
}
