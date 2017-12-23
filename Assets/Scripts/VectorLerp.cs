using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorLerp  {

    public static Vector2 vector_lerp(Vector2 start_point, Vector2 end_point, float rate) {
        float d = (start_point - end_point).magnitude;

        if (d < 0.1) {
            Debug.Log("A");
            return end_point;
        }
        else {
            return Vector2.Lerp(start_point, end_point, rate);
        }
    }

}
