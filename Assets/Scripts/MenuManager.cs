using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject inGameMenu;
    public GameObject gameOverMenu;
    public GameObject clearMenu;

    public static MenuManager instance;

    private void Awake()
    {
        //check if instance is null, if null then create 
        if (instance == null)
        {
            //refers to the GameManager class
            instance = this;
            //dont destroy gamemanger game object when loading new scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && gameOverMenu.activeInHierarchy) PlayStop();
    }

    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        if (scene.buildIndex == 0 && !mainMenu.activeInHierarchy)
        {
            mainMenu.SetActive(true);   
        }
    }

    public void PlayStart()
    {
        mainMenu.SetActive(false);
        SceneManager.LoadSceneAsync(1);
    }

    public void Pause()
    {
        inGameMenu.SetActive(!inGameMenu.activeInHierarchy);
        if (inGameMenu.activeInHierarchy) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void Restart()
    {
        inGameMenu.SetActive(!inGameMenu.activeInHierarchy);
        SceneManager.LoadSceneAsync(1);
    }

    public void PlayStop()
    {
        inGameMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        SceneManager.LoadSceneAsync(0);
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
    }

    public void Clear()
    {
        clearMenu.SetActive(true);
    }

    public void ClearAndContinue()
    {
        // TODO : 무한모드 진입
    }

    public void ClearAndStop()
    {
        inGameMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        clearMenu.SetActive(false);
        SceneManager.LoadSceneAsync(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
