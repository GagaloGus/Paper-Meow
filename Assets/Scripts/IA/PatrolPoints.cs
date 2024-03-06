using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    [SerializeField]
    List<Vector3> patrolPoints = new();
    public int originalPatrolCount, maxNextPatrolPointRange;
    private void Start()
    {
        ////resetea los patrol points
        //while (patrolPoints.Count > originalPatrolCount)
        //{
        //    patrolPoints.RemoveAt(patrolPoints.Count - 1);
        //}

        ////por conveniencia, mas comodo asi
        //originalPatrolCount--;
    }

    //Permite que si el punto a añadir esta en un rango especifico entre sus puntos de patrulla se añada a la lista
    public void AddPatrolPoint(Vector3 newPoint)
    {
        if (CheckIfPointIsInRange(newPoint))
        {
            patrolPoints.Add(newPoint);
        }
    }

    bool CheckIfPointIsInRange(Vector3 newPoint)
    {
        //recorrre toda la lista de patrolpoints para ver si newPoint esta dentro de un rango en especifico acorde a los patrolpoints
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            if (Vector3.Distance(patrolPoints[i], newPoint) <= maxNextPatrolPointRange)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        //dibuja el maximo rango desde cada patrolpoint
        for(int i = 0;i < patrolPoints.Count; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(patrolPoints[i], maxNextPatrolPointRange);
        }
    }

    public List<Vector3> get_patrolpointList
    {
        get { return patrolPoints; }
    }
}
