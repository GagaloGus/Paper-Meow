using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Potion> InventoryObjects = new List<Potion>();  // Creamos una lista pública de objetos "Potion" llamada "InventoryObjects" y la inicializamos como una lista vacía.
    public static Inventory instance;
    //public PotionsHText potionsHtext;
    public Shop shop;
    public TMPro.TMP_Text inventoryDisplay;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        UpdateInventoryDisplay();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            player playerObject = GameObject.Find("Kai").GetComponent<player>(); // Buscamos el objeto "Kai" en la escena y obtenemos su componente "Kai"
            if (InventoryObjects.Exists(h => h is HealthPotion))  // Si existe al menos una poción de salud en el inventario
            {
                UseHealthPotion(playerObject); // Usamos una poción de salud
                //PotionsHText.instance.UpdateHealthPotionText();
                Debug.Log("Pocion de salud consumida.");
            }
            else
            {
                Debug.Log("No tienes pociones de salud.");
            }
        }
        //if (Input.GetButtonDown("Fire2")) // Si se presiona el botón "Fire2"
        //{ 
        //    Kai kaiObject = GameObject.Find("Kai").GetComponent<Kai>();   // Buscamos el objeto "Kai" en la escena y obtenemos su componente "Kai"
        //    if (InventoryObjects.Exists(s => s is SpeedPotion)) // Si existe al menos una poción de velocidad en el inventario
        //    {
        //        UseSpeedPotion(kaiObject); // Usamos una poción de velocidad
        //        Debug.Log("Pocion de velocidad consumida :).");
        //    }
        //    else
        //    {
        //        Debug.Log("No tienes pociones de velocidad.");
        //    }
        //}
    }
    public void UpdateInventoryDisplay()
    {
        string inventoryText = "Inventory:\n";
        foreach (Potion potion in InventoryObjects)
        {
            inventoryText += "- " + potion.name + "\n"; // Ajusta esto según la forma en que quieras mostrar el objeto en el inventario
        }
        inventoryDisplay.text = inventoryText + GameManager.instance.healthpotions.ToString(); // Actualiza el texto en el Canvas
    }

    public void UseHealthPotion(player player) // Definimos la función "UseHealthPotion" que consume una poción de salud
    {
        List<Potion> potionsToRemove = new List<Potion>(); // Creamos una lista local de objetos "Potion" llamada "potionsToRemove" y la inicializamos como una lista vacía
        foreach (Potion potion in InventoryObjects)   // Recorremos la lista de objetos "Potion" del inventario
        {
            if (potion is HealthPotion) // Si encontramos una poción de salud
            {
                HealthPotion healthPotion = (HealthPotion)potion;  // Convertimos la poción a un objeto "HealthPotion"
                // Usamos la poción de salud encontrada
                healthPotion.UsePotion(player);
                QuitHealthPotion();
                Debug.Log("Healing");
                // Agregamos la poción de salud encontrada a la lista "potionsToRemove"
                potionsToRemove.Add(healthPotion);
                break;
            }
        }
        
        foreach (Potion potionToRemove in potionsToRemove)  // Eliminamos todas las pócimas en la lista "potionsToRemove" del inventario
        {
            InventoryObjects.Remove(potionToRemove);
        }
    }
    //public void UseSpeedPotion(player player) // Definimos la función "UseSpeedPotion" que consume una poción de velocidad
    //{
    //    List<Potion> potionsToRemove = new List<Potion>();  // Creamos una lista local de objetos "Potion" llamada "potionsToRemove" y la inicializamos como una lista vacía
    //    foreach (Potion potion in InventoryObjects)  // Recorremos la lista de objetos "Potion" del inventario
    //    {
    //        if (potion is SpeedPotion) // Si encontramos una poción de velocidad
    //        {
    //            SpeedPotion speedPotion = (SpeedPotion)potion;  // Convertimos la poción a un objeto "SpeedPotion"
    //            //Usar la speedPotion encontrada
    //            speedPotion.UsePotion(player);
    //            QuitSpeedPotion();
    //            Debug.Log("Boosting Speed");
    //            // Agregamos la poción de velocidad encontrada a la lista "potionsToRemove"
    //            potionsToRemove.Add(speedPotion);
    //            break;
    //        }
    //    }       
    //    foreach (Potion potionToRemove in potionsToRemove) // Eliminamos todas las pócimas en la lista "potionsToRemove" del inventario.
    //    {
    //        InventoryObjects.Remove(potionToRemove);
    //    }
       
    //}
    //public void QuitSpeedPotion()
    //{
    //    Shop.instance.RemovePotionSpeed();
    //}
    public void QuitHealthPotion()
    {
        Shop.instance.RemovePotionHealth();
    }
}
