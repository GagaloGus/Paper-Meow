using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickWeaponSlot : MonoBehaviour, IDropHandler
{
    public int IDSlot;
    public bool holdingWeapon;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (inventoryItem.item.itemType == Type.Weapon 
            && inventoryItem.item.weaponType != WeaponType.Garra)
        {
            inventoryItem.parentAfterDrag = transform;

            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            inventoryManager.AddItemToQuickswap(inventoryItem.item, IDSlot);
        }
    }
}
