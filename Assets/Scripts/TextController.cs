using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public TMP_Text coinText;
    public TMP_Text questDisplayText;
    // Start is called before the first frame update
    void Start()
    {
        questDisplayText.gameObject.SetActive(false);
        coinText.text = GameManager.instance.money.ToString();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += UpdateQuestText;
        GameEventsManager.instance.miscEvents.onCoinCollected += MoneyUpdate;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= UpdateQuestText;
        GameEventsManager.instance.miscEvents.onCoinCollected -= MoneyUpdate;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateQuestText(Quest quest)
    {
        questDisplayText.gameObject.SetActive(true);
        questDisplayText.text = $"Quest: {quest.info.displayName}";
    }

    void MoneyUpdate()
    {
        coinText.text = GameManager.instance.coinCount.ToString();
        coinText.gameObject.GetComponentInParent<Animator>().SetTrigger("moneyUpdate");
    }
}
