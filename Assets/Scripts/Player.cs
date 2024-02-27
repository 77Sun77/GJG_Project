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
            Debug.Log("Dead");

            health = 0;
            Destroy(gameObject);
            GameManager.instance.GameOver.SetActive(true);
            return;
        }

        health -= damage;
        // ÀÌÆåÆ®?
    }
}
