using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnterHouse : MonoBehaviour
{
    public GameObject DoorTP;
    private GameObject Player, Camera;
    private CameraController CC;
    private bool InRange = false;
    public bool isInside;
    void Start()
    {
        Player = FindAnyObjectByType<SkoController>().gameObject;
        Camera = FindAnyObjectByType<CameraController>().gameObject;
        CC = Camera.GetComponent<CameraController>();

        isInside = gameObject.name.Contains("Enter");
    }

    void Update()
    {
        if(InRange && Input.GetKeyDown(KeyCode.E))
        {
            Player.transform.position = DoorTP.transform.position;
            InRange = false;
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
