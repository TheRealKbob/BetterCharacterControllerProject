using UnityEngine;
using System.Collections;

public class GroundController {

	private LayerMask walkable;
	private PlayerController controller;

    public CollisionType collisionType;
    public Transform transform;

    private GroundHit primaryGround;

	public GroundController( LayerMask walkable, PlayerController controller )
	{
		this.walkable = walkable;
		this.controller = controller;
	}

	public void Probe( Vector3 origin )
	{
		RaycastHit hit;
		if( Physics.SphereCast(origin, controller.Radius, -controller.Up, out hit, Mathf.Infinity, walkable) )
		{
			DebugDraw.DrawMarker( hit.point, 0.25f, Color.red, 0, false );
			primaryGround = new GroundHit( hit.point, hit.normal, hit.distance );
		}
	}

	public bool IsGrounded( bool currentlyGrounded, float distance )
	{
		if( primaryGround == null || primaryGround.distance >= distance )
			return false;
		Debug.Log("Grounded");
		return true;
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
