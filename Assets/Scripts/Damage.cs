using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisi�n es con el jugador
        if (collision.gameObject.GetComponent<SkoController>())
        {
            
            // Llamar al m�todo Damage del GameManager solo si la colisi�n es con el jugador
            GameManager.instance.Damage(damage);
        }
    }
    //public void PartStart()
    //{
    //    ParticleSystem part = gameObject.GetComponent<ParticleSystem>();

    //    part.Play();

    //}

    //public void PartStop()
    //{
    //    ParticleSystem part = gameObject.GetComponent<ParticleSystem>();

    //    part.Stop();

    //}
}
