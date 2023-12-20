using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public Skill[] allSkills;
    string unlockedSkillIDs;

    Dictionary<int, Skill> skillDic;

    void Awake()
    {
        if (!instance) //instance  != null  //Detecta que no haya otro Manager en la escena.
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //Si hay otro Manager lo destruye.
        }

        AssignIDsToSkills();
        unlockedSkillIDs = UnlockedSkills();
    }

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

    public void AssignIDsToSkills()
    {
        for (int i = 0; i < allSkills.Length; i++)
        {
            allSkills[i].skillID = i+1;

            skillDic.Add(allSkills[i].skillID, allSkills[i]);
        }
    }

    string UnlockedSkills()
    {
        string idString = "";

        for(int i = 0;i < allSkills.Length; i++)
        {
            if (allSkills[i].isUnlocked)
            {
                idString += allSkills[i].skillID + "_";
            }
        }

        return idString;
    }

    //lo usaremos en el cargado de datos
    public void SetUnlockedSkills(string idString)
    {
        string[] spiltIDs = idString.Split('_');

        foreach (string stringID in spiltIDs)
        {
            int ID = (int.Parse(stringID));

            Skill skill = skillDic[ID];
            
            skill.isUnlocked = true;
        }
    }

    //lo usaremos en el guardado de datos
    public string get_UnlockIDs
    {
        get { return unlockedSkillIDs; }
    }
}
