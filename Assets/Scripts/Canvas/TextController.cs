using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public TMP_Text coinText, coinTextInventory;
    // Start is called before the first frame update
    void Start()
    {
        coinText.text = GameManager.instance.money.ToString();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onCoinCollected += MoneyUpdate;
        GameEventsManager.instance.inventoryEvents.onInventoryOpen += UpdateInventoryMoney;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onCoinCollected -= MoneyUpdate;
        GameEventsManager.instance.inventoryEvents.onInventoryOpen -= UpdateInventoryMoney;
    }

    void MoneyUpdate(int temp)
    {
        coinText.text = GameManager.instance.coinCount.ToString();
        coinText.gameObject.GetComponentInParent<Animator>().SetTrigger("moneyUpdate");
    }

    void UpdateInventoryMoney(bool temp)
    {

        coinTextInventory.text = GameManager.instance.coinCount.ToString();
    }
}
