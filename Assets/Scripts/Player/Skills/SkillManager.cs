using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public Skill[] allSkills;
    public Skill selectedSkill;
    string unlockedSkillIDs;

    GameObject player;
    public KeyCode useSkillKey;

    public float skillCooldownTimer;
    public bool skillUsed;
    void Awake()
    {
        if (!instance) //instance  != null  //Detecta que no haya otro GameManager en la escena.
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //Si hay otro GameManager lo destruye.
        }

        //le añade los IDs a las skills
        AssignIDsToSkills();

        //crea un string con todas las skills desbloqueadas
        unlockedSkillIDs = UnlockedSkills();

        //checkea si se pueden desbloquear las skills
        foreach (Skill skill in allSkills)
        {
            /*temporal*/ skill.isUnlocked = false;
            skill.CheckIfUnlockable();
        }

        skillUsed = false;
        player = FindObjectOfType<SkoController>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(useSkillKey) && !skillUsed)
        {
            selectedSkill.Use(player);
            skillCooldownTimer = selectedSkill.cooldown;
            skillUsed = true;
        }

        if(skillUsed)
        {
            print(Mathf.Round(10 * skillCooldownTimer)/10);

            skillCooldownTimer -= Time.deltaTime;
            skillUsed = skillCooldownTimer > 0;

        }
    }

    //Llamado desde los botones
    public void SelectSkill(Skill skill)
    {
        if(skill.isUnlocked) 
        { 
            ChangeSkill(skill); 
            return; 
        }
        else
        {
            UnlockSkillInTree(skill);
            foreach(Skill childSk in skill.childSkills)
            {
                childSk.CheckIfUnlockable();
            }
            return;
        }
    }
    void AssignIDsToSkills()
    {
        //cambia las ids a su orden en el array
        for (int i = 0; i < allSkills.Length; i++)
        {
            allSkills[i].skillID = i;
        }
    }

    void ChangeSkill(Skill newSkill)
    {
        skillCooldownTimer = newSkill.cooldown;
        selectedSkill = newSkill;
        newSkill.StartSkill(player);

        print($"skill seleccionada {newSkill.skillName}");
    }

    //crea un string con todas las skills desbloqueadas
    //ej. 1_7_12_18_
    string UnlockedSkills()
    {
        string idString = "";

        for(int i = 0; i < allSkills.Length; i++)
        {
            if (allSkills[i].isUnlocked)
            {
                idString += allSkills[i].skillID + "_";
            }
        }

        return idString;
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
                unlockedSkillIDs = UnlockedSkills();
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
        //Si la skill es tipo Quest
        else if (skill.unlockType == Skill.UnlockType.Quest)
        {
            //Tenemos que ver si el player tiene el objeto necesario en el inventario
        }
    }

    //para el cargado de datos
    public void SetUnlockedSkills(string idString)
    {
        //seprara el string en cada "numero" que tiene
        string[] spiltIDs = idString.Split('_');

        foreach (string stringID in spiltIDs)
        {
            //vuelve cada numero en un int
            int ID = (int.Parse(stringID));

            //busca la skill en el diccionario segun su ID
            Skill skill = allSkills[ID];

            //La desbloquea
            skill.UnlockSkill();
        }
    }

    //Automaticamente desbloquear una skill
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
