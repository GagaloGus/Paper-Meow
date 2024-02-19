using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    private GameObject player;
    public string savefile;
    public GameData gamedata = new();

    private void Awake()
    {
        savefile = Application.persistentDataPath + "\\" + "Gamedata.json"; //Asigna la ruta del archivo de guardado en formato JSON en la carpeta del proyecto en AppData.
        player = GameObject.FindGameObjectWithTag("Player"); //Busca el objeto del jugador en la escena y lo asigna a la variable "player".
        LoadData(); //Llama a la función "LoadData" para cargar los datos guardados previamente, si existen.
    }
    public void LoadData()
    {
        if (File.Exists(savefile)) //Verifica si el archivo de guardado existe en la ruta especificada.
        {
            string content = File.ReadAllText(savefile); //Lee el contenido del archivo de guardado.
            gamedata = JsonUtility.FromJson<GameData>(content); //Transforma el contenido del archivo en un objeto de tipo "GameData" utilizando la clase JsonUtility de Unity.

            // Se asignan los valores cargados a diferentes componentes del juego, como la posición del jugador y la puntuación obtenida.
            player.transform.position = gamedata.position;
            player.transform.rotation = gamedata.rotation;
            GameManager.instance.isTutorial = gamedata.isTutorial;
            SkillManager.instance.SetUnlockedSkills(gamedata.skills);
            GameManager.instance.money = gamedata.coins;

            Debug.Log("Archivo Cargado"); // Se muestra en la consola el mensaje "Archivo Cargado" indicando que hemos cargado los datos correctamente.
        }
        else
        {
            Debug.Log("El archivo no existe"); //Se muestra en la consola el mensaje "El archivo no existe" en caso de que el archivo a cargar no exista.
        }

    }
    public void SaveData()
    {
        GameData newdata = new GameData() //Creamos un nuevo objeto del tipo GameData con los datos actuales del juego "Posición del jugador y la puntuación".
        {
            position = player.transform.position,
            rotation = player.transform.rotation,
            isTutorial = GameManager.instance.isTutorial,
            skills = SkillManager.instance.UnlockedSkills(),
            coins = GameManager.instance.money,
        };

        string jsonstring = JsonUtility.ToJson(newdata); //Transforma el objeto de tipo GameData en formato JSON utilizando la clase JsonUtility de Unity.

        File.WriteAllText(savefile, jsonstring); //Escribe el JSON transformado en el archivo de guardado en la ruta especificada.

        Debug.Log("Archivo Guardado"); //Se muestra en la consola el mensaje "Archivo Guardado" para indicar que los datos dados han sido guardados.
    }
}
