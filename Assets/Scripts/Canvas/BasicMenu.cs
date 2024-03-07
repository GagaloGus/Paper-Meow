using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMenu : MonoBehaviour
{
    public BotonMenu[] botonMenus;

    public void ToggleMenu(GameObject Boton)
    {
        foreach (BotonMenu menu in botonMenus)
        {
            if (menu.boton == Boton)
            {
                menu.menu.SetActive(true);
            }
            else
            {
                menu.menu.SetActive(false);
            }
        }
    }

    public void TurnOn()
    {
        foreach (BotonMenu menu in botonMenus)
        { menu.menu.SetActive(false); }
        botonMenus[0].menu.SetActive(true);
        gameObject.SetActive(true);

    }
}
