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

    public static Quaternion RotateTowards(Transform from, Vector3 to, float turnSpeed)
    {
        Quaternion _lookRotation = Quaternion.LookRotation((to - from.position).normalized);

        return Quaternion.Slerp(from.rotation, _lookRotation, Time.deltaTime * turnSpeed);
    }

    public static bool IsRightOfVector(Vector3 center, Vector3 direction, Vector3 target)
    {
        Vector3 vectorB = center + direction;

        float result = (target.x - center.x) * (vectorB.z - center.z) - (target.z - center.z) * (vectorB.x - center.x);

        return result >= 0;
    }
}

