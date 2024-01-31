using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damageMultiplier;
    public float maxLifeTime;
    float lifetime;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        lifetime = 0;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);

        lifetime += Time.deltaTime;
        if (lifetime > maxLifeTime)
        {
            lifetime = 0;
            //aqui vendria lo de la pool
        }
    }
}
