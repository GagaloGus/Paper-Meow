using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = FindObjectOfType<SkoController>().gameObject;
    }

    //llamado desde las animaciones de ataque, solo sirve para el player
    public void NotAttacking()
    {
        SkoController cont = player.GetComponent<SkoController>();
        cont.player_isAttacking = false;
        cont.currentAttackNumber = 0;
    }

    public void FollowUpAttack()
    {
        SkoController cont = player.GetComponent<SkoController>();
        cont.canAttackAgain = true;
    }
}
