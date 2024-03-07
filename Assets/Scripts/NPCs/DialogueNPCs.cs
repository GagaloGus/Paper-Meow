using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueNPCs : MonoBehaviour
{
    public Dictionary<string, NPCBehaviour> npcDictionary = new Dictionary<string, NPCBehaviour>();
    public NPCBehaviour npcTalking;
    public List<NPCBehaviour> interatcedNPCs = new List<NPCBehaviour>();

    private void Awake()
    {
        List<NPCBehaviour> list = FindObjectsByType<NPCBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        InitializeDictionary(list);
        npcTalking = null;
        interatcedNPCs.Clear();
    }

    public void InitializeDictionary(List<NPCBehaviour> listNPCs)
    {
        string names = "";
        foreach (NPCBehaviour npc in listNPCs)
        {
            string name = npc.npcData.ID_name;

            npcDictionary.Add(name, npc);

            names += $"{(name != "" ? name : $"(default) {npc.gameObject.name}")} \n";
        }

        Debug.Log(names);
    }

    public void NPCTalking(string npcName)
    {
        if(npcName != "")
        {
            if(npcDictionary.ContainsKey(npcName))
            {
                NPCTalking(npcDictionary[npcName]);
            }
            else
            {
                Debug.LogWarning($"Npc name {npcName} no esta en el diccionario");
            }
        }
    }

    public void NPCTalking(NPCBehaviour npc)
    {
        npcTalking = npc;

        if(!interatcedNPCs.Contains(npc))
        {
            interatcedNPCs.Add(npc);
            npc.RotateTowardsPlayer();
        }

        Invoke("CheckAnimation", 0.1f);
        npcTalking.npcGameObject.GetComponent<Animator>().SetBool("isTalking", true);
    }

    void CheckAnimation()
    {
        Animator npcAnimator = npcTalking.npcGameObject.GetComponent<Animator>();
        string nombreAnimacion = npcAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (nombreAnimacion == "idle" && GameManager.instance.isInteracting)
        {
            npcTalking.npcGameObject.GetComponent<Animator>().Play("talk");
        }
    }

    public void NPCStopTalking()
    {
        npcTalking.npcGameObject.GetComponent<Animator>().SetBool("isTalking", false);
    }

    public NPCBehaviour GetNPC(string npcName) 
    {
        if (npcName != "")
        {
            return npcDictionary[npcName];
        }

        return npcTalking;
    }

    public void StopInteraction()
    {
        foreach(NPCBehaviour npc in interatcedNPCs)
        {
            npc.RotateTowardsOriginalRot();
            npc.GetComponent<Interactable>().enabled = true;
        }
        interatcedNPCs.Clear();
    }
}
