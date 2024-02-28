using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField] private Slider healthBar;

    private float currentHealth;

    [SerializeField] private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    public void Damage(int damage)
    {
        if (currentHealth - damage <= 0) 
        {
            Debug.Log("Dead");
            healthBar.value = 0;
            currentHealth = 0;
            Destroy(gameObject);
            MenuManager.instance.GameOver();
            return;
        }

        currentHealth -= damage;
        
        healthBar.value = currentHealth / health;
        pc.hitTimer = 0.2f;
        foreach (SpriteRenderer sp in pc.Sprites)
        {
            sp.color = Color.red;
            
        }
        // ÀÌÆåÆ®?
    }
}
