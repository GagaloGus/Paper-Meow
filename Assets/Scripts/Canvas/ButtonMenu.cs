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

    private void Start()
    {
        posit = GetComponentInChildren<Image>();
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("hover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("hover", false);

    }
}
