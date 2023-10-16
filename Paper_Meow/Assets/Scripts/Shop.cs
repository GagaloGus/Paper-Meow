using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance; 
    public Inventory inventory; // Invocamos al Inventario.
    public TMPro.TMP_Text textcoinsText; // Referencia al objeto Text en el canvas
    //public TMPro.TMP_Text textspeedpotionsText;
    public TMPro.TMP_Text texhealthpotionsText;
    //public int speedpotions = 0;
    public int healthpotions = 0;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateCoinsText(); // Llamamos al método UpdateCoinsText.
        //UpdateSpeedPotionText();
        UpdateHealthPotionText();
    }
    private void Update()
    {
        UpdateHealthPotionText();
    }
    public void BuyHealthPotion()
    {
        if (GameManager.instance.points >= 10) // Verifica si hay suficientes monedas para comprar la poción
        {
            inventory.InventoryObjects.Add(new HealthPotion("HealthPotion", 1, 10)); // Sihay dinero suficiente, se agrega una poción al inventario.
            GameManager.instance.points -= 10; // Resta el valor de la poción de las monedas
            //healthpotions += 1;
            Debug.Log("Poción de curación comprada.");
            AddHPotion();
            UpdateCoinsText();

        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }
    //public void BuySpeedPotion()
    //{
    //    if (coins >= 2) // Verifica si hay suficientes monedas para comprar la poción
    //    {
    //        inventory.InventoryObjects.Add(new SpeedPotion("SpeedPotion", 1, 2));
    //        coins -= 2; // Resta el valor de la poción de las monedas
            
    //        Debug.Log("Poción de velocidad comprada.");
    //        AddSpPotion();
    //        UpdateCoinsText();
    //    }
    //    else
    //    {
    //        Debug.Log("No tienes suficientes monedas.");
    //    }
    //}
    public void UpdateCoinsText() //Actualizamos el texto de coins y lo mostramos.
    {
        textcoinsText.text = ":" + GameManager.instance.GetPunt();
    }

    //public void UpdateSpeedPotionText()
    //{
    //   textspeedpotionsText.text = speedpotions.ToString();
    //}
    public void UpdateHealthPotionText()
    {
       texhealthpotionsText.text = GameManager.instance.healthpotions.ToString();
    }
    //public void AddSpPotion()
    //{
    //    speedpotions += 1;
    //    UpdateSpeedPotionText();
    //}
    public void AddHPotion()
    {
        GameManager.instance.healthpotions += 1;
        UpdateHealthPotionText();
    }

    public void RemovePotionHealth()
    {
       if (GameManager.instance.healthpotions > 0) // Verifica si hay pociones de salud disponibles
        {
           GameManager.instance.healthpotions -= 1;
           Debug.Log("Poción de curación consumida.");
           UpdateHealthPotionText();
        }
        else
        {
            Debug.Log("No tienes pociones de salud disponibles.");
        }
    }

    //public void RemovePotionSpeed()
    //{
    //    if (speedpotions > 0) // Verifica si hay pociones de velocidad disponibles
    //    {
    //        speedpotions -= 1;
    //        Debug.Log("Poción de velocidad eliminada del inventario.");
    //        UpdateSpeedPotionText();
    //    }
    //    else
    //    {
    //        Debug.Log("No tienes pociones de velocidad disponibles.");
    //    }
    //}
}
