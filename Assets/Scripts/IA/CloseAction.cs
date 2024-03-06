using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/CloseAction")]
public class CloseAction : Action
{
    public float objectiveDistance; // Distancia para considerar que est� cerca


    public override bool Check(GameObject owner)
    {


        //Verifica si se encontr� el componente Player
        if (target != null)
        {
            //Obtiene la posici�n del jugador y del due�o de esta acci�n
            Vector3 playerPosition = target.transform.position;
            Vector3 ownerPosition = owner.transform.position;

            // Calcula la distancia entre el jugador y el due�o de la acci�n
            float distance = Vector3.Distance(playerPosition, ownerPosition);
            //Debug.Log(distancia);
            // Verifica si el jugador est� lo suficientemente cerca
            return distance <= objectiveDistance;
        }
        else
        {
            Debug.LogError("No se encontr� el componente Player en el objeto " + owner.name);
            return false;
        }
    }
    public override void DrawGizmo(GameObject owner)
    {
        base.DrawGizmo(owner);
    }
}

