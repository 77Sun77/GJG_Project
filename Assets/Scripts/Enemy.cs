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

    private float lastAttackTime = 0;
    private float currentHealth;

    public bool Knockback;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        if(!Knockback) MoveOrAttack();
    }

    void Rotate()
    {
        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 20);
    }

    void MoveOrAttack()
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

    float GetPlayerDistance()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    void Move()
    {
        Vector3 direction = player.transform.position - transform.position;
        transform.position += (direction * speed * Time.deltaTime);
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
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }

    public void Damage(int damage)
    {
        if (currentHealth - damage <= 0)
        {
            Destroy(gameObject);
            return;
        }

        currentHealth -= damage;
        healthBar.UpdateValue(currentHealth, health);
    }
}
