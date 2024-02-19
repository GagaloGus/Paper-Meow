using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hit Sphere", menuName = "Player/Skills/HitSphere")]
public class HitSphereSkill : Skill
{
    public float hitRadius;
    public float hitDamage;
    public override void StartSkill()
    {
        base.StartSkill();
    }

    public override void Use()
    {
        RaycastHit[] spherecast = Physics.SphereCastAll(player.transform.position, hitRadius, Vector3.up);

        foreach (RaycastHit obj in spherecast) 
        {
            if(obj.collider.GetComponent<Enemy>())
            {
                obj.collider.GetComponent<Enemy>().TakeDamage(hitDamage);
            }
        }
    }

    public override void SkillGizmo()
    {
        base.SkillGizmo();
        Gizmos.DrawWireSphere(player.transform.position, hitRadius);
    }
}
