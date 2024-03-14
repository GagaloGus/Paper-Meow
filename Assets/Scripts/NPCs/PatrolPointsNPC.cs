using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatrolPointsNPC : MonoBehaviour
{
    Transform player; // Referencia al jugador
    public float distanciaMinima = 2f; // Distancia mínima para avanzar al siguiente punto
    public float velocidad;
    public List<PatrolList> puntos; // Lista de puntos que el objeto debe seguir


    private void Start()
    {
        player = FindObjectOfType<SkoController>().transform;
    }

    public void Avanzar(int listNumber, bool isRun)
    {
        StartCoroutine(PatrolPoins(listNumber, isRun));
    }

    IEnumerator PatrolPoins(int listNumber, bool isRun)
    {
        GetComponent<Interactable>().isInteractable = false;
        Vector3[] listaActual = puntos[listNumber].puntos;
        int indiceActual = 0;
        while (listaActual.Length > indiceActual)
        {
            Vector3 direccion = (listaActual[indiceActual] - transform.position).normalized;

            GetComponent<NPCBehaviour>().StopRotationCoroutine();
            GetComponent<NPCBehaviour>().RotateTowardsRotation(Quaternion.LookRotation(direccion));
            GetComponent<Animator>().SetBool("isWalking", true);
            // Mueve el objeto hacia la nueva posición hasta que la posicion entre el y el punto sea menor que 1
            while (Vector3.Distance(transform.position,listaActual[indiceActual]) > 1)
            {
                transform.position += direccion * (velocidad * Time.deltaTime) * (isRun ? 1.75f : 1);
                yield return null;
            }

            direccion = CoolFunctions.FlattenVector3((player.transform.position - transform.position).normalized);
            GetComponent<NPCBehaviour>().RotateTowardsRotation(Quaternion.LookRotation(direccion));
            GetComponent<Animator>().SetBool("isWalking", false);
            //Espera hasta que el player se acerque
            yield return new WaitUntil(() => Vector3.Distance(transform.position, player.position) < distanciaMinima);

            //Incremento
            indiceActual++;
        }

        puntos[listNumber].ReachDestinationEvent?.Invoke();
        GetComponent<Interactable>().isInteractable = true;
        GetComponent<NPCBehaviour>().npcData.originalRot = transform.rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, distanciaMinima);
    }
}

[System.Serializable]
public class PatrolList
{
    public string patrolName;
    public Vector3[] puntos;
    public UnityEvent ReachDestinationEvent;
}
