using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	private string updateID = "DoUpdate";
	
	void Start () {
	
	}
	
	void LateUpdate () {
		gameObject.SendMessage( updateID, SendMessageOptions.DontRequireReceiver);
	}
}
