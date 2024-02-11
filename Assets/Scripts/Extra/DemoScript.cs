using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public bool GiveOrUse;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void bibibu(int id)
    {
        if(GiveOrUse)
        {
            bool result = inventoryManager.AddItem(itemsToPickup[id], 2);
            if(result)
            {
                print("Item A�adido");
            }
            else
            {
                Debug.LogAssertion("Item no a�adido");
            }
        }
        else
        {
            inventoryManager.HasItem(itemsToPickup[id], 1);
        }
    }
}
