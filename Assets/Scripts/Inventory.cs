using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int allSlots;
    private GameObject[] slot;
    public GameObject slotHolder;

    void Awake()
    {
        allSlots = slotHolder.transform.childCount;

        slot = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slot[i].GetComponent<Slot>().item == null )
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }
    }

    private void Update()
    {
        // Verifica si el jugador ha hecho clic en un objeto del inventario
        if (Input.GetMouseButtonDown(0)) // Puedes ajustar el botón del mouse según tu configuración
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verifica si ha golpeado un objeto del inventario
            if (Physics.Raycast(ray, out hit))
            {
                // Comprueba si el objeto clickeado tiene el componente Slot
                Slot clickedSlot = hit.collider.GetComponent<Slot>();
                if (clickedSlot != null)
                {
                    // Maneja la lógica de uso del objeto
                    UseItem(clickedSlot);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Item>())
        {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            AddItem(itemPickedUp, item.ID, item.type, item.description, item.icon);
        }
    }
    public void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (!slot[i].GetComponent<Slot>().empty)
            {
                if (slot[i].GetComponent<Slot>().type == itemType)
                {
                    // Incrementa la cantidad del objeto en el slot
                    slot[i].GetComponent<Slot>().item.GetComponent<Item>().quantity++;
                    // Actualiza el slot
                    slot[i].GetComponent<Slot>().UpdateSlot();
                    Destroy(itemObject);
                    return;
                }
            }
        }

        // Si no se encontró un slot con el mismo tipo, busca un slot vacío
        for (int i = 0; i < allSlots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;

                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().ID = itemID;
                slot[i].GetComponent<Slot>().type = itemType;
                slot[i].GetComponent<Slot>().description = itemDescription;
                slot[i].GetComponent<Slot>().icon = itemIcon;

                itemObject.GetComponent<Item>().quantity = 1; // Establece la cantidad en 1 para un nuevo objeto
                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);

                slot[i].GetComponent<Slot>().UpdateSlot();

                slot[i].GetComponent<Slot>().empty = false;

                return;
            }
        }
    }

    // Método para manejar la lógica de uso del objeto
    public void UseItem(Slot clickedSlot)
    {
        // Verifica si la ranura no está vacía
        if (!clickedSlot.empty)
        {
            // Obtén el objeto del inventario
            GameObject itemObject = clickedSlot.item;


            // Después de usar el objeto, puedes eliminarlo del inventario
            RemoveItem(clickedSlot);
        }
    }

    // Método para eliminar un objeto del inventario
    void RemoveItem(Slot slot)
    {
        slot.empty = true;
        slot.item = null;
        // Puedes realizar otras acciones de limpieza aquí si es necesario.
    }
}
