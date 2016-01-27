using UnityEngine;
using System.Collections;

public class Axis2DTo3D : MonoBehaviour
{
	//role 
	public Transform Icon;
	public float Height = 0.32f;
	public float Depth = 1.0f;
	bool isShow;
	//icon distance from camera
	private float mLensScale;
	
	
	// Use this for initialization
	void Start ()
	{
		//calc for icon's position from camera
		mLensScale = Camera.main.transform.position.y;//Vector3.Distance( transform.position, Camera.main.transform.position);
		//UISprite sprite = Icon.gameObject.GetComponent<UISprite> ();
		//sprite.enabled = true;
		float newScale =mLensScale/( Camera.main.transform.position.y);//mLensScale / Vector3.Distance (transform.position, Camera.main.transform.position);
		Vector3 pos = new Vector3(transform.position.x - Height, transform.position.y + Height, transform.position.z);
		//if(needShow)
		Icon.position = WorldToUI (pos);
		Icon.localScale = new Vector3 (0.2f, 0.3f, 0.2f)* newScale;
		Icon.gameObject.SetActive (true);
		//Debug.Log (Application.persistentDataPath);
		//transform.localRotation
		
	}
	public bool IsShow()
	{
		return isShow;
	}
	public void SetShow(bool show)
	{
		Icon.gameObject.SetActive (show);
		isShow = show;
		//Icon.gameObject.SetActive (show);
		
	}
	// Update is called once per frame
	void Update ()
	{
		//check for the position change from camera
		float newScale =mLensScale/( Camera.main.transform.position.y);//mLensScale / Vector3.Distance (transform.position, Camera.main.transform.position);
		Vector3 pos = new Vector3(transform.position.x - Height, transform.position.y +Depth, transform.position.z);
		//if(needShow)
		Icon.position = WorldToUI (pos);

		//first parameter is the width of the blood 
		Icon.localScale = new Vector3 (0.3f, 0.4f, 0.2f) * newScale;
	}
	//core for 3d to 2d
	public static Vector3 WorldToUI(Vector3 point)
	{
		Vector3 pt = Camera.main.WorldToScreenPoint (point);
		Vector3 ff = UICamera.mainCamera.ScreenToWorldPoint (pt);
		ff.z = 0;
		return ff;
	}
	
}

