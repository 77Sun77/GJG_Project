using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float movespeed;
    float speedX, speedY;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * movespeed;
        speedY = Input.GetAxisRaw("Vertical") * movespeed;
        rb.velocity = new Vector2(speedX, speedY);

        // Rotate
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        if (mousePosition.x - transform.position.x < 0){
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else{
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
