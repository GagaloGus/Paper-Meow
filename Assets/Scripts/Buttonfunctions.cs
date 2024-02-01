using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public struct MenuKey
{
    public GameObject gO;
    public KeyCode keyCode;
}

public class Buttonfunctions : MonoBehaviour
{
    public static Buttonfunctions instance;

    public MenuKey[] menus;

    private void Update()
    {
        if (Input.anyKey)
        {
            foreach (MenuKey key in menus)
            {
                if (Input.GetKeyDown(key.keyCode))
                {
                    key.gO.SetActive(!key.gO.activeSelf);
                }
            }

        }
    }
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

    void KeyCode()
    {

    }
}
