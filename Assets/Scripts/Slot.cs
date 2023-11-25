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

    public void UpdateSlot()
    {
        slotIconImage.sprite = icon; // Accedo directamente al componente Image.

        if (slotIconImage == null)
        {
            Debug.LogError("Image component not found in child. Make sure the child has an Image component attached.");
        }
    }
}
