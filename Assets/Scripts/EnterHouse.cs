using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHouse : MonoBehaviour
{
    public GameObject DoorTP;
    private GameObject Player;
    private bool InRange = false;
    void Start()
    {
        Player = FindAnyObjectByType<SkoController>().gameObject;
    }

    void Update()
    {
        if(InRange && Input.GetKeyDown(KeyCode.E))
        {
            Player.transform.position = DoorTP.transform.position;
            InRange = false;
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
