using UnityEngine;
using System.Collections;

public class InGame : MonoBehaviour {

	public TweenPosition techTween;

	public UIButton tower01;
	public UIButton tower02;
	public UIButton tower03;
	public UIButton tower04;
	public UIButton tower05;
	public UIButton tower06;
	public UIButton tower07;
	public UIButton tower08;
	public UIButton tower09;
	public UIButton tower10;
	
	public UIButton stop;
	public UIButton start;
	public UIButton techTree;
	public UIButton wiki;
	public UIButton closeBtn;

	private TechNode node;
	
	void Start(){
		node = new TechNode ();

		//get the tower01 button
		UIEventListener.Get (tower01.gameObject).onClick += OnCreateTower01;
		
		UIEventListener.Get (stop.gameObject).onClick += OnStop;
		UIEventListener.Get (start.gameObject).onClick += OnStart;
		UIEventListener.Get (techTree.gameObject).onClick += OnShowTechTree;
		UIEventListener.Get (closeBtn.gameObject).onClick += OnHideTechTree;
		UIEventListener.Get (wiki.gameObject).onClick += OnShowWiki;
		Time.timeScale = 0;
	}
	
	void OnEnable(){
		
	}
	void OnDisable(){
		
	}
	
	void OnCreateTower01(GameObject obj){
		
	}
	
	public void OnStop(GameObject obj){
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
		} else if(Time.timeScale == 0){
			Time.timeScale = 1;
		}
	}
	public void OnStart(GameObject obj){
		Time.timeScale = 1;
	}

	public void OnShowTechTree(GameObject obj){
		techTween.PlayForward ();
		Time.timeScale = 0;
	}

	public void OnHideTechTree(GameObject obj){
		techTween.PlayReverse ();
	}

	void OnShowWiki(GameObject obj){
		
	}

	void OnDestory(){
		UIEventListener.Get (tower01.gameObject).onClick -= OnCreateTower01;
		
		UIEventListener.Get (stop.gameObject).onClick -= OnStop;
		UIEventListener.Get (start.gameObject).onClick -= OnStart;
		UIEventListener.Get (techTree.gameObject).onClick -= OnShowTechTree;
		UIEventListener.Get (closeBtn.gameObject).onClick -= OnHideTechTree;
		UIEventListener.Get (wiki.gameObject).onClick -= OnShowWiki;
	}

	void Update(){
		if (node.GetTower8) {
			tower08.transform.GetChild(0).gameObject.SetActive(false);
		}
	}
}
