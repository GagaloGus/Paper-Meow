using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryMenu
{
    public GameObject Menu;
    public InventorySlot[] menuSlots;
}

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems;
    public InventoryMenu[] itemMenus;

    public GameObject inventoryItemPrefab;


    private void Start()
    {
        foreach(InventoryMenu menu in itemMenus)
        {
            int childCount = menu.Menu.transform.childCount;
            menu.menuSlots = new InventorySlot[childCount];
            for(int i = 0; i < childCount; i++)
            {
                menu.menuSlots[i] = menu.Menu.transform.GetChild(i).GetComponent<InventorySlot>();
            }
        }
    }

    public bool AddItem(Item item, int amount)
    {
        InventoryMenu currentMenu = itemMenus[(int)item.itemType];

        //Encuentra algun hueco del mismo tipo
        for (int i = 0; i < currentMenu.menuSlots.Length; i++)
        {
            InventorySlot slot = currentMenu.menuSlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && 
                itemInSlot.item == item && 
                itemInSlot.itemCount < maxStackedItems &&
                itemInSlot.item.stackable)
            {
                itemInSlot.itemCount+= amount;
                itemInSlot.RefreshItemCount();
                return true;
            }
        }

        //Encuentra un hueco libre
        for (int i = 0; i < currentMenu.menuSlots.Length; i++)
        {
            InventorySlot slot = currentMenu.menuSlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if(itemInSlot == null)
            {
                if(item.stackable)
                    SpawnNewItem(item, slot, amount);
                else
                {
                    for (int j = 0; j < amount; j++)
                    {
                        SpawnNewItem(item, currentMenu.menuSlots[i+j], 1);
                    }
                }

                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(Item item, InventorySlot slot, int amount) 
    {
        GameObject newItemGameObj = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGameObj.GetComponent<InventoryItem>();

        inventoryItem.itemCount = amount;
        inventoryItem.InitializeItem(item);

    }

    public InventoryItem CheckIfHasItem(Item itemToFind)
    {
        foreach(InventoryMenu menu in itemMenus)
        {
            foreach(InventorySlot itemslot in menu.menuSlots)
            {
                InventoryItem itemInv = itemslot.GetComponentInChildren<InventoryItem>();

                if(itemInv)
                {
                    if(itemToFind == itemInv.item)
                    {
                        print($"item {itemToFind.name} encontrado");
                        return itemInv;
                    }
                }
            }
        }

        print($"no se encontro {itemToFind.name}");
        return null;
    }

    public bool HasItem(Item item, int amount)
    {
        InventoryItem chosenItem = CheckIfHasItem(item);

        if (chosenItem != null && chosenItem.itemCount >= amount)
        {
            chosenItem.itemCount-= amount;
            chosenItem.RefreshItemCount();
            return true;
        }
        return false;
    }
}
