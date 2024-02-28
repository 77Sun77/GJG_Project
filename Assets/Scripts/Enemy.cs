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
    private float range;
    [SerializeField]
    private int damage;
    [SerializeField]
    private int attackSpeed;
    [SerializeField] private float rotateOffset;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float bulletOffset;

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
        if (!player) return;

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
                realAngle = -180 - Mathf.Clamp(angle, -180, -177);
            } else if (angle > 90)
            {
                realAngle = 180 - Mathf.Clamp(angle, 177, 180);
            }

            if (!seeingLeft)
            {
                seeingLeft = true;
                flip = Quaternion.Euler(0, 0, 0);
            }
        }

        //Debug.Log(realAngle);
        //Debug.Log(Quaternion.AngleAxis(realAngle, Vector3.forward).eulerAngles.z);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, flip.eulerAngles.y, Quaternion.AngleAxis(-realAngle, Vector3.forward).eulerAngles.z);
        //transform.rotation =  Quaternion.AngleAxis(seeingLeft ? realAngle : -realAngle, Vector3.forward) * flip;

        //Debug.Log(transform.rotation);
    }

    void MoveOrAttack()
    {
        if (player is null) return;

        if (GetPlayerDistance() > range)
        {
            animator.SetBool("Moving", true);

            Move();
        }
        else
        {
            rigid.velocity = Vector2.zero;
            animator.SetBool("Moving", false);

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
        if (Time.time - attackSpeed < lastAttackTime)
        {
            return;
        }

        lastAttackTime = Time.time;
        animator.SetTrigger("Attack");
        if (type == "person" || type == "knight")
        {
            player.GetComponent<Player>().Damage(damage);
        }
        else if (type == "archer")
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;

            Instantiate(bullet, transform.position + new Vector3(0, bulletOffset, 0), Quaternion.AngleAxis(angle, Vector3.forward));
        }
        else if(type == "boss")
        {
            float playerDistance = GetPlayerDistance();
            if (playerDistance < 3) player.GetComponent<Player>().Damage(damage);
            else if (playerDistance < 10) Instantiate(bullet, transform.position, transform.rotation);
            else print("����");
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
        healthBar.UpdateValue(currentHealth, health);

    }
}
