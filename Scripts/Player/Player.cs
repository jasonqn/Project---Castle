using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //player variables
    public Rigidbody2D rb;
    public int movementSpeed;
    public Animator anim;
    
    //movement variables
    public bool isMoving = false;
    public bool hasFlipped = false;

    public bool canMove;
    DDOL DDOLScript;

    private void Start()
    {
        DDOLScript = GameObject.FindGameObjectWithTag("DDOL").GetComponent<DDOL>();
        canMove = !DDOLScript.isInCombat;
        //if in the overworld and not in combat
        if (canMove)
        {
            gameObject.transform.position = DDOLScript.startingPos;
        }
    }

    void Update() 
    {
        //Movement for player
        CheckInput();
    }

    void FixedUpdate()
    {
        CheckMovement();
    }

    //Calls for movement of X and Y axis within Unity
    public void CheckInput()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * movementSpeed;
        }
    }

    //Checks if character is moving (velocity not 0)
    public void CheckMovement()
    {
        if (rb.velocity != new Vector2 (0,0)) 
        {
            isMoving = true;
            Flip();
        }
        //Else character stands still
        else 
        {
            isMoving = false;
        }
        //Applies running animation based off boolean outcome
        anim.SetBool("isRunning", isMoving);
        
    }

    //Checks if player has from right to left then back again
    //Ensure player Transform.Rotate value at vector y is set to 0 in Unity
    public void Flip()
    {
        //Checks if we're moving Left and if so flips the character
        if (rb.velocity.x < 0 && !hasFlipped) 
        {
            gameObject.transform.Rotate(0, 180, 0);
            hasFlipped = true;
        }
        //Checks if were moving right and we have previously flipped, flips back to right
        
        if (rb.velocity.x > 0 && hasFlipped)
        {
            gameObject.transform.Rotate(0, 180, 0);
            hasFlipped = false;
        }
    }

    //puts velocity to 0 - disallows movement - relevant for when in combat or not
    public void CanMove()
    {
        canMove = !canMove;
        rb.velocity = Vector2.zero;
    }

    //animation trigger functions - relevant for BattleManager/Combat
    public void Attack()
    {
        anim.SetTrigger("attack");
    }

    public void Block()
    {
        anim.SetTrigger("block");
    }

    public void Die()
    {
        anim.SetBool("dead", true);
    }


}
