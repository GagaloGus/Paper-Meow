using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataManager
{
    public static SaveDataManager instance = new SaveDataManager();
    string savefile = Application.persistentDataPath + "\\" + "Gamedata.json";
    
    public void LoadData(GameObject player)
    {
        if (File.Exists(savefile)) //Verifica si el archivo de guardado existe en la ruta especificada.
        {
            string content = File.ReadAllText(savefile); //Lee el contenido del archivo de guardado.
            GameData gamedata = new GameData(); 

            gamedata = JsonUtility.FromJson<GameData>(content); //Transforma el contenido del archivo en un objeto de tipo "GameData" utilizando la clase JsonUtility de Unity.

            // Se asignan los valores cargados a diferentes componentes del juego, como la posición del jugador y la puntuación obtenida.
            player.transform.position = gamedata.position;
            player.transform.rotation = gamedata.rotation;
            GameManager.instance.isTutorial = gamedata.isTutorial;
            GameManager.instance.SetUnlockedSkills(gamedata.skills);
            GameManager.instance.money = gamedata.coins;

            Debug.Log("Archivo Cargado"); // Se muestra en la consola el mensaje "Archivo Cargado" indicando que hemos cargado los datos correctamente.
        }
        else
        {
            Debug.Log("El archivo no existe"); //Se muestra en la consola el mensaje "El archivo no existe" en caso de que el archivo a cargar no exista.
        }

    }
    public void SaveData(GameObject player)
    {
        GameData newdata = new GameData() //Creamos un nuevo objeto del tipo GameData con los datos actuales del juego "Posición del jugador y la puntuación".
        {
            position = player.transform.position,
            rotation = player.transform.rotation,
            isTutorial = GameManager.instance.isTutorial,
            skills = GameManager.instance.UnlockedSkills(),
            coins = GameManager.instance.money,
        };

        string jsonstring = JsonUtility.ToJson(newdata); //Transforma el objeto de tipo GameData en formato JSON utilizando la clase JsonUtility de Unity.

        File.WriteAllText(savefile, jsonstring); //Escribe el JSON transformado en el archivo de guardado en la ruta especificada.

        Debug.Log("Archivo Guardado"); //Se muestra en la consola el mensaje "Archivo Guardado" para indicar que los datos dados han sido guardados.
    }
}
