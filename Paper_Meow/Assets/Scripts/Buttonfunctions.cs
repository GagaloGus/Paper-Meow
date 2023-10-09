using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonfunctions : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject Shop;
    public AudioClip clip;
    public GameObject SettingsMenu;
    public GameObject ControlsMenu;
    public GameObject Display;
    public GameObject PauseMenuButton;
    public void PauseTimer() //Pausamos el timer.
    {
        Time.timeScale = 0;
    }
    public void StartTimer() //Iniciamos el timer.
    {
        Time.timeScale = 1;
    }
    public void ResetPunt() //Reseteamos el timer.
    {
       GameManager.instance.ResetPunt(0);
    }
    public void PauseMenuButtonON()
    {
        PauseMenuButton.SetActive(true);
    }
    public void PauseMenuButtonOFF()
    {
        PauseMenuButton.SetActive(false);
    }
    public void PauseMenuoON() //Activa el menú de pausa.
    {
        PauseMenu.SetActive(true);
    }
    public void PauseMenuoOFF() //Desactiva el menú de pausa.
    {
        PauseMenu.SetActive(false);
    }
    public void ShopMenuON() //Activa el menú de tienda.
    {
        Shop.SetActive(true);
    }
    public void ShopMenuOFF() //Desactiva el menú de tienda.
    {
        Shop.SetActive(false);
    }
    public void SettingsMenuON()
    {
        SettingsMenu.SetActive(true);
    }
    public void SettingsMenuOFF()
    {
        SettingsMenu.SetActive(false);
    }
    public void ControlsMenuON()
    {
        ControlsMenu.SetActive(true);
    }
    public void ControlsMenuOFF()
    {
        ControlsMenu.SetActive(false);
    }
    public void DisplayON()
    {
        Display.SetActive(true);
    }
    public void DisplayOFF()
    {
        Display.SetActive(false);
    }
    public void ChangeScene(string name) //Cambiamos a la escena designada, en este caso iria al menu principal y limpiamos la lista de audios.
    {
        SceneManager.LoadScene(name);
        AudioManager.instance.ClearAudioList();

    }
    public void ExitGame() //Indicamos a la aplicación que se cierre.
    {
        Application.Quit();
    }
   
    public void StopBackgroundMusic()
    {
        AudioManager.instance.StopBackgroundMusic();
    }
    public void StartBackgroundMusic()
    {
        AudioManager.instance.PlayBackgroundMusic(clip);
    }
        
}
