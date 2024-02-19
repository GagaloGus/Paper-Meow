using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoolFunctions
{
    public static float MapValues(float value, float leftMin, float leftMax, float rightMin, float rightMax)
    {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }

    public static Vector3 FlattenVector3(Vector3 value, float newYValue = 0)
    {
        value.y = newYValue;
        return value;
    }

    public static bool IsRightOfVector(Vector3 center, Vector3 direction, Vector3 target)
    {
        Vector3 vectorB = center + direction;

        float result = (target.x - center.x) * (vectorB.z - center.z) - (target.z - center.z) * (vectorB.x - center.x);

        return result >= 0;
    }

    public static Vector3 MultipyVectorValues(Vector3 v1, Vector3 v2)
    {
        Vector3 newVector = new Vector3(
            v1.x * v2.x,
            v1.y * v2.y,
            v1.z * v2.z
            );
        
        return newVector;
    }

    public static Vector3 VectorMoveAlongTransformAxis(Vector3 v1, Transform axis)
    {
        Vector3 newVector = v1.x * axis.right + v1.y * axis.up + v1.z * axis.forward;

        return newVector;
    }
}

