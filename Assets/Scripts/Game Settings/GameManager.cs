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
        GameEventsManager.instance.inventoryEvents.onInventoryOpen += SoftPauseGame;
    }
    
    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onPauseMenuOpen -= PauseGame;
        GameEventsManager.instance.inventoryEvents.onInventoryOpen -= SoftPauseGame;
    }

    private void Start()
    {
        GetPlayer();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FindObjectOfType<CameraController>().ResetCamera();
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
                LockCursor();
                FindObjectOfType<CameraController>().LockCamera();
            }


            if(Input.GetKeyUp(KeyCode.LeftAlt))
            {
                UnlockCursor();
                FindObjectOfType<CameraController>().ResetCamera();
            }
        }
    }

    #region Cursor y camara
    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindObjectOfType<CameraController>().LockCamera();
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FindObjectOfType<CameraController>().ResetCamera();
    }
    #endregion

    #region Pausa
    public void SoftPauseGame()
    {
        gameTime = 0;
        gamePaused = true;
        LockCursor();
        FindObjectOfType<CameraController>().LockCamera();
        player.GetComponent<SkoController>().PausedGame(true);
    }

    public void PauseGame()
    {
        gameTime = 0;
        gamePaused = true;
        LockCursor();
        FindObjectOfType<CameraController>().PausedLockCamera();
        player.GetComponent<SkoController>().PausedGame(true);
    }

    public void ContinueGame()
    {
        gameTime = 1;
        gamePaused = false;
        player.GetComponent<SkoController>().PausedGame(false);

        UnlockCursor();
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
    #endregion
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

    #region Interaction
    public void StartInteraction(GameObject npc)
    {
        isInteracting = true;
        LockCursor();
        player.BroadcastMessage("StartInteraction", npc);
    }

    public void EndInteraction()
    {
        isInteracting = false;
        UnlockCursor();
        player.BroadcastMessage("EndInteraction");
    }
    #endregion
}
