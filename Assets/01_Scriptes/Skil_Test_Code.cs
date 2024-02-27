using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skil_Test_Code : MonoBehaviour
{
    public GameObject FireBall;
    public GameObject RangeAttack;

    public float FireballCooltime_MAX;
    public float FireballCooltime;

    public float RangeAttackCooltime_MAX;
    public float RangeAttackCooltime;
    void Start()
    {
        
    }
    
    void Update()
    {
        FireballCooltime += Time.deltaTime;
        RangeAttackCooltime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Alpha2) /*&& GameManager.instance.EventCount >= 1*/)
        {
            if (FireballCooltime >= FireballCooltime_MAX)
            {
                Instantiate(FireBall, transform.position, Quaternion.identity);
                FireballCooltime = 0;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) /*&& GameManager.instance.EventCount >= 2*/)
        {
            if (RangeAttackCooltime >= RangeAttackCooltime_MAX)
            {
                Instantiate(RangeAttack, transform);
                RangeAttackCooltime = 0;
            }
        }
    }

    IEnumerator Delay()
    {
        GetComponent<Movement>().enabled = false;
        yield return new WaitForSeconds(1);
        GetComponent<Movement>().enabled = true;
    }
}
