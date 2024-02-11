using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using static Unity.VisualScripting.Member;

[RequireComponent (typeof(Interactable))]
public class DialogueTrigger : MonoBehaviour
{
    public NPCData info;
    [SerializeField] List<DialogueString> dialogueStrings = new List<DialogueString>();

    public void StartDialogue()
    {
        info.originalRot = transform.rotation;
        FindObjectOfType<DialogueManager>().DialogueStart(dialogueStrings, info,gameObject);
    }

    public void AddIndexToDialogues()
    {
        for (int i = 0; i < dialogueStrings.Count; i++)
        {
            dialogueStrings[i].index = i;
        }
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


