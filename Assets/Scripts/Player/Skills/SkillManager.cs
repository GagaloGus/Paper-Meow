using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public Skill[] allSkills;
    string unlockedSkillIDs;

    Dictionary<int, Skill> skillDic = new();

    GameObject player;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        //le añade los IDs a las skills
        AssignIDsToSkills();

        //crea un string con todas las skills desbloqueadas
        unlockedSkillIDs = UnlockedSkills();

        //checkea si se pueden desbloquear las skills
        foreach (Skill skill in allSkills)
        {
            skill.CheckIfUnlockable();
        }

        player = FindObjectOfType<SkoController>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignIDsToSkills()
    {
        //borra el contenido del diccionario por si acaso
        skillDic.Clear();

        //cambia las ids a su orden en el array y las añade al diccionario
        for (int i = 0; i < allSkills.Length; i++)
        {
            allSkills[i].skillID = i+1;

            skillDic.Add(allSkills[i].skillID, allSkills[i]);
        }
    }

    string UnlockedSkills()
    {
        string idString = "";

        //crea un string con todas las skills desbloqueadas
        //ej. 1_7_12_18_
        for(int i = 0; i < allSkills.Length; i++)
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
        //seprara el string en cada "numero" que tiene
        string[] spiltIDs = idString.Split('_');

        foreach (string stringID in spiltIDs)
        {
            //vuelve cada numero en un int
            int ID = (int.Parse(stringID));

            //busca la skill en el diccionario segun su ID
            Skill skill = skillDic[ID];

            //La desbloquea
            skill.UnlockSkill();
        }
    }


    public void UnlockSkillInTree(Skill skill)
    {
        SkoStats playerStats = player.GetComponent<SkoStats>();

        //Si es un tipo de SkillTree
        if(skill.unlockType == Skill.UnlockType.SkillTree)
        {
            //Si tienes todos los requisitos
            if(skill.canBeUnlocked && playerStats.money >= skill.moneyRequired)
            {
                GetSkill(skill);
                playerStats.money -= skill.moneyRequired;

                Debug.Log($"{skill.skillName} desbloqueada");
            }

            //Si no puede ser desbloqueada
            else if (!skill.canBeUnlocked)
            {
                Debug.Log("Desbloquea las anteriores skills antes");
            }

            //Si te falta dinero
            else if(playerStats.money < skill.moneyRequired)
            {
                Debug.Log("Te falta dinero :(");
            }

            //Si ya esta desbloqueada
            else if(skill.isUnlocked)
            {
                Debug.Log($"La skill {skill.skillName} ya esta desbloqueada");
            }

            //Si algo extra pasa
            else
            {
                Debug.Log("Ni idea de porque pero no puedes desbloquearla");
            }
        }
        //Si la skill no es de tipo Skilltree
        else
        {
            Debug.Log("Necesitas algo para desbloquear esta skill");
        }
    }

    //Automaticamente desbloquear una skill, sirve para los quests
    public void GetSkill(Skill skill)
    {
        skill.UnlockSkill();
    }

    //lo usaremos en el guardado de datos
    public string get_UnlockIDs
    {
        get { return unlockedSkillIDs; }
    }
}
