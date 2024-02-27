using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Data")]
    public float movespeed;
    float speedX, speedY;
    Rigidbody2D rb;

    [Space(20)]
    [Header("Skill")]
    public GameObject FireBall;
    public GameObject RangeAttack;

    public float FireballCooltime_MAX;
    public float FireballCooltime;

    public float RangeAttackCooltime_MAX;
    public float RangeAttackCooltime;
    bool isMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isMove = true;
    }

    void Update()
    {
        FireballCooltime -= Time.deltaTime;
        RangeAttackCooltime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Q) && GameManager.instance.EventCount >= 1)
        {
            if (FireballCooltime < 0)
            {
                Instantiate(FireBall, transform.position, Quaternion.identity);
                FireballCooltime = FireballCooltime_MAX;
            }

        }
        else if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.EventCount >= 2)
        {
            if (RangeAttackCooltime < 0)
            {
                Instantiate(RangeAttack, transform);
                StartCoroutine(RangeAttackDelay());
                RangeAttackCooltime = RangeAttackCooltime_MAX;
            }
        }

        if(isMove) Move();
    }
    private void Move()
    {
        speedX = Input.GetAxisRaw("Horizontal") ;
        speedY = Input.GetAxisRaw("Vertical") ;
        Vector2 dir = new Vector2(speedX, speedY);
        dir = dir.normalized * movespeed;
        rb.velocity = dir;
    }
    IEnumerator RangeAttackDelay()
    {
        isMove = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        isMove = true;
    }
}