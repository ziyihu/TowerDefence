using UnityEngine;	
using System.Collections;

public class InputListener : UnitySceneSingleton<InputListener>
{
	/*
private static IGameInput _gameInput;
	float xSpeed = 1f;
	float ySpeed = 1f;
	float x = 0.0f;
	float y = 0.0f;

	// Use this for initialization
		void Start ()
		{
			
			if (PlatformUtil.isTouchDevice)
			{
				_gameInput = new SingleTouchGameInput();
			}
			else
			{
				_gameInput = new WinGameInput();
			}

		}
		//Vector3 CurrentHitPoint;
	float deltaX = 0;
	float deltaY = 0;
	Vector3 lastPosition;
	float lastScale;
		// Update is called once per frame
		void Update ()
		{
		//if (GameManager.Instance.CurStatus != GameManager.Status.START_GAME)
		//				return;
		if (_gameInput.IsClickDown) { 

				GameObject[] objs = GameObject.FindGameObjectsWithTag("UI");
						foreach(GameObject obj in objs)
						{
							if(obj.collider == null )continue;
							Vector3  pt=UICamera.mainCamera.WorldToScreenPoint(obj.transform.position);		
							Debug.Log(pt);
							Vector3 screenPosition = UICamera.mainCamera.WorldToScreenPoint (obj.transform.position);
				Vector3 mScreenPosition=new Vector3 (_gameInput.MousePosition.x, _gameInput.MousePosition.y, screenPosition.z);
							BoxCollider mycollider =(BoxCollider)obj.collider;
							Rect rect = new Rect(pt.x -mycollider.extents.x, pt.y- mycollider.extents.y
				                     , pt.x + mycollider.extents.x, pt.y+ mycollider.extents.y);
				if(pt.x -mycollider.extents.x <=mScreenPosition.x 
				   && mScreenPosition.x <= pt.x + mycollider.extents.x
				   && pt.y - mycollider.extents.y <= mScreenPosition.y 
				   && mScreenPosition.y <= pt.y + mycollider.extents.y)
								return;

						}
						RaycastHit hit;	
			if (Physics.Raycast (Camera.main.ScreenPointToRay (_gameInput.MousePosition), out hit, 30f)) {
								print (hit.collider.name);     
								Vector3 CurrentHitPoint = hit.point; 
		//		SkillManager.Instance.FireCommand( hit.collider.gameObject.name, CurrentHitPoint);
			}

		}

	}
	float lastMoveSpeedX = 0.0f;
	float lastMoveSpeedY = 0.0f;
	float ax = 0.0f;
	float ay = 0.0f;
	bool isTwing =false;
	void LateMove()
	{
		if (Mathf.Abs (lastMoveSpeedX) - 0.0f < WinGameInput.EdgeWidth) 
		{
			lastMoveSpeedX = 0.0f;
						ax = 0.0f;
			isTwing = false;
		}
		if (Mathf.Abs (lastMoveSpeedY) - 0.0f <WinGameInput.EdgeWidth) 
		{
			lastMoveSpeedY = 0.0f;
						ay = 0.0f;
			isTwing = false;
		}
		if (ax != 0.0f) {
			if(lastMoveSpeedX < 0)
				lastMoveSpeedX += ax*Time.deltaTime;
			else
				lastMoveSpeedX -= ax*Time.deltaTime;
			
			if(Camera.main.transform.localPosition.x > WinGameInput.EdgeLeftX && Camera.main.transform.localPosition.x < WinGameInput.EdgeRightX)
				Camera.main.transform.Translate(lastMoveSpeedX,0,0);
			else
			{
				lastMoveSpeedX = 0.0f;
				isTwing = false;
			}
		} else if(ay != 0.0f){
			if(lastMoveSpeedY < 0)
				lastMoveSpeedY += ay*Time.deltaTime;
			else
				lastMoveSpeedY -= ay*Time.deltaTime;

			if(Camera.main.transform.localPosition.y > WinGameInput.EdgeDownY && Camera.main.transform.localPosition.y < WinGameInput.EdgeUpY)
				Camera.main.transform.Translate(0,lastMoveSpeedY,0);
			else
			{
				lastMoveSpeedY = 0.0f;
				isTwing = false;
			}
		}
	}
	void LateUpdate()
	{
		//if (GameManager.Instance.CurStatus != GameManager.Status.START_GAME)
		//	return;

		if (_gameInput.IsClickDown) { 
			if (!PlatformUtil.isTouchDevice)
				lastPosition = _gameInput.MousePosition;


		}
		if (_gameInput.IsClickUp) {
				if (!PlatformUtil.isTouchDevice)
					lastPosition = _gameInput.MousePosition;

				else
				{
				ax= (Mathf.Abs( WinGameInput.CameraMoveSpeed) - 0.0f)/1.0f;
				ay= (Mathf.Abs( WinGameInput.CameraMoveSpeed) - 0.0f)/1.0f;
				isTwing = true;
				}
		
		}
		LateMove ();
		if (_gameInput.IsMove) {
			if (!PlatformUtil.isTouchDevice) {
						deltaX = -(_gameInput.MousePosition - lastPosition).x;
						deltaY = -(_gameInput.MousePosition - lastPosition).y;
						float realDeltaX = 0;
						float realDeltaY = 0;
						if (Mathf.Abs (deltaX) > 9.99999944E-11f)
								realDeltaX = deltaX;
						if (Mathf.Abs (deltaY) > 9.99999944E-11f)
								realDeltaY = deltaY;
			
						Vector3 newPos = new Vector3 (Camera.main.transform.position.x + realDeltaX * xSpeed * 0.02f
			                                            , Camera.main.transform.position.y + realDeltaY * ySpeed * 0.02f
			                                            , Camera.main.transform.position.z);
				if (!(newPos.x > WinGameInput.EdgeRightX || newPos.x < WinGameInput.EdgeLeftX || newPos.y <  WinGameInput.EdgeDownY|| newPos.y > WinGameInput.EdgeUpY))
						Camera.main.transform.position = newPos;
						lastPosition = _gameInput.MousePosition;
			

					} else {
				if(!isTwing ) 
							{
										if(Mathf.Abs( Input.GetTouch(0).deltaPosition.x )> Mathf.Abs(Input.GetTouch(0).deltaPosition.y))
										{
											lastMoveSpeedY = 0.0f;
						//									if(!(Camera.main.transform.localPosition.x > WinGameInput.EdgeRightX || Camera.main.transform.localPosition.x < WinGameInput.EdgeLeftX))
						//									{
														float deltaX=Mathf.Abs( Input.GetTouch(0).deltaPosition.x*0.2f);
														
																if(Input.GetTouch(0).deltaPosition.x < 0 && Camera.main.transform.localPosition.x < WinGameInput.EdgeRightX)
											{

												lastMoveSpeedX = WinGameInput.CameraMoveSpeed*deltaX;
												Camera.main.transform.Translate(lastMoveSpeedX,0,0);
											}
																else if(Camera.main.transform.localPosition.x > WinGameInput.EdgeLeftX)
											{
												

												lastMoveSpeedX = -WinGameInput.CameraMoveSpeed*deltaX;
												Camera.main.transform.Translate(lastMoveSpeedX,0,0);
											}
														//	}
																 
										}
										else
										{
											
											lastMoveSpeedX = 0.0f;
														//	if(!(Camera.main.transform.localPosition.y < WinGameInput.EdgeUpY || Camera.main.transform.localPosition.y > WinGameInput.EdgeDownY))
															//{
												float deltaY=Mathf.Abs( Input.GetTouch(0).deltaPosition.y*0.2f);
											if(Input.GetTouch(0).deltaPosition.y > 0 && Camera.main.transform.localPosition.y > WinGameInput.EdgeDownY)
											{
												lastMoveSpeedY = -WinGameInput.CameraMoveSpeed*deltaY;
												Camera.main.transform.Translate(0,lastMoveSpeedY,0); 
											}
											else if(Camera.main.transform.localPosition.y < WinGameInput.EdgeUpY)
											{
												lastMoveSpeedY = WinGameInput.CameraMoveSpeed*deltaY;
												Camera.main.transform.Translate(0,WinGameInput.CameraMoveSpeed*deltaY,0);
											}
															//}	
										}
									
							
			//							if (!(newPos.x > WinGameInput.EdgeRightX || newPos.x < WinGameInput.EdgeLeftX || newPos.y < WinGameInput.EdgeUpY || newPos.y > WinGameInput.EdgeDownY))
			//									Camera.main.transform.position = newPos;
								} 
						}
				} 
		if (_gameInput.TouchCount > 1) 
		{
//			if( Input.GetTouch(1).phase == TouchPhase.Began )
//			{
//				lastScale = Vector3.Distance(Input.GetTouch(0).position,  Input.GetTouch(1).position);
//			}
//			if(Input.GetTouch(0).phase == TouchPhase.Ended)
//			{
//				lastScale = Vector3.Distance(Input.GetTouch(0).position,  Input.GetTouch(1).position);
//			}
			if(Input.GetTouch(0).phase == TouchPhase.Moved 
			   && Input.GetTouch(1).phase == TouchPhase.Moved )
			{
				Vector2 curDist = Input.GetTouch(0).position - Input.GetTouch(1).position; //current distance between finger touches
				Vector2 prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition)); //difference in previous locations using delta positions
				float touchDelta = curDist.magnitude - prevDist.magnitude;
				
				
				
				if ((touchDelta > 0))  //Out {}
				{
					//*touchDelta
					if(Camera.main.orthographicSize -0.5F >5.0f
					   && Camera.main.orthographicSize -0.5F <12.0f)
						Camera.main.orthographicSize -=0.5F; 
				}
					
					if ((touchDelta < 0)) //In {}
				{
					//*(-touchDelta)
					if(Camera.main.orthographicSize +0.5F >5.0f
					   && Camera.main.orthographicSize +0.5F <12.0f)
						Camera.main.orthographicSize+=0.5F; 
				}
//				//Debug.Log("====================>distance: " + dis+"<====================");
//				dis = Mathf.Clamp(dis, 5.0f, 8.0f);
//				Camera.main.orthographicSize = 8.0f - dis ;
			}
		}
	}
*/
}

