using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private float moveX, moveY;
    [SerializeField] float speed = 2.0f;


    void Start()
    {
        
        rb = gameObject.AddComponent<Rigidbody2D>();

    }

    void Update()
    {
        
        moveX = Input.GetAxisRaw("Horizontal") * speed;
        moveY = Input.GetAxisRaw("Vertical") * speed;

    }


    private void FixedUpdate(){


        rb.velocity = new Vector2 (moveX, moveY);


    }


}
