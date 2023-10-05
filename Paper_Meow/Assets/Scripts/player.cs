using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody rb;
    public float jumpforce = 10.0f; // La fuerza del salto, puedes ajustarla en el Inspector
    public bool Grounded = false; // Para controlar si el personaje está en el suelo
    public int health;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        rb.velocity = -transform.forward * z * speed + -transform.right * x * speed + transform.up * rb.velocity.y;  

        // Verificar si el personaje está en el suelo
        Grounded = Physics.Raycast(transform.position, Vector3.down, 1.2f);
        Debug.DrawRay(transform.position, Vector3.down * 1.2f, Color.red);

        // Detectar el input de salto
        if (Grounded && Input.GetButtonDown("Jump"))
        {
            // Aplicar la fuerza de salto
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }
    public void Heal(int amount)
    {
        health += amount;
    }
    public void Healing(int healvalue)
    {
        Heal(health + healvalue <= 100 ? healvalue : 100 - health);

    }
}
