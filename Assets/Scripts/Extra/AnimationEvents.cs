using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    //llamado desde las animaciones de ataque, solo sirve para el player
    public void NotAttacking()
    {
        FindObjectOfType<SkoController>().player_isAttacking = false;
    }
}
