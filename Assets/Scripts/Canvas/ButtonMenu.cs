using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Image posit;
    Animator animator;
    public bool unlocked;

    private void Start()
    {
        posit = GetComponentInChildren<Image>();
        animator = GetComponent<Animator>();

        if (unlocked)
        {
            GetComponent<Button>().enabled = true;
            transform.Find("Image").GetComponent<Image>().color = Color.white;
        }
        else 
        {
            GetComponent<Button>().enabled = false;
            transform.Find("Image").GetComponent<Image>().color = Color.gray;
        }
    }   

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (unlocked)
        {
            animator.SetBool("hover", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (unlocked)
        {
            animator.SetBool("hover", false);
        }

    }
}
