using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnterHouse : MonoBehaviour
{
    public GameObject DoorTP, LastDoorTP;
    private GameObject Player; /*LDTPparent, LDTPSons, TPparent, TPsons*/
    private bool InRange = false;
    public bool isInside;
    //private GameObject[] OutDoors, InDoors;
    void Start()
    {
        Player = FindAnyObjectByType<SkoController>().gameObject;
        isInside = gameObject.name.Contains("Enter");
        //OutDoors = new GameObject[3];
        //InDoors = new GameObject[3];
    }

    void Update()
    {
        if(InRange && Input.GetKeyDown(KeyCode.E) && isInside)
        {
            LastDoorTP = gameObject;
            /*LDTPparent = LastDoorTP.transform.parent.gameObject;
            LDTPSons = LDTPparent.GetComponentInChildren<EnterHouse>().gameObject;
            LDTPSons.GetComponent<EnterHouse>().LastDoorTP = LDTPSons.gameObject;
            TPparent = DoorTP.transform.parent.gameObject;
            TPsons = TPparent.GetComponentInChildren<EnterHouse>().gameObject;
            for (int i = 0; i < OutDoors.Length; i++)
            {
                OutDoors[i] = TPsons;
            }
            InDoors[i] = LDTPSons;
            OutDoors[i].GetComponent<EnterHouse>().DoorTP = InDoors[i].GetComponent<EnterHouse>().LastDoorTP;*/
            DoorTP.gameObject.GetComponent<EnterHouse>().DoorTP = LastDoorTP;
            Player.transform.position = DoorTP.transform.position;
            InRange = false;
            GameEventsManager.instance.playerEvents.HouseEnterChange(isInside);
        }
        else if(InRange && Input.GetKeyDown(KeyCode.E) && !isInside)
        {
            Player.transform.position = DoorTP.transform.position;
            GameEventsManager.instance.playerEvents.HouseEnterChange(isInside);
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
