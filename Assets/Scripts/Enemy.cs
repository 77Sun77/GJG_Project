using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private string type;

    [SerializeField]
    private int health;

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

    private float lastAttackTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        MoveOrAttack();
    }

    void Rotate()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
}
