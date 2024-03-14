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
        GameEventsManager.instance.playerEvents.onPlayerDeath += PlayerDied;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onCoinCollected -= MoneyUpdate;
        GameEventsManager.instance.inventoryEvents.onInventoryOpen -= UpdateInventoryMoney;
        GameEventsManager.instance.playerEvents.onPlayerDeath -= PlayerDied;
    }

    void MoneyUpdate(int coinAmount)
    {
        coinText.text = GameManager.instance.coinCount.ToString();
        coinText.gameObject.GetComponentInParent<Animator>().SetTrigger("moneyUpdate");

        GameObject coinObtained = coinText.transform.parent.Find("coins obtained").gameObject;
        coinObtained.SetActive(true);
        coinObtained.GetComponent<Animator>().SetTrigger("obtain");
        coinObtained.GetComponent<TMP_Text>().text = $"+{coinAmount}";
    }

    void UpdateInventoryMoney()
    {
        coinTextInventory.text = GameManager.instance.coinCount.ToString();
    }

    void PlayerDied()
    {
        Animator thingAnimator = transform.Find("menuButton").GetComponent<Animator>();
        thingAnimator.SetTrigger("died");
    }
}
