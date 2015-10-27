using UnityEngine;
using System;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {

	private string updateID = "DoUpdate";

	public InputBindings Input{ get; private set; }

	void Awake () {
		Input = new InputBindings();
	}
	
	void FixedUpdate () {
		gameObject.SendMessage( updateID, SendMessageOptions.DontRequireReceiver);
	}

}
