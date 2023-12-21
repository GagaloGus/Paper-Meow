using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnBola))]
public class CustomEditorSkill : Editor
{
    int width = 150;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        SpawnBola script = (SpawnBola)target;
        EditorGUILayout.BeginHorizontal();

        if(script.unlockType == Skill.UnlockType.SkillTree)
        {
            EditorGUILayout.LabelField("Can Be Unlocked", GUILayout.MaxWidth(width));
            script.canBeUnlocked = EditorGUILayout.Toggle(script.canBeUnlocked);

            //EditorGUILayout.LabelField("Money Required", GUILayout.MaxWidth(width));
            //script.moneyRequired = EditorGUILayout.IntField(script.moneyRequired);

            //(GameObject)EditorGUILayout.ObjectField(GameObject, typeof(GameObject), true);
        }
        else if(script.unlockType == Skill.UnlockType.Quest)
        {

        }

        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(script);
    }
}
