using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    List<Collider2D> HitEnemy = new List<Collider2D>();
    float ActiveTime;
    bool enable;
    void Start()
    {
        Destroy(gameObject, 3);
        enable = true;
    }

    // Update is called once per frame
    void Update()
    {
        ActiveTime += Time.deltaTime;
        if(ActiveTime >= 0.1f)
        {
            enable = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            // 대미지를 입히는 코드
            if (HitEnemy.Contains(coll) || !enable) return;
            else HitEnemy.Add(coll);

            Vector2 dir = coll.transform.position - transform.position;
            Rigidbody2D enemy = coll.GetComponent<Rigidbody2D>();
            enemy.velocity = Vector2.zero;
            enemy.AddForce(dir.normalized * 500);
            StartCoroutine(Knockback(enemy, dir));
        }
    }

    IEnumerator Knockback(Rigidbody2D enemy, Vector2 dir)
    {
        enemy.GetComponent<Enemy>().Knockback = true;
        yield return new WaitForSeconds(0.15f);
        enemy.velocity = Vector2.zero;
        enemy.AddForce(dir.normalized * 10);
        yield return new WaitForSeconds(0.5f);
        enemy.velocity = Vector2.zero;
        enemy.GetComponent<Enemy>().Knockback = false;
    }
}
