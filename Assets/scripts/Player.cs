using System;
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
        // score = 0;
        _isGrounded = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        curSpeed = speed;
        curJumpForce = jumpForce;
        // gameIsOver = false;
        // lowerScreenBound = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        // Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
        // Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        // leftScreenBound = lowerLeft.x;
        // upperScreenBound = stageDimensions.y;
        // rightScreenBound = stageDimensions.x;
        // lowerScreenBound = lowerLeft.y;
        // Debug.Log(stageDimensions+" &&height");
        
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

    
        // transform.localRotation = Quaternion.Euler(0, moveInput < 0 ? 180 : 0, 0);
        // rb.velocity = new Vector2(moveInput * curSpeed, rb.velocity.y);
        rb.AddForce(moveInput*curSpeed*Vector2.right);
    }

    private void jumpMovement()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGraund);

        // if (go.transform.position.y < lowerScreenBound)
        // {
        //     Debug.Log("game is over");
        // }

        if (Input.GetKey(KeyCode.Space))
        {

            GameController.instance.gameStarted = true;
            
            anim.SetTrigger("centered");

            
            rb.velocity = Vector2.up * jumpFactor;
        }
        //
        // if (rb.velocity.y < 0 )
        // {
        //     rb.velocity += Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime *  Vector2.up;
        // }
        // else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        // {
        //     rb.velocity += Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime *  Vector2.up;
        //
        // }
        //
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ice"))    //todo: change
        {
            Debug.Log("on ice");
            curSpeed = 3 * speed;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("ice"))
        {
            Debug.Log("nottt ice");
    
            curSpeed = speed;
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

            else if (other.gameObject.tag.Equals("gum"))
            {
                curJumpForce = 0;
            }
            
            
        }

        
        // else if (other.gameObject.tag.Equals("ice"))
        // {
        //     curSpeed = 3 * speed;
        // }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("gum"))
        {
            curJumpForce = jumpForce;
        }
        
        // else if (other.gameObject.tag.Equals("ice"))
        // {
        //     curSpeed = speed;
        // }
    }

}
