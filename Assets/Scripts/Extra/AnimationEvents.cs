using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    //llamado desde las animaciones de ataque, solo sirve para el player
    public void NotAttacking()
    {
        SkoController cont = FindObjectOfType<SkoController>();
        cont.player_isAttacking = false;
        cont.currentAttackNumber = 0;
    }

    public void FollowUpAttack()
    {
        SkoController cont = FindObjectOfType<SkoController>();
        cont.canAttackAgain = true;
    }
}
