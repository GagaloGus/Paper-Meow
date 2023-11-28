using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject item;
    public int ID;
    public string type;
    public string description;

    public bool empty;
    public Sprite icon;

    public Image slotIconImage;

    private void Start()
    {
        slotIconImage = transform.GetChild(0).GetComponent<Image>(); // Asigno directamente el componente Image.
    }
    public void LateUpdate()
    {
        UpdateSlot();
    }
    public void UpdateSlot()
    {

        if (slotIconImage != null)
        {
            slotIconImage.sprite = icon; // Accedo directamente al componente Image.
        }
        

    }
}
