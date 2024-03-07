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

    #region Player
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
    #endregion

    #region Camera
    public void DisableAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }

    #endregion

    public void DestroyGameObj()
    {
        Destroy(gameObject);
    }

    public void DesactiveGameObj()
    {
        gameObject.SetActive(false);
    }

    public void PlaySFX3D(AudioClip clip)
    {
        AudioManager.instance.PlaySFX3D(clip, transform.position);
    }

    public void PlaySFX2D(AudioClip clip)
    {
        AudioManager.instance.PlaySFX2D(clip);
    }
}
