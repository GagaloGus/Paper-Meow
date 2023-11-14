using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puntcounter : MonoBehaviour
{
    public TMPro.TMP_Text textpointsText; //Variable para el texto.
    void Update()
    {
        textpointsText.text = "" + GameManager.instance.GetPunt(); //Llamamos al texto, el cual sera igual a "La puntuación que nos devuelve el Gamemanager", esto hace que al sumarse la puntuación al GameManager, se actualice en pantalla.
    }
}
