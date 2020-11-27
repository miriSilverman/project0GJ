using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public Transform feetPos;
    public LayerMask whatIsGround;
    public bool isDead = false;
    private bool _isGrounded = true;
    
    public float speed = 10f;

    public float jumpForce = 12f;
    private float jumpTimeCounter;
    public float jumpTime=0.35f;
    private bool isJumping = false;
    
    public float checkRadius = 0.3f;
    private bool moveRight = true;

    public Camera mainCamera;
    private Vector2 screenBorders;
    private float objWidth;
    private float objHeight;
    


    void Start()
    {
        screenBorders = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width,
                Screen.height, Camera.main.transform.position.z));
        objWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        
        // float cameraPos = mainCamera.transform.position.x;
        // float cameraSize = mainCamera.orthographicSize;
        // screenBorders = new Vector2(cameraPos - cameraSize/2, cameraPos +cameraSize/2);
       
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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

    // private void FixedUpdate()
    // {
    //     if (!GameController.instance.gameOver)
    //     {
    //         Debug.Log("he ");
    //
    //         float moveInput = Input.GetAxisRaw("Horizontal");    // left = -1; right = 1;
    //         if (moveInput < -Mathf.Epsilon)
    //         {
    //             moveRight = false;
    //         }
    //         else if(moveInput > Mathf.Epsilon)
    //         {
    //             moveRight = true;
    //         }
    //         // Debug.Log("moveInput "+ moveInput);
    //         anim.SetBool("moveRight", moveRight);
    //         rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    //         // rb.AddForce(moveInput*speed*Vector2.right);
    //     }
    // }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, screenBorders.x *-1 + objWidth, screenBorders.x - objWidth);
        pos.y = Mathf.Clamp(pos.y, screenBorders.y*-1 + objHeight, screenBorders.y - objHeight);
        transform.position = pos;
    }
    

    private void Update()
    {
        if (!GameController.instance.gameOver)
        {
            float moveInput = Input.GetAxisRaw("Horizontal");    // left = -1; right = 1;
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
            if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                // GameController.instance.gameStarted = true;
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


