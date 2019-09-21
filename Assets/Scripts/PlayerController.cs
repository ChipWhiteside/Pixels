using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private int action;
    private float moving;
    private float moveSpeed;
    private bool isGrounded;
    private bool isJumping;

    public Rigidbody2D rb;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        action = 0;
        moving = 0f;
        moveSpeed = 5f;
        isJumping = false;
        //for (int i = 0; i < 6; i++)
        //{
        //    action = i;
        //    TryingTo();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            //print(Input.GetAxis("Horizontal"));
            int dir = (Input.GetAxis("Horizontal") >= 0) ? 1 : -1;
            MoveHorizontal(dir);
        }

        if (Input.GetButton("Jump"))
        {
            action = 1;
            TryingTo();
        }
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            rb.AddForce(Vector2.up * 100f);
        }
    }

    void MoveHorizontal(float dir)
    {
        //public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed = Mathf.Infinity, float deltaTime = Time.deltaTime);

        transform.position += new Vector3(dir, 0, 0) * moveSpeed * Time.deltaTime;
        //spriteRenderer.flipX = false;
        //float MoveVec = dir * speed;
        //print("dir: " + dir + " | Moving: " + MoveVec);
        //rb.AddForce(new Vector2(MoveVec, 0));
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
                isJumping = true;
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


    //private bool IsGrounded()
    //{
    //    if (rb.velocity <= 0)
    //    {
    //        foreach (Transform point in groundPoints)
    //        {
    //            Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
    //            for (int i = 0; i < colliders.Length; i++)
    //            {
    //                if (colliders[i].gameObject != gameObject)
    //                {
    //                    return true;
    //                }
    //            }
    //        }
    //    }
    //    return false;
    //}



}
