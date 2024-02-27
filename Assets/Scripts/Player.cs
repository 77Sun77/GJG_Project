using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damage)
    {
        if (health - damage <= 0) 
        {
            health = 0;
            Debug.Log("Dead");
            // 게임 끝
            return;
        }

        health -= damage;
        // 이펙트?
    }
}
