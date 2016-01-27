using UnityEngine;
using System.Collections;

public class BillBoard : MonoBehaviour {

	public Camera mCamer;
	public bool needSynRotate = false;
	Quaternion direction = new Quaternion();

	// Use this for initialization
	void Start () {
		if (mCamer == null) {
			mCamer = Camera.main;
		}
		direction.x = transform.localRotation.x;
		direction.y = transform.localRotation.y;
		direction.z = transform.localRotation.z;
		direction.w = transform.localRotation.w;
	}

	float deltaTime = 0.0f;

	//rotate the camera 
	// Update is called once per frame
	void Update () {
		Camera cam = null;
		if (mCamer != null) {
			cam = mCamer;
		} else {
			cam = Camera.current;
			if(!cam){
				return;
			}
			deltaTime += Time.deltaTime;
			if(needSynRotate){
				transform.rotation = cam.transform.rotation * new Quaternion(direction.x,direction.y,direction.z-transform.localRotation.x,direction.w);
			} else {
				transform.rotation = cam.transform.rotation * new Quaternion(direction.x,direction.y,direction.z,direction.w);
			}
		}
	}
}
