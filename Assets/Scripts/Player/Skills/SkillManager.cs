using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public Skill[] allSkills;
    public Skill selectedSkill;
    string unlockedSkillIDs;

    public GameObject player;
    KeyCode useSkillKey;

    public float skillCooldownTimer;
    bool skillUsed;

    bool skillUsable;
    void Awake()
    {
        #region manager instance
        if (!instance) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
        #endregion

        //le a�ade los IDs a las skills
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
        useSkillKey = PlayerKeybinds.useSkill;
    }


    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<SkoController>().player_canMove) 
        {
            if (Input.GetKeyDown(useSkillKey) && !skillUsed && skillUsable)
            {
                if(selectedSkill != null)
                {
                    player.GetComponent<SkoController>().StartSkillCooldownCoroutine(selectedSkill.usingSkill);
                    selectedSkill.Use();


                    skillCooldownTimer = selectedSkill.cooldown;
                    skillUsed = true;

                    print($"{selectedSkill.skillName} usada!");

                    GameEventsManager.instance.playerEvents.SkillUsed(selectedSkill);

                    StopCoroutine(nameof(ChangeGizmoCol));
                    StartCoroutine(ChangeGizmoCol());
                }
                else
                {
                    print("Selecciona una skill primero");
                }
            }

            if(selectedSkill != null)
            {
                if(selectedSkill.usablilty == Skill.Usability.Ground && player.GetComponent<SkoController>().player_isGrounded) { skillUsable = true; }
                else if(selectedSkill.usablilty == Skill.Usability.All) {  skillUsable = true; }
                else { skillUsable = false; }
            }
        }


        if(skillUsed)
        {
            //Contador
            float time = Mathf.Round(100*skillCooldownTimer)/100;
            if (time % 0.5 == 0){ print(time); }

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

    void ChangeSkill(Skill newSkill)
    {
        //Empieza el contador de la skill que le hayamos puesto
        skillCooldownTimer = newSkill.cooldown;
        selectedSkill = newSkill;
        newSkill.StartSkill();

        GameEventsManager.instance.playerEvents.SkillSwapped(newSkill);
        print($"skill seleccionada {newSkill.skillName}");
    }

    public void UnlockSkillInTree(Skill skill)
    {
        SkoStats playerStats = player.GetComponent<SkoStats>();

        //Si es un tipo de SkillTree
        if(skill.unlockType == Skill.UnlockType.SkillTree)
        {
            //Si tienes todos los requisitos
            if(skill.canBeUnlocked && GameManager.instance.money >= skill.moneyRequired)
            {
                GetSkill(skill);
                GameManager.instance.money -= skill.moneyRequired;

                Debug.Log($"{skill.skillName} desbloqueada");
                unlockedSkillIDs = UnlockedSkills();
            }

            //Si no puede ser desbloqueada
            else if (!skill.canBeUnlocked)
            {
                Debug.Log("Desbloquea las anteriores skills antes");
            }

            //Si te falta dinero
            else if(GameManager.instance.money < skill.moneyRequired)
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


    //Automaticamente desbloquear una skill
    public void GetSkill(Skill skill)
    {
        skill.UnlockSkill();
    }

    void AssignIDsToSkills()
    {
        //cambia las ids a su orden en el array
        for (int i = 0; i < allSkills.Length; i++)
        {
            allSkills[i].skillID = i;
        }
    }

    //para el cargado de datos
    public void SetUnlockedSkills(string idString)
    {
        AssignIDsToSkills();
        
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

    //lo usaremos en el guardado de datos
    //crea un string con todas las skills desbloqueadas
    //ej. 1_7_12_18_
    public string UnlockedSkills()
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

    Color gizmoCol = Color.green;
    private void OnDrawGizmos()
    {
        if(selectedSkill != null)
        {
            Gizmos.color = gizmoCol;
            selectedSkill.SkillGizmo();
        }
    }

    IEnumerator ChangeGizmoCol()
    {
        float lapso = 0.2f;
        float espera = selectedSkill.cooldown/lapso;

        for(float i = 0;i <= 1 ;i+= lapso/espera)
        {
            gizmoCol = new Color(1-i, i, 0);
            yield return new WaitForSeconds(lapso/espera);
        } 

    }
}
