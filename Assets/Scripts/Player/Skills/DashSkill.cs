using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Player/Skills/Dash")]
public class DashSkill : Skill
{
    public enum DashDirection { Left, Front, Right, Back }
    public DashDirection direction;

    [Tooltip("X es cuanto nos movemos en el eje que queremos, Y es cuanto nos movemos en el eje Y")]
    public Vector2 distances;

    public override void StartSkill()
    {
        base.StartSkill();        
    }
    public override void Use()
    {
        //X es cuanto nos movemos en el eje que queremos
        //Y es cuanto nos movemos en el eje Y
        player.GetComponent<Rigidbody>().velocity = 
            GetDirectionOfDash() * distances.x + Vector3.up * distances.y;
    }

    public override void SkillGizmo()
    {
        base.SkillGizmo();

        Vector3 direction = (GetDirectionOfDash() * distances.x / 10 + Vector3.up * distances.y / 5);

        Gizmos.DrawRay(player.transform.position, direction);
        Gizmos.DrawWireCube(player.transform.position + direction, Vector3.one/3);
    }

    Vector3 GetDirectionOfDash()
    {
        Vector3 dir = Vector3.one;
        if (direction == DashDirection.Left)
            dir = CoolFunctions.FlattenVector3(Camera.main.transform.right) * -1;

        else if (direction == DashDirection.Right)
            dir = CoolFunctions.FlattenVector3(Camera.main.transform.right);

        else if (direction == DashDirection.Front)
            dir = CoolFunctions.FlattenVector3(Camera.main.transform.forward);

        else if (direction == DashDirection.Back)
            dir = CoolFunctions.FlattenVector3(Camera.main.transform.forward) * -1;

        return dir;
    }
}
