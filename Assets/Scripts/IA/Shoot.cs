using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public State[] shootStates;
    bool canShoot;

    StateMachine machine;

    [Range(0.1f, 30)]
    public float maxShootTime;
    float waitTime;

    public GameObject gunPoint;
    public GameObjPool pool;
    public float bulletSpeed;

    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = false;
        machine = GetComponent<StateMachine>();
        target = FindObjectOfType<SkoController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Mira si uno de los States del array es igual al actual en la StateMachine
        State mach = Array.Find(shootStates, x => x == machine.get_currentState);
        if (mach == machine.get_currentState) { canShoot = true; }
        else { canShoot = false; }

        if (canShoot)
        {
            //inicia el contador
            waitTime += Time.deltaTime;
            if (waitTime > maxShootTime)
            {
                //acceder a un obj de la pull
                GameObject bullet = pool.GetFirstInactiveGameObject();
                if (bullet != null)
                {
                    //activar el objeto
                    bullet.SetActive(true);
                    bullet.transform.forward = gunPoint.transform.forward;
                    //cambiar posicion de bala a la punta pistola
                    bullet.transform.position = gunPoint.transform.position;

                    //cambia la velocidad y direccion
                    bullet.GetComponent<Bullet>()._speed = bulletSpeed;
                    bullet.GetComponent<Bullet>()._direction = (target.transform.position - gunPoint.gameObject.transform.position).normalized;

                    //GetComponent<AudioPlayer>().PlaySFX("shoot");
                }

                waitTime = 0;
            }
        }
    }
}
