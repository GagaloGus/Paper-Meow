using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Apple : MonoBehaviour
{
    public int applesToAdd = 1; // Número de manzanas que agrega al recolectar.
    public CollectQuest relatedQuest;
    private GameObject canvas;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>().gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SkoController>())
        {
            if (QuestManager.instance != null)
            {
                if (relatedQuest.itemsCollected <= relatedQuest.requiredItems)
                {
                    relatedQuest.itemsCollected += applesToAdd;
                    canvas.BroadcastMessage("LateUpdate");
                    Debug.Log("Manzana recogida");
                }
            }
            else
            {
                Debug.LogWarning("No se encontró una instancia de CollectQuest.");
            }

            if (QuestManager.instance != null && QuestManager.instance.collectQuest != null)
            {
                QuestManager.instance.collectQuest.CheckCompletion();
            }
        }
    }
}