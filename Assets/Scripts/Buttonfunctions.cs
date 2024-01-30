using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Buttonfunctions : MonoBehaviour
{
    public static Buttonfunctions instance;

    public void Toggle(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
    public void timeScale(int num) //Pausamos el timer.
    {
        Time.timeScale = num;
    }

    public void ResetPunt() //Reseteamos el timer.
    {
        GameManager.instance.ResetPunt(0);
    }

    public void ChangeScene(string name) //Cambiamos a la escena designada, en este caso iria al menu principal y limpiamos la lista de audios.
    {
        SceneManager.LoadScene(name);
        AudioManager.instance.ClearAudioList();

    }
    public void ExitGame() //Indicamos a la aplicaci�n que se cierre.
    {
        Application.Quit();
    }
}
