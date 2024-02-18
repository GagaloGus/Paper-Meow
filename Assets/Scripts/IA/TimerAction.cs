using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;

[CreateAssetMenu(menuName = "Actions/TimerAction")]
public class TimerAction : Action   
{
    public float currentTime;
    public float maxTime;

    public override void StartAction()
    {
     
        currentTime = 0;
 

    }
    public override bool Check(GameObject owner)
    {
        currentTime += Time.deltaTime;

        if ( currentTime >= maxTime)
        {
            currentTime = 0;
            return true;

        }
        return false;
    }
}
