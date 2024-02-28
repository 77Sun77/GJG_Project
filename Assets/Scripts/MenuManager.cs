using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject inGameMenu;

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
        SceneManager.LoadSceneAsync(1);
    }

    public void PlayStop()
    {
        inGameMenu.SetActive(false);
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
