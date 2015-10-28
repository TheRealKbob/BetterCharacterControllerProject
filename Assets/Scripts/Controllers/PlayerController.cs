using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public delegate void PlayerControllerEvent( string eventID );
	public PlayerControllerEvent OnControllerEvent{ get; set; }

	private Vector3 moveDirection = Vector3.zero;
	private float gravity;

	private GroundController groundController;

	public float Radius = 0.5f;
	public LayerMask Walkable;

	public Vector3 Up{ get{ return transform.up; } }
	public Vector3 Down{ get{ return -transform.up; } }

	void Awake () {
		
		groundController = new GroundController( Walkable, this );

		currentlyClampedTo = null;

	}

	void Start () {
		gravity = WorldProperties.Instance.Gravity;
	}
	
	void DoUpdate () {
		
		//Phases
		calculateMovementPhase();
		calculatePushbackPhase();
		calculateResolutionPhase();

	}

	private void calculateMovementPhase()
	{

	}

	private void calculatePushbackPhase()
	{
		bool contact = false;
		foreach( Collider c in Physics.OverlapSphere( Transform.position, Radius, Walkable ) )
		{
			if( c.isTrigger )
				continue;
		}
	}

	private void calculateResolutionPhase()
	{

	}

	public void ApplyMoveVector()
	{
		transform.position += moveDirection * Time.deltaTime;
	}

	/// Applies gravity to the movement vector
	public void Fall()
	{
		moveDirection -= Up * gravity * Time.deltaTime;
	}
}

public class PlayerControllerEvents
{
	public static string ENTER_GROUND = "ENTER_GROUND";
}
