using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int points = 0;
    public float time = 0;
    public int healthpotions = 0;
    public int health;
    public int maxHealth = 0;

    public int gameTime;

    public bool gamePaused { get; private set; }

    public GameObject player;


    public int targetFPS = 60;
    public int playerstatsRefreshRate = 3;
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

        gamePaused = false;
        gameTime = 1;

        GetPlayer();
    }


    public void GetPlayer(GameObject player)
    {
        this.player = player;
        SkillManager.instance.player = player;
    }

    public void GetPlayer()
    {
        player = FindObjectOfType<SkoController>().gameObject;
        SkillManager.instance.player = player;
    }

    private void Update()
    {
        health = (int)MathF.Abs(health);

        if (Application.targetFrameRate != targetFPS)
        { Application.targetFrameRate = targetFPS; }

        if(Input.GetKeyDown(PlayerKeybinds.pauseGame))
        {
            gamePaused = !gamePaused ;
            if(gamePaused)
                GamePaused();
            else
                GameResumed();
        }

        if(Input.GetKeyDown(KeyCode.Numlock))
        {
            ChangeScene("Test");
        }

        if(Input.GetKeyDown(KeyCode.Insert))
        {
            ChangeScene("player-npcs");
        }
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(ChangeSceneCorroutine(sceneName));
    }

    IEnumerator ChangeSceneCorroutine(string sceneName)
    {
        //carga la escena en otro proceso aparte al del juego, al terminar carga la escena de golpe
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while(!op.isDone)
        {
            // pantalla de carga
            yield return null;
        }

        GetPlayer();

    }

    void GamePaused()
    {
        gameTime = 0;
        FindObjectOfType<CameraController>().LockCamera();
        player.GetComponent<SkoController>().PausedGame(true);
    }

    void GameResumed()
    {
        gameTime = 1;
        FindObjectOfType<CameraController>().ResetCamera();
        player.GetComponent<SkoController>().PausedGame(false);
    }

    public void AddPunt(int value) //Agrega la cantidad de puntos designada.
    {
        points += value;

    }
    public void ResetPunt(int value) //Resetea la cantidad de puntos.
    {
        points *= value;
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
