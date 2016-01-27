/*using UnityEngine;
using System.Collections;
//buildings and towers
public class Building : Character {
	//Get the building transform component
	public Transform GetTransform(){
		return model.transform;
	}
	public CharacterData.buildingMode buildingType;
	//show a panel to the user 
	bool beGuided = false;

	public override void Start(){
		data.classType = (int)CharacterData.CharacterClassType.BUILDING;
		base.Start ();

		//TODO
		//update the tower
		//name
		//attack number
		//attack range

	}

	public override void Update(){
		base.Update ();
		Debug.Log ("-------------");
	}
}
*/

using UnityEngine;
using System.Collections;

public class Building :  Character
{
	public Transform GetTransform(){
		if (model != null)
			return model.transform;
		else 
			return null;
	}
	public CharacterData.buildingMode buildingType;
	bool beGuided = false;
	bool beHitted = true;
	public override void Start ()
	{
		data.classType = (int)CharacterData.CharacterClassType.BUILDING;
		base.Start ();	
//		model.GetComponent<Axis2DFrom3D> ().SetShow(true);
		
	}
	float tmpHideTime = Time.realtimeSinceStartup;
	public override void Update ()
	{
		base.Update ();
//		if (model.GetComponent<Axis2DFrom3D> ().IsShow()  && Time.realtimeSinceStartup - tmpHideTime > 2.0f) 
//		{
//			model.GetComponent<Axis2DFrom3D> ().SetShow( false);
//		}
		
		if (!beHitted && beGuided) 
		{
			OnBeGuided(false);
		}
	}
	public override void CancelHitted()
	{		
		beHitted = false;
	}
	public override void OnBeHit(int damage)
	{
		//damage = 500;
		base.OnBeHit (damage);
		data.life -= damage;
		if (data.life <= 0) {
			OnBeGuided (false);
			data.life = 0;
			status.CurPose = CharacterStatus.Pose.Die;
	//		model.GetComponent<Axis2DFrom3D> ().SetShow (false);
			if (OnDieEvent != null)
				OnDieEvent ();
			
		} else {
			OnBeGuided (true);
			beHitted = true;
		}
//		if (data.maxlife != 0) {
//			tmpHideTime = Time.realtimeSinceStartup;
//			Transform bloodFull = blood.transform.GetChild (1);
//			bloodFull.gameObject.transform.localScale = new Vector3 ((float)data.life / (float)data.maxlife
//			                                                         , bloodFull.gameObject.transform.localScale.y, bloodFull.gameObject.transform.localScale.z);
//			model.GetComponent<Axis2DFrom3D> ().SetShow( true) ;
//			
//		}
	}
	public virtual void OnBeGuided(bool show)
	{
		beGuided = show;
	}
	public override void DoAI ()
	{
		base.DoAI ();
		
	}
}

