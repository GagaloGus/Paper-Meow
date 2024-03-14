using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BotonMenu
{
    public GameObject boton;
    public GameObject menu;
    public BotonMenu(GameObject boton, GameObject menu)
    {
        this.boton = boton;
        this.menu = menu;
    }
}

public class PauseMenu : MonoBehaviour
{
    public List<BotonMenu> botonMenus;
    public GameObject[] twoPageMenus;
    public GameObject quickSwapMenu;

    private void Start()
    {
        gameObject.SetActive(false);

        //find childs
        botonMenus = new List<BotonMenu>
        {
            new BotonMenu(transform.Find("Button_PJ").gameObject, transform.Find("Player Info").gameObject),
            new BotonMenu(transform.Find("Button_Quests").gameObject, transform.Find("Quests").gameObject),
            new BotonMenu(transform.Find("Button_Skills").gameObject, transform.Find("Skills").gameObject),
            new BotonMenu(transform.Find("Button_Inventory").gameObject, transform.Find("Inventory").gameObject),
            new BotonMenu(transform.Find("Button_Settings").gameObject, transform.Find("Settings").gameObject),
            new BotonMenu(transform.Find("Button_Save").gameObject, transform.Find("Save").gameObject)
        };
    }

    public void ToggleMenu(GameObject Boton)
    {
        foreach (BotonMenu menu in botonMenus)
        {
            if(menu.boton == Boton)
            {
                menu.menu.SetActive(true);

                foreach (GameObject twoPage in twoPageMenus)
                {
                    if(menu.menu == twoPage) 
                    {
                        GetComponent<Animator>().SetBool("inventory", true);
                        break;
                    }
                    GetComponent<Animator>().SetBool("inventory", false);
                }
                
            }
            else
            {
                menu.menu.SetActive(false);
            }
        }
    }

    [HideInInspector] public bool open = false;
    public void HandleMenu()
    {
        if (!GameManager.instance.isInteracting)
        {
            if (!open)
            {
                if (!GameManager.instance.menuOpen)
                {
                    foreach (BotonMenu menu in botonMenus)
                    { menu.menu.SetActive(false); }
                    gameObject.SetActive(true);

                    GetComponent<Animator>().Play("openMenu");
                    open = true;
                    GameManager.instance.menuOpen = true;
                    GameManager.instance.PauseGame();

                }
            }
            else
            {
                GetComponent<Animator>().Play("closeMenu");
                GameManager.instance.ContinueGame();
                open = false;
                quickSwapMenu.GetComponent<QuickWeaponChange>().TurnOFF();
            }
        }
    }

    public void CloseMenu()
    {
        GetComponent<Animator>().Play("closeMenu");
        GameManager.instance.ContinueGame();
        open = false;
    }

    public void TurnOffMenu()
    {
        gameObject.SetActive(false);
    }

    public void EnablePegatina(int number)
    {
        Image pegatina = transform.Find("fondo cuaderno").Find($"pegatina{number}").GetComponent<Image>();
        pegatina.color = Color.white;
    }
}

