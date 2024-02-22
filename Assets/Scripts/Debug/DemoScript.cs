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

    public void PlaySFX2D(AudioClip clip)
    {
        AudioManager.instance.PlaySFX2D(clip);
    }

    public void FinishTutorial()
    {
        GameManager.instance.isTutorial = false;
    }

    public void TeleportToLocation(GameObject location)
    {
        transform.position = location.transform.position;
    }
}
