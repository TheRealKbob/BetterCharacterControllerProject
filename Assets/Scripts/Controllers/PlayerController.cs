using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public delegate void PlayerControllerEvent( string eventID );
	public PlayerControllerEvent OnControllerEvent{ get; set; }

	private Vector3 moveDirection = Vector3.zero;
	private float gravity;

	private CollisionSphere collisionSphere;

	private GroundController groundController;

	public float CollisionOffset;
	public float Radius = 0.5f;
	public LayerMask Walkable;

	public Vector3 Up{ get{ return transform.up; } }
	public Vector3 Down{ get{ return -transform.up; } }

	private const int MaxPushbackIterations = 2;

	void Awake () {
		
		groundController = new GroundController( Walkable, this );

	}

	void Start () {
		gravity = WorldProperties.Instance.Gravity;
		collisionSphere = new CollisionSphere( transform.position, CollisionOffset );
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
		recursivePushback();
	}

	private void recursivePushback()
	{
		recursivePushback( 0, MaxPushbackIterations );
	}

	private void recursivePushback( int depth, int maxDepth )
	{
		bool contact = false;
		foreach( Collider c in Physics.OverlapSphere( transform.position, Radius, Walkable ) )
		{
			if( c.isTrigger )
				continue;

			Vector3 position = collisionSphere.Position;
			Vector3 contactPoint = CollisionUtils.ClosestPointOnSurface( c, position, Radius );

			if (contactPoint != Vector3.zero)
            {
            	DebugDraw.DrawMarker(contactPoint, 2.0f, Color.cyan, 0.0f, false);

            	Vector3 v = contactPoint - position;
            	if( v != Vector3.zero )
            	{
            		int layer = c.gameObject.layer;
            	}

            }
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

	void OnDrawGizmos()
	{
		if( collisionSphere != null )
			Gizmos.DrawWireSphere (collisionSphere.Position, Radius);
	}
}

public class PlayerControllerEvents
{
	public static string ENTER_GROUND = "ENTER_GROUND";
}
