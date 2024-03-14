using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UIElements;

[System.Serializable]
public struct MenuKey
{
    public string name;
    public bool activated;
    public KeyCode keyCode;
    public UnityEvent enableEvent;
    public UnityEvent disableEvent;

}

public class Buttonfunctions : MonoBehaviour
{
    public static Buttonfunctions instance;

    public MenuKey[] menus;



    private void Update()
    {
        if (Input.anyKey && !GameManager.instance.isInteracting && !GameManager.instance.playerDied)
        {
            for (int i = 0; i < menus.Length; i++)
            {
                if (Input.GetKeyDown(menus[i].keyCode))
                {
                    menus[i].activated = !menus[i].activated;

                    if (menus[i].activated)
                    {
                        menus[i].enableEvent?.Invoke();
                    }
                    else
                    {
                        menus[i].disableEvent?.Invoke();
                    }
                }
            }

        }
    }
    public void Toggle(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }

    public void ChangeScene(string name) //Cambiamos a la escena designada, en este caso iria al menu principal y limpiamos la lista de audios.
    {
        GameManager.instance.ChangeScene(name, false);
    }
    public void ExitGame() //Indicamos a la aplicaci�n que se cierre.
    {
        Application.Quit();
    }

    public void TogglePauseContinueGame(bool softPause)
    {
        GameManager.instance.PauseAndContinueToggle(softPause);
    }

    public void PlaySFX3D(AudioClip clip)
    {
        AudioManager.instance.PlaySFX3D(clip, transform.position);
    }

    public void PlaySFX2D(AudioClip clip)
    {
        AudioManager.instance.PlaySFX2D(clip);
    }
}
