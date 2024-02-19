using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance;

    public PlayerEvents playerEvents;
    public MiscEvents miscEvents;
    public ItemsEvents itemsEvents;
    public InventoryEvents inventoryEvents;

    private void Awake()
    {

        instance = this;


        playerEvents = new PlayerEvents();
        miscEvents = new MiscEvents();
        itemsEvents = new ItemsEvents();
        inventoryEvents = new InventoryEvents();
    }
}
