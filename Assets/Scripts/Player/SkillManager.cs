using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Skill[] allSkills;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Skill skill in allSkills)
        {
            skill.canBeUnlocked = skill.CheckIfUnlockable();
        }
    }
}
