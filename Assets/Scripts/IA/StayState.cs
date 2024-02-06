using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/StayState")]
public class StayState : State
{
    public override void StartState(GameObject owner)
    {
        base.StartState(owner);
    }

    public override State Run(GameObject owner)
    {
        return base.Run(owner);
    }
}