using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickWeaponSlot : MonoBehaviour, IDropHandler
{
    public int IDSlot;
    public Item itemHolding;
    public bool holdingWeapon;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();

        if (inventoryItem.item.itemType == Type.Weapon && !holdingWeapon)
        {
            if(inventoryItem.item.weaponType != WeaponType.Garra)
            {
                inventoryItem.parentAfterDrag = transform;
                itemHolding = inventoryItem.item;

                inventoryManager.RemoveItemFromQuickswap(IDSlot);
                itemHolding = GetComponentInChildren<InventoryItem>().item;

                inventoryManager.AddItemToQuickswap(inventoryItem.item, IDSlot);
            }
        }
    }

    public void SetHoldingItem(Item item)
    {
        itemHolding = item;
    }

    public void ChangeSpriteOfSlot()
    {
        InventoryItem itemSlot = GetComponentInChildren<InventoryItem>();

        itemSlot.gameObject.GetComponent<Image>().sprite = itemHolding.sprite;
    }
}
