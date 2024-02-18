using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public class InventoryMenu
{
    public GameObject Menu;
    public InventorySlot[] menuSlots;
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("Inventario")]
    public int maxStackedItems;
    [SerializeField] List<InventoryMenu> itemMenus = new(4);
    public QuickWeaponSlot[] quickSwapWeapons;

    [Header("Prefabs")]
    public GameObject inventoryItemPrefab;
    public GameObject quickSwapInventoryItemPrefab;
    public Item garra;

    QuickWeaponChange quickSwapWeapon_Obj;

    [Header("Arma elegida")]
    public int currentWeaponSlot;
    public bool canSwap;

    private void Awake()
    {
        if (!instance) //instance  != null  //Detecta que no haya otro manager en la escena.
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //Si hay otro manager lo destruye.
        }
    }

    Transform inventoryParent;

    private void Start()
    {
        inventoryParent = FindObjectOfType<TextController>(true).transform.Find("InventoryCanvas").transform.Find("Inventory");

        itemMenus[0].Menu = inventoryParent.Find("ObjectMenu").gameObject;
        itemMenus[1].Menu = inventoryParent.Find("QuestMenu").gameObject;
        itemMenus[2].Menu = inventoryParent.Find("EatMenu").gameObject;
        itemMenus[3].Menu = inventoryParent.Find("WeaponMenu").gameObject;

        foreach (InventoryMenu menu in itemMenus)
        {
            int childCount = menu.Menu.transform.childCount;
            menu.menuSlots = new InventorySlot[childCount];
            for(int i = 0; i < childCount; i++)
            {
                menu.menuSlots[i] =
                    menu.Menu.transform.GetChild(i).
                    GetComponent<InventorySlot>();
            }
        }

        quickSwapWeapon_Obj = FindObjectOfType<QuickWeaponChange>(true);
        quickSwapWeapon_Obj.gameObject.SetActive(false);
        quickSwapWeapons = quickSwapWeapon_Obj.quickWeaponSlots;

        for(int i = 0;i < quickSwapWeapons.Length; i++)
        {
            RemoveItemFromQuickswap(i);
        }

        canSwap = true;
    }

    public void SwapWeapon(bool clockwise)
    {
        if (canSwap)
        {
            //Clockwise = previous
            if (clockwise)
            {
                if (currentWeaponSlot == 3) { currentWeaponSlot = 0; }
                else { currentWeaponSlot++; }
            }
            else
            {
                if (currentWeaponSlot == 0) { currentWeaponSlot = 3; }
                else { currentWeaponSlot--; }
            }

            SwapSkoWeapon(currentWeaponSlot);

            quickSwapWeapon_Obj.gameObject.SetActive(true);
            quickSwapWeapon_Obj.StartFade(true, 0.1f);
            quickSwapWeapon_Obj.StartSpin(clockwise, 7);
        }
    }

    void SwapSkoWeapon(int location)
    {
        Item weapon =
            quickSwapWeapons[location].
            GetComponentInChildren<InventoryItem>().item;

        GameEventsManager.instance.inventoryEvents.WeaponSwap(weapon);
    }

    public bool AddItemToQuickswap(Item weapon, int location)
    {
        QuickWeaponSlot currentSlot = quickSwapWeapons[location];

        InventoryItem itemInSlot = currentSlot.GetComponentInChildren<InventoryItem>();

        if(location == 0) 
        {
            GameEventsManager.instance.inventoryEvents.WeaponSwap(weapon);
        }

        if(itemInSlot.item.weaponType == WeaponType.Garra)
        {
            foreach (Transform child in currentSlot.transform)
            {
                Destroy(child.gameObject);
            }
            currentSlot.ChangeSpriteOfSlot();
            return true;
        }

        return false;
    }

    public void RemoveItemFromQuickswap(int location)
    {
        QuickWeaponSlot currentSlot = quickSwapWeapons[location];

        foreach (Transform child in currentSlot.transform)
        {
            if(child.GetComponent<InventoryItem>().item.weaponType == WeaponType.Garra)
                Destroy(child.gameObject);
        }

        GameObject newSlot = Instantiate(quickSwapInventoryItemPrefab, currentSlot.transform);

        newSlot.GetComponent<InventoryItem>().item = garra;
        currentSlot.SetHoldingItem(garra);
        currentSlot.ChangeSpriteOfSlot();
    }

    public bool AddItem(Item item, int amount = 1)
    {
        bool result = false;
        InventoryMenu currentMenu = itemMenus[(int)item.itemType];

        //Encuentra algun hueco del mismo tipo
        for (int i = 0; i < currentMenu.menuSlots.Length; i++)
        {
            InventorySlot slot = currentMenu.menuSlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && 
                itemInSlot.item == item && 
                itemInSlot.itemCount < maxStackedItems &&
                itemInSlot.item.stackable)
            {
                itemInSlot.itemCount+= amount;
                itemInSlot.RefreshItemCount();
                result = true;
                break;
            }
        }

        //Encuentra un hueco libre
        if(!result)
        {
            for (int i = 0; i < currentMenu.menuSlots.Length; i++)
            {
                InventorySlot slot = currentMenu.menuSlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                if(itemInSlot == null)
                {
                    if(item.stackable)
                        SpawnNewItem(item, slot, amount);
                    else
                    {
                        for (int j = 0; j < amount; j++)
                        {
                            SpawnNewItem(item, currentMenu.menuSlots[i+j], 1);
                        }
                    }

                    result = true;
                    break;
                }
            }
        }

        if (result)
        {
            GameEventsManager.instance.inventoryEvents.ItemAdded(item);
            GameEventsManager.instance.miscEvents.ThingObtained(item.itemName);
        }

        return result;
    }

    public void SpawnNewItem(Item item, InventorySlot slot, int amount) 
    {
        GameObject newItemGameObj = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGameObj.GetComponent<InventoryItem>();

        inventoryItem.itemCount = amount;
        inventoryItem.InitializeItem(item, false);

    }

    public InventoryItem CheckIfHasItem(Item itemToFind)
    {
        foreach(InventoryMenu menu in itemMenus)
        {
            foreach(InventorySlot itemslot in menu.menuSlots)
            {
                InventoryItem itemInv = itemslot.GetComponentInChildren<InventoryItem>();

                if(itemInv)
                {
                    if(itemToFind == itemInv.item)
                    {
                        print($"item {itemToFind.name} encontrado");
                        return itemInv;
                    }
                }
            }
        }

        print($"no se encontro {itemToFind.name}");
        return null;
    }

    public InventoryItem CheckIfHasItem(string itemToFind)
    {
        foreach (InventoryMenu menu in itemMenus)
        {
            foreach (InventorySlot itemslot in menu.menuSlots)
            {
                InventoryItem itemInv = itemslot.GetComponentInChildren<InventoryItem>();

                if (itemInv)
                {
                    if (itemToFind == itemInv.item.itemName)
                    {
                        print($"item {itemToFind} encontrado");
                        return itemInv;
                    }
                }
            }
        }

        print($"no se encontro {itemToFind}");
        return null;
    }

    public bool HasItem(Item item, int amount = 1)
    {
        InventoryItem chosenItem = CheckIfHasItem(item);

        if (chosenItem != null && chosenItem.itemCount >= amount)
        {
            return true;
        }
        return false;
    }

    public bool HasItem(string itemName, int amount = 1)
    {
        InventoryItem chosenItem = CheckIfHasItem(itemName);

        if (chosenItem != null && chosenItem.itemCount >= amount)
        {
            return true;
        }
        return false;
    }
}
