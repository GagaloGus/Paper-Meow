using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHouse : MonoBehaviour
{
    public GameObject HouseOut, HouseIn;
    public bool InRange = false, AlreadyIn;

    void Start()
    {
        
    }

    void Update()
    {
        if(InRange && !AlreadyIn)
        {
            FindAnyObjectByType<SkoController>().transform.position = HouseIn.transform.position;
            InRange = false;
        }
        else if(InRange && AlreadyIn)
        {
            FindAnyObjectByType<SkoController>().transform.position = HouseOut.transform.position + new Vector3(3, 0, 0);
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
}
