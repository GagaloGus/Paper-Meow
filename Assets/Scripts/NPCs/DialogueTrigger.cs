using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] List<DialogueString> dialogueStrings = new List<DialogueString>();

    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().DialogueStart(dialogueStrings, gameObject);
    }

    public void AddIndexToDialogues()
    {
        for (int i = 0; i < dialogueStrings.Count; i++)
        {
            dialogueStrings[i].index = i;
        }
    }
}

[System.Serializable]
public class DialogueString
{
    public string text;
    public bool isEnd;
    public int index;

    [Header("Branch")]
    public bool isQuestion;
    public List<ButtonAnswer> optionButtons;

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
class AddIndexToDialogues : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueTrigger myscript = (DialogueTrigger)target;
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add index to numbers", GUILayout.Height(30)))
        {
            myscript.AddIndexToDialogues();
        }

        GUILayout.EndHorizontal();
    }
}
