﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool isDead = false;
    private Animator anim;
    public Camera mainCamera;
    
    
    
    private Rigidbody2D rb;
    public float speed;
    public float curSpeed;

    private bool _isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGraund;
    public float jumpForce;
    public float curJumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    // private int score;
    private bool onGum;
    private float lowerScreenBound;
    private float upperScreenBound;
    private float rightScreenBound;
    private float leftScreenBound;
    public Text scoreText;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float jumpFactor;
    // private Vector3 stageDimensions;
    // private bool gameStarted;

    
    
    
    void Start()
    {
        _isGrounded = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        curSpeed = speed;
        curJumpForce = jumpForce;
    }

    
    void Update(){
        if (!isDead)
        {
            sidesMovement();
            jumpMovement();
        }
    }
    
    
    
    private void sidesMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");    // left = -1; right = 1;
        if (moveInput < 0)
        {
            anim.SetTrigger("left");
        }
        else
        {
            anim.SetTrigger("right");
        }
        rb.AddForce(moveInput*curSpeed*Vector2.right);
    }

    private void jumpMovement()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGraund);
        
        if (Input.GetKey(KeyCode.Space))
        {

            GameController.instance.gameStarted = true;
            
            anim.SetTrigger("centered");

            
            rb.velocity = Vector2.up * jumpFactor;
        }

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * (curJumpForce + rb.velocity.magnitude * jumpFactor);
        }
        
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * curJumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
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
        if (other.GetComponent<LowerScreenBound>())
        {
            isDead = true;
            rb.velocity = Vector2.zero;
            GameController.instance.playerDied();
        }

        if (!isDead)
        {
            if (other.gameObject.tag.Equals("bow"))
            {
                GameController.instance.scoring();
                Destroy(other.gameObject);
            }
        }
    }


}