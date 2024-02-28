using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject inGameMenu;

    public GameObject gameOverMenu;
    public TMP_Text gameOverKillCount;
    public TMP_Text gameOverTime;

    public GameObject clearMenu;
    public TMP_Text clearData;

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
        Time.timeScale = 1;
        inGameMenu.SetActive(!inGameMenu.activeInHierarchy);
        SceneManager.LoadSceneAsync(1);
    }

    public void PlayStop()
    {
        Time.timeScale = 1;
        inGameMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        SceneManager.LoadSceneAsync(0);
    }

    public void GameOver()
    {
        gameOverKillCount.text = $"Kill Count : {GameManager.instance.DeathCount}";
        int currentTime = (int)GameManager.instance.timer;

        string timeText = "";
        if (currentTime / 60 == 0) timeText = (currentTime).ToString() + "sec";
        else timeText = (currentTime / 60) + "min, " + ((currentTime - (currentTime / 60) * 60)).ToString() + "sec";
        gameOverTime.text = "TIME : "+ timeText;

        gameOverMenu.SetActive(true);
    }

    public void Clear()
    {
        int currentTime = (int)GameManager.instance.timer;

        string killCount = $"óġ�� �� : {GameManager.instance.DeathCount}\n";

        string timeText = "";
        if (currentTime / 60 == 0) timeText = (currentTime).ToString() + "sec";
        else timeText = (currentTime / 60) + "min, " + ((currentTime - (currentTime / 60) * 60)).ToString() + "sec";
        clearData.text = killCount + "�ɸ� �ð� : " + timeText;

        Time.timeScale = 0;
        clearMenu.SetActive(true);
    }

    public void ClearAndContinue()
    {
        // TODO : ���Ѹ�� ����
    }

    public void ClearAndStop()
    {
        Time.timeScale = 1;
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
