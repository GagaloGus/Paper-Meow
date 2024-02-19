using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData //Creaci�n de una nueva clase con nombre GameData.
{
    public static GameData instance;
    public Vector3 position; //Toma las coordenadas de la posici�n del player.
    public Quaternion rotation;
    public int coins; //Toma los datos de la puntuaci�n.
}
