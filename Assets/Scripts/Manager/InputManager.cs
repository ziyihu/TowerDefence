using UnityEngine;
using System.Collections;

public class InputManager : UnitySceneSingleton<InputManager> {

	private static IGameInput _gameInput;
	float xSpeed = 1f;
	float ySpeed = 1f;
	float x = 0.0f;
	float y = 0.0f;

	private static InputManager instance;
	public static void Init(){
		instance = InputManager.Instance;
	}

	// Use this for initialization
	void Start () {
		if (PlatformUtil.isTouchDevice) {
			_gameInput = new SingleTouchGameInput();
		} else {
			_gameInput = new WinGameInput();
		}
	}

	float deltaX = 0;
	float deltaY = 0;
	Vector3 lastPosition;
	float lastScale;

	// Update is called once per frame
	void Update () {
		//exclude UI

	}
	//move speed
	float lastMoveSpeedX = 0.0f;
	float lastMoveSpeedY = 0.0f;
	//add speed
	float ax = 0.0f;
	float ay = 0.0f;
	//is the moving dragging(more real)
	bool isTwing = false;

	void LateMove(){
		if (Mathf.Abs (lastMoveSpeedX) - 0.0f < WinGameInput.EdgeWidth) {
			lastMoveSpeedX = 0.0f;
			ax = 0.0f;
			isTwing = false;
		}
		if (Mathf.Abs (lastMoveSpeedY) - 0.0f < WinGameInput.EdgeWidth) {
			lastMoveSpeedY = 0.0f;
			ay = 0.0f;
			isTwing = false;
		}
		if (ax != 0.0f) {
			if(lastMoveSpeedX < 0){
				lastMoveSpeedX += ax * Time.deltaTime;
			} else {
				lastMoveSpeedX -= ax * Time.deltaTime;
			}

			if(Camera.main.transform.localPosition.x > WinGameInput.EdgeLeftX && Camera.main.transform.localPosition.x < WinGameInput.EdgeRightX){
				Camera.main.transform.Translate(lastMoveSpeedX,0,0);
			} else {
				lastMoveSpeedX = 0.0f;
				isTwing = false;
			}
		} else if(ay != 0.0f){
			if(lastMoveSpeedY < 0){
				lastMoveSpeedY += ay * Time.deltaTime;
			} else {
				lastMoveSpeedY -= ay * Time.deltaTime;
			}
			
			if(Camera.main.transform.localPosition.y > WinGameInput.EdgeDownY && Camera.main.transform.localPosition.y < WinGameInput.EdgeUpY){
				Camera.main.transform.Translate(0,lastMoveSpeedY,0);
			} else {
				lastMoveSpeedY = 0.0f;
				isTwing = false;
			}
		}
	}

	//Unity invoke this function after the Update() function
	void LateUpdate(){
		//Debug.Log ("hello1");
		if (_gameInput.IsClickDown) {
			if(!PlatformUtil.isTouchDevice){
				lastPosition = _gameInput.MousePosition;
			} else {
				ax = (Mathf.Abs(WinGameInput.CameraMoveSpeed)-0.0f)/1.0f;
				ay = (Mathf.Abs(WinGameInput.CameraMoveSpeed)-0.0f)/1.0f;
				isTwing = true;
			}
		}
		//twing
		LateMove ();
		if (_gameInput.IsMove) {
			if(!PlatformUtil.isTouchDevice){
				deltaX = -(_gameInput.MousePosition-lastPosition).x;
				deltaY = -(_gameInput.MousePosition-lastPosition).y;
				float realDeltaX = 0;
				float realDeltaY = 0;
				if(Mathf.Abs(deltaX) > 9.99999944E-11f){
					realDeltaX = deltaX;
				}
				if(Mathf.Abs(deltaY) > 9.99999944E-11f){
					realDeltaY = deltaY;
				}
				Vector3 newPos = new Vector3(Camera.main.transform.position.x + realDeltaX*xSpeed*0.2f,
				                             Camera.main.transform.position.y + realDeltaY*ySpeed*0.2f,
				                             Camera.main.transform.position.z);
				Debug.Log(newPos.x);
				Debug.Log(newPos.y);
				if((newPos.x > WinGameInput.EdgeRightX || newPos.x < WinGameInput.EdgeLeftX ||
				     newPos.y < WinGameInput.EdgeDownY || newPos.y > WinGameInput.EdgeUpY)){
					Debug.Log("hello2");
					Camera.main.transform.Translate(realDeltaX*xSpeed,realDeltaY*ySpeed,0);
				}  
				lastPosition = _gameInput.MousePosition;
			}
			else {
				if(!isTwing){
					//avoid shaking
					if(Mathf.Abs(Input.GetTouch(0).deltaPosition.x) > Mathf.Abs(Input.GetTouch(0).deltaPosition.y)){
						lastMoveSpeedY = 0.0f;
						float deltaX = Mathf.Abs(Input.GetTouch(0).deltaPosition.x*0.2f);
						if(Input.GetTouch(0).deltaPosition.x < 0 ){
						   //&& Camera.main.transform.localPosition.x < WinGameInput.EdgeRightX) {
							lastMoveSpeedX = WinGameInput.CameraMoveSpeed*deltaX;
							Camera.main.transform.Translate(lastMoveSpeedX, 0,0);
						}
						else if(Camera.main.transform.localPosition.x > WinGameInput.EdgeLeftX){
							lastMoveSpeedX = -WinGameInput.CameraMoveSpeed * deltaX;
							Camera.main.transform.Translate(lastMoveSpeedX,0,0);
						}
					} else {
						lastMoveSpeedX = 0.0f;
						float deltaY = Mathf.Abs(Input.GetTouch(0).deltaPosition.y*0.2f);
						if(Input.GetTouch(0).deltaPosition.y < 0 && Camera.main.transform.localPosition.y < WinGameInput.EdgeDownY) {
							lastMoveSpeedY = -WinGameInput.CameraMoveSpeed*deltaY;
							Camera.main.transform.Translate(lastMoveSpeedY, 0,0);
						}
						else if(Camera.main.transform.localPosition.y > WinGameInput.EdgeUpY){
							lastMoveSpeedY = WinGameInput.CameraMoveSpeed * deltaY;
							Camera.main.transform.Translate(lastMoveSpeedY,0,0);
						}
					}
				}

				//zoom in
				if(_gameInput.TouchCount > 1){
					if(Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).phase == TouchPhase.Moved){
						Vector2 curDist = Input.GetTouch(0).position - Input.GetTouch(1).position;
						Vector2 preDist = (Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) -
							(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);
						float touchDelta = curDist.magnitude - preDist.magnitude;
						if(touchDelta > 0){
							if((Camera.main.orthographicSize - 0.5F) > 5.0f
							   && Camera.main.orthographicSize - 0.5F < 12.0f){
								Camera.main.orthographicSize += 0.5F;
							}
						} 
						if(touchDelta < 0){
							if((Camera.main.orthographicSize + 0.5F) > 5.0f
							   && Camera.main.orthographicSize + 0.5F < 12.0f){
								Camera.main.orthographicSize += 0.5F;
							}
						}
					}
				}
			}
		}
	}
}
