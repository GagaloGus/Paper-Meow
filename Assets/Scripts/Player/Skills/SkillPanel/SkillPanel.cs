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
        allSkills = SkillManager.instance.allSkills;

        int childs = transform.childCount;
        GameObject[] temp = new GameObject[childs];
        for (int i = 0; i < childs; i++)
        {
            temp[i] = transform.GetChild(i).gameObject;
        }

        allButtons = temp;

        int count = 0;
        foreach(GameObject no in allButtons)
        {
            GameObject button = allButtons[count]; Skill skill = allSkills[count];

            button.GetComponent<Button>().onClick.
                AddListener(() => { AddSkillToButton(button, skill); });

            ChangeButtonData(button, skill);
            ChangeColorOfButton(button, skill);

            TMP_Text id = button.transform.Find("id").gameObject.GetComponent<TMP_Text>();
            id.text = count.ToString();

            count++;
        }
    }
    public void AddSkillToButton(GameObject button, Skill skill)
    {
        SkillManager.instance.SelectSkill(skill);
        ChangeColorOfButton(button, skill);
    }

    void ChangeButtonData(GameObject button, Skill skill)
    {
        TMP_Text name = button.transform.Find("name").gameObject.GetComponent<TMP_Text>();
        name.text = skill.skillName;

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
