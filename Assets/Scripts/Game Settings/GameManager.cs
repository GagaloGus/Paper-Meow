using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Settings")]
    public int gameTime;
    public bool gamePaused, isInteracting, isTutorial, menuOpen;

    [Header("Player")]
    public GameObject player;
    public bool playerDied;
    public int money = 0;
    public int health;
    public int maxHealth = 0;

    [TextArea(3,5)]public List< string> tutorials;


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
        menuOpen = false;
        //isTutorial = true;
        health = maxHealth;
        playerDied = false;
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        print($"Escena cargada: {currentScene.name}");

        if (currentScene.name != "Main_Menu")
        {
            LockCursor();
            GetPlayer();
        }
        else { UnlockCursor();}
    }

    private void Update()
    {
        if (Application.targetFrameRate != targetFPS)
        { Application.targetFrameRate = targetFPS; }

        if(!playerDied)
        {
            if (!gamePaused && !isInteracting && SceneManager.GetActiveScene().name != "Main_Menu")
            {
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    UnlockCursor();
                    FindObjectOfType<CameraController>().LockCamera();
                }


                if(Input.GetKeyUp(KeyCode.LeftAlt))
                {
                    LockCursor();
                    FindObjectOfType<CameraController>().ResetCamera();
                }
            }
        }

        if(health > 0 && Input.GetKeyDown(KeyCode.R))
        {
            RecoverHealth(1);
        }
    }

    #region Cursor
    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    #endregion

    #region Pausa

    public void SoftPauseGame()
    {
        PauseSettings();
        FindObjectOfType<CameraController>().LockCamera();
    }

    public void PauseGame()
    {
        PauseSettings();
        GameEventsManager.instance.miscEvents.PauseMenuOpen();
        FindObjectOfType<CameraController>().PausedLockCamera();
    }

    void PauseSettings()
    {
        gameTime = 0;
        gamePaused = true;
        UnlockCursor();
        player.GetComponent<SkoController>().PausedGame(true);
    }

    public void ContinueGame()
    {
        gameTime = 1;
        gamePaused = false;
        menuOpen = false;
        player.GetComponent<SkoController>().PausedGame(false);

        FindObjectOfType<CameraController>().ResetCamera();
        LockCursor();

    }

    public void PauseAndContinueToggle(bool softPause)
    {
        gamePaused = !gamePaused;
        if (gamePaused)
        {
            if (softPause)
                SoftPauseGame();
            else
                PauseGame();
        }
        else
        {
            ContinueGame();
        }
    }
    #endregion
    public void GetPlayer()
    {
        player = FindObjectOfType<SkoController>().gameObject;
        health = (int)MathF.Abs(health);
    }

    public void ChangeScene(string sceneName, bool loadScene)
    {
        AudioManager.instance.StopMusic();
        StartCoroutine(ChangeSceneCorroutine(sceneName, loadScene));
    }

    IEnumerator ChangeSceneCorroutine(string sceneName, bool loadScene)
    {
        //carga la escena en otro proceso aparte al del juego, al terminar carga la escena de golpe
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while(!op.isDone)
        {
            // pantalla de carga
            yield return null;
        }

        if(sceneName != "Main_Menu")
        {
            GetPlayer(); 
            LockCursor();
            FindObjectOfType<CameraController>().ResetCamera();
        }

        if (loadScene)
        {
            SaveDataManager.instance.LoadData(player);
        }

        gamePaused = false;
        gameTime = 1;
        money = 0;
        isInteracting = false;
        menuOpen = false;
        //isTutorial = true;
        health = maxHealth;
        playerDied = false;
        AudioManager.instance.gameObject.GetComponent<AmbientMusicPlayer>().currentZoneTag = "asgfsd";
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
        UnlockCursor();
        FindObjectOfType<CameraController>().ChangeSpeedOfCamera(120);

        player.BroadcastMessage("StartInteraction", npc);
    }

    public void EndInteraction()
    {
        isInteracting = false;
        LockCursor();
        FindObjectOfType<CameraController>().ResetCamera();
        player.BroadcastMessage("EndInteraction");
    }

    #endregion

    public void RecoverHealth(int healthRecov)
    {
        health = Mathf.Clamp(health + healthRecov, 0, maxHealth);
        GameEventsManager.instance.playerEvents.HealthUpdate();
    }

    public void TakeDamage(int damage) //El jugador toma el daño que inflingen sus enemigos y resta su vida.
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        GameEventsManager.instance.playerEvents.DamageTaken(damage);
        GameEventsManager.instance.playerEvents.HealthUpdate();

        if (health <= 0)
        {
            playerDied = true;
            UnlockCursor();
            GameEventsManager.instance.playerEvents.PlayerDeath();
            player.GetComponent<SkoController>().Death();
        }
        else
        {
            player.GetComponent<SkoController>().TakeDamage();
        }
    }

    public void SetUnlockedSkills(string idString)
    {
        FindObjectOfType<SkillManager>().SetUnlockedSkills(idString);
    }

    public string UnlockedSkills()
    {
        return FindObjectOfType<SkillManager>().UnlockedSkills();
    }
    
}
