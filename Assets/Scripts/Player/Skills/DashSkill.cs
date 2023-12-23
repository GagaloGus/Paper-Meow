using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Player/Skills/Dash")]
public class DashSkill : Skill
{
    public float dashDistance;
    public enum DashDirection { Left, Front, Right, Back }
    public DashDirection direction;

    Vector3 directionDash;
    float timer;

    public override void StartSkill(GameObject owner)
    {
        base.StartSkill(owner);

        if (direction == DashDirection.Left)
            directionDash = owner.transform.right * -1;

        else if (direction == DashDirection.Right)
            directionDash = owner.transform.right;

        else if (direction == DashDirection.Front)
            directionDash = owner.transform.forward;

        else if (direction == DashDirection.Back)
            directionDash = owner.transform.forward* -1;

        skillName = $"Dash {direction}";

        timer = 0;
    }
    public override void Use(GameObject owner)
    {
        owner.transform.position =
            Vector3.Lerp(
                owner.transform.position,
                owner.transform.position + directionDash * dashDistance,
                timer);

        timer += 0.1f;
    }
}
