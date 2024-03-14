using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Unity.VisualScripting.FullSerializer;
using System;

public class InkExternalFunctions
{
    public void Bind(Story story, NPCBehaviour npcBehaviour)
    {
        story.BindExternalFunction("playEmote", (string emoteName) => {
            PlayEmote(emoteName, npcBehaviour);
        });

        story.BindExternalFunction("changeMood", (string npcID, string emoteName) => {
            ChangeMood(npcID, emoteName);
        });

        story.BindExternalFunction("giveQuest", (string questID) => {
            GiveQuest(questID);
        });

        story.BindExternalFunction("finishQuest", (string questID) => {
            FinishQuest(questID);
        });

        story.BindExternalFunction("giveItem", (string itemID, int amount) => {
            GiveItem(itemID, amount , npcBehaviour);
        });

        story.BindExternalFunction("startPatrol", (string npcId, int listNumber, string run) => {
            bool isRun = run == "true";
            StartPatrolOfNPC(npcId, listNumber, isRun);
        });

        story.BindExternalFunction("getPegatina", (int number) => {
            EnablePegatina(number);
        });
    }

    private void FinishQuest(string questID)
    {
        QuestManager.instance.AutoFinishQuest(questID);
    }

    private void StartPatrolOfNPC(string npcId, int listNumber, bool isRun)
    {
        NPCBehaviour nPCBehaviour = DialogueManager.instance.dialogueNPCs.GetNPC(npcId);
        if (nPCBehaviour != null)
        {
            nPCBehaviour.npcGameObject.GetComponent<PatrolPointsNPC>().Avanzar(listNumber, isRun);
        }
    }

    private void EnablePegatina(int number)
    {
        UnityEngine.Object.FindObjectOfType<PauseMenu>(true).EnablePegatina(number);
    }

    public void UnBind(Story story)
    {
        story.UnbindExternalFunction("playEmote");
        story.UnbindExternalFunction("changeMood");
        story.UnbindExternalFunction("giveQuest");
        story.UnbindExternalFunction("giveItem");
    }

    void PlayEmote(string emoteName, NPCBehaviour npcBehaviour)
    {
        npcBehaviour.npcData.iconAnimator?.Play(emoteName);
    }

    void ChangeMood(string npcId,string emoteName)
    {
        NPCBehaviour npc = DialogueManager.instance.GetNPC(npcId);
        npc.npcGameObject.GetComponent<Animator>().Play(emoteName);
    }

    void GiveQuest(string quest_ID)
    {
        QuestManager.instance.QuestRequest(quest_ID);
    }
    private void GiveItem(string itemID, int amount, NPCBehaviour npcBehaviour)
    {
        if(itemID == "money") { GameEventsManager.instance.miscEvents.CoinCollected(amount); }
        else if(itemID == "xp") { GameEventsManager.instance.miscEvents.ExperienceGained(amount); }
        else { InventoryManager.instance.AddItem(itemID, amount); }   
    }
}
