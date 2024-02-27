using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    private int speed;

    [SerializeField]
    private int range;
    [SerializeField]
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Attack()
    {
        
    }
}
