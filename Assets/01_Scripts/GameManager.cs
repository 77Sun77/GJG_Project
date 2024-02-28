using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("DATA")]
    public float timer;
    public int timeSpeed;
    public int EventCount;
    public float DeathCount;


    [Space(20)]
    [Header("UI")]
    //public GameObject GameOver;
    //public GameObject menu;
    public TextMeshProUGUI timerText;
    public GameObject[] Skill_Icon;
    public Image[] SkillBlind;

    [Space(20)]
    [Header("Component")]
    public SpawnManager spm;
    public PlayerController PC;


    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) MenuManager.instance.Pause();

        timer += Time.deltaTime * timeSpeed;
        
        string str = "";
        if ((int)timer / 60 == 0) str = ((int)timer).ToString() + "sec";
        else str = ((int)timer / 60) + "min, " + (((int)timer- ((int)timer / 60) * 60)).ToString() + "sec";
        timerText.text = str;

        
        if((int)timer/60 == 3 && EventCount == 0)
        {
            EventCount++;
            spm.AddList(EventCount);
            spm.SpawnCooltime = 3;
            Skill_Icon[EventCount].SetActive(true);
        }
        else if ((int)timer / 60 == 6 && EventCount == 1)
        {
            EventCount++;
            spm.AddList(EventCount);
            spm.SpawnCooltime = 1;
            Skill_Icon[EventCount].SetActive(true);
        }
        else if ((int)timer / 60 == 9 && EventCount == 2)
        {
            spm.SpawnBoss();
            EventCount++;
        }

        SkillBlind[1].fillAmount = PC.FireballCooltime / PC.FireballCooltime_MAX;
        SkillBlind[2].fillAmount = PC.RangeAttackCooltime / PC.RangeAttackCooltime_MAX;
    }
}
