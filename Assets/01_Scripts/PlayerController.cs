using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
        FireballCooltime -= Time.deltaTime;
        RangeAttackCooltime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Alpha2) /*&& GameManager.instance.EventCount >= 1*/)
        {
            if (FireballCooltime < 0)
            {
                Instantiate(FireBall, transform.position, Quaternion.identity);
                FireballCooltime = FireballCooltime_MAX;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) /*&& GameManager.instance.EventCount >= 2*/)
        {
            if (RangeAttackCooltime < 0)
            {
                Instantiate(RangeAttack, transform);
                StartCoroutine(RangeAttackDelay());
                RangeAttackCooltime = RangeAttackCooltime_MAX;
            }
        }
    }

    IEnumerator RangeAttackDelay()
    {
        Movement movement = GetComponent<Movement>();
        movement.enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(1);
        movement.enabled = true;
    }
}
