using UnityEngine;
using System.Collections;

public class GroundController {

	private LayerMask walkable;
	private PlayerController controller;

    public CollisionType collisionType;
    public Transform transform;

	public GroundController( LayerMask walkable, PlayerController controller )
	{
		this.walkable = walkable;
		this.controller = controller;
	}

	///
	///
	///
	public void Probe()
	{
		
	}

}
