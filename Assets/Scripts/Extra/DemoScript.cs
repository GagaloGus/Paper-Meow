using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{

    SkoStats playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<SkoStats>();
    }

    public void AddItem(Item item)
    {
        InventoryManager.instance.AddItem(item);
    }

    public void Add_EXP(int exp)
    {
        playerStats.GetEXP(exp);
    }
}
