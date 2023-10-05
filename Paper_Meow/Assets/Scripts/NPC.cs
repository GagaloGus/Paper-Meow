using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject PressE;

    public void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<player>())
        {
            PressEON();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<player>())
        {
            PressEOFF();
        }
    }
    public void PressEON()
    {
        PressE.SetActive(true);
    }
    public void PressEOFF()
    {
        PressE.SetActive(false);
    }
}
