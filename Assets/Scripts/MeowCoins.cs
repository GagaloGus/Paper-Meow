using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowCoins : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SkoController>() != null)
        {
            GameManager.instance.AddPunt(10);
            //Destroy(gameObject);
        }
    }
}
