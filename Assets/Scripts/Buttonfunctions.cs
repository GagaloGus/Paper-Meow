using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public GameObject dropdownmenu;
    public GameObject buttonmap;
    public GameObject map;
    public GameObject mision;
    public GameObject dropdownbutton;
    public GameObject exitmap;
    public GameObject missionmenu;
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
        Inventory.SetActive(!Inventory.activeSelf);
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

    public void DropDownMenu()
    {
        dropdownmenu.SetActive(!dropdownmenu.activeSelf);
    }

    public void ButtonMap()
    {
        buttonmap.SetActive(!buttonmap.activeSelf);
    }

    public void Map()
    {
        map.SetActive(!map.activeSelf);
    }

    public void Mision()
    {
        mision.SetActive(!mision.activeSelf);
    }

    public void DropDownButton()
    {
        dropdownbutton.SetActive(!dropdownbutton.activeSelf);
    }

    public void ExitMap()
    {
        exitmap.SetActive(!exitmap.activeSelf);
    }

    public void MissionMenu()
    {
        missionmenu.SetActive(!missionmenu.activeSelf);
    }

    public void ToggleObject(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
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

    public void OnlyWeaponsTable()
    {
        WeaponsButton.SetActive(true);
        InventoryTable.SetActive(false);
        FruitsButton.SetActive(false);
        PotionsButton.SetActive(false);
    }
    public void OnlyFruitsTable()
    {
        FruitsButton.SetActive(true);
        InventoryTable.SetActive(false);
        WeaponsButton.SetActive(false);
        PotionsButton.SetActive(false);
    }
    public void OnlyPotionsTable()
    {
        PotionsButton.SetActive(true);
        InventoryTable.SetActive(false);
        FruitsButton.SetActive(false);
        WeaponsButton.SetActive(false);
    }
    public void OnlyObjectsTable()
    {
        InventoryTable.SetActive(true);
        PotionsButton.SetActive(false);
        InventoryTable.SetActive(false);
        FruitsButton.SetActive(false);
        WeaponsButton.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryToggle();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuoToggle();
        }
    }
}
