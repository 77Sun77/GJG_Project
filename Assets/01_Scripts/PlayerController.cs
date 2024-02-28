using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Data")]
    public float movespeed;
    float speedX, speedY;
    bool isAttack;

    [Space(20)]
    [Header("Skill")]
    public GameObject FireBall;
    public GameObject RangeAttack;
    public GameObject MeleeAttack;
    [SerializeField] private Animator MeleeAttackAnim;
    public bool isMelee;

    public float FireballCooltime_MAX;
    public float FireballCooltime;

    public float RangeAttackCooltime_MAX;
    public float RangeAttackCooltime;

    public float MeleeAttack_MAX;
    public float MeleeAttackCooltime;

    [SerializeField]bool isMove;

    [Space(20)]
    [Header("Component")]
    public Transform[] body;
    private Animator anim;
    Rigidbody2D rb;
    public Transform MeleePoint;
    public GameObject spriteParent;
    public SpriteRenderer[] Sprites;
    public float hitTimer;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isMove = true;
        isAttack = true;
        Sprites = spriteParent.GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        print(Sprites);
        FireballCooltime -= Time.deltaTime;
        RangeAttackCooltime -= Time.deltaTime;
        MeleeAttackCooltime -= Time.deltaTime;

        if (isAttack) Attack();
        if (isMove) Move();

        if(hitTimer < 0)
        {
            hitTimer = 0.2f;
            foreach(SpriteRenderer sp in Sprites)
            {
                sp.color = Color.white;
            }
        }
        else
        {
            hitTimer -= Time.deltaTime;
        }
    }
    private void Move()
    {
        speedX = Input.GetAxisRaw("Horizontal") ;
        speedY = Input.GetAxisRaw("Vertical") ;
        Vector2 dir = new Vector2(speedX, speedY);
        dir = dir.normalized * movespeed;
        rb.velocity = dir;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        float x = direction.x/ Mathf.Abs(direction.x);

        body[0].localScale = new Vector3(x, 1, 1);
        body[1].localScale = new Vector3(x, 1, 1);
        MeleePoint.right = -direction;
    }
 

    public void SpanwRangeAttack()
    {
        Instantiate(RangeAttack, transform.position, Quaternion.identity) ;
    }

    public void ResetMove()
    {
        isMove = true;
    }
    public void ResetAttack()
    {
        isAttack = true;
        if (isMelee) isMelee = false;
    }

    void Attack()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            if (MeleeAttackCooltime < 0)
            {
                anim.SetTrigger("Attack1");
                Invoke("ResetAttack", 0.4f);
                MeleeAttackAnim.SetTrigger("Attack");
                isAttack = false;
                isMelee = true;
                MeleeAttackCooltime = MeleeAttack_MAX;
            }
                
        }
        else if (Input.GetKeyDown(KeyCode.Q) && GameManager.instance.EventCount >= 1)
        {
            if (FireballCooltime < 0)
            {
                Instantiate(FireBall, transform.position, Quaternion.identity);
                FireballCooltime = FireballCooltime_MAX;
                anim.SetTrigger("Attack2");
                Invoke("ResetAttack", 0.5f);
                isAttack = false;
            }

        }
        else if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.EventCount >= 2)
        {
            if (RangeAttackCooltime < 0)
            {
                anim.SetTrigger("Attack3");
                isMove = false;
                rb.velocity = Vector2.zero;
                RangeAttackCooltime = RangeAttackCooltime_MAX;
                Invoke("ResetAttack", 0.7f);
                isAttack = false;
            }
        }
    }
}