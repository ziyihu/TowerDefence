using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TechNode : MonoBehaviour {

	private static bool isTower8 = false;

	public bool GetTower8{
		get { return isTower8; }
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
	}

	void Update(){
		if (isActive) {
			ActiveNextNode();
			isActive = false;
		}
	}
}
