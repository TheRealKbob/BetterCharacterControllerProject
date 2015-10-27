using UnityEngine;
using System.Collections;

public class GroundController {

	private LayerMask walkable;
	private PlayerController controller;

	private GroundHit primaryGround;
    private GroundHit nearGround;
    private GroundHit farGround;
    private GroundHit stepGround;
    private GroundHit flushGround;

    public CollisionType collisionType;
    public Transform transform;

	public GroundController( LayerMask walkable, PlayerController controller )
	{
		this.walkable = walkable;
		this.controller = controller;
	}

	public void ProbeGround( Vector3 origin, float tolerance )
	{
		
		reset();

		Vector3 up = controller.Up;
		Vector3 down = -up;

		Vector3 o = origin + ( up * tolerance );

		float smallerRadius = controller.Radius - ( tolerance * tolerance );

		RaycastHit hit;
		if( Physics.SphereCast( o, smallerRadius, down, out hit, Mathf.Infinity, walkable ) )
		{
			var hitcolliderType = hit.collider.gameObject.GetComponent<CollisionType>();
			if( hitcolliderType == null )
				hitcolliderType = hit.collider.gameObject.AddComponent<CollisionType>();

			transform = hit.transform;

			primaryGround = new GroundHit( hit.point, hit.normal, hit.distance );

		}

	}

	public bool IsGrounded( bool currentlyGrounded, float distance )
	{
		Vector3 n;
		return IsGrounded( currentlyGrounded, distance, out n );
	}

	public bool IsGrounded( bool currentlyGrounded, float distance, out Vector3 groundNormal )
	{
		groundNormal = Vector3.zero;

		if( primaryGround == null || primaryGround.distance > distance )
			return false;
		RaycastHit hit;
		if( Physics.SphereCast(controller.transform.position, controller.Radius, controller.Down, out hit, Mathf.Infinity, walkable) )
		{
			Debug.Log("Found it");
			primaryGround = new GroundHit(hit.point, hit.normal, hit.distance);
			return true;
		}

		return false;

	}

	private void reset()
	{
		primaryGround = null;
        nearGround = null;
        farGround = null;
        stepGround = null;
        flushGround = null;
	}

	private class GroundHit
    {
        public Vector3 point { get; private set; }
        public Vector3 normal { get; private set; }
        public float distance { get; private set; }

        public GroundHit(Vector3 point, Vector3 normal, float distance)
        {
            this.point = point;
            this.normal = normal;
            this.distance = distance;
        }
    }

}
