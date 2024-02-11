using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class DisplayName : MonoBehaviour
{ 
    TMP_Text nameText;

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


}

[CustomEditor(typeof(DisplayName))]
class DisplayNameEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Update All Names", GUILayout.Height(30)))
        {
            List<DisplayName> names = FindObjectsOfType<DisplayName>().ToList();
            foreach (DisplayName name in names)
            {
                name.UpdateOwnName();
            }
            Debug.Log($"All {names.Count} names updated");
        }

        GUILayout.EndHorizontal();

    }
}
