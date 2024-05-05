using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private float moveX, moveY;
    private Vector2 movement; 
    [SerializeField] float speed = 2.0f;
    [SerializeField] float jumpForce = 8.0f;
    public GameObject fishingRod;
    public Animator animator;


    void Start()
    {
        
        //Time.timeScale = 0f;  
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        GameObject player = GameObject.FindWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal") * speed;
        moveY = Input.GetAxisRaw("Vertical") * speed;

        

        if (Input.GetKeyDown(KeyCode.F)){
            if (!fishingRod.activeSelf) { fishingRod.SetActive(true);   }
            else                        { fishingRod.SetActive(false);  }
        }


        if(Input.GetButtonDown("Jump")){
           Jump();
        }


    }

    private void Jump()
    {
        transform.Translate(Vector3.up * Time.deltaTime * jumpForce);

    }

    private void FixedUpdate(){
        //rb.velocity = new Vector2 (moveX, moveY);
        movement = new Vector2(moveX, moveY) * speed;
        rb.velocity = movement.normalized;


        animator.SetFloat("moveForwards", rb.velocity.y);
        animator.SetFloat("moveBackwards", rb.velocity.y);
        animator.SetFloat("moveRight", Math.Abs(rb.velocity.x));





        if (moveX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveX), 1, 1);
        }


    }


}
