using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public Transform feetPos;
    public LayerMask whatIsGround;
    public bool isDead = false;
    private bool _isGrounded;
    
    public float speed;
    // public float curSpeed;

    public float jumpForce;
    // public float curJumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    // public float jumpFactor;
    
    public float checkRadius;
    private bool moveRight;

    // public Camera mainCamera;
    // public Vector2 screenBorders;


    void Start()
    {
        // float cameraPos = mainCamera.transform.position.x;
        // float cameraSize = mainCamera.orthographicSize;
        // screenBorders = new Vector2(cameraPos - cameraSize/2, cameraPos +cameraSize/2);
        // _isGrounded = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // curSpeed = speed;
        // curJumpForce = jumpForce;
    }

    //
    // void Update(){
    //     if (!GameController.instance.gameOver)
    //     {
    //         // sidesMovement();
    //         // jumpMovement();
    //         // anim.SetBool("moveRight", moveRight);
    //         
    //         
    //         
    //         // if (transform.position.x > screenBorders.y)
    //         // {
    //         //     transform.position = new Vector2(screenBorders.x, transform.position.y);
    //         // }
    //         // else if (transform.position.x < screenBorders.x)
    //         // {
    //         //     transform.position = new Vector2(screenBorders.y, transform.position.y);
    //         //     
    //         // }
    //  
    //     }
    // }

    private void FixedUpdate()
    {
        if (!GameController.instance.gameOver)
        {
            float moveInput = Input.GetAxisRaw("Horizontal");    // left = -1; right = 1;
            // if (moveInput < -Mathf.Epsilon)
            // {
            //     moveRight = false;
            // }
            // else if(moveInput > Mathf.Epsilon)
            // {
            //     moveRight = true;
            // }
            
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            
            // anim.SetBool("moveRight", moveRight);
   
            
          
        }
    }


    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            
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

    
    
    //
    // private void sidesMovement()
    // {
    //     float moveInput = Input.GetAxisRaw("Horizontal");    // left = -1; right = 1;
    //     if (moveInput < -Mathf.Epsilon)
    //     {
    //         moveRight = false;
    //     }
    //     else if(moveInput > Mathf.Epsilon)
    //     {
    //         moveRight = true;
    //     }
    //     // else
    //     // {
    //     //     anim.SetTrigger("centered");
    //     // }
    //     // anim.SetBool("moveRight", moveRight);
    //     rb.AddForce(moveInput*curSpeed*Vector2.right);
    // }
    //
    // private void jumpMovement()
    // {
    //     _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGraund);
    //
    //     if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
    //     {
    //         isJumping = true;
    //         jumpTimeCounter = jumpTime;
    //         rb.velocity = Vector2.up * (curJumpForce + rb.velocity.magnitude * jumpFactor);
    //     }
    //     
    //     if (Input.GetKey(KeyCode.Space) && isJumping)
    //     {
    //         
    //         if (jumpTimeCounter > 0)
    //         {
    //             rb.velocity = Vector2.up * curJumpForce;
    //             jumpTimeCounter -= Time.deltaTime;
    //         }
    //         else
    //         {
    //             isJumping = false;
    //         }
    //     
    //     }
    //     
    //     if (Input.GetKeyUp(KeyCode.Space))
    //     {
    //         isJumping = false;
    //     }
    //
    // }


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
