using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public enum Type { LookAtCamera, Forward, Nothing}

    public Type LookType;
    // Update is called once per frame
    void Update()
    {
        if(LookType == Type.LookAtCamera) { transform.LookAt(Camera.main.transform.position); }
        else if(LookType == Type.Forward) { transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized * -1; }
        else if(LookType == Type.Nothing) { }
        
    }
}
