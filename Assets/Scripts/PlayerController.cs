using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private int action;
    private float moving;
    private float jumpDiv;
    private float speed;
    public float walkSpeed; //slowest
    public float jogSpeed; //default speed
    public float sprintSpeed; //after walking long enough you sprint
    private int facing;
    private float dashTimeCounter;
    public float dashTime;
    public float dashSpeed;
    private bool hasDashed;
    private bool isDashing;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    private bool isJumping;

    public float jumpForceSet;
    private float jumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    private float divTimeCounter;
    private float divTime;
    //private float sprintTimeCounter;
    //public float sprintTime;

    private float momentum;
    private Camera main;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = jogSpeed;
        facing = 1;
        hasDashed = false;
        facing = 1;
        jumpForce = jumpForceSet;
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            hasDashed = false;
            jumpForce = jumpForceSet;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            divTime = jumpTime / 5;
            divTimeCounter = divTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpTimeCounter > 0 && isJumping)
            {
                rb.velocity = Vector2.up * (jumpForce);
                print("JumpForce: " + jumpForce);
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

        if (!hasDashed && Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing = true;
            dashTimeCounter = dashTime;
        }

        if (isJumping)
        {
            if (divTimeCounter <= 0)
            {
                divTimeCounter = divTime;
                jumpForce = jumpForce - 1.5f;
            }
            else
            {
                divTimeCounter -= Time.deltaTime;
            }
        }

        main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);


    }

    private void FixedUpdate()
    {
        moving = Input.GetAxisRaw("Horizontal");
        if (moving > 0)
        {
            facing = 1;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (moving < 0)
        {
            facing = -1;
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        rb.velocity = new Vector2(moving * speed, rb.velocity.y);

        if (isDashing)
        {
            if (dashTimeCounter > 0)
            {
                rb.velocity = Vector2.right * facing * dashSpeed;

                dashTimeCounter -= Time.deltaTime;
            }
            else
            {
                isDashing = false;
                hasDashed = true;
            }
        }

        
    }

    /*
     * When player presses a button for an action, "action" is set to the number
     * corresponding to that action.    
     * 
     * 0: Nothing
     * 1: Jump
     * 2: Dash
     * 3: Teleport
     * 4: Downsmash
     * 5: Walljump
     */
    bool TryingTo()
    {
        switch (action)
        {
            case 1:
                print(action + ": Jump");
                action = 0;
                //isJumping = true;
                break;
            case 2:
                print(action + ": Dash");
                action = 0;
                break;
            case 3:
                print(action + ": Teleport");
                action = 0;
                break;
            case 4:
                print(action + ": Downsmash");
                action = 0;
                break;
            case 5:
                print(action + ": Walljump");
                action = 0;
                break;
            default:
                print(action + ": Nothing (default)");
                action = 0;
                break;
        }
        return true;
    }
}
