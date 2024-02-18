using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/AttackState")]
public class AttackState : State
{
    public int damageAmount = 5; // Cantidad de daño al jugador
    public float attackDuration = 1.0f; // Duración de la animación de ataque

    private float attackTimer;
    private bool hasAttacked; //bool para asegurar haber atacado.

    public override void StartState(GameObject owner)
    {
        base.StartState(owner);
        attackTimer = 0f;
        hasAttacked = false;
    }

    public override State Run(GameObject owner)
    {

        if (animator != null && target != null)
        {
            {

                // Incrementar el temporizador de ataque
                attackTimer += Time.deltaTime;
                owner.transform.LookAt(target.transform.position);

                // Si la animación de ataque ha terminado y no hemos atacado aún
                if (attackTimer >= attackDuration && !hasAttacked)
                {
                    animator.SetTrigger("Attack");
                    // Aplicar daño al jugador
                    //GameManager.instance.Damage(damageAmount);

                    attackTimer = 0f;

                    hasAttacked = true;
                }

                // Si ha pasado suficiente tiempo desde el último ataque, permitir otro ataque
                if (attackTimer >= attackDuration * 2)
                {
                    hasAttacked = false;
                }

                //stateMachine.animator.SetTrigger("Attack");

                return base.Run(owner); // Permanecer en el estado de ataque mientras dure la animación
            }

        }
        return this;

    }
}
