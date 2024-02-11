using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickWeaponChange : MonoBehaviour
{
    QuickWeaponSlot[] slots;

    private void Awake()
    {
        slots = quickWeaponSlots;
    }

    public void ReloadSlotSprites()
    {
        for (int i = 0;i < slots.Length; i++)
        {
            Item currentItem = slots[i].GetComponentInChildren<InventoryItem>().item;

            Image slotImage = slots[i].gameObject.transform.GetChild(0).GetComponent<Image>();
            if(currentItem.weaponType != WeaponType.Garra)
            {
                slotImage.sprite = null;
            }
            else
            {
                slotImage.sprite = currentItem.sprite;
            }

        }
    }

    public void StartSpin(bool clockwise, float speed)
    {
        StartCoroutine (Spin(clockwise, speed));
    }

    IEnumerator Spin(bool clockwise, float speed)
    {
        for(float i = 0; i < 90; i+= speed)
        {
            transform.rotation *= Quaternion.Euler(0, 0, speed * (clockwise? -1 : 1));
            yield return null;
        }
    }

    public QuickWeaponSlot[] quickWeaponSlots
    {
        get 
        {
            int childs = transform.childCount;
            QuickWeaponSlot[] temp = new QuickWeaponSlot[childs];
            for (int i = 0; i < childs; i++)
            {
                temp[i] = transform.GetChild(i).gameObject.GetComponent<QuickWeaponSlot>();
                temp[i].IDSlot = i;
            }

            return temp; 
        }

    }
}

