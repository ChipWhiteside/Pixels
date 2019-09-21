using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private int action;
    private float moving;
    public float jumpForce;
    private float jumpDiv;
    private float speed;
    public float walkSpeed; //slowest
    public float jogSpeed; //default speed
    public float sprintSpeed; //after walking long enough you sprint

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    private bool isJumping;

    private float jumpTimeCounter;
    public float jumpTime;
    private float sprintTimeCounter;
    public float sprintTime;

    private float momentum;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpDiv = 2;
        speed = jogSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpTimeCounter > 0 && isJumping && jumpForce > .25f)
            {
                rb.velocity = Vector2.up * (jumpForce);
                jumpDiv *= 2f;
                print("jumpDiv: " + jumpDiv + "jumpForce: " + jumpForce / jumpDiv);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
                jumpDiv = 2;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            jumpDiv = 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            print("arrow key up");
            sprintTimeCounter = sprintTime; //reset sprint time back if movement is stopped
            speed = jogSpeed;
        }

    }

    private void FixedUpdate()
    {
        moving = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moving * speed, rb.velocity.y);
        if (moving != 0)
        {
            if (sprintTimeCounter < 0)
            {
                speed = sprintSpeed;
            }
            else
            {
                sprintTimeCounter -= Time.deltaTime;
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
