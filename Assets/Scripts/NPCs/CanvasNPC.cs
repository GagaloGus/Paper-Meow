using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class CanvasNPC : MonoBehaviour
{ 
    TMP_Text nameText;
    Image iconQuest;
    public Sprite cannotStart, canStart, cannotFinish, canFinish;

    private void Start()
    {
        iconQuest = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        GetComponentInChildren<Canvas>().transform.forward = Camera.main.transform.forward;
    }

    public void UpdateOwnName()
    {
        string name = GetComponent<DialogueTrigger>().info.name;
        nameText = GetComponentInChildren<TMP_Text>(true);

        nameText.text = name;
    }

    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {
        iconQuest.sprite = null;

        switch (newState)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                if (startPoint) { iconQuest.sprite = cannotStart; }
                break;
            case QuestState.CAN_START: 
                if (startPoint) { iconQuest.sprite = canStart; }
                break;
            case QuestState.IN_PROGRESS:
                if (finishPoint) {  iconQuest.sprite = cannotFinish; }
                break;
            case QuestState.CAN_FINISH:
                if (finishPoint) {  iconQuest.sprite = canFinish; }
                break;
            case QuestState.FINISHED: 
                break;
        }
    }

}

[CustomEditor(typeof(CanvasNPC))]
class CanvasNPCEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Update All Names", GUILayout.Height(30)))
        {
            List<CanvasNPC> names = FindObjectsOfType<CanvasNPC>().ToList();
            foreach (CanvasNPC name in names)
            {
                name.UpdateOwnName();
            }
            Debug.Log($"All {names.Count} names updated");
        }

        GUILayout.EndHorizontal();

    }
}
