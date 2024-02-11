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

    public void InitializeItem(Item newItem)
    {
        image = GetComponent<Image>();
        countText = GetComponentInChildren<TMP_Text>();

        item = newItem;
        image.sprite = newItem.sprite;
        RefreshItemCount();
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
    //sirve para arrastrar items
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item.weaponType != WeaponType.Garra)
        {
            if (GetComponentInParent<QuickWeaponSlot>())
            {
                wasGrabbedFromQuickswap = true;
                quickSwapID = GetComponentInParent<QuickWeaponSlot>().IDSlot;
                print($"{wasGrabbedFromQuickswap} + {quickSwapID}");
            }

            image.raycastTarget = false;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
        }

    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if(item.weaponType != WeaponType.Garra)
            transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(item.weaponType != WeaponType.Garra)
        {
            image.raycastTarget = true;
            transform.SetParent(parentAfterDrag);
            transform.rotation = parentAfterDrag.rotation;

            if(wasGrabbedFromQuickswap)
            {
                InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
                inventoryManager.RemoveItemFromQuickswap(quickSwapID);
                inventoryManager.ReloadSpritesOfQS();
            }

            
        }
    }


    public int itemCount
    {
        get { return count; }
        set { count = value; }
    }
}
