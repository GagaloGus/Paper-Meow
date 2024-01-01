using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class RearrangeSkillHierarchy : MonoBehaviour
{
    public enum ArrangeType { Childs, Parents}
    public ArrangeType Tipo;

    public bool OrdenarSkills = false;
    // Update is called once per frame
    void Update()
    {
        if(OrdenarSkills) 
        {
            print("ordenando...");
            foreach (Skill skill in GetComponent<SkillManager>().allSkills)
            {
                if(Tipo == ArrangeType.Childs)
                {
                    foreach (Skill childSkill in skill.childSkills)
                    {
                        Skill parentSkill = Array.Find(childSkill.parentSkills.ToArray(), x => x == skill);
                        if (parentSkill != null) { break; }

                        childSkill.parentSkills.Add(skill);
                    }
                }
                else if(Tipo == ArrangeType.Parents)
                {
                    foreach (Skill parentSkill in skill.parentSkills)
                    {
                        Skill childSkill = Array.Find(parentSkill.childSkills.ToArray(), x => x == skill);
                        if (childSkill != null) { break; }

                        parentSkill.childSkills.Add(skill);
                    }
                }
                
                //Quita si hay algun componente "nulo" o "none" en las listas de hijos y padres
                for(int i = 0; i < skill.childSkills.Count; i++)
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

            print("skills ordenadas");
            OrdenarSkills = false;
        }

    }
}
