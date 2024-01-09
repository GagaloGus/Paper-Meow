using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class RearrangeSkillHierarchy : MonoBehaviour
{
    public void ArrangeParentSkills()
    {
        print("ordenando...");

        foreach (Skill skill in GetComponent<SkillManager>().allSkills)
        {
            foreach (Skill childSkill in skill.childSkills)
            {
                Skill parentSkill = Array.Find(childSkill.parentSkills.ToArray(), x => x == skill);
                if (parentSkill != null) { continue; }

                childSkill.parentSkills.Add(skill);
            }

            CheckIfNull(skill);
        }

        print("skills padres ordenadas");
    }

    public void ArrangeChildSkills()
    {
        print("ordenando...");

        foreach (Skill skill in GetComponent<SkillManager>().allSkills)
        {
            foreach (Skill parentSkill in skill.parentSkills)
            {
                Skill childSkill = Array.Find(parentSkill.childSkills.ToArray(), x => x == skill);
                if (childSkill != null) { continue; }

                parentSkill.childSkills.Add(skill);
            }

            CheckIfNull(skill);
        }

        print("skills hijas ordenadas");

    }

    //Quita si hay algun componente "nulo" o "none" en las listas de hijos y padres
    public void CheckIfNull(Skill skill)
    {
        for (int i = 0; i < skill.childSkills.Count; i++)
        {
            if (skill.childSkills[i] == null)
            {
                skill.childSkills.RemoveAt(i);
            }
        }

        for (int i = 0; i < skill.parentSkills.Count; i++)
        {
            if (skill.parentSkills[i] == null)
            {
                skill.parentSkills.RemoveAt(i);
            }
        }
    }
}

[CustomEditor(typeof(RearrangeSkillHierarchy))]
class BotonTrucoParaOrdenarSkills : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RearrangeSkillHierarchy myscript = (RearrangeSkillHierarchy)target;
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Arrange Parent Skills", GUILayout.Height(30)))
        {
            myscript.ArrangeParentSkills();

        }
        


        if (GUILayout.Button("Arrange Child Skills", GUILayout.Height(30)))
        {
            myscript.ArrangeChildSkills();

        }

        GUILayout.EndHorizontal();

    }
}
