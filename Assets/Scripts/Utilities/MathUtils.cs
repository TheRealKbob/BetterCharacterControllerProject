using UnityEngine;
using System.Collections;

public class MathUtils {

	//Projects a vector onto a plane. The output is not normalized.
    public static Vector3 ProjectVectorOnPlane(Vector3 planeNormal, Vector3 vector)
    {

        return vector - (Vector3.Dot(vector, planeNormal) * planeNormal);
    }

}
