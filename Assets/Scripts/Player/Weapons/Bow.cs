using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [Header("Debug Variables")]
    Animator animator;
    Transform aimPoint;
    [SerializeField] bool spawnedArrow;
    [SerializeField] GameObject arrowSpawned;
    [SerializeField] bool inverseDirection;
    Transform model;



    [Header("Arrow Physics")]
    [SerializeField] Vector3 launchDir;
    [SerializeField] float launchSpeed;
    public float arrowGravity;

    [Header("Arrow Types")]
    public GameObject smallArrow;
    public GameObject heavyArrow;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        animator = GetComponentInChildren<Animator>();
        spawnedArrow = false;
        aimPoint = GetComponentInChildren<TrayectoryLine>().gameObject.transform;
        model = GetComponentInParent<ChangeTextureAnimEvent>().transform;
    }

    private void Update()
    {
        //Mira si el personaje esta mirando hacia adelante o atras para darle la vuelta a la flecha, solo sirve la variable para debug, no se usa
        if (spawnedArrow)
        {
            inverseDirection = model.localScale.z < 0;
        }
    }


    public void NormalAttack()
    {
        print("peqeu");
        animator.SetTrigger("shoot");
        SpawnArrow(smallArrow);
        ShootArrow();
    }

    public void ChargedAttack()
    {
        animator.SetBool("aiming", true);
        SpawnArrow(heavyArrow);
        arrowSpawned.transform.SetPositionAndRotation(aimPoint.position, aimPoint.rotation * Quaternion.Euler((model.localScale.z < 0 ? 180 : 0), 0, 0));
    }

    public void ShootChargedAttack()
    {
        ShootArrow();
    }

    void SpawnArrow(GameObject arrow)
    {
        if (!spawnedArrow)
        {
            arrowSpawned = Instantiate(arrow, aimPoint.position, aimPoint.rotation);
            arrowSpawned.GetComponent<Rigidbody>().velocity = Vector3.zero;
            arrowSpawned.GetComponent<ConstantForce>().force = Vector3.zero;
            arrowSpawned.GetComponent<Arrow>().enabled = false;
            spawnedArrow = true;
        }
    }

    void ShootArrow()
    {
        animator.SetBool("aiming", false);
        animator.SetTrigger("shoot");
        arrowSpawned.GetComponent<Arrow>().enabled = true;

        //lanza la flecha spawneada con su respectiva trayectoria, gravedad y fuerza
        launchDir = aimPoint.transform.forward * (model.localScale.z < 0 ? -1 : 1);
        arrowSpawned.GetComponent<Rigidbody>().velocity = launchDir * launchSpeed;
        arrowSpawned.GetComponent<ConstantForce>().force = Vector3.up * arrowGravity;

        spawnedArrow = false;
        
        gameObject.SetActive(false);
    }

}
