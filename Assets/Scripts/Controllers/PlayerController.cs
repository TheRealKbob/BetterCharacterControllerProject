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

	private const int MAX_PUSHBACK_DEPTH = 2;

	void Awake () {		
		groundController = new GroundController( Walkable, this );
	}

	void Start () {
		gravity = WorldProperties.Instance.Gravity;
		collisionSphere = new CollisionSphere( transform, CollisionOffset );
	}
	
	void DoUpdate () {
		
		groundController.Probe( collisionSphere.Position );
		ApplyMoveVector();
		recursivePushBack( 0, MAX_PUSHBACK_DEPTH );

	}

	public void ApplyMoveVector()
	{
		transform.position += moveDirection * Time.deltaTime;
	}

	/// Applies gravity to the movement vector
	public void Fall()
	{

		if( groundController.IsGrounded( false, 0.1f ) )
		{
			if( OnControllerEvent != null )
			{
				Debug.Log("Found Ground");
				moveDirection = MathUtils.ProjectVectorOnPlane( Up, moveDirection );
				OnControllerEvent( PlayerControllerEvents.ENTER_GROUND );
				return;
			}
		}

		moveDirection -= Up * gravity * Time.deltaTime;
	}

	private void recursivePushBack( int depth, int maxDepth )
	{
		bool contact = false;

		foreach( Collider c in Physics.OverlapSphere( collisionSphere.Position, Radius, Walkable ) )
		{
			if( c.isTrigger ) continue;

			/*Vector3 closestPoint = c.ClosestPointOnBounds( collisionSphere.Position );

			DebugDraw.DrawMarker( closestPoint, 2.0f, Color.cyan, 0.0f, false );
			Vector3 v = closestPoint - collisionSphere.Position;
			transform.position += Vector3.ClampMagnitude(v, Mathf.Clamp(Radius - v.magnitude, 0, Radius));
			Debug.Log("Hit Something in Sphere");
			contact = true;*/
		}
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
