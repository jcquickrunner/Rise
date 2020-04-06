﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //--------------CONFIG---------------
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed= 4f;
    //-----------------------STATE---------------------------
bool isAlive = true;
    //------------------------CACHE------------------------------
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;
    // Start is called before the first frame update
    //--------------------------INITIALIZATION-------------------
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();//CACHE
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        run();
        FlipSprite();
        Jump();
    
    }
    private void run()
    {
        float controlFlow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlFlow * runSpeed, myRigidBody.velocity.y);// velocity has an x and y
        myRigidBody.velocity = playerVelocity;// sets the velociy for player
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;// controls set in the input manager in unity
       
        myAnimator.SetBool("Run",playerHasHorizontalSpeed);//second parameter looks for true or false-- this animation"run" is true or false
        
    }
    private void Jump(){
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("ground"))) { return;}// if not touching ground layer then run the rest of jump method
        if(CrossPlatformInputManager.GetButtonDown("Jump")){
        Vector2 jumpVelocityToAdd = new Vector2 (0f, jumpSpeed);
        myRigidBody.velocity += jumpVelocityToAdd;// the x and y of velocity to add
        }
        
    }
    private void FlipSprite()
    { // turn player around
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;// when first parameter is greater than second parameter then this boolean will be true
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    // private void PlayAnimation()
    // {
    //     if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         myAnimator.SetBool("Climb", true);
    //     } else if (Input.GetKeyUp(KeyCode.Alpha1)){
    //         myAnimator.SetBool("Climb",false);
    //     }

    // ----------------------------THIS WORKS------------------------}
}
