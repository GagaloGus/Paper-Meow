using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    protected GameObject target;
    public virtual void StartAction()
    {
        
    }

    public abstract bool Check(GameObject owner);

    public virtual void DrawGizmo(GameObject owner) 
    {
        
    }

    public GameObject set_target
    {
        set { target = value; }
    }
}
