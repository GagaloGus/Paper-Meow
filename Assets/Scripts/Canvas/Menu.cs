using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Menu : MonoBehaviour
{
    public enum MenuType { PauseMenu, Inventory}
    public MenuType menuType;

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

    public bool open = false;
    public void HandleMenu()
    {
        if(!GameManager.instance.isInteracting)
        {
            if(!open)
            {
                gameObject.SetActive(true);
                foreach (BotonMenu menu in botonMenus) 
                { menu.menu.SetActive(false); }

                GetComponent<Animator>().Play("openMenu");
                open = true;

                switch (menuType)
                {
                    case MenuType.PauseMenu:
                        GameManager.instance.PauseGame();
                        break;
                    case MenuType.Inventory:
                        GameManager.instance.InventoryOpen();
                        break;
                }
            }
            else
            {
                GetComponent<Animator>().Play("closeMenu");
                GameManager.instance.ContinueGame();
                open = false;
            }
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
