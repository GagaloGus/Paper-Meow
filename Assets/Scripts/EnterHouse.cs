using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnterHouse : MonoBehaviour
{
    public GameObject DoorTP, LastDoorTP;
    private GameObject Player;
    private bool InRange = false;
    public bool isInside;
    void Start()
    {
        Player = FindAnyObjectByType<SkoController>().gameObject;
        isInside = gameObject.name.Contains("Enter");
    }

    void Update()
    {
        if(InRange && Input.GetKeyDown(KeyCode.E) && isInside)
        {
            LastDoorTP = gameObject;
            DoorTP.gameObject.GetComponent<EnterHouse>().DoorTP = LastDoorTP;
            Player.transform.position = DoorTP.transform.position;
            InRange = false;
            GameEventsManager.instance.playerEvents.HouseEnterChange(isInside);
        }
        else if(InRange && Input.GetKeyDown(KeyCode.E) && !isInside)
        {
            Player.transform.position = DoorTP.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SkoController>())
        {
            InRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SkoController>())
        {
            InRange = false;
        }
    }
}
