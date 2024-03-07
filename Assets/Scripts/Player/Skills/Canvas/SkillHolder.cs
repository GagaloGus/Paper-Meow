using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillHolder : MonoBehaviour
{
    [SerializeField] Image skillImage;
    [SerializeField] TMP_Text skillCooldown;

    // Start is called before the first frame update
    void Start()
    {
        skillImage = transform.Find("SkillSelected").GetComponent<Image>();
        skillCooldown = GetComponentInChildren<TMP_Text>();
        skillCooldown.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onSkillSwapped += SkillSwapped;
        GameEventsManager.instance.playerEvents.onSkillUsed += SkillUsed;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onSkillSwapped -= SkillSwapped;
        GameEventsManager.instance.playerEvents.onSkillUsed -= SkillUsed;
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SkillSwapped(Skill newSkill)
    {
        skillImage.sprite = newSkill.skillTreeSprite;
    }

    void SkillUsed(Skill skillUsed)
    {
        skillImage.color = Color.grey;
        skillCooldown.gameObject.SetActive(true);
        skillCooldown.text = skillUsed.cooldown.ToString();
        StartCoroutine(SkillCooldownDisplay(skillUsed));
    }

    IEnumerator SkillCooldownDisplay(Skill skill)
    {
        SkillManager skillManager = FindObjectOfType<SkillManager>();
        while(skillManager.skillCooldownTimer > 0)
        {
            float time = Mathf.Round(10 * skillManager.skillCooldownTimer) / 10;

            skillCooldown.text = time.ToString();
            yield return null;
        }

        skillCooldown.gameObject.SetActive(false);
        skillImage.color = Color.white;
    }
}
