using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Skill))]
public class CustomEditorSkill : Editor
{
    /*
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        Skill script = (Skill)target;
        EditorGUILayout.BeginHorizontal();

        if(script.unlockType == Skill.UnlockType.SkillTree)
        {
            script.canBeUnlocked = EditorGUILayout.Toggle("Can be unlocked",script.canBeUnlocked);

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            script.moneyRequired = EditorGUILayout.IntField("Money Required", script.moneyRequired);
        }
        else if(script.unlockType == Skill.UnlockType.Quest)
        {
            script.questItem = (Quest)EditorGUILayout.ObjectField("Quest",script.questItem, typeof(Quest), true);
        }

        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(script);
    }*/
}
