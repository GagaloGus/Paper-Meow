using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolFunctions : MonoBehaviour
{
    public static float MapValues(float value, float leftMin, float leftMax, float rightMin, float rightMax)
    {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }

    public static Vector3 FlattenVector3(Vector3 value)
    {
        value.y = 0;
        return value;
    }
}
