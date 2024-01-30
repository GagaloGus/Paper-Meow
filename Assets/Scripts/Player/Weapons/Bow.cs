using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    Animator animator;
    public bool spawnedArrow;

    [Header("Arrow Physics")]
    [SerializeField] Vector3 launchDir;
    [SerializeField] float launchSpeed;
    public float arrowGravity;

    [Header("Arrow Types")]
    public GameObject smallArrow;
    public GameObject heavyArrow;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spawnedArrow = false;
    }
    
    public void NormalAttack()
    {
        print("peqeu");
        SpawnArrow(smallArrow);
    }

    public void ChargedAttack()
    {
        animator.SetBool("aiming", true);
        SpawnArrow(heavyArrow);
    }

    public void ShootChargedAttack()
    {
        
    }

    void SpawnArrow(GameObject arrow)
    {
        if (!spawnedArrow)
        {
            GameObject arrowSpawned = Instantiate(arrow, GetComponentInChildren<TrayectoryLine>().gameObject.transform);
            arrowSpawned.GetComponent<ConstantForce>().force = Vector3.zero;
            spawnedArrow = true;
        }
    }
}
