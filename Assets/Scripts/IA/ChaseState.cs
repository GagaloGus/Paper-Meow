using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/Chase (S)")]
public class ChaseState : State
{
    public override void StartState(GameObject owner)
    {
        base.StartState(owner);

        animator.SetBool("isWalking", true);
        animator.SetBool("goingBackwards", false);
    }

    public override State Run(GameObject owner)
    {
        navMeshAgent.SetDestination(target.transform.position);

        //persigue al player mientras le mira
        owner.transform.LookAt(target.transform.position);

        return base.Run(owner);
    }
}
