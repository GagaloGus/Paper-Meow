using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;

    [Header("UI")]
    Image image;
    TMP_Text countText;

    [SerializeField] int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    public void InitializeItem(Item newItem, bool isWeapon)
    {
        image = GetComponent<Image>();

        item = newItem;
        image.sprite = newItem.sprite;

        if (!isWeapon)
        {
            countText = GetComponentInChildren<TMP_Text>(true);
            RefreshItemCount();
        }
    }

    public void RefreshItemCount()
    {
        countText.text = count.ToString();
        countText.gameObject.SetActive(count > 1);

        if(count <= 0)
        {
            Destroy(gameObject);
        }
    }


    bool wasGrabbedFromQuickswap = false;
    int quickSwapID = 0;
    QuickWeaponSlot previousParentSlot;
    //sirve para arrastrar items
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item.weaponType != WeaponType.Garra || item.itemType != Type.Weapon)
        {
            QuickWeaponSlot parentSlot = GetComponentInParent<QuickWeaponSlot>();
            if (parentSlot)
            {
                wasGrabbedFromQuickswap = true;
                quickSwapID = parentSlot.IDSlot;
                print($"{wasGrabbedFromQuickswap} + {quickSwapID}");
                previousParentSlot = parentSlot;
            }


            image.raycastTarget = false;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
        }

    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if(item.weaponType != WeaponType.Garra || item.itemType != Type.Weapon)
            transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(item.weaponType != WeaponType.Garra || item.itemType != Type.Weapon)
        {
            image.raycastTarget = true;
            transform.SetParent(parentAfterDrag);
            transform.rotation = parentAfterDrag.rotation;

            if(wasGrabbedFromQuickswap)
            {
                InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
                inventoryManager.RemoveItemFromQuickswap(quickSwapID);
                InitializeItem(item, true);
                wasGrabbedFromQuickswap = false;
                previousParentSlot.holdingWeapon = false;
            }

            QuickWeaponSlot parentSlot = GetComponentInParent<QuickWeaponSlot>();
            if (parentSlot)
            {
                parentSlot.holdingWeapon = true;
                parentSlot.SetHoldingItem(item);
            }
        }
    }

    public int itemCount
    {
        get { return count; }
        set { count = value; }
    }
}
