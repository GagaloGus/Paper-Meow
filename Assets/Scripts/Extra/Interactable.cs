using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] bool isInteractable;
    [SerializeField] float Distance;
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
        //si esta dentro del rango devuelve true
        playerInRange = Vector3.Distance(transform.position, player.transform.position) <= Distance;

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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Distance);
    }
}
