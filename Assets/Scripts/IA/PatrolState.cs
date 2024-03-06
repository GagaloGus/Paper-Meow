using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "(S) Patrol", menuName = "ScriptableObjects/States/Patrol (S)")]

public class PatrolState : State
{
    [Range(0f, 60f)]
    public float minSwapTime, maxSwapTime;

    public Texture2D nothingTexture2D;
        
    float count = 0, maxTime;
    int pointInt = 0;

    List<Vector3> patrolPoints = new();
    public override void StartState(GameObject owner)
    {
        base.StartState(owner);
        base.ChangeTextureQuestionPlane(owner, nothingTexture2D);

        //coje los patrolpoints del monobehaviour
        patrolPoints = owner.GetComponent<PatrolPoints>().get_patrolpointList;
        //randomiza el tiempo para cambiar de punto 
        maxTime = Random.Range(minSwapTime, maxSwapTime);

        //animator.SetBool("isInvestigating", false);
    }

    public override State Run(GameObject owner)
    {
        //espera hasta que llegue a su destino para iniciar el contador
        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            count+= Time.deltaTime;
            if(count >= maxTime)
            {
                //le suma uno al contador del array
                pointInt++;

                //Resetea el conteo de patrol a 0
                pointInt %= patrolPoints.Count;

                //reinicia el contador
                count = 0;
                //randomiza el tiempo para cambiar de punto otra vez
                maxTime = Random.Range(minSwapTime, maxSwapTime);
            }

            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        navMeshAgent.SetDestination(patrolPoints[pointInt]);

        return base.Run(owner);

    }

    public override void DrawStateGizmo(GameObject owner)
    {
        for(int i = 0; i < patrolPoints.Count; i++)
        {
            if (i == patrolPoints.Count - 1)
            {
                Gizmos.DrawRay(patrolPoints[i], patrolPoints[0] - patrolPoints[i]);
            }
            else
            {
                Gizmos.DrawRay(patrolPoints[i], patrolPoints[i + 1] - patrolPoints[i]);
            }
        }
    }
}
