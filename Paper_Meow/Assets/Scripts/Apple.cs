using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public int applesToAdd = 1; // N�mero de manzanas que agrega al recolectar.
    public CollectQuest relatedQuest;
    private GameObject canvas;
    private void Start()
    {        
         QuestManager.instance = FindObjectOfType<QuestManager>();
        canvas = FindAnyObjectByType<Canvas>().gameObject;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<player>())
        {
            if (QuestManager.instance != null)
            {
                if (relatedQuest.itemsCollected <= relatedQuest.requiredItems)
                {
                    relatedQuest.itemsCollected += applesToAdd;
                    canvas.BroadcastMessage("UpdateCanvasQuest");
                    Debug.Log("Manzana recogida");
                }

                // Desactiva o destruye el objeto de manzanas.
                Destroy(gameObject);

                // Llama a CheckCompletion para verificar si se complet� la misi�n.
                QuestManager.instance.collectQuest.CheckCompletion();
            }
            else
            {
                Debug.LogWarning("No se encontr� una instancia de CollectQuest.");
            }
        }
    }
}
