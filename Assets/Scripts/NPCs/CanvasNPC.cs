using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CanvasNPC : MonoBehaviour
{
    GameObject Canvas;
    TMP_Text nameText;
    Image iconQuest;

    

    private void Awake()
    {
        iconQuest = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        iconQuest.color = new Color(1,1,1,1);

        Canvas = GetComponentInChildren<Canvas>().gameObject;
        UpdateOwnName();
    }

    private void Update()
    {
        Canvas.transform.forward = Camera.main.transform.forward;
    }

    public void UpdateOwnName()
    {
        string name = GetComponent<DialogueTrigger>().info.name;
        nameText = GetComponentInChildren<TMP_Text>(true);

        nameText.text = name;
    }

    public void SetQuestIcon(Sprite sprite, Color color)
    {
        iconQuest.gameObject.SetActive(true);
        iconQuest.sprite = sprite;
        iconQuest.color = color;
    }

    public void DisableQuestIcon()
    {
        iconQuest.gameObject.SetActive(false);
    }

}

#if UNITY_EDITOR_WIN
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
#endif