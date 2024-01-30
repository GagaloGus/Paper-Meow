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
}

[System.Serializable]
public class BotonMenu
{
    public GameObject boton;
    public GameObject menu;
}
