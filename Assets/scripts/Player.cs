﻿using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public Transform feetPos;
    public LayerMask whatIsGround;
    public AudioSource jumpSound;
    public bool isDead = false;
    private bool _isGrounded = true;
    public float speed = 10f;
    public float jumpForce = 12f;
    private float jumpTimeCounter;
    public float jumpTime=0.35f;
    private bool isJumping = false;
    
    public float checkRadius = 0.3f;
    private int direction;

    private Vector2 screenBorders;
    private float objWidth;
    private float objHeight;
    


    void Start()
    {
        direction = 3;    // straight - facing the camera
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (!GameController.instance.gameOver)
        {
            float moveInput = Input.GetAxisRaw("Horizontal");    // left = -1; right = 1;
            if (moveInput < -Mathf.Epsilon)
            {
                 direction = 1; // facing left
            }
            else if(moveInput > Mathf.Epsilon)
            {
                direction = 2;    // facing right
            }
            anim.SetInteger("direction", direction);
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            
            jump();
        }
    }

    // player jump. can only jump when touching ground, and has a fixed time to continue its jump
    // while keeps pressing the jump key
    private void jump()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround); // check if touching ground
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            GameController.instance.gameStarted = true;
            jumpSound.Play();
            direction = 3;    // facing the camera
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
            
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }else
            {
                isJumping = false;
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LowerBound"))    // touching lower bound -> game is over
        {
            isDead = true;
            rb.velocity = Vector2.zero;
            GameController.instance.playerDied();
        }

        if (!isDead)
        {
            if (other.gameObject.tag.Equals("bow"))    // takes a pascal "diamond"
            {
                GameController.instance.scoring();
                Destroy(other.gameObject);
            }
        }
    }


}


