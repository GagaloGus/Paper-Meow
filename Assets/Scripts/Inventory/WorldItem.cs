using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class WorldItem : MonoBehaviour
{
    public Item item;

    public void GiveItemToPlayer()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        inventoryManager.AddItem(item);
        Destroy(gameObject);
    }
}
