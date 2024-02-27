using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float FireballSpeed;
    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir.z = 0;
        transform.right = (dir - transform.position);
        transform.position += transform.right.normalized; 
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = transform.right * FireballSpeed;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            // 대미지를 입히는 구간
            Destroy(coll.gameObject); // 테스트용 코드
            Destroy(gameObject);
        }
    }
}
