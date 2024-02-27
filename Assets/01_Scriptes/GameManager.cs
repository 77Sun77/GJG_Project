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



    [Space(20)]
    [Header("UI")]
    public TextMeshProUGUI timerText;

    [Space(20)]
    [Header("Component")]
    public SpawnManager spm;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        timer += Time.deltaTime* timeSpeed;
        
        string str = "";
        if ((int)timer / 60 == 0) str = ((int)timer).ToString() + "sec";
        else str = ((int)timer / 60) + "min, " + (((int)timer- ((int)timer / 60) * 60)).ToString() + "sec";
        timerText.text = str;

        if((int)timer/60 == 3&& EventCount == 0)
        {
            print("Event On");
            EventCount++;
            spm.AddList(EventCount);
        }
        else if ((int)timer / 60 == 6 && EventCount == 1)
        {
            print("Event On");
            EventCount++;
            spm.AddList(EventCount);
        }
        else if ((int)timer / 60 == 9 && EventCount == 2)
        {
            print("Event On");
            EventCount++;
            spm.AddList(EventCount);
        }
    }
}
