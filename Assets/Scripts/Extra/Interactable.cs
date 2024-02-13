using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] bool isInteractable;
    [SerializeField] float Distance;
    [SerializeField] Vector3 modifyCentre;

    Vector3 newCentre;
    KeyCode interactKey;
    [SerializeField] UnityEvent interactEvent;

    bool playerInRange;
    GameObject player;

    GameObject KeyToPress;

    private void Awake()
    {
        player = FindObjectOfType<SkoController>().gameObject;

        KeyToPress = GameObject.FindGameObjectWithTag("PressKeyCanvas");

        interactKey = PlayerKeybinds.interact;
        KeyToPress.GetComponentInChildren<TMP_Text>().text = interactKey.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        newCentre = transform.position + CoolFunctions.VectorMoveAlongTransformAxis(modifyCentre, transform);
        //si esta dentro del rango devuelve true
        playerInRange = Vector3.Distance(newCentre, player.transform.position) <= Distance;

        //Si estamos dentro del rango y el objeto es interactuable
        if(playerInRange && isInteractable) 
        {
            //Si le damos a la tecla de interactuar
            if (Input.GetKeyDown(interactKey))
            {
                KeyToPress.SetActive(false);

                interactEvent.Invoke();
                GetComponent<Interactable>().enabled = false;
            }
            else
            {
                KeyToPress.SetActive(true);
            }
        }
        else
        {
            KeyToPress.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        //newCentre = transform.position + CoolFunctions.MultipyVectorValues(modifyCentre, transform.forward);

        newCentre = transform.position + CoolFunctions.VectorMoveAlongTransformAxis(modifyCentre, transform);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(newCentre, Distance);
    }
}
