using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	public string Type = null;
	public Character Born(){
			GameObject startPoint = GameObject.Find ("StartPoint");
		if (Type == "bowman") {
			return GameManager.Instance.SpawnCharacter (CharacterData.CharacterClassType.CHARACTER, CharacterData.CharacterModel.BOWMAN, 0, 1, startPoint.transform.position,
		                                    startPoint.transform.eulerAngles, CharacterStatus.Pose.Run);
		} else if(Type == "gaint") {
			return GameManager.Instance.SpawnCharacter (CharacterData.CharacterClassType.CHARACTER, CharacterData.CharacterModel.GIANT, 0, 1, startPoint.transform.position,
			                                     startPoint.transform.eulerAngles, CharacterStatus.Pose.Run);
		}else if(Type == "viking") {
			return GameManager.Instance.SpawnCharacter (CharacterData.CharacterClassType.CHARACTER, CharacterData.CharacterModel.VIKING, 0, 1, startPoint.transform.position,
			                                     startPoint.transform.eulerAngles, CharacterStatus.Pose.Run);
		}
		return null;
	}
}
