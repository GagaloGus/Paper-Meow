using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int points = 0;
    public float time = 0;
    public int healthpotions = 0;
    public int health;
    public int maxHealth = 0;

    GameObject player;


    public int targetFPS = 60;
    public int playerStats_refreshRate = 3;
    void Awake()
    {
       if (!instance) //instance  != null  //Detecta que no haya otro GameManager en la escena.
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //Si hay otro GameManager lo destruye.
        }

        Application.targetFrameRate = targetFPS;
        player = FindObjectOfType<SkoController>().gameObject;
    }

    private void Update()
    {
        health = (int)MathF.Abs(health);

        if (Application.targetFrameRate != targetFPS)
        { Application.targetFrameRate = targetFPS; }
    }

    public void AddPunt(int value) //Agrega la cantidad de puntos designada.
    {
        points += value;

    }
    public void ResetPunt(int value) //Resetea la cantidad de puntos.
    {
        points  *= value;
    }
    public int GetPunt() //Recibe los puntos.
    {
        return points;
    }

    public void Heal(int amount)
    {
        health += amount;
    }
    public void Healing(int healvalue)
    {
        Heal(health + healvalue <= 100 ? healvalue : 100 - health);

    }

    public void StartInteraction(GameObject npc)
    {
        player.BroadcastMessage("StartInteraction", npc);
    }

    public void EndInteraction(GameObject npc)
    {
        player.BroadcastMessage("EndInteraction");
    }
}
