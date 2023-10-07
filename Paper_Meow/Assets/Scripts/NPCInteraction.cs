using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public CollectQuest collectQuest; // Asigna la misi�n de recolectar en el Inspector.
    private bool playerInRange = false; // Variable para rastrear si el jugador est� dentro del rango.
    public GameObject PressE;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<player>())
        {
            PressEON();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<player>())
        {
            PressEOFF();

            playerInRange = false;

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
    void Update()
    {
        // Verifica si el jugador est� dentro del rango y presiona la tecla "E".
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithNPC();
        }
    }

    private void InteractWithNPC()
    {
        QuestManager questManager = FindObjectOfType<QuestManager>(); // Encuentra el QuestManager en la escena (ajusta seg�n tu estructura).

        if (questManager != null)
        {
            // Verifica si el jugador puede recibir la misi�n (puedes personalizar esta l�gica).
            if (!questManager.HasQuest(collectQuest) && !collectQuest.isCompleted)
            {
                questManager.AcceptQuest(collectQuest); // Asigna la misi�n al jugador.
                Debug.Log("NPC: �Por favor, ayuda a recolectar 3 manzanas!");
            }
        }
    }
}
