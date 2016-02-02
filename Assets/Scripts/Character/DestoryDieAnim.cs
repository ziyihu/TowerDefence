using UnityEngine;
using System.Collections;

public class DestoryDieAnim : MonoBehaviour {
	public float timer = 2;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, timer);
	}
	
	// Update is called once per frame
	void Update () {
		}


}
