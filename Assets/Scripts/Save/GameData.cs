using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData //Creación de una nueva clase con nombre GameData.
{
    public static GameData instance;
    public Vector3 position; //Toma las coordenadas de la posición del player.
    public Quaternion rotation;
    public int coins; //Toma los datos de la puntuación.
}
