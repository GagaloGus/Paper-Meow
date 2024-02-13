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
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += UpdateQuestText;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= UpdateQuestText;
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = GameManager.instance.coinCount.ToString();
    }

    void UpdateQuestText(Quest quest)
    {
        questDisplayText.gameObject.SetActive(true);
        questDisplayText.text = $"Quest: {quest.info.displayName}";
    }
}
