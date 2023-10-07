using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public int applesToAdd = 1; // N�mero de manzanas que agrega al recolectar.

    private void Start()
    {        
         QuestManager.instance = FindObjectOfType<QuestManager>();

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<QuestManager>())
        {
            if (QuestManager.instance != null)
            {
                if (QuestManager.instance.collectQuest.itemsCollected <= QuestManager.instance.collectQuest.requiredItems)
                {
                    QuestManager.instance.collectQuest.itemsCollected += applesToAdd;
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
