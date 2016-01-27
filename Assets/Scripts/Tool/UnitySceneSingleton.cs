using UnityEngine;
using System.Collections;
//this game object will only be effective when using in the current scene
//if current scene destory, the game object will be destoried
public class UnitySceneSingleton<T> : MonoBehaviour 
where T:Component
{
	private static T _Instance;
	public static T Instance{
		get{
			if(_Instance == null){
				_Instance = FindObjectOfType(typeof(T)) as T;
				if(_Instance == null){
					GameObject obj = new GameObject();
					obj.hideFlags = HideFlags.HideAndDontSave;
					_Instance = obj.AddComponent(typeof(T)) as T;
				}
			}
			return _Instance;
		}
	}

}
