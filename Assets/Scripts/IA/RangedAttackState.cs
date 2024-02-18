using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "States/Ranged Atk")]
public class RangedAttackState : State
{
    public float maxShootTime;
    float currentTime;
    public override void StartState(GameObject owner)
    {
        base.StartState(owner);
        currentTime = 0;

        //se mueve algo para evitar errores
        //Vector3 newPos = (target.transform.position - owner.transform.position).normalized * 2 + owner.transform.position;
        navMeshAgent.SetDestination(owner.transform.position);

        animator.SetBool("isWalking", false);
    }
    public override State Run(GameObject owner)
    {
        currentTime += Time.deltaTime;
        if (currentTime > maxShootTime)
        {
            animator.SetTrigger("shoot");
            currentTime = 0;
        }

        //se queda quieto mientras apunta al player
        owner.transform.LookAt(target.transform.position);
        return base.Run(owner);
    }
}
