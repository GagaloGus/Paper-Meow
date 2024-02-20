using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEvents
{
    public event Action<Item> onItemAdded;
    public void ItemAdded(Item item)
    {
        if (onItemAdded != null)
        {
            onItemAdded(item);
        }
    }

    public event Action<Item> onWeaponSwap;
    public void WeaponSwap(Item item)
    {
        if (onWeaponSwap != null)
        {
            onWeaponSwap(item);
        }
    }

    public event System.Action onInventoryOpen;
    public void InventoryOpen() 
    {
        if(onInventoryOpen != null)
        {
            onInventoryOpen();
        }
    }
}
