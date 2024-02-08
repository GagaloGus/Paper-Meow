using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public BotonMenu[] botonMenus;
    
    public void ToggleMenu(GameObject Boton)
    {
        foreach (BotonMenu menu in botonMenus)
        {
            if(menu.boton == Boton)
            {
                menu.menu.SetActive(true);
            }
            else
            {
                menu.menu.SetActive(false);
            }
        }
    }

    bool open = false;
    public void HandleMenu()
    {
        if(!open)
        {
            gameObject.SetActive(true);
            foreach (BotonMenu menu in botonMenus) 
            { menu.menu.SetActive(false); }

            GetComponent<Animator>().Play("openMenu");
            open = true;
        }
        else
        {
            GetComponent<Animator>().Play("closeMenu");
            open = false;
        }
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}

[System.Serializable]
public class BotonMenu
{
    public GameObject boton;
    public GameObject menu;
}
