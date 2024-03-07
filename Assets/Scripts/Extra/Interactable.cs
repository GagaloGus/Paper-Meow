using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public bool isInteractable, needsToClickToInteract;
    [SerializeField] float Distance;
    [SerializeField] Vector3 modifyCentre;

    [Header("Visual Que")]
    [SerializeField] Image visualQue;
    public Color visualQueColor;

    Vector3 newCentre;
    KeyCode interactKey;

    [Header("Events")]
    [SerializeField] UnityEvent interactEvent;

    bool playerInRange;
    GameObject player;

    private void Awake()
    {
        player = FindObjectOfType<SkoController>().gameObject;
        interactKey = PlayerKeybinds.interact;

        visualQue = transform.Find("Canvas").Find("visualQue").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        newCentre = transform.position + CoolFunctions.VectorMoveAlongTransformAxis(modifyCentre, transform);
        //si esta dentro del rango devuelve true
        playerInRange = Vector3.Distance(newCentre, player.transform.position) <= Distance;

        //Si estamos dentro del rango y el objeto es interactuable
        if(playerInRange && isInteractable && player.GetComponent<SkoController>().player_canMove) 
        {
            visualQue.transform.forward = -Camera.main.transform.forward;
            visualQueColor.a = 1;

            //Si le damos a la tecla de interactuar
            if ((Input.GetKeyDown(interactKey) && needsToClickToInteract) || !needsToClickToInteract)
            {
                interactEvent.Invoke();
                GetComponent<Interactable>().enabled = false;
                visualQueColor.a = 0;
            }
        }
        else
        {
            visualQueColor.a = 0;
        }

        visualQue.color = visualQueColor;
    }

    private void OnDrawGizmos()
    {
        newCentre = transform.position + CoolFunctions.VectorMoveAlongTransformAxis(modifyCentre, transform);

        Gizmos.color = isInteractable ? Color.yellow : Color.grey;
        Gizmos.DrawWireSphere(newCentre, Distance);
    }
}
