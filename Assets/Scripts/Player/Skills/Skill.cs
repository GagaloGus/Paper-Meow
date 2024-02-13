using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public enum UnlockType { SkillTree, Quest}
    public enum Usability {Ground, Air, All}
    public string skillName;
    [TextArea(3,1)] public string skillDescription;

    public int skillID;
    public Sprite skillTreeSprite;
    public bool isUnlocked;
    public bool canBeUnlocked;
    public List<Skill> parentSkills = new();
    public List<Skill> childSkills = new();
    public float usingSkill, cooldown;

    public UnlockType unlockType;
    public Usability usablilty;

    [Tooltip("Solo util en Skill Tree")]
    public int moneyRequired;

    [Tooltip("Solo util en Quest")]
    public Item questItem;

    protected GameObject player;

    //Solo se ejecuta cuando seleccionamos la skill
    public virtual void StartSkill()
    {
        player = FindObjectOfType<SkoController>().gameObject;
    }
    
    //No es un Update, solo lo ejecuta cuando le damos a la tecla de skill
    public abstract void Use();

    public virtual void SkillGizmo()
    {

    }

    //Si las skills padres estan desbloqueadas se puede desbloquear esta
    public void CheckIfUnlockable()
    {
        foreach(Skill parent in parentSkills)
        {
            if(!parent.isUnlocked)
            {
                canBeUnlocked = false;
                return;
            }
        }

        canBeUnlocked = true;
    }

    public void UnlockSkill()
    {
        isUnlocked = true;
    }   
}

#if UNITY_EDITOR_WIN
[CustomEditor(typeof(Skill))]
public class SkillEditor : Editor
{
    Skill skill;

    private void OnEnable()
    {
        skill = (Skill)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (skill.skillTreeSprite == null)
            return;

        Texture2D texture = AssetPreview.GetAssetPreview(skill.skillTreeSprite);

        GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
    }
}
#endif

