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

    [SerializeField]
    private GameObject bullet;

    public Animator animator;
    private GameObject player;
    private FloatingHealthBar healthBar;
    private Rigidbody2D rigid;

    private float lastAttackTime = 0;
    private float currentHealth;

    public bool Knockback;
    public float KnockbackTimer;

    public bool isDeath;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        rigid = GetComponent<Rigidbody2D>();
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        if(!Knockback) MoveOrAttack();
        else
        {
            KnockbackTimer += Time.deltaTime;
            if (KnockbackTimer > 1)
            {
                Knockback = false;
                KnockbackTimer = 0;
            }
        }
    }

    void Rotate()
    {
        if (player)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 20);
        }
        
    }

    void MoveOrAttack()
    {
        if (player)
        {
            float playerDistance = GetPlayerDistance();
            if (playerDistance > range)
            {
                Move();
            }
            else
            {
                Attack();
            }
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
        else if(type == "boss")
        {
            float playerDistance = GetPlayerDistance();
            if (playerDistance < 3) player.GetComponent<Player>().Damage(damage);
            else if (playerDistance < 10) Instantiate(bullet, transform.position, transform.rotation);
            else print("µ¹Áø");
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
