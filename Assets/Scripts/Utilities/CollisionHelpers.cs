using UnityEngine;
using System;
using System.Collections;

public class CollisionType : MonoBehaviour {

    public float StandAngle = 80.0f;
    public float SlopeLimit = 80.0f;
}

public struct IgnoredCollider
{
    public Collider collider;
    public int layer;

    public IgnoredCollider(Collider collider, int layer)
    {
        this.collider = collider;
        this.layer = layer;
    }
}

[Serializable]
public class CollisionSphere
{
    public float offset;

    public CollisionSphere(float offset)
    {
        this.offset = offset;
    }
}

public struct CollisionData
{
    public CollisionSphere collisionSphere;
    public CollisionType collisionType;
    public GameObject gameObject;
    public Vector3 point;
    public Vector3 normal;
}