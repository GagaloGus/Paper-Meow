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


    //No funciona, sirve para arrastrar items
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }


    public int itemCount
    {
        get { return count; }
        set { count = value; }
    }
}
