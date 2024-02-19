using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Interactable))]
public class DialogueTrigger : MonoBehaviour
{
    [Header("Sound Effects")]
    public AudioClip[] typingSfxs;

    [Header("Dialogues")]
    public NPCData info;
    [SerializeField] public List<DialogueString> dialogueStrings = new List<DialogueString>();



    public void StartDialogue()
    {
        info.originalRot = transform.rotation;
        FindObjectOfType<DialogueManager>().DialogueStart(dialogueStrings, info,gameObject, typingSfxs);
    }

    public void AddIndexToDialogues()
    {
        for (int i = 0; i < dialogueStrings.Count; i++)
        {
            dialogueStrings[i].index = i;
        }
    }

    public void GiveItem(Item item)
    {
        InventoryManager.instance.AddItem(item);
    }
}

public enum AnimationTypes { Normal, Laugh, Angry }

[System.Serializable]
public class DialogueString
{
    [TextArea(3, 5)] public string text;
    public bool isEnd;
    public int index;
    public AnimationTypes specialNPCAnimation;

    [Header("Who is talking?")]
    public bool NPCTalks = true;

    public Sprite NPCIcon;
    public string newName;
    public Sprite newIcon;

    [Header("Question")]
    public bool isQuestion;

    [Tooltip("No puede haber mas de 4 opciones")] public List<ButtonAnswer> optionButtons;

    [Header("Jump Index Option")]
    [Tooltip("Solo funciona si es distinto de 0")] public uint jumpToIndex;

    [Header("Triggered Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;
}

[System.Serializable]
public class ButtonAnswer
{
    public string answer;
    public int jumpIndex;
}

#if UNITY_EDITOR_WIN
[CustomEditor(typeof(DialogueTrigger))]
class DialogueEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DialogueTrigger myscript = (DialogueTrigger)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add index number to dialogues", GUILayout.Height(30)))
        {
            myscript.AddIndexToDialogues();
        }

        GUILayout.EndHorizontal();
    }
}
#endif

