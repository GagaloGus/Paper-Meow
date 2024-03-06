using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/OnWayState")]
public class OnWayState : State
{
    public float waypointRadius = 1f;
    public List<Vector3> waypoints;

    private int currentWaypointIndex = 0;

    public override State Run(GameObject owner)
    {
        if (Vector3.Distance(owner.transform.position, waypoints[currentWaypointIndex]) <= waypointRadius)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0; // Reiniciar al principio si se alcanza el final de la lista
            }
        }

        navMeshAgent.SetDestination(waypoints[currentWaypointIndex]);

        return null;
    }

    public override void StartState(GameObject owner)
    {
        navMeshAgent = owner.GetComponent<NavMeshAgent>();
        base.StartState(owner);

        if (waypoints.Count > 0)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex]);
        }
        else
        {
            Debug.LogError("La lista de waypoints está vacía en el estado OnWay. Añade al menos un waypoint.");
        }
    }

    public override void DrawStateGizmo(GameObject owner)
    {
        base.DrawStateGizmo(owner);
    }
}
