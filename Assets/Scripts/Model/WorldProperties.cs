using UnityEngine;
using System.Collections;

public class WorldProperties : MonoBehaviour {

	private static WorldProperties instance;
	public static WorldProperties Instance{ get{ return instance; } private set{ instance = value; } }

	[SerializeField]
	private float gravity = 25.0f;
	public float Gravity{ get{ return gravity; } }

	// Use this for initialization
	void Awake () {
		instance = this;
	}

}
