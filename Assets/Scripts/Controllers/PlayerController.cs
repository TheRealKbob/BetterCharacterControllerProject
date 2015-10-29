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
	private const string TemporaryLayer = "TempCast";
	private int TemporaryLayerIndex;
	public const float Tolerance = 0.05f;
    public const float TinyTolerance = 0.01f;

	void Awake () {
		TemporaryLayerIndex = LayerMask.NameToLayer(TemporaryLayer);
		groundController = new GroundController( Walkable, this );
	}

	void Start () {
		gravity = WorldProperties.Instance.Gravity;
		collisionSphere = new CollisionSphere( transform.position, CollisionOffset );
	}
	
	void DoUpdate () {
		
		

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
