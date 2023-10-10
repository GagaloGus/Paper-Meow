using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonfunctions : MonoBehaviour
{
    public static Buttonfunctions instance;
    public GameObject MenuCanvas;
    public GameObject PauseMenu;
    public GameObject Shop;
    public AudioClip clip;
    public GameObject SettingsMenu;
    public GameObject ControlsMenu;
    public GameObject Display;
    public GameObject PauseMenuButton;
    public GameObject Inventory;
    public GameObject InventoryButton;
    public GameObject WeaponsButton;
    public GameObject FruitsButton;
    public GameObject PotionsButton;
    public GameObject InventoryTable;
    public bool Menu = false;

    private void Start()
    {
        PauseMenu = MenuCanvas.transform.Find("Pause_Menu").gameObject;
        SettingsMenu = MenuCanvas.transform.Find("Settings").gameObject;
        ControlsMenu = MenuCanvas.transform.Find("Display").gameObject;
        WeaponsButton = Inventory.transform.Find("Inventory_Weapons").gameObject;
        FruitsButton = MenuCanvas.transform.Find("Inventory_Fruits").gameObject;


    }
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
    public void PauseMenuButtonToggle()
    {
        PauseMenuButton.SetActive(!PauseMenuButton.activeSelf);
    }
    public void PauseMenuoToggle() //Activa el menú de pausa.
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
    }
    public void ShopMenuToggle() //Activa el menú de tienda.
    {
        Shop.SetActive(!Shop.activeSelf);
    }
    public void InventoryButtonToggle()
    {
        InventoryButton.SetActive(!InventoryButton.activeSelf);
    }
    public void WeaponsButtonToggle()
    {
        WeaponsButton.SetActive(!WeaponsButton.activeSelf);
    }
    public void FruitsButtonToggle()
    {
        FruitsButton.SetActive(!FruitsButton.activeSelf);
    }

    public void PotionsButtonToggle()
    {
        PotionsButton.SetActive(!PotionsButton.activeSelf);
    }

    public void InventoryTableToggle()
    {
        InventoryTable.SetActive(!InventoryTable.activeSelf);
    }

    public void InventoryToggle()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Inventory.SetActive(!Inventory.activeSelf);
        }  
    }
    public void SettingsMenuToggle()
    {
        SettingsMenu.SetActive(!SettingsMenu.activeSelf);
    }

    public void ControlsMenuToggle()
    {
        ControlsMenu.SetActive(!ControlsMenu.activeSelf);
    }

    public void DisplayToggle()
    {
        Display.SetActive(!Display.activeSelf);
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
    private void Update()
    {
        InventoryToggle();
    }
}
