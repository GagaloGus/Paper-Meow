using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public GameObject[] allButtons;

    Skill[] allSkills;
    // Start is called before the first frame update
    void Start()
    {
        SkillManager skillManager = FindObjectOfType<SkillManager>();
        //se guarda el array de todas las skills
        allSkills = FindObjectOfType<SkillManager>().allSkills;
        
        //guarda todos los hijos del skill panel en orden el el array allButtons
        int childs = transform.childCount;
        GameObject[] temp = new GameObject[childs];
        for (int i = 0; i < childs; i++)
        {
            temp[i] = transform.GetChild(i).gameObject;
        }
        allButtons = temp;

        //bucle foreach solo por conveniencia, la variable "no" no se usa
        int count = 0;
        foreach(GameObject no in allButtons)
        {
            //se guarda el boton y la skill que toca
            GameObject button = allButtons[count]; Skill skill = allSkills[count];

            //le añade un onclick al boton
            button.GetComponent<Button>().onClick.
                AddListener(() => { AddSkillToButton(button, skill); });

            //cambia los datos visuales del boton
            ChangeButtonData(button, skill);

            //cambia el color del boton
            ChangeColorOfButton(button, skill);

            //pone el id de la skill para guiarnos
            TMP_Text id = button.transform.Find("id").gameObject.GetComponent<TMP_Text>();
            id.text = skill.skillID.ToString();

            count++;
        }
    }
    public void AddSkillToButton(GameObject button, Skill skill)
    {
        //checkea si se puede desbloquear la skill
        FindObjectOfType<SkillManager>().SelectSkill(skill);

        //le cambia el color al boton
        ChangeColorOfButton(button, skill);
    }

    void ChangeButtonData(GameObject button, Skill skill)
    {
        //cambia el nombre que se ve abajo del boton para que sea el mismo que el de la skill
        TMP_Text name = button.transform.Find("name").gameObject.GetComponent<TMP_Text>();
        name.text = skill.skillName;

        TMP_Text money = button.transform.Find("money").GetComponent<TMP_Text>();
        money.text = $"Cost: {skill.moneyRequired}";

        //le cambia el sprite
        button.GetComponent<Image>().sprite = skill.skillTreeSprite;
    }


    void ChangeColorOfButton(GameObject button, Skill skill)
    {
        if (skill.isUnlocked)
        {
            button.GetComponent<Image>().color = Color.white;
        }
        else
        {
            button.GetComponent<Image>().color = Color.gray;
        }
    }
}
