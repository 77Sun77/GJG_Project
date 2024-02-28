using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameManager gameManager;

    public GameObject mainMenu;
    public GameObject inGameMenu;
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

        if (gameManager is null && scene.buildIndex == 1)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        if (scene.buildIndex == 0 && !mainMenu.activeInHierarchy)
        {
            mainMenu.SetActive(true);   
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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

    public void PlayStop()
    {
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
