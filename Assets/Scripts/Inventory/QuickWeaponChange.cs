using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickWeaponChange : MonoBehaviour
{
    public float speedSpin;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(PlayerKeybinds.swapPrevousWeapon))
        {
            StartCoroutine(Spin(false, speedSpin));
        }

        if(Input.GetKeyDown(PlayerKeybinds.swapNextWeapon))
        {
            StartCoroutine(Spin(true, speedSpin));
        }
    }

    IEnumerator Spin(bool clockwise, float speed)
    {
        for(float i = 0; i < 90; i+= speed)
        {
            transform.rotation *= Quaternion.Euler(0, 0, speed * (clockwise? -1 : 1));
            yield return null;
        }
    }
}

