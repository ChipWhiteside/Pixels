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

    public float jumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    //private float sprintTimeCounter;
    //public float sprintTime;

    private float momentum;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //jumpDiv = 2;
        speed = jogSpeed;
        facing = 1;
        hasDashed = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            hasDashed = false;
            //isJumping = false;
            //isDashing = false;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            print("jTC > 0: " + (jumpTimeCounter > 0));
            print("isjumping: " + isJumping);
            if (jumpTimeCounter > 0 && isJumping)
            {
                print("jumpTimeCounter: " + jumpTimeCounter + " JumpTime: "+ jumpTime);
                rb.velocity = Vector2.up * (jumpForce);
                //jumpDiv *= 2f;
                //print("jumpDiv: " + jumpDiv + "jumpForce: " + jumpForce / jumpDiv);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                //print("falling");

                isJumping = false;
                //jumpDiv = 2;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            //jumpDiv = 2;
        }

        //if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    print("arrow key up");
        //    sprintTimeCounter = sprintTime; //reset sprint time back if movement is stopped
        //    speed = jogSpeed;
        //}

        if (!hasDashed && Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing = true;
            dashTimeCounter = dashTime;
        }

    }

    private void FixedUpdate()
    {
        moving = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moving * speed, rb.velocity.y);

        if (isDashing)
        {
            print("dashing");
            if (dashTimeCounter > 0)
            {
                rb.velocity = Vector2.right * moving * dashSpeed;
                print("Moving: " + moving);
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
