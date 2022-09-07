using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    public enum GameState { Pause, Game, Win, Lose }

    public AudioManager audioManager;
    public EffectManager effectManager;
    public UnityEvent onGameStart = new UnityEvent();
    public UnityEvent onLevelComplete = new UnityEvent();
    public UnityEvent onGameEnd = new UnityEvent();
    public UnityEvent onGameWin = new UnityEvent();
    public UnityEvent onGameLose = new UnityEvent();
    public List<string> stageList = new List<string>();
    public FadeScreen fadeScreen;
    public int score = 0;
    public int numEnemiesToWin = 4;
    public GameState state = GameState.Pause;
    public Player player;

    void Start()
    {
        
        //DontDestroyOnLoad(gameObject);

        GameStart();
    }

    public void DelayedLoadMenu(int seconds)
    {
        StartCoroutine(DoDelayedLoadMenuScene(seconds));

    }

    IEnumerator DoDelayedLoadMenuScene(int d) { 
        // Fade out
         yield return new WaitForSeconds(d);
        SceneManager.LoadScene("MainMenu");
        // This makes sure all interactables are associated with the correct rig.
        UpdateInteractables();
    }

    public void LoadNextLevel()
    {
        // Unload the previous level, load the next one.
    }

    //  Call when the game starts (ie from the menu UI)
    public void GameStart()
    {
        Debug.Log("Calling GameStart callbacks.");
        state = GameState.Game;
        onGameStart.Invoke();
    }

    public void GameWin()
    {
        Debug.Log("Calling GameWin callbacks.");

        state = GameState.Win;
        onGameWin.Invoke();
    }

    public void GameLose()
    {
        Debug.Log("Calling GameLose callbacks.");
        state = GameState.Lose;
        onGameLose.Invoke();
    }   

    public void LevelComplete()
    {
        Debug.Log("Calling LevelComplete callbacks.");
        onLevelComplete.Invoke();
    }

    public void AddScore(int i)
    {
        score += i;
    }

    public void EnableSpanwers()
    {
        Debug.Log("Enabling spawners");
        Spawner[] spawners = GameObject.FindObjectsOfType<Spawner>();

        foreach (Spawner spawner in spawners)
        {
            spawner.EnableSpawning();
        }
    }


    public void SetGameState(GameState s)
    {
        state = s;
    }

    public static GameManager GetGameManager()
    {
       return GameObject.Find("GameManager").GetComponent<GameManager>();

    }


    public void UpdateInteractables()
    {
        Debug.Log("Associatinga ll XR Grabbables with player.");
        XRInteractionManager xim = player.GetComponent<XRInteractionManager>();
        foreach(var go in GameObject.FindObjectsOfType<XRGrabInteractable>())
        {
            if(go.interactionManager ==  null)
                go.interactionManager = xim;
        }
        Debug.Log("Associatinga ll XR Grabbables with player.");
        foreach (var go in GameObject.FindObjectsOfType<XRSocketInteractor>())
        {
            if (go.interactionManager == null)
                go.interactionManager = xim;
        }

    }
    public void CheckGameEndConditions()
    {
        if (score >= numEnemiesToWin)
        {
            GameWin();
        }
        else
        {
            GameLose();
        }
    }

}
