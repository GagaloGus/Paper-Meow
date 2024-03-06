using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/CloseAction")]
public class CloseAction : Action
{
    public float objectiveDistance; // Distancia para considerar que está cerca


    public override bool Check(GameObject owner)
    {


        //Verifica si se encontró el componente Player
        if (target != null)
        {
            //Obtiene la posición del jugador y del dueño de esta acción
            Vector3 playerPosition = target.transform.position;
            Vector3 ownerPosition = owner.transform.position;

            // Calcula la distancia entre el jugador y el dueño de la acción
            float distance = Vector3.Distance(playerPosition, ownerPosition);
            //Debug.Log(distancia);
            // Verifica si el jugador está lo suficientemente cerca
            return distance <= objectiveDistance;
        }
        else
        {
            Debug.LogError("No se encontró el componente Player en el objeto " + owner.name);
            return false;
        }
    }
    public override void DrawGizmo(GameObject owner)
    {
        base.DrawGizmo(owner);
    }
}

