using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class player : MonoBehaviour
{
    public static player instance;
    public float speed = 20f;
    private Rigidbody rb;
    public float jumpforce = 10.0f; // La fuerza del salto, puedes ajustarla en el Inspector
    public bool Grounded = false; // Para controlar si el personaje está en el suelo
    public int health;  
    public HealthBar healthBar;


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

    //public player()
    //{
    //    healthBar = new HealthBar();
    //    healthBar.sprites = new Sprite[] {
    //        (Sprite) Resources.Load("Assets/Sprites/Health/100.png") ,
    //        (Sprite)Resources.Load("Assets/Sprites/Health/80.png") ,
    //        (Sprite) Resources.Load("Assets/Sprites/Health/60.png") ,
    //        (Sprite) Resources.Load("Assets/Sprites/Health/40.png") ,
    //        (Sprite) Resources.Load("Assets/Sprites/Health/20.png") ,
    //        (Sprite) Resources.Load("Assets/Sprites/Health/0.png")
    //    };

    //    healthBar.currenthealth = 100;
    //    healthBar.totalhealth = 100;

    //    gameObject.AddComponent(healthBar);
    //}
}
