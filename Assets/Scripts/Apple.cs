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
                    canvas.BroadcastMessage("UpdateCanvasQuests");
                    Debug.Log("Manzana recogida");
                }
            }
            else
            {
                Debug.LogWarning("No se encontró una instancia de CollectQuest.");
            }

            // Almacena la referencia al GameObject antes de destruirlo.
            //var appleObject = gameObject;

            //// Destruye el objeto de manzanas.
            //Destroy(appleObject);

            // Llama a CheckCompletion para verificar si se completó la misión.
            if (QuestManager.instance != null && QuestManager.instance.collectQuest != null)
            {
                QuestManager.instance.collectQuest.CheckCompletion();
            }
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<player>())
    //    {      
    //        if (QuestManager.instance != null)
    //        {
    //            if (relatedQuest.itemsCollected <= relatedQuest.requiredItems)
    //            {
    //                relatedQuest.itemsCollected += applesToAdd;
    //                canvas.BroadcastMessage("UpdateCanvasQuest");
    //                Debug.Log("Manzana recogida");
    //            }
    //        }
    //                //Destruye el objeto de manzanas.
    //                Destroy(gameObject);

    //                // Llama a CheckCompletion para verificar si se completó la misión.
    //                QuestManager.instance.collectQuest.CheckCompletion();
    //            }
    //            else
    //            {
    //                Debug.LogWarning("No se encontró una instancia de CollectQuest.");
    //            }
    //        }
}