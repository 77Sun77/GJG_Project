using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private string type;

    [SerializeField]
    private float health;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int range;
    [SerializeField]
    private int damage;
    [SerializeField]
    private int attackSpeed;
    [SerializeField] private float rotateOffset;

    [SerializeField]
    private GameObject bullet;

    private Animator animator;
    private GameObject player;
    private FloatingHealthBar healthBar;
    private Rigidbody2D rigid;

    private float lastAttackTime = 0;
    private float currentHealth;
    private bool seeingLeft = true;
    private Quaternion flip;

    public bool Knockback;
    public float KnockbackTimer;

    public bool isDeath;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponentInChildren<Animator>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        rigid = GetComponent<Rigidbody2D>();
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        if(!Knockback) MoveOrAttack();
        
        if(!Knockback) MoveOrAttack();
        else
        {
            KnockbackTimer += Time.deltaTime;
            if (KnockbackTimer > 1)
            {
                Knockback = false;
                KnockbackTimer = 0;
            }

            rigid.velocity = Vector2.zero;
            animator.SetBool("Moving", false);
        }
    }

    void Rotate()
    {
        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotateOffset;
        //Debug.Log(angle);

        float realAngle = 0;
        
        if (angle > -90 && angle < 90)
        {
            realAngle = Mathf.Clamp(angle, -3, 3);

            if (seeingLeft)
            {
                seeingLeft = false;
                flip = Quaternion.Euler(0, 180, 0);
            }
        }
        else if (angle < -90 || angle > 90)
        {
            if (angle < -90)
            {
                realAngle = Mathf.Clamp(angle, -180, -177);
            } else if (angle > 90)
            {
                realAngle = Mathf.Clamp(angle, 177, 180);
            }

            if (!seeingLeft)
            {
                seeingLeft = true;
                flip = Quaternion.Euler(0, 0, 0);
            }

        }

        //Debug.Log(realAngle);

        transform.rotation = flip;
        //transform.rotation =  Quaternion.AngleAxis(seeingLeft ? realAngle : -realAngle, Vector3.forward) * flip;

        //Debug.Log(transform.rotation);
    }

    void MoveOrAttack()
    {
        if (player)
        {
            Move();
        }
        else
        {
            Attack();
        }
    }

    float GetPlayerDistance()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    void Move()
    {
        Vector3 direction = player.transform.position - transform.position;

        rigid.velocity = (direction.normalized * speed);

    }

    public void Attack()
    {
        rigid.velocity = Vector2.zero;
        if (Time.time - attackSpeed < lastAttackTime)
        {
            return;
        }

        lastAttackTime = Time.time;
        //animator.SetTrigger("Attack");
        if (type == "person" || type == "knight")
        {
            player.GetComponent<Player>().Damage(damage);
        }
        else if (type == "archer")
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }

    public void Damage(int damage)
    {
        if (currentHealth - damage <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.spm.SpawnCount--;
            GameManager.instance.DeathCount++;
            isDeath = true;
            return;
        }

        currentHealth -= damage;
        //healthBar.UpdateValue(currentHealth, health);

    }
}
