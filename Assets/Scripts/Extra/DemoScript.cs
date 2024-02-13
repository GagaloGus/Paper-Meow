using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    InventoryManager inventoryManager;
    SkoStats playerStats;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        playerStats = FindObjectOfType<SkoStats>();
    }

    public void AddItem(Item item)
    {
        inventoryManager.AddItem(item);
    }

    public void Add_EXP(int exp)
    {
        playerStats.GetEXP(exp);
    }
}
