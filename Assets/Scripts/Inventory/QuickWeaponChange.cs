using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickWeaponChange : MonoBehaviour
{

    public float dissapearMaxTime;
    [SerializeField] float currentTime;
    [SerializeField] bool notMoving, canRotate = true;


    private void Update()
    {
        if (notMoving)
        {
            currentTime += Time.deltaTime;
            if (currentTime > dissapearMaxTime)
            {
                StartFade(false, 0.1f);
                currentTime = 0;
                notMoving = false;
            }
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
    public void StartSpin(bool clockwise, float speed)
    {
        if(canRotate)
        StartCoroutine (Spin(clockwise, speed));
    }

    IEnumerator Spin(bool clockwise, float speed)
    {
        float newSpeed = (int)Mathf.Floor(90 / (speed * 2));

        notMoving = true;
        currentTime = 0;
        for(float i = 0; i < 90; i+= newSpeed)
        {
            transform.rotation *= Quaternion.Euler(0, 0, newSpeed * (clockwise? -1 : 1));
            yield return null;
        }

    }

    public void StartFade(bool fadeIn, float speed)
    {
        StartCoroutine(Fade(fadeIn, speed));
    }

    IEnumerator Fade(bool fadeIn, float speed)
    {
        Image image = GetComponent<Image>();
        if (fadeIn)
        {
            for (float i = 0; i <= 1; i += speed)
            {
                image.color = new Color(1, 1, 1, i + image.color.a);
                yield return null;
            }
        }
        else
        {
            for (float i = 1; i >= 0; i -= speed)
            {
                image.color = new Color(1, 1, 1, i);
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }

    bool isOpen = false;
    public void InventoryOpenClose()
    {
        if (!isOpen)
        {
            gameObject.SetActive (true);
            StartFade(true, 0.1f);
            canRotate = false;
            notMoving = false;
            InventoryManager.instance.canSwap = false;
            isOpen = true;
        }
        else
        {
            canRotate = true;
            StartFade(false, 0.1f);
            InventoryManager.instance.canSwap = true;

            isOpen = false;
        }
    }


}

