using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Quest;

public class DisplayObtained : MonoBehaviour
{
    public GameObject textoObtenido;

    [Header("Quest Displays")]
    public Transform questAddDisplay, questFinishDisplay;
    [SerializeField] TMP_Text questATitle, questADescription, questAProgress, questFTitle, questFDescription, questFProgress;

    public Quest followingQuest;

    private void Start()
    {
        questAddDisplay = transform.Find("QuestDisplay").Find("QuestAddedDisplay");
        questFinishDisplay = transform.Find("QuestDisplay").Find("QuestFinishedDisplay");

        questATitle = questAddDisplay.Find("Title").GetComponent<TMP_Text>();
        questADescription = questAddDisplay.Find("Objective").GetComponent<TMP_Text>();
        questAProgress = questAddDisplay.Find("Progress").GetComponent<TMP_Text>();
        questFTitle = questFinishDisplay.Find("Title").GetComponent<TMP_Text>();
        questFDescription = questFinishDisplay.Find("Objective").GetComponent<TMP_Text>();
        questFProgress = questFinishDisplay.Find("Progress").GetComponent<TMP_Text>();


        questAddDisplay.gameObject.SetActive(false);
        questFinishDisplay.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onThingObtained += DisplayThingObtained;
        GameEventsManager.instance.npcEvents.onAddedQuest += ShowAddedQuest;
        GameEventsManager.instance.npcEvents.onFinishedQuest += ShowFinishedQuest;
        GameEventsManager.instance.npcEvents.onFollowQuest += ShowFollowingQuest;
        GameEventsManager.instance.npcEvents.onUnfollowQuest += HideFollowingQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onThingObtained -= DisplayThingObtained;
        GameEventsManager.instance.npcEvents.onAddedQuest -= ShowAddedQuest;
        GameEventsManager.instance.npcEvents.onFinishedQuest -= ShowFinishedQuest;
        GameEventsManager.instance.npcEvents.onFollowQuest -= ShowFollowingQuest;
        GameEventsManager.instance.npcEvents.onUnfollowQuest -= HideFollowingQuest;
    }

    #region Items
    void DisplayThingObtained(string name, int amount, Sprite icon, Rarity thingRarity)
    {
        StartCoroutine(DisplayItem(name,amount,icon, thingRarity));
    }

    IEnumerator DisplayItem(string name, int amount, Sprite icon, Rarity thingRarity)
    {        
        yield return new WaitForSeconds(0.75f);
        GameObject texto = Instantiate(textoObtenido);
        texto.GetComponent<Animator>().Play("enter");
        texto.transform.SetParent(transform.Find("ObtainedTextPlaceholder"));

        Transform textoParent = texto.transform.Find("thingParent");
        textoParent.GetComponent<Image>().color = CoolFunctions.ChangeRarityColor(thingRarity);  

        texto.transform.localScale = Vector3.one;
        TMP_Text textoName = textoParent.Find("name").GetComponent<TMP_Text>();
        TMP_Text textoAmount = textoParent.Find("amount").GetComponent<TMP_Text>();
        Image textoIcon = textoParent.Find("icon").GetComponent<Image>();

        textoName.text = name;
        textoAmount.text = $"x{amount}";
        textoIcon.sprite = icon;
    }
    #endregion

    #region Quests

    void ShowAddedQuest(Quest quest)
    {
        HideFollowingQuest();
        questAddDisplay.gameObject.SetActive(true);
        questAddDisplay.gameObject.GetComponent<Animator>().SetTrigger("play");
        questAddDisplay.gameObject.GetComponent<Animator>().SetBool("hide", true);

        questATitle.text = quest.title;
        questADescription.text = quest.description;
        questAProgress.text = $"{quest.questObjective}: {quest.questObjectiveCount}/{quest.questObjectiveRequirement}";

    }

    void ShowFinishedQuest(Quest quest)
    {
        HideFollowingQuest();
        displayFollowingQuest = false;
        questFinishDisplay.gameObject.GetComponent<Animator>().SetTrigger("play");
        questFinishDisplay.gameObject.SetActive(true);

        questFTitle.text = quest.title;
        questFDescription.text = quest.description;
        questFProgress.text = $"";
    }

    public bool displayFollowingQuest;
    void ShowFollowingQuest(Quest quest)
    {
        followingQuest = quest;
        questAddDisplay.gameObject.SetActive(true);
        questAddDisplay.gameObject.GetComponent<Animator>().SetTrigger("play");
        questAddDisplay.gameObject.GetComponent<Animator>().SetBool("hide", false);

        displayFollowingQuest = true;
    }

    private void Update()
    {
        if (displayFollowingQuest)
        {
            questATitle.text = followingQuest.title;
            questADescription.text = followingQuest.description;
            questAProgress.text = $"{followingQuest.questObjective}: {followingQuest.questObjectiveCount}/{followingQuest.questObjectiveRequirement}";
        }
        else
        {
            return;
        }

    }

    void HideFollowingQuest()
    {
        displayFollowingQuest = false;
        followingQuest = null;
        questAddDisplay.gameObject.GetComponent<Animator>().SetBool("hide", true);
    }

    #endregion
}
