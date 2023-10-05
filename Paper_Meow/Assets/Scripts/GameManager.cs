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
}
