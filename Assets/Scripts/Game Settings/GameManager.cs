using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int money = 0;
    public int health;
    public int maxHealth = 0;

    public int gameTime;

    public bool gamePaused, isInteracting;

    public GameObject player;


    public int targetFPS = 60;
    public int playerstatsRefreshRate = 3;
    void Awake()
    {
        if (!instance) //instance  != null  //Detecta que no haya otro manager en la escena.
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //Si hay otro manager lo destruye.
        }

        Application.targetFrameRate = targetFPS;

        gamePaused = false;
        gameTime = 1;
        money = 0;
        isInteracting = false;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onPauseMenuOpen += PauseGame;
    }
    
    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onPauseMenuOpen -= PauseGame;
    }

    private void Start()
    {
        GetPlayer();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FindObjectOfType<CameraController>().ResetCamera();
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
            PauseAndContinueToggle();
        }

        if (!gamePaused && !isInteracting)
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                LockCursorAndCamera();
            }


            if(Input.GetKeyUp(KeyCode.LeftAlt))
            {
                UnlockCursorAndCamera();
            }
        }


    }

    void LockCursorAndCamera()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindObjectOfType<CameraController>().LockCamera();
    }

    void UnlockCursorAndCamera()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FindObjectOfType<CameraController>().ResetCamera();
    }

    public void PauseGame()
    {
        gameTime = 0;
        FindObjectOfType<CameraController>().PausedLockCamera();
        player.GetComponent<SkoController>().PausedGame(true);
    }

    public void ContinueGame()
    {
        gameTime = 1;
        player.GetComponent<SkoController>().PausedGame(false);

        UnlockCursorAndCamera ();
    }

    public void PauseAndContinueToggle()
    {
        gamePaused = !gamePaused;
        if (gamePaused)
        {
            PauseGame();
        }

        else
        {
            ContinueGame();
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

    public void MoneyUpdate(int amount)
    {
        money += amount;
    }

    public int coinCount
    {
        get { return money; }
        set { money = value; }
    }

    public void StartInteraction(GameObject npc)
    {
        isInteracting = true;
        LockCursorAndCamera();
        player.BroadcastMessage("StartInteraction", npc);
    }

    public void EndInteraction()
    {
        isInteracting = false;
        UnlockCursorAndCamera();
        player.BroadcastMessage("EndInteraction");
    }
}
