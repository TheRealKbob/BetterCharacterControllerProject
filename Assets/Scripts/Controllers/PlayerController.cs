using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public delegate void PlayerControllerEvent( string eventID );
	public PlayerControllerEvent OnControllerEvent{ get; set; }

	private Vector3 moveDirection = Vector3.zero;
	private float gravity;

	private List<CollisionData> collisionData;

	private GroundController groundController;

	private bool clamping = true;
	private Transform currentlyClampedTo;

	public float Radius = 0.5f;
	public LayerMask Walkable;

	public Vector3 Up{ get{ return transform.up; } }
	public Vector3 Down{ get{ return -transform.up; } }

	private static float Tolerance = 0.05f;
    private static float TinyTolerance = 0.01f;

    private const int MaxPushbackIterations = 2;

	void Awake () {
		collisionData = new List<CollisionData>();
		groundController = new GroundController( Walkable, this );

		currentlyClampedTo = null;

	}

	void Start () {
		gravity = WorldProperties.Instance.Gravity;
	}
	
	void DoUpdate () {
		
		groundController.ProbeGround( transform.position, Tolerance );

		recursivePushback( 0, MaxPushbackIterations );

	}

	private void recursivePushback( int depth, int maxDepth )
	{
		bool contact = false;

		foreach( Collider col in Physics.OverlapSphere(transform.position, Radius, Walkable) )
		{

		}
	}

	public void ApplyMoveVector()
	{
		transform.position += moveDirection * Time.deltaTime;
	}

	public void ApplyGravity()
	{
		if( groundController.IsGrounded( false, 0.01f ) )
		{
			moveDirection = MathUtils.ProjectVectorOnPlane(Up, moveDirection);
			if( OnControllerEvent != null )
				OnControllerEvent( PlayerControllerEvents.ENTER_GROUND );
			return;
		}

		moveDirection -= Up * gravity * Time.deltaTime;
	}
}

public class PlayerControllerEvents
{
	public static string ENTER_GROUND = "ENTER_GROUND";
}
