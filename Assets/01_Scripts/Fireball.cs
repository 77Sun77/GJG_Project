using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float FireballSpeed;
    Rigidbody2D rigid;
    [SerializeField]
    int Damage;
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
            
            Enemy enemy_Script = coll.GetComponent<Enemy>();
            enemy_Script.Damage(Damage);


            Destroy(gameObject);
        }
    }
}
