using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnterHouse : MonoBehaviour
{
    public GameObject DoorTP;
    private GameObject Player, Camera;
    private CinemachineFreeLook CFK;
    private bool InRange = false;
    public bool IsInside;
    void Start()
    {
        Player = FindAnyObjectByType<SkoController>().gameObject;
        Camera = FindAnyObjectByType<CinemachineFreeLook>().gameObject;
        CFK = Camera.GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        if(InRange && Input.GetKeyDown(KeyCode.E))
        {
            Player.transform.position = DoorTP.transform.position;
            InRange = false;
            if (IsInside)
            {
                CFK.m_YAxis.m_SpeedMode = 0;
            }
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
